using Mafi;
using Mafi.Localization;
using Mafi.Unity.UiFramework;
using Mafi.Unity.UiFramework.Components;
using Mafi.Unity.UserInterface;
using Mafi.Unity.UserInterface.Components;
using UnityEngine;
using Mafi.Unity;
using Mafi.Core.Entities;
using Mafi.Core.World;
using Mafi.Core.Prototypes;
using Mafi.Base.Prototypes.World;
using System.Xml.Linq;
using UnityEngine.UIElements;
using Mafi.Core.World.Entities;
using System.Collections.Generic;
using System.Linq;
using COIExtended.World;
using System.Reflection;
using Mafi.Core.Buildings.Cargo.Modules;
using COIExtended.Extensions;
using Mafi.Core;
using Mafi.Unity.InputControl.Inspectors;

namespace COIExtended.UI
{
    [GlobalDependency(RegistrationMode.AsEverything)]
    public class COIDebugWindowView : WindowView
    {

        private StackContainer _debugWindow;
        private EntitiesManager m_entitiesManager;
        private TravelingFleetManager m_fleetManager;
        private WorldMapManager m_worldManager;
        private ProtosDb m_protosDb;
        private CargoDepotManager m_cargoManager;
        
        //private readonly IInputScheduler _inputScheduler;

        public COIDebugWindowView(EntitiesManager entityManager, TravelingFleetManager fleetManager, WorldMapManager worldmapManager, ProtosDb protoDBB, CargoDepotManager c_manager) : base("COIDebugWindowView")
        {
            m_entitiesManager = entityManager;
            m_fleetManager = fleetManager;
            m_worldManager = worldmapManager;
            m_protosDb = protoDBB;
            m_cargoManager = c_manager;
        }

        protected override void BuildWindowContent()
        {
            SetTitle(new LocStrFormatted("COI: Extended - Debug Menu"));
            var size = new Vector2(420f, 420f);
            SetContentSize(size);
            PositionSelfToCenter();
            MakeMovable();
            _debugWindow = Builder.NewStackContainer("COIDebugStackContainer")
                .SetStackingDirection(StackContainer.Direction.TopToBottom)
                .SetSizeMode(StackContainer.SizeMode.Dynamic);




            AddSectionTitle(_debugWindow, new LocStrFormatted("Finish All Exploration"), null, new Offset(15, 0, 0, 0));
            var villagesBtn = Builder.NewBtnPrimary("GO")
                .SetButtonStyle(Builder.Style.Global.PrimaryBtn)
                .SetText(new LocStrFormatted("Reveal All Locations"))
                .OnClick(() => RevealAllVillages());
            villagesBtn.AppendTo(_debugWindow, villagesBtn.GetOptimalSize(), ContainerPosition.MiddleOrCenter);

            AddSectionTitle(_debugWindow, new LocStrFormatted("Add Cargo Ship"), null, new Offset(15,0,0,0));
            var cargoBtn = Builder.NewBtnPrimary("GO")
                .SetButtonStyle(Builder.Style.Global.PrimaryBtn)
                .SetText(new LocStrFormatted("Add Ship"))
                .OnClick(() => AddCargoShip());
            cargoBtn.AppendTo(_debugWindow, cargoBtn.GetOptimalSize(), ContainerPosition.MiddleOrCenter);
            
            _debugWindow.PutTo(GetContentPanel());

        }

        public void RevealAllVillages()
        {
            foreach (KeyValuePair<string, WorldMapLocation> locationEntry in WorldMapLocationsHolder.Locations)
            {
                string locationKey = locationEntry.Key; // This is the key in the dictionary, e.g., "Location1"
                WorldMapLocation locationValue = locationEntry.Value; // This is the actual WorldMapLocation object
                m_worldManager.Map.Visit(locationValue, 2, 500);
            }

        }


        public void AddCargoShip()
        {
            
            int numnow = m_cargoManager.AmountOfShipsRepaired + 1;
            int numnow2 = m_cargoManager.AmountOfShipsDiscovered + 1;

            Debug.LogFormat("Amount of ships repaired: {0}",numnow);
            PropertyInfo myProperty = typeof(CargoDepotManager).GetProperty("AmountOfShipsRepaired", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            Debug.LogFormat("Grabbed property {0}",myProperty);
            MethodInfo setMethod = myProperty.GetSetMethod(nonPublic: true);
            setMethod.Invoke(m_cargoManager, new object[] { numnow });

            Debug.LogFormat("Amount of ships discovered: {0}", numnow);
            PropertyInfo myProperty2 = typeof(CargoDepotManager).GetProperty("AmountOfShipsDiscovered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            Debug.LogFormat("Grabbed property {0}", myProperty2);
            MethodInfo setMethod2 = myProperty2.GetSetMethod(nonPublic: true);
            setMethod2.Invoke(m_cargoManager, new object[] { numnow2 });
        }

    }
}
