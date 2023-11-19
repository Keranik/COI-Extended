using Mafi.Core.Mods;
using System;
using System.Collections.Generic;
using Mafi.Core.Prototypes;
using Mafi.Base.Prototypes.Machines;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Entities;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Products;
using Mafi.Core.Terrain;
using Mafi.Localization;
using Mafi;

namespace COIExtended.Prototypes.Machines;
internal class LiquidDumps : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        MachineProto.ID wasteDump = NewIDs.Machines.LargeLiquidDump;
        Proto.Str strings = Proto.CreateStrFormatDesc1(NewIDs.Machines.LargeLiquidDump, "Liquid Dump II", "Dumps liquid into the ocean. Some liquid will cause water pollution which can affect health and happiness of your people. Works at the maximum height of {0} from the ocean level.", new LocStrFormatted(11.ToString()), "{0} is an integer specifying max height such as '5'");
        EntityLayoutParser layoutParser = registrator.LayoutParser;
        Predicate<LayoutTile> ignoreTilesForCore = (LayoutTile x) => x.Constraint == LayoutTileConstraint.None || x.Constraint.HasAnyConstraints(LayoutTileConstraint.Ocean);
        CustomLayoutToken[] array = new CustomLayoutToken[2];
        array[0] = new CustomLayoutToken("~~~", (EntityLayoutParams p, int h) => new LayoutTokenSpec(-12, -10, LayoutTileConstraint.Ocean, null, null, null, null, null, null, false, false, 0));
        array[1] = new CustomLayoutToken("-0}", delegate (EntityLayoutParams p, int h)
        {
            int heightFrom = -h;
            int heightToExcl = 2;
            LayoutTileConstraint constraint = LayoutTileConstraint.None;
            int? maxTerrainHeight = new int?(0);
            return new LayoutTokenSpec(heightFrom, heightToExcl, constraint, null, null, maxTerrainHeight, null, null, null, false, false, 0);
        });
        EntityLayout layout = layoutParser.ParseLayoutOrThrow(new EntityLayoutParams(ignoreTilesForCore, array, false, null, null, null, null, null, default(Option<IEnumerable<KeyValuePair<char, int>>>)), new string[]
        {
                "                                                         [2][2][2]   ",
                "~~~-1}-1}-1}-1}-1}-2}-2}-2}-2}-2}-2}-2}-2}-5}-5}{2}{2}{2}[2][2][2]A@<",
                "~~~                                                      [2][2][2]   ",
                "~~~-1}-1}-1}-1}-1}-2}-2}-2}-2}-2}-2}-2}-2}-5}-5}{2}{2}{2}[2][2][2]B@<",
                "~~~                                                      [2][2][2]   ",
                "~~~-1}-1}-1}-1}-1}-2}-2}-2}-2}-2}-2}-2}-2}-5}-5}{2}{2}{2}[2][2][2]C@<",
                "                                                         [2][2][2]   "
        });

        EntityCostsTpl LargeLiquidDump = Costs.Build.CP(40).Workers(2);
        EntityCosts costs = LargeLiquidDump.MapToEntityCosts(registrator, false);
        Electricity zero = Electricity.Zero;
        ImmutableArray<AnimationParams> empty = ImmutableArray<AnimationParams>.Empty;
        ImmutableArray<ImmutableArray<RectangleTerrainArea2iRelative>> reservedOceanAreasSets = ImmutableArray.Create(ImmutableArray.Create(new RectangleTerrainArea2iRelative(new RelTile2i(-33, -4), new RelTile2i(4, 6))));
        HeightTilesI minGroundHeight = new HeightTilesI(1);
        HeightTilesI maxGroundHeight = new HeightTilesI(30);
        string prefabPath = "Assets/COIExtended/Buildings/LargeWasteDump/LargeWasteDump.prefab";
        RelTile3f relTile3f = new RelTile3f(-0.25.ToFix32(), -0.125.ToFix32(), 0);
        ImmutableArray<ToolbarCategoryProto> categoriesProtos = registrator.GetCategoriesProtos(new Proto.ID[]
        {
                Ids.ToolbarCategories.Waste
        });
        RelTile3f prefabOffset = relTile3f;
        ImmutableArray<ParticlesParams> particlesParams = ImmutableArray.Create(ParticlesParams.Loop("WasteParticles", false, null, (RecipeProto r) => r.AllInputs.First.Product.Graphics.Color));
        Option<string> machineSoundPrefabPath = "Assets/Base/Machines/Water/WasteDump/WasteDump_Sound.prefab";

        OceanLiquidDumpProto machine = registrator.PrototypesDb.Add(new OceanLiquidDumpProto(wasteDump, strings, layout, costs, zero, empty, reservedOceanAreasSets, minGroundHeight, maxGroundHeight, new MachineProto.Gfx(prefabPath, categoriesProtos, prefabOffset, "Assets/COIExtended/Buildings/LargeWasteDump/COIEWasteDump.png", particlesParams, default(ImmutableArray<EmissionParams>), machineSoundPrefabPath, false, true, null, null, default(ColorRgba), null), null, false, default(Option<MachineProto>), default(Computing), null, true, false, null), false);
        registrator.RecipeProtoBuilder.Start("Large Water Dumping", NewIDs.Recipes.LargeWaterDumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.Water, "*", false).SetDurationSeconds(2).EnablePartialExecution(1.Percent()).BuildAndAdd();
        registrator.RecipeProtoBuilder.Start("Large Brine Dumping", NewIDs.Recipes.LargeBrineDumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.Brine, "*", false).SetDurationSeconds(2).EnablePartialExecution(1.Percent()).BuildAndAdd();
        registrator.RecipeProtoBuilder.Start("Large Waste Water Dumping", NewIDs.Recipes.LargeWasteWaterDumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.WasteWater, "*", false).AddOutput(2, Ids.Products.PollutedWater, "VIRTUAL", false, false).SetDurationSeconds(2).EnablePartialExecution(1.Percent()).BuildAndAdd();
        registrator.RecipeProtoBuilder.Start("Large Sour Water Dumping", NewIDs.Recipes.LargeSourWaterDumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.SourWater, "*", false).AddOutput(4, Ids.Products.PollutedWater, "VIRTUAL", false, false).SetDurationSeconds(2).EnablePartialExecution(1.Percent()).BuildAndAdd();
        registrator.RecipeProtoBuilder.Start("Large Acid Dumping", NewIDs.Recipes.LargeAcidDumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.Acid, "*", false).AddOutput(4, Ids.Products.PollutedWater, "VIRTUAL", false, false).SetDurationSeconds(2).EnablePartialExecution(1.Percent()).BuildAndAdd();
        registrator.RecipeProtoBuilder.Start("Large Toxic Dumping", NewIDs.Recipes.LargeToxicDumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.ToxicSlurry, "*", false).AddOutput(5, Ids.Products.PollutedWater, "VIRTUAL", false, false).SetDurationSeconds(2).EnablePartialExecution(1.Percent()).BuildAndAdd();
        registrator.RecipeProtoBuilder.Start("Large Seawater Dumping", NewIDs.Recipes.LargeSeaWaterDumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.Seawater, "*", false).SetDurationSeconds(2).EnablePartialExecution(1.Percent()).BuildAndAdd();
        registrator.RecipeProtoBuilder.Start("Large Fertilizer Dumping", NewIDs.Recipes.LargeFert1Dumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(14, Ids.Products.FertilizerOrganic, "*", false).AddOutput(1, Ids.Products.PollutedWater, "VIRTUAL", false, false).SetDurationSeconds(4).EnablePartialExecution(1.Percent()).BuildAndAdd();
        registrator.RecipeProtoBuilder.Start("Large Fertilizer Dumping", NewIDs.Recipes.LargeFert2Dumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.FertilizerChemical, "*", false).AddOutput(1, Ids.Products.PollutedWater, "VIRTUAL", false, false).SetDurationSeconds(4).EnablePartialExecution(1.Percent()).BuildAndAdd();
        registrator.RecipeProtoBuilder.Start("Large Fertilizer Dumping", NewIDs.Recipes.LargeFert3Dumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.FertilizerChemical2, "*", false).AddOutput(2, Ids.Products.PollutedWater, "VIRTUAL", false, false).SetDurationSeconds(4).EnablePartialExecution(1.Percent()).BuildAndAdd();
    }

}
