using Mafi.Core.Mods;
using System;
using Mafi.Core.Prototypes;
using Mafi.Base.Prototypes.Machines;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Terrain;
using Mafi.Localization;
using Mafi;

namespace COIExtended.Prototypes.Machines;
internal class SeawaterPump : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        MachineProto.ID oceanWaterPumpT = NewIDs.Machines.OceanWaterPumpT2;
        Proto.Str strings = Proto.CreateStrFormatDesc1(NewIDs.Machines.OceanWaterPumpT2, "Seawater Pump II", "Pumps water from ocean. Works at the maximum height of {0} from the ocean level.", new LocStrFormatted(5.ToString()), "{0} is an integer specifying max height such as '5'");
        EntityLayoutParser layoutParser = registrator.LayoutParser;
        Predicate<LayoutTile> ignoreTilesForCore = (x) => x.Constraint == LayoutTileConstraint.None || x.Constraint.HasAnyConstraints(LayoutTileConstraint.Ocean);
        CustomLayoutToken[] array = new CustomLayoutToken[4];
        array[0] = new CustomLayoutToken("-0~", (p, h) => new LayoutTokenSpec(-h - 1, -h + 2, LayoutTileConstraint.Ocean, null, null, null, null, null, null, false, false, 0));
        array[1] = new CustomLayoutToken("-0}", (p, h) => new LayoutTokenSpec(-h - 1, -h + 2, LayoutTileConstraint.None, null, null, null, null, null, null, false, false, 0));
        array[2] = new CustomLayoutToken("~~~", (p, h) => new LayoutTokenSpec(-13, -10, LayoutTileConstraint.Ocean, null, null, null, null, null, null, false, false, 0));
        array[3] = new CustomLayoutToken("{P}", delegate (EntityLayoutParams p, int h)
        {
            int heightFrom = -5;
            int heightToExcl = 2;
            LayoutTileConstraint constraint = LayoutTileConstraint.None;
            int? maxTerrainHeight = new int?(0);
            return new LayoutTokenSpec(heightFrom, heightToExcl, constraint, null, null, maxTerrainHeight, null, null, null, false, false, 0);
        });
        OceanWaterPumpProto machine = registrator.PrototypesDb.Add(new OceanWaterPumpProto(oceanWaterPumpT, strings, layoutParser
            .ParseLayoutOrThrow(new EntityLayoutParams(ignoreTilesForCore, array, false, null, null, null, null, null, default), new string[]
        {
                "                                    [2][2]         ",
                "-6~-5}-4}-3}-2}-1}-1}{P}{2}{2}{P}{2}[2][2][2][2]>@X",
                "-6~-5}-4}-3}-2}-1}-1}{P}{2}{2}{P}{2}[2][2][2][2]   ",
                "                                 {1}[2][2][2][2]   "
        }), NewCosts.Machines.OceanWaterPumpT2
        .MapToEntityCosts(registrator, false), 200.Kw(), ImmutableArray
        .Create<AnimationParams>(AnimationParams.Loop(null, false, null)), ImmutableArray.Create(ImmutableArray.Create(new RectangleTerrainArea2iRelative(new RelTile2i(-17, -2), new RelTile2i(3, 4)))), new HeightTilesI(1), new HeightTilesI(5), new MachineProto.Gfx("Assets/Base/Machines/Pump/OceanWaterPumpT1.prefab", registrator.GetCategoriesProtos(new Proto.ID[]
        {
                Ids.ToolbarCategories.MachinesWater
        }), default, "Assets/Unity/Generated/Icons/LayoutEntity/OceanWaterPumpT1.png", default, default, default, false, true, null, null, ColorRgba.Blue, null), null, false, default, default, null, false, false, null), false);
        registrator.RecipeProtoBuilder
            .Start("Ocean water pumping", NewIDs.Recipes.LargeSeaWaterPumping, machine)
            .AddOutput(54, Ids.Products.Seawater, "X", false, false)
            .SetDuration(10.Seconds())
            .BuildAndAdd();
        MachineProto.ID oceanWaterPumpLarge = NewIDs.Machines.OceanWaterPumpTallT2;
        Proto.Str strings2 = Proto.CreateStrFormatDesc1(NewIDs.Machines.OceanWaterPumpTallT2, "Seawater Pump II (Tall)", "Larger pump that can be placed up to height of {0} from the ocean level. Requires more power to run.", new LocStrFormatted(11.ToString()), "{0} is an integer specifying max height such as '10'");
        EntityLayoutParser layoutParser2 = registrator.LayoutParser;
        Predicate<LayoutTile> ignoreTilesForCore2 = (x) => x.Constraint == LayoutTileConstraint.None || x.Constraint.HasAnyConstraints(LayoutTileConstraint.Ocean);
        CustomLayoutToken[] array2 = new CustomLayoutToken[4];
        array2[0] = new CustomLayoutToken("-0~", (p, h) => new LayoutTokenSpec(-h - 6, -h - 3, LayoutTileConstraint.Ocean, null, null, null, null, null, null, false, false, 0));
        array2[1] = new CustomLayoutToken("-0}", (p, h) => new LayoutTokenSpec(-h - 6, -h - 3, LayoutTileConstraint.None, null, null, null, null, null, null, false, false, 0));
        array2[2] = new CustomLayoutToken("~~~", (p, h) => new LayoutTokenSpec(-22, -19, LayoutTileConstraint.Ocean, null, null, null, null, null, null, false, false, 0));
        array2[3] = new CustomLayoutToken("{P}", delegate (EntityLayoutParams p, int h)
        {
            int heightFrom = -9;
            int heightToExcl = 2;
            LayoutTileConstraint constraint = LayoutTileConstraint.None;
            int? maxTerrainHeight = new int?(0);
            return new LayoutTokenSpec(heightFrom, heightToExcl, constraint, null, null, maxTerrainHeight, null, null, null, false, false, 0);
        });
        OceanWaterPumpProto machine2 = registrator.PrototypesDb.Add(new OceanWaterPumpProto(oceanWaterPumpLarge, strings2, layoutParser2.ParseLayoutOrThrow(new EntityLayoutParams(ignoreTilesForCore2, array2, false, null, null, null, null, null, default), new string[]
        {
                "                                                         {1}(2)[2][2][2]   ",
                "-6~-5}-4}-3}-2}-1}-5}-4}-3}-2}-1}{1}{1}{1}{1}{P}{2}{2}{P}{2}(2)[2][2][2]>@X",
                "-6~-5}-4}-3}-2}-1}-5}-4}-3}-2}-1}{1}{1}{1}{1}{P}{2}{2}{P}{2}(2)[2][2][2]   ",
                "                                                         {1}(2)[2][2][2]   "
        }), NewCosts.Machines.OceanWaterPumpTallT2.MapToEntityCosts(registrator, false), 500.Kw(), ImmutableArray.Create<AnimationParams>(AnimationParams.Loop(null, false, null)), ImmutableArray.Create(ImmutableArray.Create(new RectangleTerrainArea2iRelative(new RelTile2i(-25, -1), new RelTile2i(3, 4)))), new HeightTilesI(1), new HeightTilesI(11), new MachineProto.Gfx("Assets/Base/Machines/Pump/OceanWaterPumpT2.prefab", registrator.GetCategoriesProtos(new Proto.ID[]
        {
                Ids.ToolbarCategories.MachinesWater
        }), default, "Assets/Unity/Generated/Icons/LayoutEntity/OceanWaterPumpLarge.png", default, default, default, false, true, null, null, default, null), null, false, default, default, null, false, false, null), false);
        registrator.RecipeProtoBuilder
            .Start("Ocean Water Pumping II", NewIDs.Recipes.LargeSeaWaterPumpingTall, machine2)
            .AddOutput(54, Ids.Products.Seawater, "X", false, false)
            .SetDuration(10.Seconds()).BuildAndAdd();
    }

}
