using Mafi.Core.Mods;
using Mafi.Base;
using Mafi.Core.Research;
using Mafi;

namespace COIExtended.Prototypes.Research;
internal class Titanium : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
                 registrator.ResearchNodeProtoBuilder
                 .Start("Ilmenite Processing", NewIDs.Research.IlmeniteOreProcessing)
                 .Description("The process of extracting Titanium Ore from Ilmenite and creating Titanium Ingots.")
                 .AddProductToUnlock(NewIDs.Products.IlmeniteOre)
                 .AddProductToUnlock(NewIDs.Products.IlmeniteOreCrushed)
                 .AddProductToUnlock(NewIDs.Products.IlmeniteOreConcentrate)
                 .AddProductToUnlock(NewIDs.Products.TitaniumOre)
                 .AddProductToUnlock(NewIDs.Products.TitaniumDioxide)
                 .AddProductToUnlock(NewIDs.Products.MoltenTitanium)
                 .AddProductToUnlock(NewIDs.Products.ImpureTitanium)
                 .AddProductToUnlock(NewIDs.Products.TitaniumIngots)
                 .AddRecipeToUnlock(NewIDs.Recipes.IlmeniteSettling)
                 .AddRecipeToUnlock(NewIDs.Recipes.IlmeniteCrushing)
                 .AddRecipeToUnlock(NewIDs.Recipes.MoltenTitanium)
                 .AddRecipeToUnlock(NewIDs.Recipes.TitaniumElectrolysis)
                 .AddRecipeToUnlock(NewIDs.Recipes.GoldSettlingII)
                 .AddRecipeToUnlock(NewIDs.Recipes.UraniumLeachingII)
                 .AddRecipeToUnlock(NewIDs.Recipes.FlourideLeachingII)
                 .AddRecipeToUnlock(NewIDs.Recipes.NitricAcidGoldSettlingII)
                 .AddRecipeToUnlock(NewIDs.Recipes.NitricAcidUraniumSettlingII)
                 .AddRecipeToUnlock(NewIDs.Recipes.NitricAcidHFSettlingII)
                 .AddMachineToUnlock(NewIDs.Machines.SettlingTankII)
                 .SetCosts(new ResearchCostsTpl(29))
                 .AddProductIcon(NewIDs.Products.TitaniumIngots)
                 .SetGridPosition(new Vector2i(116, 42))
                 .AddParents(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.ResearchLab5))
                 .BuildAndAdd();
    }
}
