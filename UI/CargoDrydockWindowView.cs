using Mafi;
using Mafi.Localization;
using Mafi.Unity.UiFramework;
using Mafi.Unity.UiFramework.Components;
using Mafi.Unity.UserInterface.Components;
using UnityEngine;
using Mafi.Unity;
using Mafi.Core.Entities;
using Mafi.Core.World;
using Mafi.Core.Prototypes;
using System.Collections.Generic;
using COIExtended.World;
using System.Reflection;
using Mafi.Core.Buildings.Cargo.Modules;
using Mafi.Unity.InputControl.Inspectors;
using COIExtended.Prototypes.Buildings;
using COIExtended;
using Mafi.Base;
using Mafi.Core.Syncers;
using Mafi.Core.Population;
using System;
using Mafi.Core;
using System.Diagnostics;
using Mafi.Unity.UiFramework.Components.Tabs;
using Mafi.Core.Maintenance;
using UnityEngine.PlayerLoop;
using Mafi.Core.Input;
using static Mafi.Unity.Assets.Unity;
using Mafi.Base.Prototypes.Buildings;
using Mafi.Core.Products;
using Mafi.Core.Entities.Static;
using Mafi.Core.Buildings.RainwaterHarvesters;

namespace COIExtended.UI
{
    [GlobalDependency(RegistrationMode.AsEverything)]
    public class CargoDrydockWindowView : StaticEntityInspectorBase<CargoDrydock>

    {
       

        //private StackContainer _ShipyardWindow;
        public bool DeactivateOnNonUiClick = true;
        //private IEntityInspector m_inspector;
        private IInputScheduler m_inputScheduler;
        private CargoDepotManager m_cargoManager;
        public CargoDrydock Shipyard => m_controller.SelectedEntity;
        private readonly CargoDrydockInspector m_controller;
        protected override CargoDrydock Entity => m_controller.SelectedEntity;
        BufferView cargoShipProgressBuffer { get; set; }
        BufferView titaniumBuffer { get; set; }
        BufferView CP4Buffer { get; set; }
        BufferView E3Buffer { get; set; }
        Btn deployShip { get; set; }

        public CargoDrydockWindowView(CargoDrydockInspector controller, IInputScheduler inputScheduler, CargoDepotManager cargoManager) : base(controller)
        {
            m_inputScheduler = inputScheduler;
            //m_inspector = inspector;
            m_cargoManager = cargoManager;
            m_controller = controller;
            
               
        }

        



        protected override void AddCustomItems(StackContainer itemContainer)
        {
            
            base.AddCustomItems(itemContainer);
            UpdaterBuilder uBuilder = UpdaterBuilder.Start();
            
            AddSectionTitle(itemContainer, "SHIP CONSTRUCTION PROGRESS");
            

            cargoShipProgressBuffer = AddBufferView(69, null, null, null, false);
            cargoShipProgressBuffer.SetCustomIcon("Assets/Unity/UserInterface/WorldMap/CargoShipStoryIcon256.png");
            cargoShipProgressBuffer.SetTextToShowWhenEmpty("None");
            

            deployShip = AddButton(itemContainer, null, "Deploy New Ship");
            deployShip.SetEnabled(false);
            deployShip.OnClick(() => AddCargoShip());
            AddSectionTitle(itemContainer, "INPUTS");

            titaniumBuffer = AddBufferView(69, null, null, null, false);
            titaniumBuffer.SetCustomIcon("Assets/COIExtended/Icons/Titanium.png");

            CP4Buffer = AddBufferView(69, null, null, null, false);
            CP4Buffer.SetCustomIcon("Assets/Base/Products/Icons/ConstructionParts4.svg");
            E3Buffer = AddBufferView(69, null, null, null, false);
            E3Buffer.SetCustomIcon("Assets/Base/Products/Icons/Electronics3.svg");

        uBuilder.Observe(() => Entity.titaniumBuffer.ValueOrNull?.Quantity)
                .Do(quantity => {
                    if (Entity.titaniumBuffer.HasValue && quantity.HasValue)
                    {
                        ProductProto product = Entity.titaniumBuffer.Value.Product;
                        Quantity capacity = Entity.titaniumBuffer.Value.Capacity;
                        titaniumBuffer.UpdateState(product, capacity, quantity.Value);
                    }
                });

            uBuilder.Observe(() => Entity.cp4Buffer.ValueOrNull?.Quantity)
                    .Do(quantity => {
                        if (Entity.cp4Buffer.HasValue && quantity.HasValue)
                        {
                            ProductProto product = Entity.cp4Buffer.Value.Product;
                            Quantity capacity = Entity.cp4Buffer.Value.Capacity;
                            CP4Buffer.UpdateState(product, capacity, quantity.Value);
                        }
                    });

            uBuilder.Observe(() => Entity.e3Buffer.ValueOrNull?.Quantity)
                    .Do(quantity => {
                        if (Entity.e3Buffer.HasValue && quantity.HasValue)
                        {
                            ProductProto product = Entity.e3Buffer.Value.Product;
                            Quantity capacity = Entity.e3Buffer.Value.Capacity;
                            E3Buffer.UpdateState(product, capacity, quantity.Value);
                        }
                    });
            uBuilder.Observe(() => Entity.cargoShipProgess.ValueOrNull?.Quantity)
                    .Do(quantity => {
                        if (Entity.cargoShipProgess.HasValue && quantity.HasValue)
                        {
                            ProductProto product = Entity.cargoShipProgess.Value.Product;
                            Quantity capacity = Entity.cargoShipProgess.Value.Capacity;
                            cargoShipProgressBuffer.UpdateState(product, capacity, quantity.Value);

                            if (Entity.cargoShipProgess.Value.Quantity.Value >= 100 && !deployShip.IsEnabled)
                            {
                                deployShip.SetEnabled(true);
                            }
                            else if (Entity.cargoShipProgess.Value.Quantity.Value < 100 && deployShip.IsEnabled)
                            {
                                deployShip.SetEnabled(false);
                            }
                        }
                    });

            AddUpdater(uBuilder.Build());
        }
        protected override void AddCustomItemsEnd(StackContainer itemContainer)
        {
            base.AddCustomItemsEnd(itemContainer);   
        }
        protected override void BuildWindowContent()
        {
            base.BuildWindowContent();            
        }   
        public override void RenderUpdate(GameTime gameTime)
        {
            
            base.RenderUpdate(gameTime);
        }
        public override void SyncUpdate(GameTime gameTime)
        {
            base.SyncUpdate(gameTime);
        }
        public void AddCargoShip()
        {
            int numnow = m_cargoManager.AmountOfShipsRepaired + 1;
            int numnow2 = m_cargoManager.AmountOfShipsDiscovered + 1;

            PropertyInfo myProperty = typeof(CargoDepotManager).GetProperty("AmountOfShipsRepaired", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            MethodInfo setMethod = myProperty.GetSetMethod(nonPublic: true);
            setMethod.Invoke(m_cargoManager, new object[] { numnow });

            PropertyInfo myProperty2 = typeof(CargoDepotManager).GetProperty("AmountOfShipsDiscovered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            MethodInfo setMethod2 = myProperty2.GetSetMethod(nonPublic: true);
            setMethod2.Invoke(m_cargoManager, new object[] { numnow2 });

            Entity.cargoShipProgess.Value.RemoveAll();
        }
     }
}
