using Mafi;
using Mafi.Core.Mods;
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
            "Titanium can be used in many industries such as spacecraft.",
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
            "Impure Titanium needs to be put through the electrorefining process to be made into Titanium Ingots.",
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
    }

}