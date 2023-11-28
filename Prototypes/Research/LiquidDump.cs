using Mafi.Core.Mods;
using Mafi.Base;
using Mafi.Core.Research;
using Mafi;

namespace COIExtended.Prototypes.Research;
internal class LiquidDump : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        registrator.ResearchNodeProtoBuilder
                .Start("Large Waste Disposal", NewIDs.Research.UnlockLargeWasteDumps)
                .Description("The next generation in waste removal!")
                .AddMachineToUnlock(NewIDs.Machines.LargeLiquidDump, false)
                .SetCosts(new ResearchCostsTpl(7))
                .AddRecipeToUnlock(NewIDs.Recipes.LargeWaterDumping)
                .AddRecipeToUnlock(NewIDs.Recipes.LargeSeaWaterDumping)
                .AddRecipeToUnlock(NewIDs.Recipes.LargeBrineDumping)
                .AddRecipeToUnlock(NewIDs.Recipes.LargeWasteWaterDumping)
                .AddRecipeToUnlock(NewIDs.Recipes.LargeSourWaterDumping)
                .AddRecipeToUnlock(NewIDs.Recipes.LargeAcidDumping)
                .AddRecipeToUnlock(NewIDs.Recipes.LargeToxicDumping)
                .AddRecipeToUnlock(NewIDs.Recipes.LargeFert1Dumping)
                .AddRecipeToUnlock(NewIDs.Recipes.LargeFert2Dumping)
                .AddRecipeToUnlock(NewIDs.Recipes.LargeFert3Dumping)
                .AddLayoutEntityToUnlock(NewIDs.Transports.StackerT2)
                .AddLayoutEntityToUnlock(NewIDs.Transports.StackerLong)
                .SetGridPosition(new Vector2i(36, 28))
                .AddParents(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.ThermalDesalinationBasic))
                .BuildAndAdd();
    }
}
