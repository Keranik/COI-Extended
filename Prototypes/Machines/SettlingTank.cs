using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Base;
using Mafi.Core.Entities.Animations;
using Mafi;
using Mafi.Collections.ImmutableCollections;

namespace COIExtended.Prototypes.Machines;
internal class SettlingTank : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        registrator.MachineProtoBuilder
     .Start("Settling Tank II", NewIDs.Machines.SettlingTankII, "settling tank II")
     .SetElectricityConsumption(180.Kw())
     .SetCategories(new Proto.ID[] { Ids.ToolbarCategories.Machines })
     .SetCost(Costs.Build.CP4(80).Workers(6).MaintenanceT2(2), false).SetLayout(new string[]
     {
                "         [3][3][3][3]            ",
                "   A@>[2][3][3][3][3][2][2][2]>~X",
                "C~>[2][3][3][3][3][3][2][2][2]>~Z",
                "   B@>[2][3][3][3][3][2][2][2]>@Y",
                "         [3][3][3][3]            "
     })
     .SetPrefabPath("Assets/COIExtended/Buildings/SettlingTankII/SettlingTankII.prefab")
     .SetMachineSound("Assets/Base/Machines/Gold/SettlingTank/SettlingTankSound.prefab")
     .SetCustomIconPath("Assets/COIExtended/Buildings/SettlingTankII/SettlingTankII.png")
     .SetAnimationParams(AnimationParams.Loop(new Percent?(80.Percent()), false, null))
     .EnableSemiInstancedRendering(default(ImmutableArray<string>))
     .Description("A settling tank is a large basin where solids in suspension, such as those from mining processes, settle to the bottom under gravity, allowing for the separation and extraction of valuable materials.")
     .BuildAndAdd();

        registrator.RecipeProtoBuilder
            .Start("Gold settling", NewIDs.Recipes.GoldSettlingII, NewIDs.Machines.SettlingTankII)
            .AddInput(24, Ids.Products.GoldOrePowder, "*", false)
            .AddInput(8, Ids.Products.Acid, "*", false)
            .SetDurationSeconds(20)
            .AddOutput(6, Ids.Products.GoldOreConcentrate, "X", false, false)
            .AddOutput(18, Ids.Products.ToxicSlurry, "Y", false, false)
            .BuildAndAdd();

        registrator.RecipeProtoBuilder
            .Start("Uranium leaching", NewIDs.Recipes.UraniumLeachingII, NewIDs.Machines.SettlingTankII)
            .AddInput(24, Ids.Products.UraniumOreCrushed, "*", false)
            .AddInput(6, Ids.Products.Acid, "*", false)
            .SetDurationSeconds(40)
            .AddOutput(12, Ids.Products.Yellowcake, "X", false, false)
            .AddOutput(12, Ids.Products.ToxicSlurry, "Y", false, false)
            .BuildAndAdd();


        registrator.RecipeProtoBuilder
            .Start("Fluoride leaching", NewIDs.Recipes.FlourideLeachingII, NewIDs.Machines.SettlingTankII)
            .AddInput(16, Ids.Products.Rock, "*", false)
            .AddInput(8, Ids.Products.Acid, "*", false)
            .SetDurationSeconds(40)
            .AddOutput(16, Ids.Products.HydrogenFluoride, "Y", false, false)
            .AddOutput(4, Ids.Products.Slag, "X", false, false)
            .BuildAndAdd();

        registrator.RecipeProtoBuilder
            .Start("Nitric Acid Gold Settling", NewIDs.Recipes.NitricAcidGoldSettlingII, NewIDs.Machines.SettlingTankII)
            .SetDurationSeconds(20)
            .AddInput(20, Ids.Products.GoldOreCrushed, "*", false)
            .AddInput(4, NewIDs.Products.NitricAcid, "*", false)
            .AddOutput(6, Ids.Products.GoldOreConcentrate, "*", false)
            .AddOutput(12, Ids.Products.ToxicSlurry, "*", false)
            .BuildAndAdd();

        registrator.RecipeProtoBuilder
            .Start("Nitric Acid Uranium Settling", NewIDs.Recipes.NitricAcidUraniumSettlingII, NewIDs.Machines.SettlingTankII)
            .SetDurationSeconds(40)
            .AddInput(24, Ids.Products.UraniumOreCrushed, "*", false)
            .AddInput(4, NewIDs.Products.NitricAcid, "*", false)
            .AddOutput(12, Ids.Products.Yellowcake, "*", false)
            .AddOutput(8, Ids.Products.ToxicSlurry, "*", false)
            .BuildAndAdd();

        registrator.RecipeProtoBuilder
             .Start("Nitric Acid HF Settling", NewIDs.Recipes.NitricAcidHFSettlingII, NewIDs.Machines.SettlingTankII)
             .SetDurationSeconds(40)
             .AddInput(16, Ids.Products.Rock, "*", false)
             .AddInput(8, NewIDs.Products.NitricAcid, "*", false)
             .AddOutput(20, Ids.Products.HydrogenFluoride, "*", false)
             .BuildAndAdd();

        registrator.RecipeProtoBuilder
            .Start("Nitric Acid Ilmenite Settling", NewIDs.Recipes.IlmeniteSettling, NewIDs.Machines.SettlingTankII)
            .SetDurationSeconds(20)
            .AddInput(20, NewIDs.Products.IlmeniteOreCrushed, "*", false)
            .AddInput(4, NewIDs.Products.NitricAcid, "*", false)
            .AddOutput(12, NewIDs.Products.TitaniumOre, "X", false)
            .AddOutput(6, NewIDs.Products.IlmeniteOreConcentrate, "Z", false)
        .BuildAndAdd();

        registrator.RecipeProtoBuilder
            .Start("Ilmenite Ore Crushing", NewIDs.Recipes.IlmeniteCrushing, Ids.Machines.CrusherLarge)
            .SetDurationSeconds(30)
            .AddInput(45, NewIDs.Products.IlmeniteOre, "*", false)
            .AddOutput(45, NewIDs.Products.IlmeniteOreCrushed, "X", false)
            .BuildAndAdd();
    }

}
