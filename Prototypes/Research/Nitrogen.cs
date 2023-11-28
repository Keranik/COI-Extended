using Mafi.Core.Mods;
using Mafi.Base;
using Mafi.Core.Research;
using Mafi;

namespace COIExtended.Prototypes.Research;
internal class Nitrogen : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        registrator.ResearchNodeProtoBuilder
                .Start("Nitrogen Manipulation", NewIDs.Research.NitrogenManipulation)
                .Description("More processes involving the usage of Nitrogen gas.")
                .AddMachineToUnlock(NewIDs.Machines.AirSeparatorT2, false)
                .AddRecipeToUnlock(NewIDs.Recipes.AirSeparationT2)
                .AddRecipeToUnlock(NewIDs.Recipes.NitrogenDioxide)
                .AddRecipeToUnlock(NewIDs.Recipes.NitricAcidRecipe)
                .AddRecipeToUnlock(NewIDs.Recipes.NitricAcidElectrolysis)
                .AddRecipeToUnlock(NewIDs.Recipes.NitricAcidGlassMix)
                .AddRecipeToUnlock(NewIDs.Recipes.NitricAcidGoldSettling)
                .AddRecipeToUnlock(NewIDs.Recipes.NitricAcidHFSettling)
                .AddRecipeToUnlock(NewIDs.Recipes.NitricAcidUraniumSettling)
                .AddProductToUnlock(NewIDs.Products.NitrogenDioxide)
                .AddProductToUnlock(NewIDs.Products.NitricAcid)
                .SetCosts(new ResearchCostsTpl(22))
                .AddProductIcon(NewIDs.Products.NitrogenDioxide)
                .AddProductIcon(NewIDs.Products.NitricAcid)
                .AddParents(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.GoldSmelting))
                .SetGridPosition(new Vector2i(104, 14))
                .BuildAndAdd();
    }
}
