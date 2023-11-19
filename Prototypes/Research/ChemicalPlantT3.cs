using Mafi.Core.Mods;
using Mafi.Core.Research;
using Mafi;
using Mafi.Base;

namespace COIExtended.Prototypes.Research;
internal class ChemicalPlantT3 : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        registrator.ResearchNodeProtoBuilder
            .Start("Chemical Plant III", NewIDs.Research.ChemicalPlantT3)
            .Description("A better chemical plant.")
            .AddMachineToUnlock(NewIDs.Machines.ChemicalPlantIII, false)
            .AddRecipeToUnlock(NewIDs.Recipes.TitaniumDioxide)
            .AddRecipeToUnlock(NewIDs.Recipes.FertilizerProductionT3)
            .AddRecipeToUnlock(NewIDs.Recipes.AmmoniaSynthesisT3)
            .AddRecipeToUnlock(NewIDs.Recipes.FuelGasSynthesisT3)
            .AddRecipeToUnlock(NewIDs.Recipes.GraphiteProductionT3)
            .AddRecipeToUnlock(NewIDs.Recipes.DisinfectantProductionT3)
            .AddRecipeToUnlock(NewIDs.Recipes.AnestheticsProductionT3)
            .AddRecipeToUnlock(NewIDs.Recipes.MorphineProductionT3)
            .SetCosts(new ResearchCostsTpl(45))
            .SetGridPosition(new Vector2i(124, 44))
            .AddParents(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(NewIDs.Research.IlmeniteOreProcessing))
            .BuildAndAdd();

    }
}