using HarmonyLib;
using System;
using System.Reflection;
using Mafi.Core.Buildings.Cargo.Ships;
using Mafi;


namespace COIExtended.Patches
{


    [HarmonyPatch(typeof(CargoShip), "cctor")]
    internal static class cargoShipPatcher
    {
        
        public static void runPatch()
        {
            try
            {
                Type cargoShipType = typeof(CargoShip);
                FieldInfo worldDelayField = cargoShipType.GetField("WORLD_DELAY_ONE_WAY", BindingFlags.Static | BindingFlags.NonPublic);

                if (worldDelayField != null)
                {
                    worldDelayField.SetValue(null, 55.38.Seconds());
                }


            }
            catch { }
        }

    }
}