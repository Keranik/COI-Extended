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
using NewWIDs = COIExtended.COIExtended.World;
using Mafi.Core.World.Contracts;
using Mafi.Core.World.QuickTrade;
using Mafi.Core.World.Entities;
using Mafi.Base.Prototypes.Machines.PowerGenerators;
using Mafi.Core.Factory.MechanicalPower;
using static Mafi.Base.Assets.Base.Machines.MetalWorks;
using Mafi.Unity.UiFramework.Components;
using UnityEngine;
using Mafi.Base.Prototypes.World;
using Mafi.Core.Economy;
using Mafi.Core.Game;
using Mafi.Core.Maintenance;
using Mafi.Core.Population;
using Mafi.Base.Prototypes.Buildings;
using Mafi.Core.Buildings.Cargo;
using System.Linq;
using System.Xml.Linq;
using Mafi.Core.Localization.Quantity;

namespace FluidChanges
{
    public class FluidChangePH
    {
        public static readonly FluidChangePH Instance = new FluidChangePH();
        private ProtoRegistrator registrator;
        
        //private RandomProvider randomProvider;
        //private EntitiesManager m_entitiesManager;
        //private ContractsManager m_contractManager;

        public void StartUp(ProtoRegistrator registrator)
        {
            this.registrator = registrator;
            

            bool allowStoreAllFluids = ModCFG.allowStoreAllFluids;
            bool addT5StorageTier = ModCFG.addT5StorageTier;

            if (allowStoreAllFluids) { MakeFluidsStorable(); }
            if (addT5StorageTier) { AddTier5Storages(); AddMegaStorageResearchData(); }


            CreateNewProducts();
            //AddNewSettlements();

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
            AddTitaniumRelatedStuff();
            AddTitaniumResearches();
            
            AddCargoShipRecipe();
            AddCargoShipResearch();
            AddDryDock();


            //AddDryDockOld();
            // Vertical Farming
            CreateNewWorldEntities();
            // High Rise Housing / Luxury Housing
            
            // Nanotechnology: Nano computing relies on advances in nanotechnology, which enables the manipulation and control of individual molecules and atoms to perform computational tasks.

            // Nuclear Reclamation
            // Settling Tank 2, Reprocess Fission Product, High grade uranium, more core fuel recipes
            // Acid Icon
            // Plastic Recycling


            //var instance = new NewMapGenerator(m_protosDb,randomProvider);
            //instance.CreateWorldMap();
        }

        public void CreateNewWorldEntities()
        {
            ProtosDb protosDb = registrator.PrototypesDb;
            GameDifficultyConfig gameDifficultyConfig = registrator.GetConfigOrThrow<GameDifficultyConfig>();
            // Initialize all the variables    
            WorldMapEntitiesData newMapEntities = new WorldMapEntitiesData();
            ProductProto oil = protosDb.GetOrThrow<ProductProto>(Ids.Products.CrudeOil);
            ProductProto orThrow = protosDb.GetOrThrow<ProductProto>(Ids.Products.Iron);
            ProductProto orThrow2 = protosDb.GetOrThrow<ProductProto>(Ids.Products.Steel);
            ProductProto cp2 = protosDb.GetOrThrow<ProductProto>(Ids.Products.ConstructionParts2);
            ProductProto cp3 = protosDb.GetOrThrow<ProductProto>(Ids.Products.ConstructionParts3);
            ProductProto cp4 = protosDb.GetOrThrow<ProductProto>(Ids.Products.ConstructionParts4);
            ProductProto orThrow3 = protosDb.GetOrThrow<ProductProto>(Ids.Products.Copper);
            VirtualProductProto maintenanceT1 = protosDb.GetOrThrow<VirtualProductProto>(Ids.Products.MaintenanceT1);
            VirtualProductProto maintenanceT2 = protosDb.GetOrThrow<VirtualProductProto>(Ids.Products.MaintenanceT2);

            Proto.ID id2 = new Proto.ID("UpointsStatsCat_WorldMapMines_COIE");
            UpointsStatsCategoryProto upointsStatsCategoryProto = registrator.PrototypesDb.Add(new UpointsStatsCategoryProto(id2, Proto.CreateStr(id2, "World Map Mines (COIE)", ""), new UpointsStatsCategoryProto.Gfx("Assets/Base/Icons/WorldMap/OilRig.svg")));
            UpointsCategoryProto oilRigUpointsCategory = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewWIDs.COIE_OilRigCost1, "Assets/Base/Icons/WorldMap/OilRig.svg", upointsStatsCategoryProto));

            Proto.Str oilRigStr = Proto.CreateStr(NewWIDs.COIE_OilRigCost1, "Oil rig", "This station provides crude oil when assigned with workers.");

            GameDifficultyConfig configOrThrow = registrator.GetConfigOrThrow<GameDifficultyConfig>();

            addOilRig(NewWIDs.COIE_OilRigCost1, new AssetValue(cp2.WithQuantity(120)), 1040000);
            addOilRig(NewWIDs.COIE_OilRigCost2, new AssetValue(cp2.WithQuantity(240)), 1040000);
            addOilRig(NewWIDs.COIE_OilRigCost3, new AssetValue(cp2.WithQuantity(240)), 780000);


            UpointsCategoryProto upointsCategory = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewWIDs.COIE_WaterWell, "Assets/Base/Icons/WorldMap/WaterWell.svg", upointsStatsCategoryProto));
            ProductProto orThrow4 = protosDb.GetOrThrow<ProductProto>(Ids.Products.Water);
            protosDb.Add(new WorldMapMineProto(NewWIDs.COIE_WaterWell, Proto.CreateStr(NewWIDs.COIE_WaterWell, "Groundwater well", "This station can extract fresh water."), new ProductQuantity(orThrow4, 8.Quantity()), 10.Seconds(), 0.2.Upoints(), upointsCategory, new EntityCosts(new AssetValue(cp3.WithQuantity(100).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)), 8), waterWellCostFunc, 8, new WorldMapEntityProto.Gfx("Assets/Unity/UserInterface/WorldMap/PumpjackBig.svg", "Assets/Base/Icons/WorldMap/WaterWell.svg")));
            LocStr1 locStr = Loc.Str1("WorldMine__Desc", "This site mines {0} when assigned with workers.", "description of a world mine, example use: This site mines Coal when assigned with workers.");


            UpointsCategoryProto upointsCategory2 = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewWIDs.COIE_SulfurMine, "Assets/Base/Icons/WorldMap/Sulfur.svg", upointsStatsCategoryProto));
            ProductProto orThrow5 = protosDb.GetOrThrow<ProductProto>(Ids.Products.Sulfur);
            protosDb.Add(new WorldMapMineProto(NewWIDs.COIE_SulfurMine, Proto.CreateStr(NewWIDs.COIE_SulfurMine, "Sulfur mine", LocalizationManager.CreateAlreadyLocalizedFormatted(NewWIDs.COIE_SulfurMine.Value, locStr.Format(orThrow5.Strings.Name))), new ProductQuantity(orThrow5, 6.Quantity()), 20.Seconds(), 0.2.Upoints(), upointsCategory2, new EntityCosts(new AssetValue(cp2.WithQuantity(200).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)), 8), sulfurMineCostFunc, 8, new WorldMapEntityProto.Gfx("Assets/Base/Products/Icons/Sulfur.svg", "Assets/Base/Icons/WorldMap/Sulfur.svg")));
            UpointsCategoryProto upointsCategoryProto = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewWIDs.COIE_CoalMine, "Assets/Base/Icons/WorldMap/Sulfur.svg", upointsStatsCategoryProto));
            ProductProto orThrow6 = protosDb.GetOrThrow<ProductProto>(Ids.Products.Coal);
            protosDb.Add(new WorldMapMineProto(NewWIDs.COIE_CoalMine, Proto.CreateStr(NewWIDs.COIE_CoalMine, "Coal mine", LocalizationManager.CreateAlreadyLocalizedFormatted(NewWIDs.COIE_CoalMine.Value, locStr.Format(orThrow6.Strings.Name))), new ProductQuantity(orThrow6, 9.Quantity()), 20.Seconds(), 0.5.Upoints(), upointsCategoryProto, new EntityCosts(new AssetValue(cp3.WithQuantity(200).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)), 8), coalMineCostFunc, 20, quantityAvailable: withDifficulty(450000.Quantity()), graphics: new WorldMapEntityProto.Gfx("Assets/Base/Products/Icons/Coal.svg", "Assets/Base/Icons/WorldMap/Coal.svg")));
            UpointsCategoryProto upointsCategoryProto2 = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewWIDs.COIE_QuartzMine, "Assets/Base/Icons/WorldMap/Quartz.svg", upointsStatsCategoryProto));

            ProductProto orThrow7 = protosDb.GetOrThrow<ProductProto>(Ids.Products.Quartz);
            protosDb.Add(new WorldMapMineProto(NewWIDs.COIE_QuartzMine, Proto.CreateStr(NewWIDs.COIE_QuartzMine, "Quartz mine", LocalizationManager.CreateAlreadyLocalizedFormatted(NewWIDs.COIE_QuartzMine.Value, locStr.Format(orThrow7.Strings.Name))), new ProductQuantity(orThrow7, 8.Quantity()), 20.Seconds(), 0.4.Upoints(), upointsCategoryProto2, new EntityCosts(new AssetValue(cp3.WithQuantity(300).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)), 8), quartzMineCostFunc, 20, quantityAvailable: withDifficulty(1000000.Quantity()), graphics: new WorldMapEntityProto.Gfx("Assets/Base/Products/Icons/Quartz.svg", "Assets/Base/Icons/WorldMap/Quartz.svg")));
            UpointsCategoryProto upointsCategoryProto3 = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewWIDs.COIE_UraniumMine, "Assets/Base/Icons/WorldMap/Uranium.svg", upointsStatsCategoryProto));
            ProductProto orThrow8 = protosDb.GetOrThrow<ProductProto>(Ids.Products.UraniumOre);
            protosDb.Add(new WorldMapMineProto(NewWIDs.COIE_UraniumMine, Proto.CreateStr(NewWIDs.COIE_UraniumMine, "Uranium mine", LocalizationManager.CreateAlreadyLocalizedFormatted(NewWIDs.COIE_UraniumMine.Value, locStr.Format(orThrow8.Strings.Name))), new ProductQuantity(orThrow8, 3.Quantity()), 20.Seconds(), 0.4.Upoints(), upointsCategoryProto3, new EntityCosts(new AssetValue(cp3.WithQuantity(400)).ScaledBy(gameDifficultyConfig.ConstructionCostsMult), 8), uraniumMineCostFunc, 20, quantityAvailable: withDifficulty(400000.Quantity()), graphics: new WorldMapEntityProto.Gfx("Assets/Base/Products/Icons/Uranium.svg", "Assets/Base/Icons/WorldMap/Uranium.svg")));
            UpointsCategoryProto upointsCategory6 = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewWIDs.COIE_RockMine, "Assets/Base/Icons/WorldMap/Bauxite.svg", upointsStatsCategoryProto));
            ProductProto orThrow9 = protosDb.GetOrThrow<ProductProto>(Ids.Products.Rock);
            protosDb.Add(new WorldMapMineProto(NewWIDs.COIE_RockMine, Proto.CreateStr(NewWIDs.COIE_RockMine, "Rock mine", LocalizationManager.CreateAlreadyLocalizedFormatted(NewWIDs.COIE_RockMine.Value, locStr.Format(orThrow9.Strings.Name))), new ProductQuantity(orThrow9, 6.Quantity()), 20.Seconds(), 0.2.Upoints(), upointsCategory6, new EntityCosts(new AssetValue(cp3.WithQuantity(200).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)), 8), rockMineCostFunc, 16, new WorldMapEntityProto.Gfx("Assets/Base/Products/Icons/Rock.svg", "Assets/Base/Icons/WorldMap/Rock.svg")));
            UpointsCategoryProto upointsCategoryProto4 = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewWIDs.COIE_LimestoneMine, "Assets/Base/Icons/WorldMap/Bauxite.svg", upointsStatsCategoryProto));

            ProductProto orThrow10 = protosDb.GetOrThrow<ProductProto>(Ids.Products.Limestone);
            protosDb.Add(new WorldMapMineProto(NewWIDs.COIE_LimestoneMine, Proto.CreateStr(NewWIDs.COIE_LimestoneMine, "Limestone quarry", LocalizationManager.CreateAlreadyLocalizedFormatted(NewWIDs.COIE_LimestoneMine.Value, locStr.Format(orThrow10.Strings.Name))), new ProductQuantity(orThrow10, 6.Quantity()), 20.Seconds(), 0.4.Upoints(), upointsCategoryProto4, new EntityCosts(new AssetValue(cp3.WithQuantity(200).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)), 8), limestoneMineCostFunc, 16, quantityAvailable: withDifficulty(400000.Quantity()), graphics: new WorldMapEntityProto.Gfx("Assets/Base/Products/Icons/Limestone.svg", "Assets/Base/Icons/WorldMap/Limestone.svg")));


            UpointsCategoryProto upointsCategoryProto69 = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewWIDs.COIE_IlmeniteMine, "Assets/COIExtended/Icons/IlmeniteOre.png", upointsStatsCategoryProto));
            ProductProto orThrow69 = protosDb.GetOrThrow<ProductProto>(NewIDs.IlmeniteOre);
            protosDb.Add(new WorldMapMineProto(NewWIDs.COIE_IlmeniteMine, Proto.CreateStr(NewWIDs.COIE_IlmeniteMine, "Ilmenite Mine", LocalizationManager.CreateAlreadyLocalizedFormatted(NewWIDs.COIE_IlmeniteMine.Value, locStr.Format(orThrow69.Strings.Name))), new ProductQuantity(orThrow69, 6.Quantity()), 20.Seconds(), 0.4.Upoints(), upointsCategoryProto69, new EntityCosts(new AssetValue(cp4.WithQuantity(200).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)), 8), IlmeniteMineCostFunc, 16, quantityAvailable: withDifficulty(400000.Quantity()), graphics: new WorldMapEntityProto.Gfx("Assets/COIExtended/Icons/IlmeniteOre.png", "Assets/COIExtended/Icons/IlmeniteOre.png")));

            Proto.Str settlementStr = Proto.CreateStr(new Proto.ID("SettlementSmall1"), "Settlement");
            addSettlement(NewWIDs.COIE_Settlement1, 100.Percent(), ImmutableArray.Create(createTrade(Ids.Products.Wood, 20, Ids.Products.Bricks, 20, 1), createTrade(Ids.Products.Wood, 40, Ids.Products.ConcreteSlab, 40, 1), createTrade(Ids.Products.Wood, 40, Ids.Products.IronScrap, 48, 1), createTrade(Ids.Products.ConstructionParts, 20, Ids.Products.Potato, 20, 1), createTrade(Ids.Products.ConstructionParts, 40, Ids.Products.Copper, 40, 1), createTrade(Ids.Products.ConstructionParts, 40, Ids.Products.Rubber, 40, 1), createTrade(Ids.Products.Diesel, 60, Ids.Products.Coal, 80, 1), createTrade(Ids.Products.Coal, 80, Ids.Products.Diesel, 40, 1)), ImmutableArray<ContractProto>.Empty, 1);
            addSettlement(NewWIDs.COIE_Settlement2, 100.Percent(), ImmutableArray.Create(createTrade(Ids.Products.ConstructionParts2, 10, Ids.Products.Diesel, 40, 0), createTrade(Ids.Products.Rubber, 20, Ids.Products.Wood, 40, 0), createTrade(Ids.Products.ConstructionParts, 20, Ids.Products.Coal, 60, 0), createTrade(Ids.Products.ConstructionParts3, 20, Ids.Products.Glass, 40, 1), createTrade(Ids.Products.ConstructionParts2, 20, Ids.Products.Chicken, 30, 2, 16, ignoreTradeMultipliers: true)), ImmutableArray.Create(createContract(Ids.Products.Cement, 10, Ids.Products.Coal, 43, 0.11, 0.2, 2), createContract(Ids.Products.HouseholdGoods, 10, Ids.Products.Coal, 28, 0.1, 0.2, 2), createContract(Ids.Products.Rubber, 10, Ids.Products.Wood, 5, 0.11, 0.2, 2), createContract(Ids.Products.HouseholdAppliances, 10, Ids.Products.Wood, 77, 0.11, 0.2, 3), createContract(Ids.Products.LabEquipment2, 10, Ids.Products.CopperOre, 49, 0.13, 0.2, 2)));
            addSettlement(NewWIDs.COIE_Settlement3, 120.Percent(), ImmutableArray.Create(createTrade(Ids.Products.Diesel, 100, Ids.Products.Bread, 30, 1), createTrade(Ids.Products.Coal, 20, Ids.Products.Sulfur, 40, 1), createTrade(Ids.Products.Electronics, 60, Ids.Products.IronOre, 40, 1), createTrade(Ids.Products.ConstructionParts3, 80, Ids.Products.Electronics2, 40, 2), createTrade(Ids.Products.ConstructionParts3, 60, Ids.Products.MedicalSupplies, 20, 2)), ImmutableArray.Create(createContract(Ids.Products.ConstructionParts2, 10, Ids.Products.Limestone, 90, 0.12, 0.2, 2), createContract(Ids.Products.VehicleParts2, 10, Ids.Products.IronOre, 49, 0.11, 0.3, 2), createContract(Ids.Products.HouseholdAppliances, 10, Ids.Products.CopperOre, 54, 0.13, 0.3, 2), createContract(Ids.Products.Slag, 540, Ids.Products.SourWater, 218, 0.2, 0.4, 3)));
            addSettlement(NewWIDs.COIE_Settlement4, 150.Percent(), ImmutableArray.Create(createTrade(Ids.Products.Gold, 80, Ids.Products.Microchips, 20, 2)), ImmutableArray.Create(createContract(Ids.Products.Diesel, 100, Ids.Products.Gold, 8, 0.16, 0.4, 3), createContract(Ids.Products.Coal, 120, Ids.Products.Quartz, 120, 0.14, 0.3, 2), createContract(Ids.Products.VehicleParts2, 10, Ids.Products.Quartz, 49, 0.14, 0.3, 2), createContract(Ids.Products.Sulfur, 10, Ids.Products.Sludge, 45, 0.1, 0.1, 2)));
            addSettlement(NewWIDs.COIE_SettlementForFuel, 150.Percent(), ImmutableArray.Empty, ImmutableArray.Create(createContract(Ids.Products.FoodPack, 48, Ids.Products.CrudeOil, 200, 0.15, 0.3, 1), createContract(Ids.Products.VehicleParts2, 10, Ids.Products.CrudeOil, 110, 0.15, 0.3, 2), createContract(Ids.Products.Gold, 10, Ids.Products.CrudeOil, 189, 0.17, 0.4, 3), createContract(Ids.Products.ConsumerElectronics, 10, Ids.Products.CrudeOil, 347, 0.17, 0.4, 3)), 0, popsAdoptionEnabled: false);
            addSettlement(NewWIDs.COIE_SettlementForUranium, 150.Percent(), ImmutableArray.Empty, ImmutableArray.Create(createContract(Ids.Products.FoodPack, 48, Ids.Products.UraniumOre, 16, 0.3, 0.3, 1), createContract(Ids.Products.Gold, 10, Ids.Products.UraniumOre, 16, 0.4, 0.4, 2), createContract(Ids.Products.LabEquipment4, 10, Ids.Products.UraniumOre, 58, 0.4, 0.4, 3)), 0, popsAdoptionEnabled: false);
            addSettlement(NewWIDs.COIE_Settlement5, 200.Percent(), ImmutableArray.Create(createTrade(Ids.Products.Electronics, 40, Ids.Products.Quartz, 40, 1)), ImmutableArray.Create(createContract(Ids.Products.Server, 10, Ids.Products.IronOre, 386, 0.12, 0.3, 1), createContract(Ids.Products.MedicalSupplies3, 10, Ids.Products.CopperOre, 36, 0.12, 0.2, 2), createContract(Ids.Products.LabEquipment3, 10, Ids.Products.Coal, 216, 0.12, 0.2, 2), createContract(Ids.Products.SolarCell, 10, Ids.Products.Quartz, 41, 0.12, 0.3, 2), createContract(Ids.Products.ConsumerElectronics, 10, Ids.Products.Quartz, 216, 0.12, 0.3, 3), createContract(Ids.Products.Server, 10, Ids.Products.GoldOre, 386, 0.12, 0.3, 3)), 0, popsAdoptionEnabled: false);

            addSettlement(NewIDs.SettlementForTitanium, 200.Percent(), ImmutableArray.Create(createTrade(Ids.Products.Microchips, 40, Ids.Products.ConstructionParts4, 40, 1)), ImmutableArray.Create(createContract(Ids.Products.ConstructionParts4, 10, NewIDs.IlmeniteOre, 386, 0.12, 0.3, 1), createContract(Ids.Products.Water, 10, Ids.Products.SourWater, 30, 0.12, 0.2, 2), createContract(Ids.Products.LabEquipment3, 10, Ids.Products.ConstructionParts4, 30, 0.12, 0.2, 2), createContract(Ids.Products.ConstructionParts4, 5, Ids.Products.Rock, 41, 0.12, 0.3, 2), createContract(NewIDs.NitricAcidProduct, 20, Ids.Products.FertilizerChemical2, 216, 0.12, 0.3, 3), createContract(Ids.Products.RetiredWaste, 10, Ids.Products.UraniumOre, 100, 0.12, 0.3, 3)), 0, popsAdoptionEnabled: false);


            ProductProto orThrow11 = protosDb.GetOrThrow<ProductProto>(Ids.Products.CargoShip);
            ProductProto orThrow12 = protosDb.GetOrThrow<ProductProto>(Ids.Products.ConstructionParts3);
            Quantity quantity = 600.Quantity();


            Proto.Str cargoShipStr = Proto.CreateStr(NewWIDs.CargoBuildProgressWreckCost1, "Damaged cargo ship");
            addShipWreck(NewWIDs.CargoBuildProgressWreckCost1, new AssetValue(orThrow.WithQuantity(240)));
            addShipWreck(NewWIDs.CargoBuildProgressWreckCost2, new AssetValue(orThrow2.WithQuantity(300), orThrow3.WithQuantity(200)));
            WorldMapMineProto addOilRig(EntityProto.ID id, AssetValue costToFix, int quantityAvailable)
            {
                return protosDb.Add(new WorldMapMineProto(id, oilRigStr, new ProductQuantity(oil, 9.Quantity()), 20.Seconds(), 0.4.Upoints(), oilRigUpointsCategory, new EntityCosts(costToFix.ScaledBy(gameDifficultyConfig.ConstructionCostsMult), 8), oilRigCostFunc, 16, quantityAvailable: withDifficulty(quantityAvailable.Quantity()), graphics: new WorldMapEntityProto.Gfx("Assets/Unity/UserInterface/WorldMap/OilRigBig.svg", "Assets/Base/Icons/WorldMap/OilRig.svg")));
            }

            void addSettlement(EntityProto.ID id, Percent befriendCostMult, ImmutableArray<QuickTradePairProto> quickTrades, ImmutableArray<ContractProto> contracts, int startingReputation = 0, bool popsAdoptionEnabled = true)
            {

                protosDb.Add(new WorldMapVillageProto(id, settlementStr, popsAdoptionEnabled ? 1 : (-1), startingReputation, costPerLevel: costPerReputation, upointsPerPopToAdopt: 0.25.Upoints(), quickTrades: quickTrades, contracts: contracts, graphics: new WorldMapEntityProto.Gfx("Assets/Unity/UserInterface/WorldMap/VillageBig.svg", "Assets/Unity/UserInterface/WorldMap/Village.svg")));
                AssetValue costPerReputation(int level)
                {
                    if (level <= 1)
                    {
                        return new AssetValue(cp2.WithQuantity(80)).ScaledBy(befriendCostMult);
                    }

                    return level switch
                    {
                        2 => new AssetValue(cp2.WithQuantity(120)).ScaledBy(befriendCostMult),
                        3 => new AssetValue(cp3.WithQuantity(100)).ScaledBy(befriendCostMult),
                        _ => new AssetValue(cp3.WithQuantity(140)).ScaledBy(befriendCostMult),
                    };
                }

            }

            void addShipWreck(EntityProto.ID id, AssetValue costToFix)
            {
                protosDb.Add(new WorldMapCargoShipWreckProto(id, cargoShipStr, costToFix, new WorldMapEntityProto.Gfx("Assets/Unity/UserInterface/WorldMap/CargoShipStoryIcon256.png", "Assets/Unity/UserInterface/Toolbar/CargoShip.svg")));
            }

            EntityCosts coalMineCostFunc(int level)
            {
                AssetValue price5 = new AssetValue(cp3.WithQuantity(100 + (level - 1).Max(0) * 100).ScaledBy(gameDifficultyConfig.ConstructionCostsMult));
                return new EntityCosts(price5, 9, level * 25, new MaintenanceCosts(maintenanceT1, level * 12.Quantity()));
            }

            ContractProto createContract(ProductProto.ID productToPayWith, int quantityToPayWith, ProductProto.ID productToBuy, int quantityToBuy, double per100Quantity, double perMonth, int minReputation)
            {
                Quantity quantity2 = quantityToBuy.ScaledByRounded(gameDifficultyConfig.ContractsProfitMult).Quantity();
                return protosDb.Add(new ContractProto(new Proto.ID("Contract_" + productToBuy.Value + "_For_" + productToPayWith.Value + "_COIE"), protosDb.GetOrThrow<ProductProto>(productToBuy).WithQuantity(quantity2), protosDb.GetOrThrow<ProductProto>(productToPayWith).WithQuantity(quantityToPayWith), perMonth.Upoints(), per100Quantity.Upoints(), minReputation));
            }

            QuickTradePairProto createTrade(ProductProto.ID productToPayWith, int quantityToPayWith, ProductProto.ID productToBuy, int quantityToBuy, int minReputation, int maxSteps = 16, bool ignoreTradeMultipliers = false)
            {
                return protosDb.Add(new QuickTradePairProto(new Proto.ID("Trade_" + productToBuy.Value + "_For_" + productToPayWith.Value + "_COIE"), protosDb.GetOrThrow<ProductProto>(productToBuy).WithQuantity(quantityToBuy), protosDb.GetOrThrow<ProductProto>(productToPayWith).WithQuantity(quantityToPayWith), 0.5.Upoints(), maxSteps, minReputation, 2, 5.Minutes(), 125.Percent(), 100.Percent(), ignoreTradeMultipliers));
            }

            EntityCosts limestoneMineCostFunc(int level)
            {
                AssetValue price = new AssetValue(cp3.WithQuantity(100 + (level - 1).Max(0) * 60).ScaledBy(gameDifficultyConfig.ConstructionCostsMult));
                return new EntityCosts(price, 9, level * 25, new MaintenanceCosts(maintenanceT1, level * 10.Quantity()));
            }

            EntityCosts IlmeniteMineCostFunc(int level)
            {
                AssetValue price = new AssetValue(cp4.WithQuantity(100 + (level - 1).Max(0) * 60).ScaledBy(gameDifficultyConfig.ConstructionCostsMult));
                return new EntityCosts(price, 12, level * 30, new MaintenanceCosts(maintenanceT2, level * 10.Quantity()));
            }

            EntityCosts oilRigCostFunc(int level)
            {
                AssetValue price8 = ((level > 4) ? new AssetValue(cp3.WithQuantity(100 + (level - 3) * 100).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)) : new AssetValue(cp2.WithQuantity(200).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)));
                return new EntityCosts(price8, 9, level * 18, new MaintenanceCosts(maintenanceT1, level * 8.Quantity()));
            }

            EntityCosts quartzMineCostFunc(int level)
            {
                AssetValue price4 = new AssetValue(cp3.WithQuantity(120 + (level - 1).Max(0) * 60).ScaledBy(gameDifficultyConfig.ConstructionCostsMult));
                return new EntityCosts(price4, 9, level * 25, new MaintenanceCosts(maintenanceT1, level * 12.Quantity()));
            }

            EntityCosts rockMineCostFunc(int level)
            {
                AssetValue price2 = new AssetValue(cp3.WithQuantity(100 + (level - 1).Max(0) * 60).ScaledBy(gameDifficultyConfig.ConstructionCostsMult));
                return new EntityCosts(price2, 9, level * 25, new MaintenanceCosts(maintenanceT1, level * 8.Quantity()));
            }

            EntityCosts sulfurMineCostFunc(int level)
            {
                AssetValue price6 = new AssetValue(cp3.WithQuantity(80 + (level - 1).Max(0) * 80).ScaledBy(gameDifficultyConfig.ConstructionCostsMult));
                return new EntityCosts(price6, 9, level * 12, new MaintenanceCosts(maintenanceT1, level * 10.Quantity()));
            }

            EntityCosts uraniumMineCostFunc(int level)
            {
                AssetValue price3 = new AssetValue(cp3.WithQuantity(200 + (level - 1).Max(0) * 80).ScaledBy(gameDifficultyConfig.ConstructionCostsMult));
                return new EntityCosts(price3, 9, level * 25, new MaintenanceCosts(maintenanceT1, level * 10.Quantity()));
            }

            EntityCosts waterWellCostFunc(int level)
            {
                AssetValue price7 = new AssetValue(cp3.WithQuantity(80 + (level - 1).Max(0) * 40).ScaledBy(gameDifficultyConfig.ConstructionCostsMult));
                return new EntityCosts(price7, 9, level * 16, new MaintenanceCosts(maintenanceT1, level * 4.Quantity()));
            }

            Quantity? withDifficulty(Quantity depositSize)
            {
                if (gameDifficultyConfig.WorldMinesReservesMult.HasValue)
                {
                    return depositSize.ScaledBy(gameDifficultyConfig.WorldMinesReservesMult.Value);
                }

                return null;
            }

            //ProductProto orThrow = protosDb.GetOrThrow<ProductProto>(Ids.Products.Iron);
            Debug.Log("Done setting up entities");


        }



  

    }
}