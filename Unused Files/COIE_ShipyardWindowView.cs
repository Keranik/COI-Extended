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
using COIExtended.Prototypes.Buildings;
using Mafi.Core.Syncers;
using UnityEngine.PlayerLoop;
using Mafi.Core.Buildings.Beacons;
using System.Diagnostics.Eventing.Reader;
using Mafi.Core.Maintenance;
using System;

namespace COIExtended.UI
{
    [GlobalDependency(RegistrationMode.AsSelf)]
    public class COIE_ShipyardWindowView : StaticEntityInspectorBase<COIE_Shipyard>, IUiElementWithUpdater
    {

        private StackContainer _ShipyardWindow;
        private readonly ShipConstructionManager m_shipManager;
        //private readonly IInputScheduler _inputScheduler;
        protected override COIE_Shipyard Entity
        {
            get { return m_shipManager.COIE_Shipyard.Value; }
        }

        public COIE_ShipyardWindowView(IEntityInspector inspector, ShipConstructionManager shipManager) : base(inspector)
        {
            m_shipManager = shipManager;
        }

        public IUiUpdater Updater { get; }

        protected override void AddCustomItems(StackContainer itemContainer)
        {
            Debug.Log("ADD CUSTOM ITEMS TRIGGERED IN WINDOW");
            if (itemContainer != null)
            {
                Debug.Log("ITEM CONTAINER EXISTS");
                base.AddCustomItems(itemContainer);

                Panel panelDaddy = base.GetContentPanel();
                UpdaterBuilder updateBuilder = UpdaterBuilder.Start();

                _ShipyardWindow = Builder.NewStackContainer("COIDebugStackContainer")
                    .SetStackingDirection(StackContainer.Direction.TopToBottom)
                    .SetSizeMode(StackContainer.SizeMode.Dynamic);

                AddSectionTitle(_ShipyardWindow, new LocStrFormatted("Finish All Exploration"), null, new Offset(15, 0, 0, 0));
                var villagesBtn = Builder.NewBtnPrimary("GO")
                    .SetButtonStyle(Builder.Style.Global.PrimaryBtn)
                    .SetText(new LocStrFormatted("Reveal All Locations"));
                villagesBtn.AppendTo(_ShipyardWindow, villagesBtn.GetOptimalSize(), ContainerPosition.MiddleOrCenter);

                AddSectionTitle(_ShipyardWindow, new LocStrFormatted("Add Cargo Ship"), null, new Offset(15, 0, 0, 0));
                var cargoBtn = Builder.NewBtnPrimary("GO")
                    .SetButtonStyle(Builder.Style.Global.PrimaryBtn)
                    .SetText(new LocStrFormatted("Add Ship"));
                cargoBtn.AppendTo(_ShipyardWindow, cargoBtn.GetOptimalSize(), ContainerPosition.MiddleOrCenter);

                _ShipyardWindow.PutTo(panelDaddy);
                AddUpdater(updateBuilder.Build());
            }
            {
                Debug.Log("ITEM CONTAINER DOES NOT EXISTS");
            }
            
        }
    }
}
