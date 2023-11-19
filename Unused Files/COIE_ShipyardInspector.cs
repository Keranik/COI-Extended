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
using UnityEngine.UIElements;
using Mafi.Core.Buildings.Offices;
using Mafi.Unity.UserInterface;
using System.Drawing;
using Mafi.Unity;

namespace COIExtended.UI;

[GlobalDependency(RegistrationMode.AsEverything)]
public class COIE_ShipyardInspector : IEntityInspector<COIE_Shipyard>
{
    private readonly IInputScheduler m_InputScheduler;
    //private readonly COIE_ShipyardWindowController m_controller;
    private readonly COIE_ShipyardWindowView m_ShipyardWindow;
    public virtual bool DeactivateOnNonUiClick => true;
    public COIE_Shipyard SelectedEntity { get; private set; }
    public InspectorContext Context { get; private set; }
    public COIE_ShipyardInspector(InspectorContext inspectorContext, IInputScheduler inputScheduler, ShipConstructionManager shipManager)
    {
        m_InputScheduler = inputScheduler;
        Debug.Log("COIE_ShipyardInspector called");
        Context = inspectorContext;
        m_ShipyardWindow = new COIE_ShipyardWindowView(this, shipManager);
        if (m_ShipyardWindow != null)
        {
            Debug.Log("Shipyard window created and exists");
        }
        else
        {
            Debug.Log("Shipyard window created and doesn't exist.");
        }
    }
    public void Activate()
    {
        Debug.Log("COIE_ShipyardInspector(Activate) was called");
        m_ShipyardWindow.Show();
        
    }

    public void Deactivate()
    {
        Debug.Log("COIE_ShipyardInspector(Deactivate) was called");
    }

    public void SyncUpdate(GameTime gameTime)
    {
        Debug.Log("sync update");
    }

    public void RenderUpdate(GameTime gameTime)
    {
        Debug.Log("render update");
    }

    

    public bool InputUpdate(IInputScheduler inputScheduler)
    {
        Debug.Log("ShipyardInspector InputUpdate");
        return true; // or false depending on whether the input was handled
    }

    public IEntityInspector Create(COIE_Shipyard entity)
    {
        Debug.Log("ShipyardInspector/Create/Setting SelectedEntity");
        SelectedEntity = entity.CheckNotNull();
        return this;
    }
}
