using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Base;
using Mafi.Core.Entities.Animations;
using Mafi;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Entities;
using Mafi.Core.Factory.Machines;
using System.Collections.Generic;
using System;

namespace COIExtended.Prototypes.Machines;
internal class AirSeparator : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        LayoutEntityBuilderState<MachineProtoBuilder.MachineProtoBuilderStateBase> layoutEntityBuilderState = registrator.MachineProtoBuilder
               .Start("Air Separator II", NewIDs.Machines.AirSeparatorT2, "name of a machine")
               .Description("An air separator is a device used for fractionally distilling atmospheric air to separate and extract its primary components, notably oxygen and nitrogen.", "short description of a machine")
               .SetCost(Costs.Build.CP3(50).Workers(4).MaintenanceT2(3), false)
               .SetElectricityConsumption(375.Kw())
               .SetCategories(new Proto.ID[]
           {
                Ids.ToolbarCategories.Machines
           });
        Predicate<LayoutTile> ignoreTilesForCore = null;
        CustomLayoutToken[] array = new CustomLayoutToken[1];
        array[0] = new CustomLayoutToken("[0!", delegate (EntityLayoutParams p, int h)
        {
            int heightFrom = 0;
            int heightToExcl = h + 2;
            LayoutTileConstraint constraint = LayoutTileConstraint.None;
            int? terrainSurfaceHeight = new int?(0);
            Proto.ID? surfaceId = new Proto.ID?(p.HardenedFloorSurfaceId);
            return new LayoutTokenSpec(heightFrom, heightToExcl, constraint, terrainSurfaceHeight, null, null, null, null, surfaceId, false, false, 0);
        });
        MachineProto machine = layoutEntityBuilderState.SetLayout(new EntityLayoutParams(ignoreTilesForCore, array, false, null, null, null, null, null, default(Option<IEnumerable<KeyValuePair<char, int>>>)), new string[]
        {
                "   [4][4][4][4][4][4][4][5][1]   ",
                "A@>[4][4][4][4][9][9][9][6][6]>@X",
                "   [4][4][4][9][9![9![9![9![3]   ",
                "   [4][4][4][9][9![9![9![5][3]   ",
                "B@>[4][4][4][4][9][9][9][6][6]>@Y",
                "   [4][4][4][4][4][4][4][5][1]   "
        })
            .SetPrefabPath("Assets/Base/Machines/MetalWorks/AirSeparator.prefab")
            .SetCustomIconPath("Assets/Unity/Generated/Icons/LayoutEntity/AirSeparator.png")
            .SetAnimationParams(AnimationParams.Loop(new Percent?(50.Percent()), false, null))
            .AddParticleParams(ParticlesParams.Loop("Steam", false, null, null))
            .EnableSemiInstancedRendering(default(ImmutableArray<string>))
            .BuildAndAdd();

        registrator.RecipeProtoBuilder
            .Start("Better Air Separation", NewIDs.Recipes.AirSeparationT2, machine)
            .SetDurationSeconds(20)
            .AddOutput(30, Ids.Products.Oxygen, "Y", false, false)
            .AddOutput(30, Ids.Products.Nitrogen, "X", false, false)
            .BuildAndAdd();
        registrator.RecipeProtoBuilder
            .Start("Make Nitrogen Dioxide", NewIDs.Recipes.NitrogenDioxide, machine)
            .SetDurationSeconds(20)
            .AddInput(10, Ids.Products.Nitrogen, "A", false)
            .AddInput(20, Ids.Products.Oxygen, "B", false)
            .AddOutput(20, NewIDs.Products.NitrogenDioxide, "X", false, false)
            .BuildAndAdd();

        registrator.RecipeProtoBuilder
            .Start("Make Nitric Acid", NewIDs.Recipes.NitricAcidRecipe, Ids.Machines.ChemicalPlant2)
            .SetDurationSeconds(20)
            .AddInput(8, NewIDs.Products.NitrogenDioxide, "A", false)
            .AddInput(4, Ids.Products.Hydrogen, "B", false)
            .AddInput(4, Ids.Products.ChilledWater, "C", false)
            .AddOutput(8, NewIDs.Products.NitricAcid, "X", false, false)
            .BuildAndAdd();

        registrator.RecipeProtoBuilder
            .Start("Nitric Acid Electrolysis", NewIDs.Recipes.NitricAcidElectrolysis, Ids.Machines.CopperElectrolysis)
            .SetDurationSeconds(40)
            .AddInput(16, Ids.Products.ImpureCopper, "*", false)
            .AddInput(2, NewIDs.Products.NitricAcid, "*", false)
            .AddOutput(16, Ids.Products.Copper, "*", false, false)
            .BuildAndAdd();

        registrator.RecipeProtoBuilder
            .Start("Nitric Acid Glass Mix", NewIDs.Recipes.NitricAcidGlassMix, Ids.Machines.IndustrialMixerT2)
            .SetDurationSeconds(10)
            .AddInput(16, Ids.Products.Sand, "*", false)
            .AddInput(2, NewIDs.Products.NitricAcid, "*", false)
            .AddInput(4, Ids.Products.Limestone, "*", false)
            .AddInput(2, Ids.Products.Salt, "*", false)
            .AddOutput(16, Ids.Products.GlassMix, "*", false, false)
            .BuildAndAdd();

        registrator.RecipeProtoBuilder
            .Start("Nitric Acid Gold Settling", NewIDs.Recipes.NitricAcidGoldSettling, Ids.Machines.SettlingTank)
            .SetDurationSeconds(20)
            .AddInput(10, Ids.Products.GoldOreCrushed, "*", false)
            .AddInput(2, NewIDs.Products.NitricAcid, "*", false)
            .AddOutput(3, Ids.Products.GoldOreConcentrate, "*", false)
            .AddOutput(6, Ids.Products.ToxicSlurry, "*", false)
            .BuildAndAdd();

        registrator.RecipeProtoBuilder
            .Start("Nitric Acid Uranium Settling", NewIDs.Recipes.NitricAcidUraniumSettling, Ids.Machines.SettlingTank)
            .SetDurationSeconds(40)
            .AddInput(12, Ids.Products.UraniumOreCrushed, "*", false)
            .AddInput(2, NewIDs.Products.NitricAcid, "*", false)
            .AddOutput(6, Ids.Products.Yellowcake, "*", false)
            .AddOutput(4, Ids.Products.ToxicSlurry, "*", false)
            .BuildAndAdd();

        registrator.RecipeProtoBuilder
             .Start("Nitric Acid HF Settling", NewIDs.Recipes.NitricAcidHFSettling, Ids.Machines.SettlingTank)
             .SetDurationSeconds(40)
             .AddInput(8, Ids.Products.Rock, "*", false)
             .AddInput(4, NewIDs.Products.NitricAcid, "*", false)
             .AddOutput(10, Ids.Products.HydrogenFluoride, "*", false)
             .BuildAndAdd();
    }

}
