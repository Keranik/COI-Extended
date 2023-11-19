using Mafi.Core.Research;
using Mafi.Core.Mods;
using Mafi;


namespace COIExtended.Prototypes.Research;
internal class PhotoOxidation : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        registrator.ResearchNodeProtoBuilder
        .Start("Photocatalytic Oxidation", NewIDs.Research.PhotoOxidation)
                .Description("Under UV light, titanium dioxide generates electron-hole pairs. These can react with water vapor and oxygen in the air, producing hydroxyl radicals and superoxide ions. These reactive species can oxidize volatile organic compounds (VOCs) and other pollutants, breaking them down into less harmful substances like carbon dioxide and water.")
                .AddMachineToUnlock(NewIDs.Machines.ScrubberT2, false)
                .AddRecipeToUnlock(NewIDs.Recipes.PhotoOxiScrub, false)
                .SetCosts(new ResearchCostsTpl(69))
                .SetGridPosition(new Vector2i(128, 44))
                .AddParents(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(NewIDs.Research.ChemicalPlantT3))
                .BuildAndAdd();
    }
}
