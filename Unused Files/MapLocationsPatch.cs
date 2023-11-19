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
using Mafi.Core.Mods;


namespace COIExtended.Patches
{
    internal class TestPatcher : APatcher<TestPatcher>
    {
        public override bool DefaultState => true;
        public override bool Enabled => true;
        private static Type Typ;
        private static ProtosDb m_protosDb;
        public TestPatcher() : base("WorldMapLocation")
        {
            Debug.Log("Test Patcher Loaded");
            Typ = Assembly.Load("Mafi.Core").GetType("Mafi.Core.World.WorldMapLocation");
            AddMethod(Typ, "SetEntityProto", this.GetHarmonyMethod("MyPrefix"), true);
        }
        public override void OnInit(DependencyResolver resolver)
        {
            Debug.Log("Test Patcher Initialized");
            Option<ProtosDb> protosDbOption = resolver.GetResolvedInstance<ProtosDb>();
            Debug.Log("Test Patcher Initialized2");
            ProtosDb protosDb = protosDbOption.Value;
            Debug.Log("Test Patcher Initialized3");
            m_protosDb = protosDb;
            Debug.Log("Test Patcher Initialized4");

        }


        private static void MyPrefix(WorldMapLocation __instance, ref WorldMapEntityProto entityProto)
        {
            Debug.Log("Test Patcher caught SetEntityProto in the action");
            __instance.SetField("EntityProto", COIExtended.World.COIE_OilRigCost1);
            Debug.Log("Test Patcher caught SetEntityProto in the action");



        }

    }
}