using Mafi.Core.Localization.Quantity;
using Mafi;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;

namespace COIExtended.Prototypes.Products;
internal class Virtual : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        // Define the formatter for the product quantity
        QuantityFormatter formatter = ProductCountQuantityFormatter.Instance;

        // Define the graphics for the virtual product
        VirtualProductProto.Gfx cargoShipGFX = new VirtualProductProto.Gfx(
            Option<string>.None, // Prefab path, none for virtual products
            "Assets/COIExtended/Icons/CargoShip.png",
            ColorRgba.Gold // Color for the product representation
        );

        // Create the descriptive strings for the product
        Proto.Str cargoShipString = Proto.CreateStr(
            NewIDs.Virtual.CargoShipProgress,
            "Cargo Ship",
            "A new cargo ship!",
            null
        );

        // Create the virtual product prototype
        VirtualProductProto cargoShipProduct = new VirtualProductProto(
            NewIDs.Virtual.CargoShipProgress,
            cargoShipString,
            cargoShipGFX,
            false, // Is countable, typically false for virtual products
            true, // Is hidden, set to true if the product shouldn't be displayed in certain UIs
            formatter, // The quantity formatter
            null // Tags, none required here
        );

        registrator.PrototypesDb.Add( cargoShipProduct );
    }

}
