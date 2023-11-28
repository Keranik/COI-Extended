using System;
using Mafi;
using Mafi.Core.GameLoop;
using Mafi.Unity;
using Mafi.Unity.InputControl;
using Mafi.Unity.InputControl.Toolbar;
using Mafi.Unity.UiFramework;
using Mafi.Unity.UserInterface;
using UnityEngine;


namespace COIExtended.UI
{
    [GlobalDependency(RegistrationMode.AsEverything)]
    public class COIExtendedWindowController : BaseWindowController<COIDebugWindowView>, IToolbarItemInputController
    {
        private readonly ToolbarController _toolbarController;
        

        public COIExtendedWindowController(IUnityInputMgr inputManager, IGameLoopEvents gameLoop, COIDebugWindowView debugWindowView, ToolbarController toolbarController)
            : base(inputManager, gameLoop, debugWindowView)
        {
            _toolbarController = toolbarController;
        }


        public override void RegisterUi(UiBuilder builder)
        {
            _toolbarController.AddMainMenuButton("COI: Extended Debug Menu", this, "Assets/COIExtended/Icons/DebugMenu.png", 1338f, _ => KeyBindings.FromKey(KbCategory.Tools, KeyCode.F9));

            base.RegisterUi(builder);
        }

        public bool IsVisible => true;
        public bool DeactivateShortcutsIfNotVisible => false;
        public event Action<IToolbarItemInputController> VisibilityChanged;
    }
}