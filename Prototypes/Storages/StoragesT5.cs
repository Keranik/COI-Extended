using Mafi.Core.Mods;
using Mafi.Base;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities;
using Mafi.Core.Factory;

namespace COIExtended.Prototypes.Storages;
internal class StoragesT5 : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        registrator.StorageProtoBuilder
           .Start("Fluid Storage V", NewIDs.Buildings.StorageFluidT5)
           .Description("Fluid storage with more capacity and more IO")
           .SetCost(Costs.Build.CP4(180).Priority(4))
           .ShowTerrainDesignatorsOnCreation()
           .SetNoTransferLimit()
           .SetLayout(
           "      [9][9][9][9][9][9][9][9]      ",
           "A@>[9][9][9][9][9][9][9][9][9][9]>@X",
           "E@>[9][9][9][9][9][9][9][9][9][9]>@S",
           "B@>[9][9][9][9][9][9][9][9][9][9]>@Y",
           "F@>[9][9][9][9][9][9][9][9][9][9]>@T",
           "G@>[9][9][9][9][9][9][9][9][9][9]>@U",
           "C@>[9][9][9][9][9][9][9][9][9][9]>@Z",
           "H@>[9][9][9][9][9][9][9][9][9][9]>@V",
           "D@>[9][9][9][9][9][9][9][9][9][9]>@W",
           "      [9][9][9][9][9][9][9][9]      "
           )
           .SetCapacity(8640)
           .SetCategories(Ids.ToolbarCategories.Storages)
           .SetPrefabPath("Assets/Base/Buildings/Storages/GasT4.prefab")
           .SetCustomIconPath("Assets/Unity/Generated/Icons/LayoutEntity/StorageFluidT4.png")
           .SetFluidIndicatorGfxParams("gas_1010_T2_seg3/liquid", new FluidIndicatorGfxParams(1f, 2.6f, 2f))
           .BuildAsFluidAndAdd()
           .AddParam(new DrawArrowWileBuildingProtoParam(4f));

        registrator.StorageProtoBuilder
                .Start("Loose Storage V", NewIDs.Buildings.StorageLooseT5)
                .Description("Loose storage with more capacity and more IO")
                .SetCost(Costs.Build.CP4(180).Priority(4))
                .ShowTerrainDesignatorsOnCreation()
                .SetNoTransferLimit()
                .SetLayout(
                "      [9][9][9][9][9][9][9][9]      ",
                "A~>[9][9][9][9][9][9][9][9][9][9]>~X",
                "E~>[9][9][9][9][9][9][9][9][9][9]>~S",
                "B~>[9][9][9][9][9][9][9][9][9][9]>~Y",
                "F~>[9][9][9][9][9][9][9][9][9][9]>~T",
                "G~>[9][9][9][9][9][9][9][9][9][9]>~U",
                "C~>[9][9][9][9][9][9][9][9][9][9]>~Z",
                "H~>[9][9][9][9][9][9][9][9][9][9]>~V",
                "D~>[9][9][9][9][9][9][9][9][9][9]>~W",
                "      [9][9][9][9][9][9][9][9]      "
                )
                .SetCapacity(8640)
                .SetCategories(Ids.ToolbarCategories.Storages)
                .SetPrefabPath("Assets/Base/Buildings/Storages/LooseT4.prefab")
                .SetCustomIconPath("Assets/Unity/Generated/Icons/LayoutEntity/StorageLooseT4.png")
                .SetPileGfxParams("Pile_Soft", "Pile_Soft", new LoosePileTextureParams(0.2f, 0f, 0f))
                .BuildAsLooseAndAdd()
                .AddParam(new DrawArrowWileBuildingProtoParam(4f));

        registrator.StorageProtoBuilder
               .Start("Unit Storage V", NewIDs.Buildings.StorageUnitT5)
               .Description("Unit storage with more capacity and more IO")
               .SetCost(Costs.Build.CP4(180).Priority(4))
               .ShowTerrainDesignatorsOnCreation()
               .SetNoTransferLimit()
               .SetLayout(
               "      [9][9][9][9][9][9][9][9]      ",
               "A#>[9][9][9][9][9][9][9][9][9][9]>#X",
               "E#>[9][9][9][9][9][9][9][9][9][9]>#S",
               "B#>[9][9][9][9][9][9][9][9][9][9]>#Y",
               "F#>[9][9][9][9][9][9][9][9][9][9]>#T",
               "G#>[9][9][9][9][9][9][9][9][9][9]>#U",
               "C#>[9][9][9][9][9][9][9][9][9][9]>#Z",
               "H#>[9][9][9][9][9][9][9][9][9][9]>#V",
               "D#>[9][9][9][9][9][9][9][9][9][9]>#W",
               "      [9][9][9][9][9][9][9][9]      "
               )
               .SetCapacity(8640)
               .SetCategories(Ids.ToolbarCategories.Storages)
               .SetPrefabPath("Assets/Base/Buildings/Storages/UnitT4.prefab")
               .SetCustomIconPath("Assets/Unity/Generated/Icons/LayoutEntity/StorageUnitT4.png")
               .BuildUnitAndAdd(new UnitStorageRackData[]
                {
                        new UnitStorageRackData(12, 4, -9f),
                        new UnitStorageRackData(12, 4, -3f),
                        new UnitStorageRackData(12, 4, 3f)
                }, "Assets/Base/Buildings/Storages/UnitT4_rack.prefab")
               .AddParam(new DrawArrowWileBuildingProtoParam(4f));
    }

}
