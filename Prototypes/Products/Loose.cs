using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Core.Products;
using Mafi;
using static Mafi.Base.Assets.Core;
using Mafi.Base;
using ProductID = Mafi.Core.Products.ProductProto.ID;
using Mafi.Core.World.Entities;
using Mafi.Core;

namespace COIExtended.Prototypes.Products;
internal class Loose : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {

        ProductProto protoToAssign = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.GoldOre);
        var orePrefabPath = protoToAssign.Graphics.PrefabsPath.Value;
        ProductProto protoToAssign2 = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.GoldOreCrushed);
        var crushedPrefabPath = protoToAssign2.Graphics.PrefabsPath.Value;
        ProductProto protoToAssign3 = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.GoldOreConcentrate);
        var concentratePrefabPath = protoToAssign3.Graphics.PrefabsPath.Value;


        LooseProductProto.Gfx ilmeniteOreGFX = new LooseProductProto.Gfx(
            orePrefabPath,
            "Assets/COIExtended/Materials/IlmeniteOre.mat", 
            true, 
            new ColorRgba(29, 29, 27), 
            "Assets/COIExtended/Icons/IlmeniteOre.png" 
        );

        // Create the descriptive strings for the product
        Proto.Str ilmeniteOreString = Proto.CreateStr(
            NewIDs.Products.IlmeniteOre, // Product ID
            "Ilmenite Ore", // Display name
            "Ilmenite ore is a black, heavy, titanium-iron oxide mineral, commonly used as the primary source for titanium production and various pigment applications.", // Description
            null // Tooltip, not needed so set to null
        );

        // Create the loose product prototype
        LooseProductProto ilmeniteOreProduct = new LooseProductProto(
            NewIDs.Products.IlmeniteOre, // Product ID
            ilmeniteOreString, // Strings containing name and description
            true, // Is countable, typically false for loose materials
            true, // Is hidden, set to true if the product shouldn't be displayed in certain UIs
            ilmeniteOreGFX, // Graphics object for the product
            false, // Is radioactive, set to true if the material is radioactive
            false, // Can be sold, set to true if the product can be sold
            false, // Can be bought, set to true if the product can be bought
            false, // Is tradable, set to true if the product can be traded
            null, // Upgraded from, set to another ProductID if this product is an upgrade
            null, // Upgrade quantity, set to PartialQuantity if this product is an upgrade
            null // Tags, set to a collection of tags if needed
        );

        registrator.PrototypesDb.Add(ilmeniteOreProduct, false);
        // Define the graphics for the loose product
        LooseProductProto.Gfx crushedIlmeniteOreGFX = new LooseProductProto.Gfx(
            crushedPrefabPath, // Prefab path
            "Assets/COIExtended/Materials/IlmeniteOreCrushed.mat", // Icon path, set to null if using prefab's renderer
            false, // Is icon unique, false since we are using prefab's renderer
            new ColorRgba(29, 29, 27), // Color for the product representation
            "Assets/COIExtended/Icons/IlmeniteOreCrushed.png" // Path to the icon image
        );

        // Create the descriptive strings for the product
        Proto.Str crushedIlmeniteOreString = Proto.CreateStr(
            NewIDs.Products.IlmeniteOreCrushed, // Product ID
            "Ilmenite Ore (Crushed)", // Display name
            "Ilmenite that has been mechanically processed into smaller particles, facilitating the extraction of titanium during subsequent refining processes.", // Description
            null // Tooltip, not needed so set to null
        );

        // Create the loose product prototype
        LooseProductProto crushedIlmeniteOreProduct = new LooseProductProto(
            NewIDs.Products.IlmeniteOreCrushed, // Product ID
            crushedIlmeniteOreString, // Strings containing name and description
            false, // Is countable, typically false for loose materials
            true, // Is hidden, set to true if the product shouldn't be displayed in certain UIs
            crushedIlmeniteOreGFX, // Graphics object for the product
            false, // Is radioactive, set to true if the material is radioactive
            false, // Can be sold, set to true if the product can be sold
            false, // Can be bought, set to true if the product can be bought
            false, // Is tradable, set to true if the product can be traded
            null, // Upgraded from, set to another ProductID if this product is an upgrade
            null, // Upgrade quantity, set to PartialQuantity if this product is an upgrade
            null // Tags, set to a collection of tags if needed
        );
        registrator.PrototypesDb.Add(crushedIlmeniteOreProduct, false);
 
        // Define the graphics for the loose product
        LooseProductProto.Gfx titaniumOreGFX = new LooseProductProto.Gfx(
            orePrefabPath, // Prefab path
            "Assets/COIExtended/Materials/TitaniumOre.mat", // Icon path, set to null if using prefab's renderer
            false, // Is icon unique, false since we are using prefab's renderer
            new ColorRgba(192, 192, 192), // Color for the product representation
            "Assets/COIExtended/Icons/TitaniumOre.png" // Path to the icon image
        );

        // Create the descriptive strings for the product
        Proto.Str titaniumOreString = Proto.CreateStr(
            NewIDs.Products.TitaniumOre, // Product ID
            "Titanium Ore", // Display name
            "Titanium ore is a naturally occurring mineral, primarily in the forms of ilmenite and rutile, that is mined and processed to extract titanium for various industrial uses.", // Description
            null // Tooltip, not needed so set to null
        );

        // Create the loose product prototype
        LooseProductProto titaniumOreProduct = new LooseProductProto(
            NewIDs.Products.TitaniumOre, // Product ID
            titaniumOreString, // Strings containing name and description
            true, // Is countable, typically false for loose materials
            true, // Is hidden, set to true if the product shouldn't be displayed in certain UIs
            titaniumOreGFX, // Graphics object for the product
            false, // Is radioactive, set to true if the material is radioactive
            false, // Can be sold, set to true if the product can be sold
            false, // Can be bought, set to true if the product can be bought
            false, // Is tradable, set to true if the product can be traded
            null, // Upgraded from, set to another ProductID if this product is an upgrade
            null, // Upgrade quantity, set to PartialQuantity if this product is an upgrade
            null // Tags, set to a collection of tags if needed
        );
        registrator.PrototypesDb.Add(titaniumOreProduct, false);
        // Define the graphics for the loose product
        LooseProductProto.Gfx ilmeniteOreConcentrateGFX = new LooseProductProto.Gfx(
            concentratePrefabPath, // Prefab path
            "Assets/COIExtended/Materials/IlmeniteOreConcentrate.mat", // Icon path, set to null if using prefab's renderer
            false, // Is icon unique, false since we are using prefab's renderer
            new ColorRgba(29, 29, 27), // Color for the product representation
            "Assets/COIExtended/Icons/IlmeniteOreConcentrate.png" // Path to the icon image
        );

        // Create the descriptive strings for the product
        Proto.Str ilmeniteOreConcentrateString = Proto.CreateStr(
            NewIDs.Products.IlmeniteOreConcentrate, // Product ID
            "Ilmenite Ore Concentrate", // Display name
            "Ilmenite concentrate is a processed form of ilmenite ore, enriched in titanium and iron content, typically used as a raw material in the production of titanium dioxide and metal.", // Description
            null // Tooltip, not needed so set to null
        );

        // Create the loose product prototype
        LooseProductProto ilmeniteOreConcentrateProduct = new LooseProductProto(
            NewIDs.Products.IlmeniteOreConcentrate, // Product ID
            ilmeniteOreConcentrateString, // Strings containing name and description
            false, // Is countable, typically false for loose materials
            true, // Is hidden, set to true if the product shouldn't be displayed in certain UIs
            ilmeniteOreConcentrateGFX, // Graphics object for the product
            false, // Is radioactive, set to true if the material is radioactive
            false, // Can be sold, set to true if the product can be sold
            false, // Can be bought, set to true if the product can be bought
            false, // Is tradable, set to true if the product can be traded
            null, // Upgraded from, set to another ProductID if this product is an upgrade
            null, // Upgrade quantity, set to PartialQuantity if this product is an upgrade
            null // Tags, set to a collection of tags if needed
        );
        registrator.PrototypesDb.Add(ilmeniteOreConcentrateProduct, false);


        
    }

}