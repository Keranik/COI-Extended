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
using System.Drawing.Drawing2D;
using Mafi.Core.PropertiesDb;
using static COIExtended.World.NewMapGenerator;
using Mafi.Collections;
using static Mafi.Base.Assets.Core;
using Mafi.Core.Simulation;

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
        private IReadOnlyDictionary<WorldMapLocId, WorldMapLocation> m_locations;
        private WorldMap m_currentMap;
        private IGameOverManager m_gameManager;

        //private readonly IInputScheduler _inputScheduler;

        public COIDebugWindowView(EntitiesManager entityManager, TravelingFleetManager fleetManager, WorldMapManager worldmapManager, ProtosDb protoDBB, CargoDepotManager c_manager, IGameOverManager gameManager) : base("COIDebugWindowView")
        {
            m_entitiesManager = entityManager;
            m_fleetManager = fleetManager;
            m_worldManager = worldmapManager;
            m_protosDb = protoDBB;
            m_cargoManager = c_manager;
            m_gameManager = gameManager;
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

            AddSectionTitle(_debugWindow, new LocStrFormatted("Generate New Map"), null, new Offset(15,0,0,0));
            var cargoBtn = Builder.NewBtnPrimary("GO")
                .SetButtonStyle(Builder.Style.Global.PrimaryBtn)
                .SetText(new LocStrFormatted("Make New Map (Slow)"))
                .OnClick(() => AddTitaniumSettlement());
            cargoBtn.AppendTo(_debugWindow, cargoBtn.GetOptimalSize(), ContainerPosition.MiddleOrCenter);

            var descTxt = Builder.NewTxt("Explanation")
                .SetText("This function will create a new map.  It can take a long time to do so!  Old contracts and quick trades will be duplicated in your list, this has no effect on gameplay.  New world map will take place of the old world map and new locations will be included.");
            descTxt.AppendTo(_debugWindow);

            AddSectionTitle(_debugWindow, new LocStrFormatted("Reverse Game Over"), null, new Offset(15, 0, 0, 0));
            var gameoverBtn = Builder.NewBtnPrimary("GO")
                .SetButtonStyle(Builder.Style.Global.PrimaryBtn)
                .SetText(new LocStrFormatted("Game Not Over"))
                .OnClick(() => UndoGameOver());
            gameoverBtn.AppendTo(_debugWindow, gameoverBtn.GetOptimalSize(), ContainerPosition.MiddleOrCenter);

            _debugWindow.PutTo(GetContentPanel());

        }

        public void UndoGameOver()
        {
            PropertyInfo isGameOverProperty = typeof(GameOverManager).GetProperty("IsGameOver", BindingFlags.Public | BindingFlags.Instance);
            if (isGameOverProperty != null && isGameOverProperty.CanWrite)
            {
                isGameOverProperty.SetValue(m_gameManager, false, null);
            }
            else
            {
                // Handle the case where the property is not found or cannot be written to
            }
        }
        public void AddTitaniumSettlement()
        {
            NewMapGenerator worldGen = new NewMapGenerator(m_protosDb);
            WorldMap newWorldMap = worldGen.CreateWorldMap();
            m_worldManager.SetMap(newWorldMap);
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
