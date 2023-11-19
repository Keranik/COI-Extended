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

using Mafi.Core;
using Mafi.Core.World;

namespace COIExtended.Patches
{


    internal class WorldMapPatcher : APatcher<WorldMapPatcher>
    {
        public override bool DefaultState => true;
        public override bool Enabled => true;
        private static Type Typ;

        public WorldMapPatcher() : base("Generator")
        {
            Debug.Log("World Map Patcher Loaded");
                             // DLL to Target            // Class to target
            Typ = Assembly.Load("Mafi.Base").GetType("Mafi.Base.Prototypes.World.StaticWorldMap.Generator");
                            // Method to Target         // Type of Patch to Use
            AddMethod(Typ, "generateEntity", this.GetHarmonyMethod("MyPrefix"), true);
        }
        public override void OnInit(DependencyResolver resolver)
        {
            Debug.Log("World Map Patcher Initialized");
        }

                                       // Class __instance, References from the original method, [ref Typ ref1, ref Typ ref2]
        private static void MyPrefix(Type typeof(__instance), ref Proto.ID entityId, ref int kmStart, ref int kmEnd)
        {

            Debug.Log("World Map Patcher Going");
            



        }

    }
}
