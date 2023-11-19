using Mafi.Core.Mods;
using Mafi.Base;
using Mafi.Core.Entities.Animations;
using Mafi.Localization;
using Mafi;
using Mafi.Core.Factory.Machines;

namespace COIExtended.Prototypes.Machines;
internal class ChemicalPlant : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        LocStr desc = Loc.Str(NewIDs.Machines.ChemicalPlantIII.ToString() + "__desc", "Performs variety of chemical recipes including processing of fluids and their packaging.", "description of a machine");
        MachineProto machineProto = registrator.MachineProtoBuilder.Start("Chemical plant III", NewIDs.Machines.ChemicalPlantIII).Description(desc).SetCost(NewCosts.Machines.ChemicalPlantT3)
            .SetElectricityConsumption(400.Kw())
            .SetCategories(Ids.ToolbarCategories.MachinesOil)
            .SetLayout("~E>[7][8][7][6][5][5][5]   ",
                       "~F>[7][7][7][6][5][5][5]   ", 
                       "#D>[6][6][6][6][5][5][5]>X@", 
                       "@A>[5][5][5][5][5][5][5]>Y#", 
                       "@B>[5][5][5][5][5][5][5]>Z@", 
                       "@C>[5][5][5][5][5][5][5]   ", 
                       "   [5][5][5][5][5][5][5]   ")
            .SetPrefabPath("Assets/COIExtended/Buildings/ChemicalPlantT3/ChemicalPlantT3.prefab")
            .SetCustomIconPath("Assets/COIExtended/Buildings/ChemicalPlantT3/ChemicalPlantT3.png")
            .SetAnimationParams(AnimationParams.Loop())
            .EnableSemiInstancedRendering()
            .BuildAndAdd();

        registrator.RecipeProtoBuilder.Start("Titanium Dioxide", NewIDs.Recipes.TitaniumDioxide, NewIDs.Machines.ChemicalPlantIII)
             .AddInput(24, NewIDs.Products.IlmeniteOreConcentrate, "E")
             .AddInput(6, NewIDs.Products.NitricAcid, "A")
             .AddInput(6, Ids.Products.Chlorine, "B")
             .AddInput(12, Ids.Products.Water, "C")
             .SetDurationSeconds(30)
             .AddOutput(24, NewIDs.Products.TitaniumDioxide, "X")
             .BuildAndAdd();

        registrator.RecipeProtoBuilder.Start("Fertilizer synthesis", NewIDs.Recipes.FertilizerProductionT3, NewIDs.Machines.ChemicalPlantIII)
            .AddInput(4, Ids.Products.Ammonia, "A")
            .AddInput(6, Ids.Products.Oxygen, "B")
            .SetDurationSeconds(5)
            .AddOutput(10, Ids.Products.FertilizerChemical)
            .BuildAndAdd();

        registrator.RecipeProtoBuilder.Start("Ammonia synthesis", NewIDs.Recipes.AmmoniaSynthesisT3, NewIDs.Machines.ChemicalPlantIII)
            .AddInput(4, Ids.Products.Hydrogen, "A")
            .AddInput(8, Ids.Products.Nitrogen, "B")
            .SetDurationSeconds(10)
            .AddOutput(4, Ids.Products.Ammonia)
            .BuildAndAdd();

        registrator.RecipeProtoBuilder.Start("FuelGas synthesis", NewIDs.Recipes.FuelGasSynthesisT3, NewIDs.Machines.ChemicalPlantIII)
            .AddInput(14, Ids.Products.Hydrogen, "A")
            .AddInput(12, Ids.Products.CarbonDioxide, "B")
            .SetDurationSeconds(10)
            .AddOutput(12, Ids.Products.FuelGas, "X")
            .AddOutput(1, Ids.Products.Water, "Z")
            .BuildAndAdd();

        registrator.RecipeProtoBuilder.Start("Graphite production", NewIDs.Recipes.GraphiteProductionT3, NewIDs.Machines.ChemicalPlantIII)
            .AddInput(24, Ids.Products.CarbonDioxide)
            .SetDurationSeconds(5)
            .AddOutput(1, Ids.Products.Graphite)
            .BuildAndAdd();

        registrator.RecipeProtoBuilder.Start("Disinfectant production", NewIDs.Recipes.DisinfectantProductionT3, NewIDs.Machines.ChemicalPlantIII)
            .AddInput(3, Ids.Products.Ethanol)
            .AddInput(2, Ids.Products.Plastic)
            .SetDurationSeconds(10)
            .AddOutput(8, Ids.Products.Disinfectant)
            .BuildAndAdd();

        registrator.RecipeProtoBuilder.Start("Anesthetics production", NewIDs.Recipes.AnestheticsProductionT3, NewIDs.Machines.ChemicalPlantIII)
            .AddInput(2, Ids.Products.Ammonia)
            .AddInput(4, Ids.Products.HydrogenFluoride)
            .AddInput(1, Ids.Products.Steel)
            .SetDurationSeconds(10)
            .AddOutput(8, Ids.Products.Anesthetics)
            .BuildAndAdd();

        registrator.RecipeProtoBuilder.Start("Morphine production", NewIDs.Recipes.MorphineProductionT3, NewIDs.Machines.ChemicalPlantIII)
            .AddInput(4, Ids.Products.Poppy)
            .AddInput(2, Ids.Products.Acid)
            .AddInput(2, Ids.Products.Glass)
            .SetDurationSeconds(10)
            .AddOutput(8, Ids.Products.Morphine)
            .BuildAndAdd();




    }

}
