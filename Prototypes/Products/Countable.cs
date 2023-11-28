using Mafi;
using Mafi.Base;
using Mafi.Core.Entities.Static;
using Mafi.Core.Game;
using Mafi.Core.Mods;
using Mafi.Core.Population;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;

namespace COIExtended.Prototypes.Products;
internal class Countable : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        Proto.Str titaniumStrings = Proto.CreateStr(
            NewIDs.Products.TitaniumIngots, 
            "Titanium",
            "Titanium is a lightweight, strong, corrosion-resistant metal with a silver color, used widely in aerospace, medical, and consumer applications due to its high strength-to-weight ratio.",
            null
        );
        CountableProductProto.Gfx titaniumGFX = new CountableProductProto.Gfx(
            "Assets/COIExtended/Products/Titanium.prefab",
            "Assets/COIExtended/Icons/Titanium.png",
            CountableProductStackingMode.Auto,
            false,
            false
        );
        CountableProductProto titaniumProduct = new CountableProductProto(
            NewIDs.Products.TitaniumIngots,
            titaniumStrings,
            new Quantity(3),
            true,
            titaniumGFX,
            0, // Base price
            false, // Can be sold
            true, // Can be bought
            false, // Is tradable
            NewIDs.Products.ImpureTitanium, // Upgraded from
            new PartialQuantity(0.5.ToFix32()), // Upgrade quantity
            null // Custom order proto
        );
        registrator.PrototypesDb.Add(titaniumProduct, false);

        Proto.Str impureTitaniumString = Proto.CreateStr(
            NewIDs.Products.ImpureTitanium,
            "Impure Titanium",
            "Impure titanium, prior to electrorefining, refers to the raw, unrefined form of titanium metal that contains various other elements and impurities, making it less suitable for specialized applications.",
            null
        );
        CountableProductProto.Gfx impureTitaniumGFX = new CountableProductProto.Gfx(
            "Assets/COIExtended/Products/Titanium.prefab",
            "Assets/COIExtended/Icons/ImpureTitanium.png",
            CountableProductStackingMode.Auto,
            false,
            false
        );
        CountableProductProto impureTitaniumProduct = new CountableProductProto(
            NewIDs.Products.ImpureTitanium,
            impureTitaniumString,
            new Quantity(3), // Stack size
            true, // Is countable
            impureTitaniumGFX,
            0, // Base price
            false, // Can be sold
            true, // Can be bought
            false, // Is tradable
            NewIDs.Products.MoltenTitanium, // Upgraded from
            new PartialQuantity(0.5.ToFix32()), // Upgrade quantity
            null // Custom order proto
        );        
        registrator.PrototypesDb.Add(impureTitaniumProduct,false);


        Proto.Str CP5string = Proto.CreateStr(
            NewIDs.Products.ConstructionParts5,
            "Construction Parts V",
            "Construction Parts V",
            null
        );

        CountableProductProto.Gfx CP5GFX = new CountableProductProto.Gfx(
            "Assets/COIExtended/Products/ConstructionPartsV.prefab",
            "Assets/COIExtended/Icons/ConstructionParts5.png",
            CountableProductStackingMode.Auto,
            false,
            false
        );

        CountableProductProto CP5Product = new CountableProductProto(
            NewIDs.Products.ConstructionParts5,
            CP5string,
            new Quantity(3), // Stack size
            true, // Is countable
            CP5GFX,
            0, // Base price
            false, // Can be sold
            true, // Can be bought
            false, // Is tradable
            Ids.Products.ConstructionParts4, // Upgraded from
            new PartialQuantity(0.5.ToFix32())       
             
        );

        registrator.PrototypesDb.Add(CP5Product, false);

        Proto.Str freshFishString = Proto.CreateStr(
            NewIDs.Products.FreshFish,
            "Fresh Fish",
            "Fresh fish is characterized by bright, clear eyes, firm flesh, a moist appearance, and a mild sea breeze-like scent, without any overpowering fishy odor.",
            null
        );
        CountableProductProto.Gfx freshFishGFX = new CountableProductProto.Gfx(
            "Assets/COIExtended/Products/FreshFish.prefab",
            "Assets/COIExtended/Icons/FreshFish.png",
            CountableProductStackingMode.Auto,
            false,
            false
        );
        CountableProductProto freshFishProduct = new CountableProductProto(
            NewIDs.Products.FreshFish,
            freshFishString,
            new Quantity(3), // Stack size
            true, // Is countable
            freshFishGFX,
            0, // Base price
            false, // Can be sold
            true, // Can be bought
            false, // Is tradable
            null, // Upgraded from
            new PartialQuantity(0.5.ToFix32()), // Upgrade quantity
            null // Custom order proto
        );
        registrator.PrototypesDb.Add(freshFishProduct, false);

        
        GameDifficultyConfig gameDifficultyConfig = registrator.GetConfigOrThrow<GameDifficultyConfig>();
        Fix32 consumedPerHundredPopsPerMonth = withDifficulty(5.4);
        FoodCategoryProto foodCategoryVitamins = registrator.PrototypesDb.GetOrThrow<FoodCategoryProto>(Ids.FoodCategories.Vitamins);
        Proto.ID fishFood = NewIDs.FoodTypes.FreshFish;
        ProductProto fishFoodProto = registrator.PrototypesDb.GetOrThrow<ProductProto>(NewIDs.Products.FreshFish);
        registrator.PrototypesDb.Add(new FoodProto(fishFood, fishFoodProto, foodCategoryVitamins, consumedPerHundredPopsPerMonth, withDifficultyUpoints(0.55)));

        Fix32 withDifficulty(double consumed)
        {
            return consumed.ToFix32().ScaledBy(gameDifficultyConfig.SettlementConsumptionMult);
        }
        Upoints withDifficultyUpoints(double unity)
        {
            return unity.Upoints().ScaledBy(gameDifficultyConfig.UnityProductionMult);
        }
    }

}