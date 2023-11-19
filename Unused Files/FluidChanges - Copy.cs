using COIExtended.HarmonyExt;
using Mafi;
using Mafi.Base;
using Mafi.Core.Entities;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Factory;
using Mafi.Core.Research;
using Mafi.Core.Buildings.Storages;
using COIExtended;
using Mafi.Base.Prototypes.Machines;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Prototypes;
using Mafi.Core.Terrain;
using Mafi.Localization;
using System.Collections.Generic;
using System;
using Mafi.Collections;
using Mafi.Core.Entities.Static;
using Mafi.Core.Factory.Transports;
using Mafi.Core.Factory.WellPumps;
using Mafi.Core;
using NewIDs = COIExtended.COIExtended.Research;
using Mafi.Core.World.Contracts;
using Mafi.Core.World.QuickTrade;
using Mafi.Core.World.Entities;
using Mafi.Core.World;
using COIExtended.EntityHelper;
using Mafi.Unity.UiFramework.Components.Tabs;
using UnityEngine;
using System.Linq;

namespace FluidChanges
{
    [GlobalDependency(RegistrationMode.AsEverything, false, false)]
    public class FluidChangePH : IWorldMapManager, IWorldMapGenerator
    {
        public static readonly FluidChangePH Instance = new FluidChangePH();
        private ProtoRegistrator registrator;
        private readonly WorldMapManager m_worldManager;
        private readonly Event<IWorldMapEntity> m_onWorldEntityCreated;
        private WorldMap m_map;
        public event Action<WorldMap> MapReplaced;

        //private EntitiesManager m_entitiesManager;
        //private ContractsManager m_contractManager;

        public IEvent<IWorldMapEntity> OnWorldEntityCreated
        {
            get
            {
                return m_onWorldEntityCreated;
            }
        }

        
        public void StartUp(ProtoRegistrator registrator)
        {
            this.registrator = registrator;
            bool allowStoreAllFluids = ModCFG.allowStoreAllFluids;
            bool addT5StorageTier = ModCFG.addT5StorageTier;

            if (allowStoreAllFluids) { MakeFluidsStorable(); }
            if (addT5StorageTier) { AddTier5Storages(); AddMegaStorageResearchData(); }


            CreateNewProducts();
            //AddNewSettlements();
            Debug.Log("Attempting to create new map");
            //m_map = CreateWorldMap();

            // Advanced Waste Disposal
            AddLargeLiquidDumps();
            AddStackerT2Transport();
            AddStackerLongTransport();
            AddLargeLiquidDumpResearchData();

            // Advanced Pumps
            AddLandWaterWells();
            AddOceanWaterPumps();
            AddAdvancedPumpingResearchData();

            // Nitrogen Manipulation
            AddAirSeparatorT2();
            AddNitrogenProcessing();
            AddNitrogenResearches();

            // Vertical Farming

            // High Rise Housing / Luxury Housing

            // Nanotechnology: Nano computing relies on advances in nanotechnology, which enables the manipulation and control of individual molecules and atoms to perform computational tasks.

            // Nuclear Reclamation
            // Settling Tank 2, Reprocess Fission Product, High grade uranium, more core fuel recipes
            // Acid Icon
            // Plastic Recycling



        }

       
        public void CreateNewProducts()
        {
            FluidProductProto newFluidProduct = registrator.FluidProductProtoBuilder
                .Start("Nitrogen Dioxide", NewIDs.NitrogenDioxideProduct, null)
                .SetIsStorable(true)
                .SetCanBeDiscarded(true)
                .SetIsWaste(false)
                .SetColor(new ColorRgba(255, 69, 0))
                .SetCustomIconPath("Assets/COIExtended/Icons/NitrogenDioxide.png")
                .BuildAndAdd();

            FluidProductProto newFluidProduct2 = registrator.FluidProductProtoBuilder
                .Start("Nitric Acid", NewIDs.NitricAcidProduct, null)
                .SetIsStorable(true)
                .SetCanBeDiscarded(true)
                .SetIsWaste(false)
                .SetColor(new ColorRgba(0, 187, 187))
                .SetCustomIconPath("Assets/COIExtended/Icons/NitricAcid.png")
                .BuildAndAdd();


            /* TRYING TO CHANGE THE ICON FOR ACID WTF, BUNCHA SMALL PROCEDURES IN HERE TO KEEP
            FluidProductProto myTestProto;
            registrator.PrototypesDb.TryGetProto(Ids.Products.Acid, out myTestProto);
            registrator.PrototypesDb.TryRemove(Ids.Products.Acid);
            FluidProductProto.Gfx AcidGraphics = new FluidProductProto.Gfx(Option<string>.None, "Assets/COIExtended/Icons/Acid.png", 13192230);
            Proto.Str strings = Proto.CreateStr(Ids.Products.Acid, "Sulfuric Acid", "translation comment", null);
            FluidProductProto newManualProto = new FluidProductProto(Ids.Products.Acid, strings, true, false, false, AcidGraphics, false, null, null);
            registrator.PrototypesDb.Add(newManualProto, false);   */
            registrator.PrototypesDb.RemoveOrThrow(Ids.Products.Acid);
            
            FluidProductProto newFluidProduct3 = registrator.FluidProductProtoBuilder
                         .Start("Sulfuric Acid", Ids.Products.Acid, null)
                         .SetIsStorable(true)
                         .SetCanBeDiscarded(true)
                         .SetIsWaste(false)
                         .SetColor(13192230)
                         .SetCustomIconPath("Assets/COIExtended/Icons/Acid.png")                         
                         .BuildAndAdd();

            /*RecipeProto[] array = (from x in registrator.PrototypesDb.All<RecipeProto>()
                                    select x).ToArray<RecipeProto>();
            foreach (RecipeProto foundRecipe in array)
            {

            }*/
 
        }

        public void AddNewSettlements()
        {
            
            ProductProto NitricAcid;
            registrator.PrototypesDb.TryGetProto(NewIDs.NitricAcidProduct, out NitricAcid);
            ProductProto CP4;
            registrator.PrototypesDb.TryGetProto(Ids.Products.ConstructionParts4, out CP4);
            ProductProto Diesel;
            registrator.PrototypesDb.TryGetProto(Ids.Products.Diesel, out Diesel);
            ProductProto CarbonDioxide;
            registrator.PrototypesDb.TryGetProto(Ids.Products.CarbonDioxide, out CarbonDioxide);
            Quantity amountForTrade = new Quantity(250);

            // Creates the village prototype
            WorldMapEntityProto.Gfx graphics = new WorldMapEntityProto.Gfx("Assets/Base/Icons/WorldMap/Coal.svg", "Assets/Base/Icons/WorldMap/Coal.svg");
            Proto.Str stringInfo = Proto.CreateStr(new Proto.ID("Luxurious Island"), "Settlement", null, null);

            registrator.PrototypesDb.Add<WorldMapVillageProto>(new WorldMapVillageProto(NewIDs.Settlement_Ilmenite,stringInfo,0,0,Upoints.Zero, null, ImmutableArray.Create<QuickTradePairProto>(new QuickTradePairProto[]
            {
                 new QuickTradePairProto(NewIDs.Settlement_Ilmenite, new ProductQuantity(Diesel, amountForTrade), new ProductQuantity(CarbonDioxide, amountForTrade), Upoints.Zero, 10, 0, 5, Duration.FromDays(14), Percent.FromInt(15), Percent.FromInt(15), false)

            }), ImmutableArray.Create<ContractProto>(new ContractProto[]
           {
                new ContractProto(NewIDs.Settlement_Ilmenite, new ProductQuantity(NitricAcid,amountForTrade), new ProductQuantity(CP4, amountForTrade), Upoints.Zero, Upoints.Zero, 0)
           }), graphics, null));
            //Create the location prototype
            
          








        }
        public void SetMap(WorldMap newMap)
        {
            Debug.Log("SetMap() running");
            if (m_map == newMap)
            {
                Debug.Log("Map is already the map?");
                return;
            }
            WorldMap map = m_map;
            Debug.Log("SetMap() setting m_map to new map");
            if (map != null)
            {
                map.Deactivate();
            }
            m_map = newMap.CheckNotNull("Fix it!");
            Debug.Log("SetMap() Trying to Initialize the map");
            m_map.Initialize(m_worldManager);
            Debug.Log("SetMap() running");
            Action<WorldMap> mapReplaced = MapReplaced;

            if (mapReplaced == null)
            {
                Debug.Log("SetMap() returned cause mapReplaced is null");
                return;
            }
            Debug.Log("SetMap() running map replace action");
            mapReplaced(newMap);
        }

        public WorldMap CreateWorldMap()
        {
            Debug.Log("Making new map");
            WorldMap COIEMap = new WorldMap();
            Debug.Log("Adding a location");
            WorldMapLocation worldMapLocation = new WorldMapLocation("test", new Vector2i(5,5));
            COIEMap.AddLocation(worldMapLocation, false);
            Debug.Log("Setting it as home");
            COIEMap.SetHomeLocation(COIEMap.Locations.First<WorldMapLocation>());
            Debug.Log("Attempting to set map");
            SetMap(COIEMap);
            Debug.Log("Setting map done?");

            return COIEMap;
        }
        public WorldMap Map
        {
            get
            {
                if (m_map == null)
                {
                    throw new Exception("Getting map which was not generated yet.");
                }
                return m_map;
            }
        }

        

        private void AddNitrogenResearches()
        {
            try
            {
                ResearchNodeProto NitrogenManipulation = registrator.ResearchNodeProtoBuilder
                .Start("Nitrogen Manipulation", NewIDs.NitrogenManipulation)
                .Description("More processes involving the usage of Nitrogen gas.")
                .AddMachineToUnlock(NewIDs.AirSeparatorT2, false)
                .AddRecipeToUnlock(NewIDs.AirSeparationT2)
                .AddRecipeToUnlock(NewIDs.NitrogenDioxideRecipe)
                .AddRecipeToUnlock(NewIDs.NitricAcidRecipe)
                .AddRecipeToUnlock(NewIDs.NitricAcidElectrolysis)
                .AddRecipeToUnlock(NewIDs.NitricAcidGlassMix)
                .AddRecipeToUnlock(NewIDs.NitricAcidGoldSettling)
                .AddRecipeToUnlock(NewIDs.NitricAcidHFSettling)
                .AddRecipeToUnlock(NewIDs.NitricAcidUraniumSettling)
                .AddProductToUnlock(NewIDs.NitrogenDioxideProduct)
                .AddProductToUnlock(NewIDs.NitricAcidProduct)
                .SetCosts(new ResearchCostsTpl(22))
                .AddProductIcon(NewIDs.NitrogenDioxideProduct)
                .AddProductIcon(NewIDs.NitricAcidProduct)
                .BuildAndAdd();
                NitrogenManipulation.GridPosition = new Vector2i(104, 14);
                NitrogenManipulation.AddParent(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.GoldSmelting));
            }
            catch { }
        }

       
        private void AddAirSeparatorT2()
        {
            LayoutEntityBuilderState<MachineProtoBuilder.MachineProtoBuilderStateBase> layoutEntityBuilderState = registrator.MachineProtoBuilder
                .Start("Air Separator II", NewIDs.AirSeparatorT2, "name of a machine").Description("Performs a cryogenic distillation process at temperatures reaching -200 °C to separate atmospheric air into its primary components - oxygen and nitrogen.", "short description of a machine")
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
                .Start("Better Air Separation", NewIDs.AirSeparationT2, machine)
                .SetDurationSeconds(20)
                .AddOutput(30, Ids.Products.Oxygen, "Y", false, false)
                .AddOutput(30, Ids.Products.Nitrogen, "X", false, false)
                .BuildAndAdd();
            registrator.RecipeProtoBuilder
                .Start("Make Nitrogen Dioxide", NewIDs.NitrogenDioxideRecipe, machine)
                .SetDurationSeconds(20)
                .AddInput(10, Ids.Products.Nitrogen, "A", false)
                .AddInput(20, Ids.Products.Oxygen, "B", false)
                .AddOutput(20, NewIDs.NitrogenDioxideProduct, "X", false, false)
                .BuildAndAdd();

            // Add a bunch of Nitric Acid related recipes to game Prototypes to unlock later

            MachineProto chemicalPlant;
            registrator.PrototypesDb.TryGetProto(Ids.Machines.ChemicalPlant2, out chemicalPlant);

            registrator.RecipeProtoBuilder
                .Start("Make Nitric Acid", NewIDs.NitricAcidRecipe, chemicalPlant)
                .SetDurationSeconds(20)
                .AddInput(8, NewIDs.NitrogenDioxideProduct, "A", false)
                .AddInput(4, Ids.Products.Hydrogen, "B", false)
                .AddInput(4, Ids.Products.ChilledWater, "C", false)
                .AddOutput(8, NewIDs.NitricAcidProduct, "X", false, false)
                .BuildAndAdd();

            MachineProto copperPlant;
            registrator.PrototypesDb.TryGetProto(Ids.Machines.CopperElectrolysis, out copperPlant);

            registrator.RecipeProtoBuilder
                .Start("Nitric Acid Electrolysis", NewIDs.NitricAcidElectrolysis, copperPlant)
                .SetDurationSeconds(40)
                .AddInput(16, Ids.Products.ImpureCopper, "*", false)
                .AddInput(2, NewIDs.NitricAcidProduct, "*", false)
                .AddOutput(16, Ids.Products.Copper, "*", false, false)
                .BuildAndAdd();

            MachineProto mixerPlant;
            registrator.PrototypesDb.TryGetProto(Ids.Machines.IndustrialMixerT2, out mixerPlant);

            registrator.RecipeProtoBuilder
                .Start("Nitric Acid Glass Mix", NewIDs.NitricAcidGlassMix, mixerPlant)
                .SetDurationSeconds(10)
                .AddInput(16, Ids.Products.Sand, "*", false)
                .AddInput(2, NewIDs.NitricAcidProduct, "*", false)
                .AddInput(4, Ids.Products.Limestone, "*", false)    
                .AddInput(2, Ids.Products.Salt, "*", false) 
                .AddOutput(16, Ids.Products.GlassMix, "*", false, false)
                .BuildAndAdd();

            MachineProto settlingPlant;
            registrator.PrototypesDb.TryGetProto(Ids.Machines.SettlingTank, out settlingPlant); 
            
            registrator.RecipeProtoBuilder  
                .Start("Nitric Acid Gold Settling", NewIDs.NitricAcidGoldSettling, settlingPlant)
                .SetDurationSeconds(20)
                .AddInput(10, Ids.Products.GoldOreCrushed, "*", false)
                .AddInput(2, NewIDs.NitricAcidProduct, "*", false)
                .AddOutput(3, Ids.Products.GoldOreConcentrate, "*", false)
                .AddOutput(6, Ids.Products.ToxicSlurry, "*", false) 
                .BuildAndAdd();

            registrator.RecipeProtoBuilder
                .Start("Nitric Acid Uranium Settling", NewIDs.NitricAcidUraniumSettling, settlingPlant)
                .SetDurationSeconds(40)
                .AddInput(12, Ids.Products.UraniumOreCrushed, "*", false)
                .AddInput(2, NewIDs.NitricAcidProduct, "*", false)
                .AddOutput(6, Ids.Products.Yellowcake, "*", false)
                .AddOutput(4, Ids.Products.ToxicSlurry, "*", false)
                .BuildAndAdd();

            registrator.RecipeProtoBuilder
                 .Start("Nitric Acid HF Settling", NewIDs.NitricAcidHFSettling, settlingPlant)
                 .SetDurationSeconds(40)
                 .AddInput(8, Ids.Products.Rock, "*", false)
                 .AddInput(4, NewIDs.NitricAcidProduct, "*", false)
                 .AddOutput(10, Ids.Products.HydrogenFluoride, "*", false)
                 .BuildAndAdd();
        }
        private void AddNitrogenProcessing()
        {
        }

        
        
        private void AddAdvancedPumpingResearchData()
        {
            try
            {
                ResearchNodeProto AdvancedPumping = registrator.ResearchNodeProtoBuilder
                .Start("Advanced Pumping", NewIDs.AdvancedPumping)
                .Description("The next generation of pumping!")
                .AddMachineToUnlock(NewIDs.OceanWaterPumpT2, false)
                .AddMachineToUnlock(NewIDs.OceanWaterPumpTallT2, false)
                .AddMachineToUnlock(NewIDs.LandWaterPumpT2, false)
                .AddRecipeToUnlock(NewIDs.LargeLandWaterPumping)
                .AddRecipeToUnlock(NewIDs.LargeSeaWaterPumping)
                .AddRecipeToUnlock(NewIDs.LargeSeaWaterPumpingTall)
                .SetCosts(new ResearchCostsTpl(14))
                .BuildAndAdd();
                AdvancedPumping.GridPosition = new Vector2i(76, 29);
                AdvancedPumping.AddParent(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.PipeTransportsT3));
            }
            catch { }
        }
        private void AddOceanWaterPumps()
        {
            ProtosDb prototypesDb = registrator.PrototypesDb;
            ProtosDb protosDb = prototypesDb;
            MachineProto.ID oceanWaterPumpT = NewIDs.OceanWaterPumpT2;
            Proto.Str strings = Proto.CreateStrFormatDesc1(NewIDs.OceanWaterPumpT2, "Seawater Pump II", "Pumps water from ocean. Works at the maximum height of {0} from the ocean level.", new LocStrFormatted(5.ToString()), "{0} is an integer specifying max height such as '5'");

            EntityLayoutParser layoutParser = registrator.LayoutParser;

            Predicate<LayoutTile> ignoreTilesForCore = (LayoutTile x) => x.Constraint == LayoutTileConstraint.None || x.Constraint.HasAnyConstraints(LayoutTileConstraint.Ocean);
            CustomLayoutToken[] array = new CustomLayoutToken[4];
            array[0] = new CustomLayoutToken("-0~", (EntityLayoutParams p, int h) => new LayoutTokenSpec(-h - 1, -h + 2, LayoutTileConstraint.Ocean, null, null, null, null, null, null, false, false, 0));
            array[1] = new CustomLayoutToken("-0}", (EntityLayoutParams p, int h) => new LayoutTokenSpec(-h - 1, -h + 2, LayoutTileConstraint.None, null, null, null, null, null, null, false, false, 0));
            array[2] = new CustomLayoutToken("~~~", (EntityLayoutParams p, int h) => new LayoutTokenSpec(-13, -10, LayoutTileConstraint.Ocean, null, null, null, null, null, null, false, false, 0));
            array[3] = new CustomLayoutToken("{P}", delegate (EntityLayoutParams p, int h)
            {
                int heightFrom = -5;
                int heightToExcl = 2;
                LayoutTileConstraint constraint = LayoutTileConstraint.None;
                int? maxTerrainHeight = new int?(0);
                return new LayoutTokenSpec(heightFrom, heightToExcl, constraint, null, null, maxTerrainHeight, null, null, null, false, false, 0);
            });
            OceanWaterPumpProto machine = protosDb.Add<OceanWaterPumpProto>(new OceanWaterPumpProto(oceanWaterPumpT, strings, layoutParser
                .ParseLayoutOrThrow(new EntityLayoutParams(ignoreTilesForCore, array, false, null, null, null, null, null, default(Option<IEnumerable<KeyValuePair<char, int>>>)), new string[]
            {
                "                                    [2][2]         ",
                "-6~-5}-4}-3}-2}-1}-1}{P}{2}{2}{P}{2}[2][2][2][2]>@X",
                "-6~-5}-4}-3}-2}-1}-1}{P}{2}{2}{P}{2}[2][2][2][2]   ",
                "                                 {1}[2][2][2][2]   "
            }), Costs.Machines.OceanWaterPump
            .MapToEntityCosts(registrator, false), 200.Kw(), ImmutableArray
            .Create<AnimationParams>(AnimationParams.Loop(null, false, null)), ImmutableArray.Create<ImmutableArray<RectangleTerrainArea2iRelative>>(ImmutableArray.Create<RectangleTerrainArea2iRelative>(new RectangleTerrainArea2iRelative(new RelTile2i(-17, -2), new RelTile2i(3, 4)))), new HeightTilesI(1), new HeightTilesI(5), new MachineProto.Gfx("Assets/Base/Machines/Pump/OceanWaterPumpT1.prefab", registrator.GetCategoriesProtos(new Proto.ID[]
            {
                Ids.ToolbarCategories.MachinesWater
            }), default(RelTile3f), "Assets/Unity/Generated/Icons/LayoutEntity/OceanWaterPumpT1.png", default(ImmutableArray<ParticlesParams>), default(ImmutableArray<EmissionParams>), default(Option<string>), false, true, null, null, ColorRgba.Blue, null), null, false, default(Option<MachineProto>), default(Computing), null, false, false, null), false);
            registrator.RecipeProtoBuilder
                .Start("Ocean water pumping", NewIDs.LargeSeaWaterPumping, machine)
                .AddOutput(54, Ids.Products.Seawater, "X", false, false)
                .SetDuration(10.Seconds())
                .BuildAndAdd();

            ProtosDb protosDb2 = prototypesDb;
            MachineProto.ID oceanWaterPumpLarge = NewIDs.OceanWaterPumpTallT2;
            Proto.Str strings2 = Proto.CreateStrFormatDesc1(NewIDs.OceanWaterPumpTallT2, "Seawater Pump II (Tall)", "Larger pump that can be placed up to height of {0} from the ocean level. Requires more power to run.", new LocStrFormatted(11.ToString()), "{0} is an integer specifying max height such as '10'");
            EntityLayoutParser layoutParser2 = registrator.LayoutParser;
            Predicate<LayoutTile> ignoreTilesForCore2 = (LayoutTile x) => x.Constraint == LayoutTileConstraint.None || x.Constraint.HasAnyConstraints(LayoutTileConstraint.Ocean);
            CustomLayoutToken[] array2 = new CustomLayoutToken[4];
            array2[0] = new CustomLayoutToken("-0~", (EntityLayoutParams p, int h) => new LayoutTokenSpec(-h - 6, -h - 3, LayoutTileConstraint.Ocean, null, null, null, null, null, null, false, false, 0));
            array2[1] = new CustomLayoutToken("-0}", (EntityLayoutParams p, int h) => new LayoutTokenSpec(-h - 6, -h - 3, LayoutTileConstraint.None, null, null, null, null, null, null, false, false, 0));
            array2[2] = new CustomLayoutToken("~~~", (EntityLayoutParams p, int h) => new LayoutTokenSpec(-22, -19, LayoutTileConstraint.Ocean, null, null, null, null, null, null, false, false, 0));
            array2[3] = new CustomLayoutToken("{P}", delegate (EntityLayoutParams p, int h)
            {
                int heightFrom = -9;
                int heightToExcl = 2;
                LayoutTileConstraint constraint = LayoutTileConstraint.None;
                int? maxTerrainHeight = new int?(0);
                return new LayoutTokenSpec(heightFrom, heightToExcl, constraint, null, null, maxTerrainHeight, null, null, null, false, false, 0);
            });
            OceanWaterPumpProto machine2 = protosDb2.Add<OceanWaterPumpProto>(new OceanWaterPumpProto(oceanWaterPumpLarge, strings2, layoutParser2.ParseLayoutOrThrow(new EntityLayoutParams(ignoreTilesForCore2, array2, false, null, null, null, null, null, default(Option<IEnumerable<KeyValuePair<char, int>>>)), new string[]
            {
                "                                                         {1}(2)[2][2][2]   ",
                "-6~-5}-4}-3}-2}-1}-5}-4}-3}-2}-1}{1}{1}{1}{1}{P}{2}{2}{P}{2}(2)[2][2][2]>@X",
                "-6~-5}-4}-3}-2}-1}-5}-4}-3}-2}-1}{1}{1}{1}{1}{P}{2}{2}{P}{2}(2)[2][2][2]   ",
                "                                                         {1}(2)[2][2][2]   "
            }), Costs.Machines.OceanWaterPumpLarge.MapToEntityCosts(registrator, false), 500.Kw(), ImmutableArray.Create<AnimationParams>(AnimationParams.Loop(null, false, null)), ImmutableArray.Create<ImmutableArray<RectangleTerrainArea2iRelative>>(ImmutableArray.Create<RectangleTerrainArea2iRelative>(new RectangleTerrainArea2iRelative(new RelTile2i(-25, -1), new RelTile2i(3, 4)))), new HeightTilesI(1), new HeightTilesI(11), new MachineProto.Gfx("Assets/Base/Machines/Pump/OceanWaterPumpT2.prefab", registrator.GetCategoriesProtos(new Proto.ID[]
            {
                Ids.ToolbarCategories.MachinesWater
            }), default(RelTile3f), "Assets/Unity/Generated/Icons/LayoutEntity/OceanWaterPumpLarge.png", default(ImmutableArray<ParticlesParams>), default(ImmutableArray<EmissionParams>), default(Option<string>), false, true, null, null, default(ColorRgba), null), null, false, default(Option<MachineProto>), default(Computing), null, false, false, null), false);
            registrator.RecipeProtoBuilder
                .Start("Ocean Water Pumping II", NewIDs.LargeSeaWaterPumpingTall, machine2)
                .AddOutput(54, Ids.Products.Seawater, "X", false, false)
                .SetDuration(10.Seconds()).BuildAndAdd();
        }
               
        private void AddLandWaterWells()
        {            
 
            LocStr locStr2 = Loc.Str("GroundReserveTooltip__Groundwater", "Shows the overall status of the reserve of groundwater. Groundwater is replenished during rain and can temporarily run out if pumped out too much.", "tooltip");
            LocStr desc = Loc.Str(Ids.Machines.LandWaterPump.Value + "__desc", "Pumps water from the ground deposit which is replenished during rain. Has to be built on top of a groundwater deposit.", "description of ground water pump");

            WellPumpProto machine2 = registrator.WellPumpProtoBuilder
                .Start("Groundwater Pump II", locStr2.TranslatedString, NewIDs.LandWaterPumpT2).Description(desc)
                .SetCost(Costs.Machines.LandWaterPump, false)
                .SetElectricityConsumption(240.Kw())
                .SetMinedProduct(IdsCore.Products.Groundwater)
                .NotifyWhenBelow(40.Percent())
                .SetCategories(new Proto.ID[]
            {
                Ids.ToolbarCategories.MachinesWater
            })
                .SetLayout(new string[]
            {
                "[2][7][7][2]   ",
                "[2][7][7][2]   ",
                "[2][7][7][2]   ",
                "[2][4][4][2]>@X",
                "   [2][2][2]   ",
                "   [2][2][2]   "
            })
                .SetPrefabPath("Assets/Base/Machines/Pump/LandWaterPump.prefab")
                .SetCustomIconPath("Assets/Unity/Generated/Icons/LayoutEntity/LandWaterPump.png")
                .SetAnimationParams(AnimationParams.Loop(null, false, null))
                .SetMachineSound("Assets/Base/Machines/Pump/LandWaterPump/LandWaterPump_Sound.prefab")
                .EnableSemiInstancedRendering(default(ImmutableArray<string>))
                .BuildAndAdd();
            registrator.RecipeProtoBuilder.Start("Water Pumping II", NewIDs.LargeLandWaterPumping, machine2).AddOutput(24, Ids.Products.Water, "*", false, false).SetDurationSeconds(10)
                .BuildAndAdd();
        }
        private void AddLargeLiquidDumps()
        {
            ProtosDb prototypesDb = registrator.PrototypesDb;
            ProtosDb protosDb = prototypesDb;
            MachineProto.ID wasteDump = NewIDs.LargeLiquidDump;
            Proto.Str strings = Proto.CreateStrFormatDesc1(NewIDs.LargeLiquidDump, "Liquid Dump II", "Dumps liquid into the ocean. Some liquid will cause water pollution which can affect health and happiness of your people. Works at the maximum height of {0} from the ocean level.", new LocStrFormatted(11.ToString()), "{0} is an integer specifying max height such as '5'");
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
            ImmutableArray<ImmutableArray<RectangleTerrainArea2iRelative>> reservedOceanAreasSets = ImmutableArray.Create<ImmutableArray<RectangleTerrainArea2iRelative>>(ImmutableArray.Create<RectangleTerrainArea2iRelative>(new RectangleTerrainArea2iRelative(new RelTile2i(-33, -4), new RelTile2i(4, 6))));
            HeightTilesI minGroundHeight = new HeightTilesI(1);
            HeightTilesI maxGroundHeight = new HeightTilesI(30);
            string prefabPath = "Assets/Base/Machines/Water/WasteDump.prefab";
            RelTile3f relTile3f = new RelTile3f(-0.25.ToFix32(), -0.125.ToFix32(), 0);
            ImmutableArray<ToolbarCategoryProto> categoriesProtos = registrator.GetCategoriesProtos(new Proto.ID[]
            {
                Ids.ToolbarCategories.Waste
            });
            RelTile3f prefabOffset = relTile3f;
            ImmutableArray<ParticlesParams> particlesParams = ImmutableArray.Create<ParticlesParams>(ParticlesParams.Loop("WasteParticles", false, null, (RecipeProto r) => r.AllInputs.First.Product.Graphics.Color));
            Option<string> machineSoundPrefabPath = "Assets/Base/Machines/Water/WasteDump/WasteDump_Sound.prefab";

            OceanLiquidDumpProto machine = protosDb.Add<OceanLiquidDumpProto>(new OceanLiquidDumpProto(wasteDump, strings, layout, costs, zero, empty, reservedOceanAreasSets, minGroundHeight, maxGroundHeight, new MachineProto.Gfx(prefabPath, categoriesProtos, prefabOffset, "Assets/Unity/Generated/Icons/LayoutEntity/WasteDump.png", particlesParams, default(ImmutableArray<EmissionParams>), machineSoundPrefabPath, false, true, null, null, default(ColorRgba), null), null, false, default(Option<MachineProto>), default(Computing), null, true, false, null), false);
            registrator.RecipeProtoBuilder.Start("Large Water Dumping", NewIDs.LargeWaterDumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.Water, "*", false).SetDurationSeconds(2).EnablePartialExecution(1.Percent()).BuildAndAdd();
            registrator.RecipeProtoBuilder.Start("Large Brine Dumping", NewIDs.LargeBrineDumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.Brine, "*", false).SetDurationSeconds(2).EnablePartialExecution(1.Percent()).BuildAndAdd();
            registrator.RecipeProtoBuilder.Start("Large Waste Water Dumping", NewIDs.LargeWasteWaterDumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.WasteWater, "*", false).AddOutput(2, Ids.Products.PollutedWater, "VIRTUAL", false, false).SetDurationSeconds(2).EnablePartialExecution(1.Percent()).BuildAndAdd();
            registrator.RecipeProtoBuilder.Start("Large Sour Water Dumping", NewIDs.LargeSourWaterDumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.SourWater, "*", false).AddOutput(4, Ids.Products.PollutedWater, "VIRTUAL", false, false).SetDurationSeconds(2).EnablePartialExecution(1.Percent()).BuildAndAdd();
            registrator.RecipeProtoBuilder.Start("Large Acid Dumping", NewIDs.LargeAcidDumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.Acid, "*", false).AddOutput(4, Ids.Products.PollutedWater, "VIRTUAL", false, false).SetDurationSeconds(2).EnablePartialExecution(1.Percent()).BuildAndAdd();
            registrator.RecipeProtoBuilder.Start("Large Toxic Dumping", NewIDs.LargeToxicDumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.ToxicSlurry, "*", false).AddOutput(5, Ids.Products.PollutedWater, "VIRTUAL", false, false).SetDurationSeconds(2).EnablePartialExecution(1.Percent()).BuildAndAdd();
            registrator.RecipeProtoBuilder.Start("Large Seawater Dumping", NewIDs.LargeSeaWaterDumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.Seawater, "*", false).SetDurationSeconds(2).EnablePartialExecution(1.Percent()).BuildAndAdd();
            registrator.RecipeProtoBuilder.Start("Large Fertilizer Dumping", NewIDs.LargeFert1Dumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(14, Ids.Products.FertilizerOrganic, "*", false).AddOutput(1, Ids.Products.PollutedWater, "VIRTUAL", false, false).SetDurationSeconds(4).EnablePartialExecution(1.Percent()).BuildAndAdd();
            registrator.RecipeProtoBuilder.Start("Large Fertilizer Dumping", NewIDs.LargeFert2Dumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.FertilizerChemical, "*", false).AddOutput(1, Ids.Products.PollutedWater, "VIRTUAL", false, false).SetDurationSeconds(4).EnablePartialExecution(1.Percent()).BuildAndAdd();
            registrator.RecipeProtoBuilder.Start("Large Fertilizer Dumping", NewIDs.LargeFert3Dumping, machine).SetProductsDestroyReason(DestroyReason.DumpedOnTerrain).AddInput(12, Ids.Products.FertilizerChemical2, "*", false).AddOutput(2, Ids.Products.PollutedWater, "VIRTUAL", false, false).SetDurationSeconds(4).EnablePartialExecution(1.Percent()).BuildAndAdd();

        }


        private void MakeFluidsStorable()
        {
                FluidProductProto coreFuelProto;
                registrator.PrototypesDb.TryGetProto(Ids.Products.CoreFuel, out coreFuelProto);
                coreFuelProto.SetField("IsStorable", true);
                registrator.PrototypesDb.TryGetProto(Ids.Products.CoreFuelDirty, out coreFuelProto);
                coreFuelProto.SetField("IsStorable", true);
                registrator.PrototypesDb.TryGetProto(Ids.Products.SteamSp, out coreFuelProto);
                coreFuelProto.SetField("IsStorable", true);
                registrator.PrototypesDb.TryGetProto(Ids.Products.SteamHi, out coreFuelProto);
                coreFuelProto.SetField("IsStorable", true);
                registrator.PrototypesDb.TryGetProto(Ids.Products.SteamLo, out coreFuelProto);
                coreFuelProto.SetField("IsStorable", true);
                registrator.PrototypesDb.TryGetProto(Ids.Products.SteamDepleted, out coreFuelProto);
                coreFuelProto.SetField("IsStorable", true);
                registrator.PrototypesDb.TryGetProto(Ids.Products.Exhaust, out coreFuelProto);
                coreFuelProto.SetField("IsStorable", true);
        }
        private void AddMegaStorageResearchData()
        {
            try
            {           
                ResearchNodeProto MegaStorageResearch = registrator.ResearchNodeProtoBuilder
                .Start("Mega Storage", NewIDs.UnlockExpandedStorages)
                .Description("Even more options for storage with more IO support.")
                .AddLayoutEntityToUnlock(NewIDs.StorageFluidT5, false)
                .AddLayoutEntityToUnlock(NewIDs.StorageUnitT5, false)
                .AddLayoutEntityToUnlock(NewIDs.StorageLooseT5, false)
                .SetCosts(new ResearchCostsTpl(69))
                .BuildAndAdd();
                MegaStorageResearch.GridPosition = new Vector2i(130, 18);
                MegaStorageResearch.AddParent(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.ConsumerElectronics));

            }
            catch { }
        }
        private void AddStackerT2Transport()
        {
            ProtosDb prototypesDb = registrator.PrototypesDb;
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
                prototypesDb.GetOrThrow<ToolbarCategoryProto>(Ids.ToolbarCategories.Transports)
            };
            LayoutEntityProto.VisualizedLayers empty = LayoutEntityProto.VisualizedLayers.Empty;
            ProtosDb protosDb = prototypesDb;

            EntityCostsTpl StackerT2Cost = Costs.Build.CP2(25).Product(25, Ids.Products.Rubber);

            StaticEntityProto.ID stacker = NewIDs.StackerT2;
            Proto.Str strings = Proto.CreateStr(NewIDs.StackerT2, "Stacker II", "Dumps material from connected conveyor belts directly on the terrain.", null);
            EntityLayout layout = registrator.LayoutParser.ParseLayoutOrThrow(layoutParams, new string[]
            {
                "   [2][2][2]            (1G(1G                                             ",
                "A~>[2][2][2]      (3A(2G(2X(3X(4B(5A                                       ",
                "   [2][2][2](3A(3A(4B(4G(5X(5X(6D(6B(7A(7A(8A(8A(9B(9A10B10A11B11A12B12A12A",
                "B~>[2][2][2]      (3A(2G(2X(3X(4B(5A                                       ",
                "   [2][2][2]            (1G(1G                                             "
            });
            EntityCosts costs = StackerT2Cost.MapToEntityCosts(registrator, false);
            Electricity consumedPowerPerTick = 80.Kw();
            ThicknessTilesI minDumpOffset = 1.TilesThick();
            ThicknessTilesI defaultDumpOffset = 2.TilesThick();
            protosDb.Add<StackerProto>(new StackerProto(stacker, strings, layout, costs, consumedPowerPerTick, minDumpOffset, new RelTile3i(22, 0, 11), 2.Seconds(), 1.Ticks(), new StackerProto.Gfx("Assets/Base/Transports/Stacker/Stacker.prefab", new RelTile3f(-10.5.ToFix32(), 0, 0), "Assets/Unity/Generated/Icons/LayoutEntity/Stacker.png", lyst2.ToImmutableArray(), ImmutableArray<EmissionParams>.Empty, Option<string>.None, ColorRgba.Empty, false, false, empty, lyst3.ToImmutableArray()), defaultDumpOffset, null), false);

        }

        private void AddStackerLongTransport()
        {
            ProtosDb prototypesDb = registrator.PrototypesDb;
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
                prototypesDb.GetOrThrow<ToolbarCategoryProto>(Ids.ToolbarCategories.Transports)
            };
            LayoutEntityProto.VisualizedLayers empty = LayoutEntityProto.VisualizedLayers.Empty;
            ProtosDb protosDb = prototypesDb;

            EntityCostsTpl StackerLongCost = Costs.Build.CP2(25).Product(50, Ids.Products.Rubber);

            StaticEntityProto.ID stacker = NewIDs.StackerLong;
            Proto.Str strings = Proto.CreateStr(NewIDs.StackerLong, "Stacker (Long)", "NOTE: This stacker uses the original prefab for the game and as such will not appear to be dumping in the correct location, this is normal.  The correct location will be where the green overlay is placed as on a normal Stacker.", null);
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
            protosDb.Add<StackerProto>(new StackerProto(stacker, strings, layout, costs, consumedPowerPerTick, minDumpOffset, new RelTile3i(42, 0, 21), 2.Seconds(), 1.Ticks(), new StackerProto.Gfx("Assets/COIExtended/Buildings/StackerLong/StackerLong.obj", new RelTile3f(0, 0, 0), "Assets/Unity/Generated/Icons/LayoutEntity/Stacker.png", lyst2.ToImmutableArray(), ImmutableArray<EmissionParams>.Empty, Option<string>.None, ColorRgba.Empty, false, false, empty, lyst3.ToImmutableArray()), defaultDumpOffset, null), false);

            
        }
        private void AddLargeLiquidDumpResearchData()
        {
            try
            {
                ResearchNodeProto LargeLiquidDump = registrator.ResearchNodeProtoBuilder
                .Start("Large Waste Disposal", NewIDs.UnlockLargeWasteDumps)
                .Description("The next generation in waste removal!")
                .AddMachineToUnlock(NewIDs.LargeLiquidDump, false)
                .SetCosts(new ResearchCostsTpl(7))
                .AddRecipeToUnlock(NewIDs.LargeWaterDumping)
                .AddRecipeToUnlock(NewIDs.LargeSeaWaterDumping)
                .AddRecipeToUnlock(NewIDs.LargeBrineDumping)
                .AddRecipeToUnlock(NewIDs.LargeWasteWaterDumping)
                .AddRecipeToUnlock(NewIDs.LargeSourWaterDumping)
                .AddRecipeToUnlock(NewIDs.LargeAcidDumping)
                .AddRecipeToUnlock(NewIDs.LargeToxicDumping)
                .AddRecipeToUnlock(NewIDs.LargeFert1Dumping)
                .AddRecipeToUnlock(NewIDs.LargeFert2Dumping)
                .AddRecipeToUnlock(NewIDs.LargeFert3Dumping)
                .AddLayoutEntityToUnlock(NewIDs.StackerT2)
                .AddLayoutEntityToUnlock(NewIDs.StackerLong)
                .BuildAndAdd();
                LargeLiquidDump.GridPosition = new Vector2i(36, 28);
                LargeLiquidDump.AddParent(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.ThermalDesalinationBasic));
            }
            catch { }
        }
        private void AddTier5Storages()
        {
            registrator.StorageProtoBuilder
                .Start("Fluid Storage V", NewIDs.StorageFluidT5)
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
                    .Start("Loose Storage V", NewIDs.StorageLooseT5)
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
                   .Start("Unit Storage V", NewIDs.StorageUnitT5)
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
}