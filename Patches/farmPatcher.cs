using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Mafi.Core.Buildings.Cargo.Ships;
using Mafi;
using Mafi.Core.Buildings.Farms;

namespace COIExtended
{
    [HarmonyPatch(typeof(Farm), "cctor")]
    internal static class farmPatcher
    {
        public static void runPatch()
        {
            try
            {
                Type farmType = typeof(Farm);
                FieldInfo sliderValue = farmType.GetField("FERTILITY_PER_SLIDER_STEP", BindingFlags.Public | BindingFlags.Static);
                FieldInfo sliderValueMax = farmType.GetField("MAX_FERTILITY_SLIDER_VALUE", BindingFlags.Public | BindingFlags.Static);

                if (sliderValue != null)
                {
                    sliderValue.SetValue(null, 1.Percent());
                }                

                if (sliderValueMax != null)
                {
                    sliderValueMax.SetValue(null, 140.Percent());
                }
            }
            catch { }
        }

    }
}