using Mafi.Core.Entities.Static;
using Mafi.Core.Products;
using Mafi;
using System;
using Mafi.Collections.ReadonlyCollections;
using Mafi.Collections;
using Mafi.Core.Entities;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Maintenance;
using Mafi.Core.Notifications;
using Mafi.Core.Simulation;
using Mafi.Core.Stats;
using Mafi.Core;
using Mafi.Serialization;
using System.Collections.Generic;

namespace COIExtended.Logistics
{
    public class CargoShipBuildManager : IVirtualBufferProvider
    {
        public VirtualProductProto Product { get; private set; }
        public Quantity Quantity { get; private set; }
        public Quantity Capacity { get; private set; }

        public CargoShipBuildManager(VirtualProductProto product, Quantity initialCapacity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Capacity = initialCapacity.CheckNotNegative();
            Quantity = Quantity.Zero;
        }
        private class Buffer : IProductBuffer, IProductBufferReadOnly, IMaintenanceBufferReadonly
        {
            private VirtualProductProto m_product;

            private PartialQuantity m_consumedThisMonth;

            private PartialQuantity m_consumedUnreportedPartial;

            private PartialQuantity m_producedThisMonth;

            private PartialQuantity m_quantity;

            private bool m_notEnoughMaintenanceThisMonth;

            private readonly MaintenanceManager m_maintenanceManager;

            private readonly ProductsManager m_productsManager;

            private NotificatorWithProtoParam m_notEnoughMaintenanceNotif;

            private readonly Lyst<MaintenanceDepot> m_depots;

            private readonly Lyst<EntityMaintenanceProvider> m_sortedProviders;

            internal LystStruct<ConsumptionPerProto> ConsumptionStatsPerProto;

            [DoNotSaveCreateNewOnLoad(null, 0)]
            private readonly Dict<IEntityProto, int> m_consumerProtoIdsMap;

            [DoNotSaveCreateNewOnLoad(null, 0)]
            internal LystStruct<ConsumptionLastTick> ConsumptionStatsCache;

            private static readonly Action<object, BlobWriter> s_serializeDataDelayedAction;

            private static readonly Action<object, BlobReader> s_deserializeDataDelayedAction;

            public ProductProto Product => m_product;

            public Quantity UsableCapacity => (Capacity - Quantity).Max(Quantity.Zero);

            public Quantity Capacity { get; private set; }

            public Quantity Quantity => m_quantity.IntegerPart;

            public PartialQuantity DeltaLastMonth { get; private set; }

            public bool ShouldBeLastDeltaReported { get; private set; }

            public QuantitySumStats ProducedTotalStats { get; private set; }

            public QuantitySumStats ConsumedTotalStats { get; private set; }

            public PartialQuantity MonthlyNeededMaintenance { get; set; }

            /// <summary>
            /// Does not take idleness into account.
            /// </summary>
            public PartialQuantity MonthlyNeededMaintenanceMax { get; set; }

            public bool ShouldShowInUi
            {
                get
                {
                    if (!m_depots.IsNotEmpty && !Quantity.IsPositive && !MonthlyNeededMaintenance.IsPositive)
                    {
                        return m_consumedThisMonth.IsPositive;
                    }
                    return true;
                }
            }

            public IIndexable<EntityMaintenanceProvider> SortedProviders => m_sortedProviders;

            public Buffer(VirtualProductProto product, MaintenanceManager maintenanceManager, ProductsManager productsManager, INotificationsManager notificationsManager, ICalendar calendar, StatsManager statsManager)
            {
                MBiHIp97M4MqqbtZOh.Rbva8xJFA();
                m_depots = new Lyst<MaintenanceDepot>();
                m_sortedProviders = new Lyst<EntityMaintenanceProvider>();
                m_consumerProtoIdsMap = new Dict<IEntityProto, int>();
                base._002Ector();
                m_product = product;
                m_maintenanceManager = maintenanceManager;
                m_productsManager = productsManager;
                calendar.NewMonth.Add(this, onNewMonth);
                ProducedTotalStats = new QuantitySumStats(statsManager, isMonthlyEvent: true);
                ConsumedTotalStats = new QuantitySumStats(statsManager, isMonthlyEvent: true);
                m_notEnoughMaintenanceNotif = notificationsManager.CreateNotificatorFor(IdsCore.Notifications.NotEnoughMaintenance);
            }

            [InitAfterLoad(InitPriority.High)]
            private void initSelf(int saveVersion)
            {
                if (ConsumptionStatsCache.IsNotEmpty)
                {
                    Log.Error($"ConsumptionStatsCache is not empty, has {ConsumptionStatsCache.Count} elements!");
                }
                for (int i = 0; i < ConsumptionStatsPerProto.Count; i++)
                {
                    m_consumerProtoIdsMap.Add(ConsumptionStatsPerProto[i].Proto, i);
                    ConsumptionStatsCache.Add(ConsumptionLastTick.Empty);
                }
                Lyst<EntityMaintenanceProvider>.Enumerator enumerator = m_sortedProviders.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    EntityMaintenanceProvider current = enumerator.Current;
                    assignProtoTokenTo(current);
                }
                if (saveVersion < 108 && !verifyAssignersOrder())
                {
                    m_sortedProviders.Sort((EntityMaintenanceProvider x, EntityMaintenanceProvider y) => x.Priority.CompareTo(y.Priority));
                }
            }

            private bool verifyAssignersOrder()
            {
                for (int i = 1; i < m_sortedProviders.Count; i++)
                {
                    EntityMaintenanceProvider entityMaintenanceProvider = m_sortedProviders[i - 1];
                    EntityMaintenanceProvider entityMaintenanceProvider2 = m_sortedProviders[i];
                    if (entityMaintenanceProvider.Priority > entityMaintenanceProvider2.Priority)
                    {
                        return false;
                    }
                }
                return true;
            }

            public void AddConsumer(EntityMaintenanceProvider provider)
            {
                m_sortedProviders.PriorityListInsertSorted(provider, priorityProvider);
                assignProtoTokenTo(provider);
                static int priorityProvider(EntityMaintenanceProvider h)
                {
                    return h.Priority;
                }
            }

            public void RemoveConsumer(EntityMaintenanceProvider provider)
            {
                m_sortedProviders.RemoveAndAssert(provider);
            }

            Quantity IProductBuffer.StoreAsMuchAs(Quantity quantity)
            {
                Assert.That(quantity).IsNotNegative();
                Quantity quantity2 = ((quantity < UsableCapacity) ? quantity : UsableCapacity);
                if (quantity2.IsPositive)
                {
                    m_quantity += quantity2.AsPartial;
                    ProducedTotalStats.Add(quantity2);
                    m_producedThisMonth += quantity2.AsPartial;
                }
                return quantity - quantity2;
            }

            Quantity IProductBuffer.RemoveAsMuchAs(Quantity maxQuantity)
            {
                Log.Error("Maintenance cannot be consumed from global buffer.");
                return Quantity.Zero;
            }

            public PartialQuantity RemoveAsMuchAs(PartialQuantity maxQuantity)
            {
                Assert.That(maxQuantity).IsNotNegative();
                PartialQuantity partialQuantity = ((Quantity.AsPartial <= maxQuantity) ? Quantity.AsPartial : maxQuantity);
                m_quantity -= partialQuantity;
                m_consumedThisMonth += partialQuantity;
                m_consumedUnreportedPartial += partialQuantity;
                m_notEnoughMaintenanceThisMonth |= partialQuantity < maxQuantity;
                return partialQuantity;
            }

            public bool AddDepot(MaintenanceDepot depot)
            {
                if (m_depots.Contains(depot))
                {
                    return false;
                }
                m_depots.Add(depot);
                return true;
            }

            public bool RemoveDepot(MaintenanceDepot depot)
            {
                return m_depots.Remove(depot);
            }

            public void SetCapacity(Quantity newCapacity)
            {
                if (newCapacity.IsNegative)
                {
                    Log.Error($"Cannot set negative capacity {newCapacity}");
                }
                else
                {
                    Capacity = newCapacity;
                }
            }

            public void ReportConsumption()
            {
                Quantity integerPart = m_consumedUnreportedPartial.IntegerPart;
                if (integerPart.IsPositive)
                {
                    m_productsManager.ProductDestroyed(m_product, integerPart, DestroyReason.Maintenance);
                    ConsumedTotalStats.Add(integerPart);
                    m_consumedUnreportedPartial = m_consumedUnreportedPartial.FractionalPart;
                }
            }

            private void onNewMonth()
            {
                bool flag = false;
                Lyst<MaintenanceDepot>.Enumerator enumerator = m_depots.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    MaintenanceDepot current = enumerator.Current;
                    if (current.CurrentState == Machine.State.OutputFull)
                    {
                        flag = true;
                        break;
                    }
                }
                DeltaLastMonth = m_producedThisMonth - m_consumedThisMonth;
                ShouldBeLastDeltaReported = !flag || DeltaLastMonth.IsPositive;
                m_producedThisMonth = PartialQuantity.Zero;
                m_consumedThisMonth = PartialQuantity.Zero;
                bool shouldNotify = m_notEnoughMaintenanceThisMonth || (DeltaLastMonth.IsNegative && (Capacity.IsZero || Percent.FromRatio(Quantity.Value, Capacity.Value) < 50.Percent()));
                m_notEnoughMaintenanceNotif.NotifyIff(Product, shouldNotify);
                if (m_notEnoughMaintenanceThisMonth)
                {
                    m_maintenanceManager.m_notEnoughMaintenanceThisMonth.Invoke(m_product);
                }
                m_notEnoughMaintenanceThisMonth = false;
            }

            public IEnumerable<ConsumptionPerProto> GetConsumptionStatsPerProto()
            {
                for (int i = 0; i < ConsumptionStatsPerProto.Count; i++)
                {
                    ConsumptionStatsPerProto.GetRefAt(i).EntitiesTotal = 0;
                }
                IndexableEnumerator<EntityMaintenanceProvider> enumerator = SortedProviders.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    EntityMaintenanceProvider current = enumerator.Current;
                    if (current.Entity.IsEnabled)
                    {
                        ConsumptionStatsPerProto.GetRefAt(current.ProtoToken).EntitiesTotal++;
                    }
                }
                return ConsumptionStatsPerProto.AsEnumerable();
            }

            private void assignProtoTokenTo(EntityMaintenanceProvider consumer)
            {
                if (!m_consumerProtoIdsMap.TryGetValue(consumer.Entity.Prototype, out var value))
                {
                    ConsumptionPerProto item = new ConsumptionPerProto(consumer.Entity.Prototype);
                    value = ConsumptionStatsPerProto.Count;
                    ConsumptionStatsPerProto.Add(item);
                    ConsumptionStatsCache.Add(ConsumptionLastTick.Empty);
                    m_consumerProtoIdsMap.Add(consumer.Entity.Prototype, value);
                }
                consumer.ProtoToken = value;
            }

            static Buffer()
            {
                MBiHIp97M4MqqbtZOh.Rbva8xJFA();
                s_serializeDataDelayedAction = delegate (object obj, BlobWriter writer)
                {
                    ((Buffer)obj).SerializeData(writer);
                };
                s_deserializeDataDelayedAction = delegate (object obj, BlobReader reader)
                {
                    ((Buffer)obj).DeserializeData(reader);
                };
            }
        }

        public Quantity UsableCapacity => (Capacity - Quantity).Max(Quantity.Zero);

        public bool CanStore(Quantity quantity)
        {
            return quantity <= UsableCapacity;
        }

        public Quantity StoreAsMuchAs(Quantity quantity)
        {
            var storeQuantity = quantity.Min(UsableCapacity);
            Quantity += storeQuantity;
            OnQuantityChanged(storeQuantity);
            return storeQuantity;
        }

        public bool CanRemove(Quantity quantity)
        {
            return quantity <= Quantity;
        }

        public Quantity RemoveAsMuchAs(Quantity maxQuantity)
        {
            var removeQuantity = Quantity.Min(maxQuantity);
            Quantity -= removeQuantity;
            OnQuantityChanged(-removeQuantity);
            return removeQuantity;
        }

        protected virtual void OnQuantityChanged(Quantity diff)
        {
            // This method can be used to trigger events or notifications when the quantity changes
            // For example, when the buffer reaches full capacity, you might complete the construction of a cargo ship
            // Implementation depends on your game logic
        }

        // If needed, implement OnCapacityChanged similarly to OnQuantityChanged
        // This is optional and depends on whether your game logic requires handling capacity changes
        public ImmutableArray<ProductProto> ProvidedProducts
        {
            get
            {

                    // Return an ImmutableArray with the retrieved VirtualProductProto
                    return ImmutableArray.Create<ProductProto>(Product);
               
            }
        }

        public Option<IProductBuffer> GetBuffer(ProductProto product, IStaticEntity entity)
        {
            if (!(product == Product))
            {
                return Option<IProductBuffer>.None;
            }
            return (Option<IProductBuffer>)(IProductBuffer)m_pollutedAirBuffer;
        }
    }
}
