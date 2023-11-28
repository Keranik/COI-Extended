using System.Linq;
using System.Runtime.CompilerServices;
using COIExtended.Prototypes.Buildings;
using Mafi.Core.Input;
using Mafi.Core.Population.Edicts;
using Mafi;
using Mafi.Base;
using Mafi.Unity.InputControl;
using Mafi.Unity.InputControl.Inspectors;
using Mafi.Unity.InputControl.Inspectors.Buildings;
using Mafi.Core;
using UnityEngine;

using Mafi.Core.Entities;
using Mafi.Unity.UiFramework;
using Mafi.Unity.UserInterface;
using System.Drawing;
using Mafi.Core.Buildings.Offices;
using Mafi.Core.GameLoop;
using Mafi.Core.Buildings.Cargo.Modules;

namespace COIExtended.UI;

[GlobalDependency(RegistrationMode.AsEverything, false, false)]
public class CargoDrydockInspector : IEntityInspector<CargoDrydock>
{
    private readonly IGameLoopEvents m_gameLoopEvents;
    public readonly IInputScheduler m_InputScheduler;
    private readonly CargoDrydockWindowView m_windowView;
    public bool DeactivateOnNonUiClick { get; private set; } = true;
    public CargoDrydock SelectedEntity { get; private set; }
    public InspectorContext Context { get; private set; }
    private readonly UiBuilder m_builder;

    public CargoDrydockInspector(InspectorContext inspectorContext, IInputScheduler inputScheduler,UiBuilder uiBuilder, IGameLoopEvents gameLoopEvents, CargoDepotManager cargoManager)
    {
        m_InputScheduler = inputScheduler;
        m_builder = uiBuilder;
        Context = inspectorContext;
        m_gameLoopEvents = gameLoopEvents;

        CargoDrydockWindowView happyWindow = new CargoDrydockWindowView(this, inputScheduler, cargoManager);
        if (happyWindow != null) { Debug.Log("New window created in inspector construct"); m_windowView = happyWindow;  } else { Debug.Log("New window not created?? FIX ME"); }
        m_windowView.BuildUi(uiBuilder);
    }

    public void Activate()
    {
        m_windowView.Show();
    }


    public void Deactivate()
    {
        m_windowView.Hide();
    }

    public void SyncUpdate(GameTime time)
    {
        if (m_windowView.IsVisible)
        {
            m_windowView.SyncUpdate(time);
        }
    }

    public void RenderUpdate(GameTime time)
    {
        if (m_windowView.IsVisible)
        {
            m_windowView.RenderUpdate(time);
        }
    }
    public bool InputUpdate(IInputScheduler inputScheduler)
    {
        return false;
    }

    public IEntityInspector Create(CargoDrydock entity)
    {
        Debug.LogFormat("Activating on Entity {0}", entity.Id);
        SelectedEntity = entity.CheckNotNull(); 
        return this;
    }
}
