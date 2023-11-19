using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Base;
using Mafi.Core.Entities.Animations;
using Mafi.Localization;
using Mafi;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Entities.Static.Layout;
using System;
using System.Linq;
using Mafi.Core.Products;


namespace COIExtended.Prototypes.Buildings;
public class DryDock : IModData
{
    public const int PADDING_SIZE = 16;

    public const int PADDING_WIDTH = 13;

    public const string SHIP_PAD = "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";

    public const string NOTHING_ = "";

    public static string[] CreatePadding(int size, string token, string layoutBase, bool isReversed)
    {
        string[] array = new string[size];
        for (int i = 0; i < size; i++)
        {
            int num = (isReversed ? (size - i) : (i + 1));
            if (num <= 3)
            {
                num = 0;
            }
            array[i] = token.RepeatString(num) + "   ".RepeatString(size - num) + layoutBase;
        }
        return array;
    }
    public void RegisterData(ProtoRegistrator registrator)
    {

        EntityLayout layout = new EntityLayoutParser(registrator.PrototypesDb).ParseLayoutOrThrow(new EntityLayoutParams((LayoutTile x) => x.Constraint == LayoutTileConstraint.None || x.Constraint.HasAnyConstraints(LayoutTileConstraint.Ocean), new CustomLayoutToken[3]
        {
            new CustomLayoutToken("~0!", (EntityLayoutParams p, int i) => new LayoutTokenSpec(-10, i, LayoutTileConstraint.Ocean)),
            new CustomLayoutToken("{0!", delegate(EntityLayoutParams p, int i)
            {
                int? maxTerrainHeight = 0;
                return new LayoutTokenSpec(-10, i, LayoutTileConstraint.None, null, null, maxTerrainHeight);
            }),
            new CustomLayoutToken("~~~", (EntityLayoutParams p, int i) => new LayoutTokenSpec(-12, -10, LayoutTileConstraint.Ocean | LayoutTileConstraint.NoRubbleAfterCollapse))
        }), CreatePadding(16, "~~~", "                                                            ", isReversed: false).Concat("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(11)).Concat("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{9!{9!{9!{9!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(3)).Concat("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(27))
            .Concat("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{9!{9!{9!{9!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(1))
            .Concat("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{9!{9!{9!{9!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)<#C".Repeat(1))
            .Concat("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{9!{9!{9!{9!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(1))
            .Concat("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{9!{9!{9!{9!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)<#B".Repeat(1))
            .Concat("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{9!{9!{9!{9!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(1))
            .Concat("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)<#A".Repeat(1))
            .Concat("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(14))
            .Concat(CreatePadding(16, "~~~", "                                                            ", isReversed: true))
            .ToArray());
        ProductProto inputProduct = registrator.PrototypesDb.GetOrThrow<ProductProto>(NewIDs.Products.TitaniumIngots);
        ProductProto inputProduct2 = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Electronics3);
        ProductProto inputProduct3 = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.ConstructionParts4);
        ProductProto inputProduct4 = registrator.PrototypesDb.GetOrThrow<ProductProto>(NewIDs.Virtual.CargoShipProgress);
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
        LocStr descShort = Loc.Str("Dry Dock", "Finally! You can build your own cargo ship!", "description of a cargo dock");
        Proto.Str strings = Proto.CreateStr(NewIDs.Buildings.DryDock, "Cargo Ship: Drydock", descShort, null);
        
        EntityCosts cargoCosts = NewCosts.Buildings.DryDock.MapToEntityCosts(registrator);
        Electricity consumedPowerPerTick = 20000.Kw();
        ImmutableArray<AnimationParams> empty = ImmutableArray<AnimationParams>.Empty;
        HeightTilesI minGroundHeight = new HeightTilesI(1);
        HeightTilesI maxGroundHeight = new HeightTilesI(30);
        LayoutEntityProto.Gfx mynewgfx = new LayoutEntityProto.Gfx("Assets/Base/Buildings/Shipyard/ShipyardT2.prefab", new RelTile3f(8.5.ToFix32(), 0, 0), "Assets/COIExtended/Buildings/Drydock/Drydock.png", ColorRgba.Blue, true, null, registrator.GetCategoriesProtos(new Proto.ID[]
        {
                Ids.ToolbarCategories.Buildings
        }), false, false, null, null, default, 2147483647);

        CargoDrydockProto dryDock = new CargoDrydockProto(NewIDs.Buildings.DryDock, strings, layout, cargoCosts, inputProduct, inputProduct2, inputProduct3, inputProduct4, mynewgfx, consumedPowerPerTick, Computing.FromTFlops(10), Upoints.FromFraction(10, 5), null);
        registrator.PrototypesDb.Add(dryDock, false);
    }





}
