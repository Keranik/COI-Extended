using Mafi;
using Mafi.Base;
using Mafi.Core.Buildings.Settlements;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Game;
using Mafi.Core.Mods;
using Mafi.Core.Population;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;


namespace COIExtended.Prototypes.Settlement;
internal class HardwareStoreModule : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        // Try to get the UPoints "Services" Category 
        UpointsStatsCategoryProto upointsStatsCategoryProto;
        registrator.PrototypesDb.TryFindProtoIgnoreCase("Services", out upointsStatsCategoryProto);
        // Try to get the proto for CP4
        ProductProto cp4Proto = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.ConstructionParts4);
        // Create a sub-category for hardware store Upoints
        UpointsCategoryProto hardwareUPointsCat = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewIDs.PopNeeds.HardwareStoreNeed, "Assets/Base/Products/Icons/ConstructionParts4.svg", upointsStatsCategoryProto, hideCount: true));
        // Get the game difficulty for scaling
        GameDifficultyConfig difficultyConfig = registrator.GetConfigOrThrow<GameDifficultyConfig>();
        // Create the Population Need 
        PopNeedProto hardwareNeedProto = registrator.PrototypesDb.Add(new PopNeedProto(NewIDs.PopNeeds.HardwareStoreNeed, Proto.CreateStr(NewIDs.PopNeeds.HardwareStoreNeed, "Construction Parts IV", "", "translation"), withDifficulty(1.8.Upoints()), hardwareUPointsCat, null, Ids.Properties.ConsumerElectronicsConsumptionMultiplier, Ids.Properties.ConsumerElectronicsUnityMultiplier, new PopNeedProto.Gfx("Assets/Base/Products/Icons/ConstructionParts4.svg")));
        // Build the module with proto builder
        SettlementModuleProtoBuilder sBuilder = new SettlementModuleProtoBuilder(registrator);
        sBuilder
            .Start("Hardware Store Module", NewIDs.Buildings.SettlementHardwareStore)
            .Description("Provides your settlers with hardware, which generates Unity!")
            .SetNeed(hardwareNeedProto)
            .SetCost(Costs.Buildings.SettlementHouseholdGoodsModule)
            .SetElectricityConsumed(150.Kw())
            .SetCategories(Ids.ToolbarCategories.Housing)
            .SetLayout(new EntityLayoutParams(null, null, portsCanOnlyConnectToTransports: false, Ids.TerrainTileSurfaces.SettlementPaths), "[4][4][4][4][5][5][5][5][5][5][4]   ", "[4][4][4][4][5][5][5][5][5][5][4]   ", "[4][4][4][4][5][5][5][5][5][5][4]<A#", "[4][4][4][4][5][5][5][5][5][5][4]   ", "[4][4][4][4][5][5][5][5][5][5][4]<B#", "[4][4][4][4][5][5][5][5][5][5][4]   ", "[4][4][4][4][5][5][5][5][5][5][4]   ")
            .SetInput(cp4Proto, 0.011.ToFix32(), 240)
            .SetPrefabPath("Assets/COIExtended/Buildings/HardwareStore/HardwareStore2.prefab")
            .SetAnimationParams(AnimationParams.Loop(60.Percent()))
            .SetEmissionIntensity(2)
            .SetCustomIconPath("Assets/COIExtended/Buildings/HardwareStore/HardwareStore.png")
            .BuildAndAdd();
        // For scaling UPoints
        Upoints withDifficulty(Upoints unity)
        {
            return unity.ScaledBy(difficultyConfig.UnityProductionMult);
        }


    }
}
