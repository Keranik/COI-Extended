using Mafi.Core.Mods;
using Mafi.Base;
using Mafi.Core.Research;
using Mafi;

namespace COIExtended.Prototypes.Research;
internal class AdvancedPumping : IModData
{ 
    public void RegisterData(ProtoRegistrator registrator)
    {
                registrator.ResearchNodeProtoBuilder
                .Start("Advanced Pumping", NewIDs.Research.AdvancedPumping)
                .Description("The next generation of pumping!")
                .AddMachineToUnlock(NewIDs.Machines.OceanWaterPumpT2, false)
                .AddMachineToUnlock(NewIDs.Machines.OceanWaterPumpTallT2, false)
                .AddMachineToUnlock(NewIDs.Machines.LandWaterPumpT2, false)
                .AddRecipeToUnlock(NewIDs.Recipes.LargeLandWaterPumping)
                .AddRecipeToUnlock(NewIDs.Recipes.LargeSeaWaterPumping)
                .AddRecipeToUnlock(NewIDs.Recipes.LargeSeaWaterPumpingTall)
                .SetGridPosition(new Vector2i(76, 29))
                .SetCosts(new ResearchCostsTpl(14))
                .AddParents(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.PipeTransportsT3))
                .BuildAndAdd();
    }

}
