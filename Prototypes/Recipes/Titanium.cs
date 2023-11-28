using Mafi.Core.Mods;
using Mafi;
using Mafi.Base;

namespace COIExtended.Prototypes.Recipes;
internal class Titanium : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
         
        registrator.RecipeProtoBuilder.Start("Titanium Smelting", NewIDs.Recipes.MoltenTitanium, Ids.Machines.ArcFurnace2)
                 .AddInput(12, NewIDs.Products.TitaniumOre)
                 .AddInput(2, Ids.Products.Coal)
                 .AddInput(1, Ids.Products.Graphite)
                 .AddInput(2, Ids.Products.Water, "D")
                 .SetDuration(Duration.FromSec(20))
                 .AddOutput(16, NewIDs.Products.MoltenTitanium)
                 .AddOutput(2, Ids.Products.Slag)
                 .AddOutput(2, Ids.Products.SteamLo, "Z")
                 .BuildAndAdd();

        registrator.RecipeProtoBuilder.Start("Titanium Casting", NewIDs.Recipes.ImpureTitanium, Ids.Machines.CasterCooledT2)
                 .AddInput(18, NewIDs.Products.MoltenTitanium)
                 .AddInput(4, Ids.Products.Water)
                 .SetDuration(Duration.FromSec(45))
                 .AddOutput(18, NewIDs.Products.ImpureTitanium)
                 .BuildAndAdd();

        registrator.RecipeProtoBuilder.Start("Titanium Purifying", NewIDs.Recipes.TitaniumElectrolysis, Ids.Machines.CopperElectrolysis)
                 .AddInput(18, NewIDs.Products.ImpureTitanium)
                 .AddInput(4, NewIDs.Products.NitricAcid)
                 .SetDuration(Duration.FromSec(45))
                 .AddOutput(18, NewIDs.Products.TitaniumIngots)
                 .BuildAndAdd();

        registrator.RecipeProtoBuilder.Start("Construction Parts V", NewIDs.Recipes.ConstructionPartsV, Ids.Machines.AssemblyRoboticT2)
                .AddInput(8, Ids.Products.ConstructionParts4)
                .AddInput(4, NewIDs.Products.TitaniumIngots)
                .AddOutput(4, NewIDs.Products.ConstructionParts5)
                .SetDuration(Duration.FromSec(20))
                .BuildAndAdd();


    }

}