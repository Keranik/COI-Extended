using Mafi;
using Mafi.Core;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Priorities;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory.ElectricPower;
using Mafi.Core.Population;
using Mafi.Serialization;
using Mafi.Core.Simulation;
using Mafi.Core.Ports;
using Mafi.Core.Ports.Io;
using Mafi.Core.Products;
using Mafi.Core.Maintenance;
using Mafi.Core.Utils;
using System;

namespace COIExtended.Prototypes.Buildings;

[GenerateSerializer(false,null,0)]
public class CargoDrydock : LayoutEntity, IEntityWithWorkers, IEntityWithGeneralPriority, IEntity, IIsSafeAsHashKey, IElectricityConsumingEntity, IEntityWithSimUpdate, IEntityWithEmission, IStaticEntity, IEntityWithPorts, IMaintainedEntity
{
    public SimStep LastWorkedOnSimStep { get; private set; }
    public new readonly CargoDrydockProto Prototype;
    public override bool CanBePaused => true;
    public Option<ProductBuffer> cp4Buffer;
    public Option<ProductBuffer> e3Buffer;
    public Option<ProductBuffer> titaniumBuffer;
    public Option<ProductBuffer> cargoShipProgess;
    MaintenanceCosts IMaintainedEntity.MaintenanceCosts => Prototype.Costs.Maintenance;
    public bool IsIdleForMaintenance => true;
    public IEntityMaintenanceProvider Maintenance { get; private set; }
    private readonly IElectricityConsumer m_electricityConsumer;
    public State CurrentState { get; private set; }
    Electricity IElectricityConsumingEntity.PowerRequired => Prototype.ElectricityConsumed;
    private readonly IProductsManager m_productsManager;
    public Option<IElectricityConsumerReadonly> ElectricityConsumer => ((IElectricityConsumerReadonly)m_electricityConsumer).SomeOption();
    int IEntityWithWorkers.WorkersNeeded => Prototype.Costs.Workers;
    [DoNotSave(0, null)]
    bool IEntityWithWorkers.HasWorkersCached { get; set; }
    public bool IsActive { get; private set; }
    private TickTimer m_progressTimer;
    public bool canProgress => m_progressTimer.IsNotFinished;
    public int partialProgress;

    private static readonly Action<object, BlobWriter> s_serializeDataDelayedAction;

    private static readonly Action<object, BlobReader> s_deserializeDataDelayedAction;

    public enum State
    {
        None,
        Paused,
        NotEnoughWorkers,
        NotEnoughPower,
        Working
    }
    [DoNotSave(0, null)]
    public float? EmissionIntensity
    {
        get
        {
            if (!Prototype.EmissionIntensity.HasValue)
            {
                return null;
            }
            return IsActive ? Prototype.EmissionIntensity.Value : 0;
        }
    }

    public CargoDrydock(EntityId id, CargoDrydockProto proto, TileTransform transform, EntityContext context, ISimLoopEvents simLoop, IEntityMaintenanceProvidersFactory maintenanceProvidersFactory, IProductsManager productsManager) : base(id, proto, transform, context)
    {
        Prototype = proto;
        m_electricityConsumer = base.Context.ElectricityConsumerFactory.CreateConsumer(this);
        m_productsManager = productsManager;
        cp4Buffer = new ProductBuffer(new Quantity(100), Prototype.InputConstructionParts4.Value);
        titaniumBuffer = new ProductBuffer(new Quantity(100), Prototype.InputTitanium.Value);
        e3Buffer = new ProductBuffer(new Quantity(100), Prototype.InputElectronics3.Value);
        cargoShipProgess = new ProductBuffer(new Quantity(100), Prototype.ShipProgressItem.Value);
        Maintenance = maintenanceProvidersFactory.CreateFor(this);
        m_progressTimer = new TickTimer();
        partialProgress = 0;
    }

    void IEntityWithSimUpdate.SimUpdate()
    {
        CurrentState = isOperational();
        IsActive = CurrentState == State.Working;

        if (!m_progressTimer.Decrement())
        {
            if (canMakeShipProgress())
            {

                ProductBuffer tBuffer = titaniumBuffer.Value;
                ProductBuffer cBuffer = cp4Buffer.Value;
                ProductBuffer eBuffer = e3Buffer.Value;
                ProductBuffer sBuffer = cargoShipProgess.Value;
                partialProgress++;
                tBuffer.RemoveExactly(1.Quantity());
                m_productsManager.ProductDestroyed(tBuffer.Product, 1.Quantity(), DestroyReason.General);
                cBuffer.RemoveExactly(1.Quantity());
                m_productsManager.ProductDestroyed(cBuffer.Product, 1.Quantity(), DestroyReason.General);
                eBuffer.RemoveExactly(1.Quantity());
                m_productsManager.ProductDestroyed(eBuffer.Product, 1.Quantity(), DestroyReason.General);

                if (partialProgress >= 10)
                {
                    sBuffer.StoreExactly(1.Quantity());
                    m_productsManager.ProductCreated(sBuffer.Product, 1.Quantity(), CreateReason.General);
                    partialProgress = 0;
                }
            }
            m_progressTimer.Start(new Duration(1.Days().Ticks));            
        
        }
        
    }

    bool canMakeShipProgress()
    {
        if (cargoShipProgess.Value.Quantity.Value >= 100)
            return false;
        if (CurrentState != State.Working)
            return false;
        if (titaniumBuffer.Value.Quantity.Value >= 10 && cp4Buffer.Value.Quantity.Value >= 10 && e3Buffer.Value.Quantity.Value >= 10)
            return true;
        if (!base.IsEnabled) {partialProgress = 0; return false;}


        return false;
    }

    private State isOperational()
    {
        if (!base.IsEnabled)
        {
            return State.Paused;
        }
        if (Entity.IsMissingWorkers(this))
        {
            return State.NotEnoughWorkers;
        }
        if (!m_electricityConsumer.TryConsume())
        {
            return State.NotEnoughPower;
        }
        return State.Working;
    }




    Quantity IEntityWithPorts.ReceiveAsMuchAsFromPort(ProductQuantity pq, IoPortToken sourcePort)
    {
        if (titaniumBuffer.HasValue && titaniumBuffer.Value.Product == pq.Product)
        {
            return titaniumBuffer.Value.StoreAsMuchAs(pq);
        }

        if (e3Buffer.HasValue && e3Buffer.Value.Product == pq.Product)
        {
            return e3Buffer.Value.StoreAsMuchAs(pq);
        }

        if (cp4Buffer.HasValue && cp4Buffer.Value.Product == pq.Product)
        {
            return cp4Buffer.Value.StoreAsMuchAs(pq);
        }


        return pq.Quantity;
    }




    public static void Serialize(CargoDrydock value, BlobWriter writer)
    {
        if (writer.TryStartClassSerialization(value))
        {
            writer.EnqueueDataSerialization(value, s_serializeDataDelayedAction);
        }
    }

    protected override void SerializeData(BlobWriter writer)
    {
        base.SerializeData(writer);
        SimStep.Serialize(LastWorkedOnSimStep, writer);
        writer.WriteGeneric(m_electricityConsumer);
        writer.WriteGeneric(Prototype);
        writer.WriteGeneric(Maintenance);
        Option<ProductBuffer>.Serialize(cp4Buffer, writer);
        Option<ProductBuffer>.Serialize(e3Buffer, writer);
        Option<ProductBuffer>.Serialize(titaniumBuffer, writer);
        Option<ProductBuffer>.Serialize(cargoShipProgess, writer);
        TickTimer.Serialize(m_progressTimer, writer);
        writer.WriteInt(partialProgress);
    }

    public static CargoDrydock Deserialize(BlobReader reader)
    {
        if (reader.TryStartClassDeserialization(out CargoDrydock obj, (Func<BlobReader, Type, CargoDrydock>)null))
        {
            reader.EnqueueDataDeserialization(obj, s_deserializeDataDelayedAction);
        }
        return obj;
    }

    protected override void DeserializeData(BlobReader reader)
    {
        base.DeserializeData(reader);
        LastWorkedOnSimStep = SimStep.Deserialize(reader);
        reader.SetField(this, "m_electricityConsumer", reader.ReadGenericAs<IElectricityConsumer>());
        reader.SetField(this, "Prototype", reader.ReadGenericAs<CargoDrydockProto>());
        Maintenance = reader.ReadGenericAs<IEntityMaintenanceProvider>();
        cp4Buffer = Option<ProductBuffer>.Deserialize(reader);
        e3Buffer = Option<ProductBuffer>.Deserialize(reader);
        titaniumBuffer = Option<ProductBuffer>.Deserialize(reader);
        cargoShipProgess = Option<ProductBuffer>.Deserialize(reader);
        reader.SetField(this, "m_progressTimer", TickTimer.Deserialize(reader));
        partialProgress = reader.ReadInt();
    }

    static CargoDrydock()
    {
        s_serializeDataDelayedAction = delegate (object obj, BlobWriter writer)
        {
            ((CargoDrydock)obj).SerializeData(writer);
        };
        s_deserializeDataDelayedAction = delegate (object obj, BlobReader reader)
        {
            ((CargoDrydock)obj).DeserializeData(reader);
        };
    }
}
