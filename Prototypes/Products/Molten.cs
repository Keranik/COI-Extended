using Mafi;
using Mafi.Base;
using Mafi.Core.Mods;
using Mafi.Core.Products;

namespace COIExtended.Prototypes.Products;
internal class Molten : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        ProductProto protoToAssign = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.MoltenSteel);
        var moltenPrefabPath = protoToAssign.Graphics.PrefabsPath.Value;
        MoltenProductProto newMoltenProduct = registrator.MoltenProductProtoBuilder
                .Start("Molten Titanium", NewIDs.Products.MoltenTitanium, null)
                .SetIsStorable(false)
                .SetCanBeDiscarded(false)
                .SetPrefabPath(moltenPrefabPath)
                .SetCustomIconPath("Assets/COIExtended/Icons/MoltenTitanium.png")
                .SetSourceProduct(NewIDs.Products.TitaniumOre, 1)
                .SetMaterial("Assets/Base/Machines/MetalWorks/Caster/MoltenSteel/MoltenSteel.mat")
                .SetColor(new ColorRgba(255, 50, 0))
                .SetRadioactivity(0)
                .Description("Molten titanium ore is the liquefied form of titanium minerals, typically achieved through high-temperature processing, used in the extraction and purification of titanium.")
                .BuildAndAdd();
    }

}
