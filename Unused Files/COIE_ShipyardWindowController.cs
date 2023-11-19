using System;
using System.Xml;
using Mafi;
using Mafi.Core;
using Mafi.Core.GameLoop;
using Mafi.Core.Input;
using Mafi.Unity;
using Mafi.Unity.InputControl;
using Mafi.Unity.InputControl.Inspectors;
using Mafi.Unity.InputControl.Toolbar;
using Mafi.Unity.UiFramework;
using Mafi.Unity.UserInterface;
using UnityEngine;


namespace COIExtended.UI
{
    [GlobalDependency(RegistrationMode.AsEverything)]
    public class COIE_ShipyardWindowController : BaseWindowController<COIE_ShipyardWindowView>, IUnityUi, IUnityInputController
    {
        private readonly IGameLoopEvents m_gameLoop;
        private readonly COIE_ShipyardWindowView m_window;
        

        public COIE_ShipyardWindowController(IUnityInputMgr inputManager, IGameLoopEvents gameLoop, COIE_ShipyardWindowView shipyardWindowView)
            : base(inputManager, gameLoop, shipyardWindowView)
        {
            m_gameLoop = gameLoop;  
            m_window = shipyardWindowView;
            Debug.Log("Controller called");
        }     

        public bool IsVisible => true;
        public bool DeactivateShortcutsIfNotVisible => false;

    }
}