using Mafi.Core.Mods;
using Mafi.Base;
using Mafi.Core.Research;
using Mafi;

namespace COIExtended.Prototypes.Research;
internal class ConstructionPartsV : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        registrator.ResearchNodeProtoBuilder
                .Start("Construction Parts V", NewIDs.Research.UnlockConstructionPartsV)
                .Description("Production of Construction Parts with Titanium.")
                .AddProductToUnlock(NewIDs.Products.ConstructionParts5,true,false)
                .AddRecipeToUnlock(NewIDs.Recipes.ConstructionPartsV)
                .SetCosts(new ResearchCostsTpl(65))
                .SetGridPosition(new Vector2i(138, 18))
                .AddParents(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(NewIDs.Research.CargoShipResearch))
                .BuildAndAdd();
    }
}