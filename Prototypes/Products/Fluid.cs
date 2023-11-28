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
            .Description("Nitrogen dioxide is a toxic and reactive reddish-brown gas, commonly found in emissions from vehicles and industrial processes, and known for contributing to air pollution and respiratory problems.")
            .BuildAndAdd();

        FluidProductProto NitricAcid = registrator.FluidProductProtoBuilder
            .Start("Nitric Acid", NewIDs.Products.NitricAcid, null)
            .SetIsStorable(true)
            .SetCanBeDiscarded(true)
            .SetIsWaste(false)
            .SetColor(new ColorRgba(0, 187, 187))
            .SetCustomIconPath("Assets/COIExtended/Icons/NitricAcid.png")
            .Description("Nitric acid is a highly corrosive and toxic mineral acid, often used in fertilizers and explosives, and known for its role in various industrial and chemical processes.")
            .BuildAndAdd();

        FluidProductProto TitaniumDioxide = registrator.FluidProductProtoBuilder
            .Start("Titanium Dioxide", NewIDs.Products.TitaniumDioxide, null)
            .SetIsStorable(true)
            .SetCanBeDiscarded(true)
            .SetIsWaste(false)
            .SetColor(new ColorRgba(255, 255, 255))
            .SetCustomIconPath("Assets/COIExtended/Icons/TitaniumDioxide.png")
            .Description("Titanium dioxide is a bright white pigment widely used in paints, coatings, plastics, and sunscreens, known for its high refractive index and UV resistance.")
            .BuildAndAdd();
    }

}