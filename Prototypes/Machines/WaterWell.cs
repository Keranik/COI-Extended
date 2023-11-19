using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Base;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Factory.WellPumps;
using Mafi.Core;
using Mafi.Localization;
using Mafi;

namespace COIExtended.Prototypes.Machines;
internal class Waterwell : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        LocStr locStr2 = Loc.Str("GroundReserveTooltip__Groundwater", "Shows the overall status of the reserve of groundwater. Groundwater is replenished during rain and can temporarily run out if pumped out too much.", "tooltip");
        LocStr desc = Loc.Str(Ids.Machines.LandWaterPump.Value + "__desc", "Pumps water from the ground deposit which is replenished during rain. Has to be built on top of a groundwater deposit.", "description of ground water pump");

        WellPumpProto machine2 = registrator.WellPumpProtoBuilder
            .Start("Groundwater Pump II", locStr2.TranslatedString, NewIDs.Machines.LandWaterPumpT2).Description(desc)
            .SetCost(NewCosts.Machines.LandWaterPumpT2, false)
            .SetElectricityConsumption(240.Kw())
            .SetMinedProduct(IdsCore.Products.Groundwater)
            .NotifyWhenBelow(40.Percent())
            .SetCategories(new Proto.ID[]
        {
                Ids.ToolbarCategories.MachinesWater
        })
            .SetLayout(new string[]
        {
                "[2][7][7][2]   ",
                "[2][7][7][2]   ",
                "[2][7][7][2]   ",
                "[2][4][4][2]>@X",
                "   [2][2][2]   ",
                "   [2][2][2]   "
        })
            .SetPrefabPath("Assets/Base/Machines/Pump/LandWaterPump.prefab")
            .SetCustomIconPath("Assets/Unity/Generated/Icons/LayoutEntity/LandWaterPump.png")
            .SetAnimationParams(AnimationParams.Loop(null, false, null))
            .SetMachineSound("Assets/Base/Machines/Pump/LandWaterPump/LandWaterPump_Sound.prefab")
            .EnableSemiInstancedRendering(default)
            .BuildAndAdd();
        registrator.RecipeProtoBuilder.Start("Water Pumping II", NewIDs.Recipes.LargeLandWaterPumping, machine2).AddOutput(24, Ids.Products.Water, "*", false, false).SetDurationSeconds(10)
            .BuildAndAdd();
    }

}
