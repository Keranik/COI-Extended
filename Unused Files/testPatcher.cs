using COIExtended.HarmonyExt;
using COIExtended.Patcher;
using Mafi;
using Mafi.Unity;
using Mafi.Unity.InputControl;
using Mafi.Core.Factory.Transports;
using Mafi.Unity.InputControl.Inspectors;
using Mafi.Unity.UiFramework;
using Mafi.Unity.UiFramework.Components;
using Mafi.Unity.UserInterface;
using System;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;
using Mafi.Base.Prototypes.World;
using Mafi.Base.Prototypes;
using Mafi.Core.Prototypes;
using Mafi.Core.Factory.Machines;
using Mafi.Core;
using Mafi.Core.World;
using Mafi.Core.World.Entities;

namespace COIExtended.Patches
{


    internal class TestPatcher : APatcher<TestPatcher>
    {
        public override bool DefaultState => true;
        public override bool Enabled => true;
        private static Type Typ;

        public TestPatcher() : base("WorldMapManager")
        {
            Debug.Log("Test Patcher Loaded");
            Typ = Assembly.Load("Mafi.Core").GetType("Mafi.Core.Factory.Machines.MachineProtoBuilder");
            AddMethod(Typ, "Start", this.GetHarmonyMethod("MyPrefix"), true);
        }
        public override void OnInit(DependencyResolver resolver)
        {
            Debug.Log("Test Patcher Initialized");
        }

        
        private static void MyPrefix(MachineProtoBuilder __instance, ref string name, ref MachineProto.ID machineId, ref string translationComment)
        {
            Debug.Log("Test Patcher caught Setmap in the action");
        }

    }
}
