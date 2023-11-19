using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi;

namespace COIExtended.Prototypes.Products;
internal class Fluid : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        FluidProductProto NitrogenDioxide = registrator.FluidProductProtoBuilder
            .Start("Nitrogen Dioxide", NewIDs.Products.NitrogenDioxide, null)
            .SetIsStorable(true)
            .SetCanBeDiscarded(true)
            .SetIsWaste(false)
            .SetColor(new ColorRgba(255, 69, 0))
            .SetCustomIconPath("Assets/COIExtended/Icons/NitrogenDioxide.png")
            .BuildAndAdd();

        FluidProductProto NitricAcid = registrator.FluidProductProtoBuilder
            .Start("Nitric Acid", NewIDs.Products.NitricAcid, null)
            .SetIsStorable(true)
            .SetCanBeDiscarded(true)
            .SetIsWaste(false)
            .SetColor(new ColorRgba(0, 187, 187))
            .SetCustomIconPath("Assets/COIExtended/Icons/NitricAcid.png")
            .BuildAndAdd();

        FluidProductProto TitaniumDioxide = registrator.FluidProductProtoBuilder
            .Start("Titanium Dioxide", NewIDs.Products.TitaniumDioxide, null)
            .SetIsStorable(true)
            .SetCanBeDiscarded(true)
            .SetIsWaste(false)
            .SetColor(new ColorRgba(255, 255, 255))
            .SetCustomIconPath("Assets/COIExtended/Icons/TitaniumDioxide.png")
            .BuildAndAdd();
    }

}