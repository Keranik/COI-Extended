using Mafi.Core.Mods;
using System;
using System.Collections.Generic;
using Mafi.Core.Prototypes;
using Mafi.Base;
using Mafi.Core.Entities;
using Mafi.Collections.ImmutableCollections;
using Mafi.Collections;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Entities.Static;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Factory.Transports;
using Mafi.Core.Products;
using Mafi;

namespace COIExtended.Prototypes.Transports;
internal class StackerLong : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        Predicate<LayoutTile> ignoreTilesForCore = (LayoutTile x) => x.TileSurfaceProto.IsNone;
        CustomLayoutToken[] array = new CustomLayoutToken[9];
        array[0] = new CustomLayoutToken("(0A", (EntityLayoutParams p, int h) => new LayoutTokenSpec(h - 2, h, LayoutTileConstraint.None, null, null, null, null, null, null, false, false, 0));
        array[1] = new CustomLayoutToken("(0B", (EntityLayoutParams p, int h) => new LayoutTokenSpec(h - 3, h, LayoutTileConstraint.None, null, null, null, null, null, null, false, false, 0));
        array[2] = new CustomLayoutToken("(0C", (EntityLayoutParams p, int h) => new LayoutTokenSpec(h - 4, h, LayoutTileConstraint.None, null, null, null, null, null, null, false, false, 0));
        array[3] = new CustomLayoutToken("(0D", (EntityLayoutParams p, int h) => new LayoutTokenSpec(h - 5, h, LayoutTileConstraint.None, null, null, null, null, null, null, false, false, 0));
        array[4] = new CustomLayoutToken("(0E", (EntityLayoutParams p, int h) => new LayoutTokenSpec(h - 6, h, LayoutTileConstraint.None, null, null, null, null, null, null, false, false, 0));
        array[5] = new CustomLayoutToken("(0G", (EntityLayoutParams p, int h) => new LayoutTokenSpec(0, h, LayoutTileConstraint.None, null, null, null, null, null, null, false, false, 0));
        array[6] = new CustomLayoutToken("10A", (EntityLayoutParams p, int h) => new LayoutTokenSpec(h + 10 - 2, h + 10, LayoutTileConstraint.None, null, null, null, null, null, null, false, false, 0));
        array[7] = new CustomLayoutToken("10B", (EntityLayoutParams p, int h) => new LayoutTokenSpec(h + 10 - 3, h + 10, LayoutTileConstraint.None, null, null, null, null, null, null, false, false, 0));
        array[8] = new CustomLayoutToken("(0X", delegate (EntityLayoutParams p, int h)
        {
            int heightFrom = 0;
            LayoutTileConstraint constraint = LayoutTileConstraint.None;
            int? minTerrainHeight = new int?(-4);
            int? maxTerrainHeight = new int?(4);
            return new LayoutTokenSpec(heightFrom, h, constraint, null, minTerrainHeight, maxTerrainHeight, null, null, null, false, false, 0);
        });
        bool portsCanOnlyConnectToTransports = false;
        int? customCollapseVerticesThreshold = new int?(2);
        EntityLayoutParams layoutParams = new EntityLayoutParams(ignoreTilesForCore, array, portsCanOnlyConnectToTransports, null, null, null, customCollapseVerticesThreshold, null, default(Option<IEnumerable<KeyValuePair<char, int>>>));
        Lyst<ParticlesParams> lyst = new Lyst<ParticlesParams>();
        lyst.Add(ParticlesParams.Loop("WasteParticles", false, null, delegate (RecipeProto r)
        {
            LooseProductProto valueOrNull = r.AllInputs.First.Product.DumpableProduct.ValueOrNull;
            if (valueOrNull == null)
            {
                return ColorRgba.Empty;
            }
            return valueOrNull.TerrainMaterial.Value.Graphics.ParticleColor.Rgba;
        }));
        Lyst<ParticlesParams> lyst2 = lyst;
        Lyst<ToolbarCategoryProto> lyst3 = new Lyst<ToolbarCategoryProto>
            {
                registrator.PrototypesDb.GetOrThrow<ToolbarCategoryProto>(Ids.ToolbarCategories.Transports)
            };
        LayoutEntityProto.VisualizedLayers empty = LayoutEntityProto.VisualizedLayers.Empty;
 
        EntityCostsTpl StackerLongCost = Costs.Build.CP2(25).Product(50, Ids.Products.Rubber);

        StaticEntityProto.ID stacker = NewIDs.Transports.StackerLong;
        Proto.Str strings = Proto.CreateStr(NewIDs.Transports.StackerLong, "Stacker (Long)", "NOTE: This stacker uses the original prefab for the game and as such will not appear to be dumping in the correct location, this is normal.  The correct location will be where the green overlay is placed as on a normal Stacker.", null);
        EntityLayout layout = registrator.LayoutParser.ParseLayoutOrThrow(layoutParams, new string[]
        {
                    "   [2][2][2]            (1G(1G                                                                                                         ",
                    "A~>[2][2][2]      (3A(2G(2X(3X(4B(5A                                                                                                   ",
                    "   [2][2][2](3A(3A(4B(4G(5X(5X(6D(6B(7A(7A(8A(8A(9B(9A10B10A11B11A12B12A13B13A14B14A15B15A16B16A17B17A18B18A19B19A19A19A19A19A19A19A19A",
                    "B~>[2][2][2]      (3A(2G(2X(3X(4B(5A                                                                                                   ",
                    "   [2][2][2]            (1G(1G                                                                                                         "
        });
        EntityCosts costs = StackerLongCost.MapToEntityCosts(registrator, false);
        Electricity consumedPowerPerTick = 100.Kw();
        ThicknessTilesI minDumpOffset = 1.TilesThick();
        ThicknessTilesI defaultDumpOffset = 2.TilesThick();
        registrator.PrototypesDb.Add(new StackerProto(stacker, strings, layout, costs, consumedPowerPerTick, minDumpOffset, new RelTile3i(42, 0, 21), 2.Seconds(), 1.Ticks(), new StackerProto.Gfx("Assets/COIExtended/Buildings/StackerLong/StackerLong.obj", new RelTile3f(0, 0, 0), "Assets/Unity/Generated/Icons/LayoutEntity/Stacker.png", lyst2.ToImmutableArray(), ImmutableArray<EmissionParams>.Empty, Option<string>.None, ColorRgba.Empty, false, false, empty, lyst3.ToImmutableArray()), defaultDumpOffset, null), false);
    }

}
