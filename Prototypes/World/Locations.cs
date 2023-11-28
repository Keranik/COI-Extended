using Mafi.Base.Prototypes.World;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Economy;
using Mafi.Core.Entities;
using Mafi.Core.Game;
using Mafi.Core.Maintenance;
using Mafi.Core.Population;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Core.World.Contracts;
using Mafi.Core.World.Entities;
using Mafi.Core.World.QuickTrade;
using Mafi.Core;
using Mafi.Localization;
using Mafi;
using Mafi.Core.Mods;

namespace COIExtended.Prototypes.World;

internal class Locations : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
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

        Mafi.Core.Prototypes.Proto.ID id2 = new Mafi.Core.Prototypes.Proto.ID("UpointsStatsCat_WorldMapMines_COIE");
        UpointsStatsCategoryProto upointsStatsCategoryProto = registrator.PrototypesDb.Add(new UpointsStatsCategoryProto(id2, Mafi.Core.Prototypes.Proto.CreateStr(id2, "World Map Mines (COIE)", ""), new UpointsStatsCategoryProto.Gfx("Assets/Base/Icons/WorldMap/OilRig.svg")));
        UpointsCategoryProto oilRigUpointsCategory = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewIDs.World.OilRigCost1, "Assets/Base/Icons/WorldMap/OilRig.svg", upointsStatsCategoryProto));

        Mafi.Core.Prototypes.Proto.Str oilRigStr = Mafi.Core.Prototypes.Proto.CreateStr(NewIDs.World.OilRigCost1, "Oil rig", "This station provides crude oil when assigned with workers.");

        GameDifficultyConfig configOrThrow = registrator.GetConfigOrThrow<GameDifficultyConfig>();

        addOilRig(NewIDs.World.OilRigCost1, new AssetValue(cp2.WithQuantity(120)), 1040000);
        addOilRig(NewIDs.World.OilRigCost2, new AssetValue(cp2.WithQuantity(240)), 1040000);
        addOilRig(NewIDs.World.OilRigCost3, new AssetValue(cp2.WithQuantity(240)), 780000);


        UpointsCategoryProto upointsCategory = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewIDs.World.WaterWell, "Assets/Base/Icons/WorldMap/WaterWell.svg", upointsStatsCategoryProto));
        ProductProto orThrow4 = protosDb.GetOrThrow<ProductProto>(Ids.Products.Water);
        protosDb.Add(new WorldMapMineProto(NewIDs.World.WaterWell, Mafi.Core.Prototypes.Proto.CreateStr(NewIDs.World.WaterWell, "Groundwater well", "This station can extract fresh water."), new ProductQuantity(orThrow4, 8.Quantity()), 10.Seconds(), 0.2.Upoints(), upointsCategory, new EntityCosts(new AssetValue(cp3.WithQuantity(100).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)), 8), waterWellCostFunc, 8, new WorldMapEntityProto.Gfx("Assets/Unity/UserInterface/WorldMap/PumpjackBig.svg", "Assets/Base/Icons/WorldMap/WaterWell.svg")));
        LocStr1 locStr = Loc.Str1("WorldMine__Desc", "This site mines {0} when assigned with workers.", "description of a world mine, example use: This site mines Coal when assigned with workers.");


        UpointsCategoryProto upointsCategory2 = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewIDs.World.SulfurMine, "Assets/Base/Icons/WorldMap/Sulfur.svg", upointsStatsCategoryProto));
        ProductProto orThrow5 = protosDb.GetOrThrow<ProductProto>(Ids.Products.Sulfur);
        protosDb.Add(new WorldMapMineProto(NewIDs.World.SulfurMine, Mafi.Core.Prototypes.Proto.CreateStr(NewIDs.World.SulfurMine, "Sulfur mine", LocalizationManager.CreateAlreadyLocalizedFormatted(NewIDs.World.SulfurMine.Value, locStr.Format(orThrow5.Strings.Name))), new ProductQuantity(orThrow5, 6.Quantity()), 20.Seconds(), 0.2.Upoints(), upointsCategory2, new EntityCosts(new AssetValue(cp2.WithQuantity(200).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)), 8), sulfurMineCostFunc, 8, new WorldMapEntityProto.Gfx("Assets/Base/Products/Icons/Sulfur.svg", "Assets/Base/Icons/WorldMap/Sulfur.svg")));
        UpointsCategoryProto upointsCategoryProto = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewIDs.World.CoalMine, "Assets/Base/Icons/WorldMap/Sulfur.svg", upointsStatsCategoryProto));
        ProductProto orThrow6 = protosDb.GetOrThrow<ProductProto>(Ids.Products.Coal);
        protosDb.Add(new WorldMapMineProto(NewIDs.World.CoalMine, Mafi.Core.Prototypes.Proto.CreateStr(NewIDs.World.CoalMine, "Coal mine", LocalizationManager.CreateAlreadyLocalizedFormatted(NewIDs.World.CoalMine.Value, locStr.Format(orThrow6.Strings.Name))), new ProductQuantity(orThrow6, 9.Quantity()), 20.Seconds(), 0.5.Upoints(), upointsCategoryProto, new EntityCosts(new AssetValue(cp3.WithQuantity(200).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)), 8), coalMineCostFunc, 20, quantityAvailable: withDifficulty(450000.Quantity()), graphics: new WorldMapEntityProto.Gfx("Assets/Base/Products/Icons/Coal.svg", "Assets/Base/Icons/WorldMap/Coal.svg")));
        UpointsCategoryProto upointsCategoryProto2 = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewIDs.World.QuartzMine, "Assets/Base/Icons/WorldMap/Quartz.svg", upointsStatsCategoryProto));

        ProductProto orThrow7 = protosDb.GetOrThrow<ProductProto>(Ids.Products.Quartz);
        protosDb.Add(new WorldMapMineProto(NewIDs.World.QuartzMine, Mafi.Core.Prototypes.Proto.CreateStr(NewIDs.World.QuartzMine, "Quartz mine", LocalizationManager.CreateAlreadyLocalizedFormatted(NewIDs.World.QuartzMine.Value, locStr.Format(orThrow7.Strings.Name))), new ProductQuantity(orThrow7, 8.Quantity()), 20.Seconds(), 0.4.Upoints(), upointsCategoryProto2, new EntityCosts(new AssetValue(cp3.WithQuantity(300).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)), 8), quartzMineCostFunc, 20, quantityAvailable: withDifficulty(1000000.Quantity()), graphics: new WorldMapEntityProto.Gfx("Assets/Base/Products/Icons/Quartz.svg", "Assets/Base/Icons/WorldMap/Quartz.svg")));
        UpointsCategoryProto upointsCategoryProto3 = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewIDs.World.UraniumMine, "Assets/Base/Icons/WorldMap/Uranium.svg", upointsStatsCategoryProto));
        ProductProto orThrow8 = protosDb.GetOrThrow<ProductProto>(Ids.Products.UraniumOre);
        protosDb.Add(new WorldMapMineProto(NewIDs.World.UraniumMine, Mafi.Core.Prototypes.Proto.CreateStr(NewIDs.World.UraniumMine, "Uranium mine", LocalizationManager.CreateAlreadyLocalizedFormatted(NewIDs.World.UraniumMine.Value, locStr.Format(orThrow8.Strings.Name))), new ProductQuantity(orThrow8, 3.Quantity()), 20.Seconds(), 0.4.Upoints(), upointsCategoryProto3, new EntityCosts(new AssetValue(cp3.WithQuantity(400)).ScaledBy(gameDifficultyConfig.ConstructionCostsMult), 8), uraniumMineCostFunc, 20, quantityAvailable: withDifficulty(400000.Quantity()), graphics: new WorldMapEntityProto.Gfx("Assets/Base/Products/Icons/Uranium.svg", "Assets/Base/Icons/WorldMap/Uranium.svg")));
        UpointsCategoryProto upointsCategory6 = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewIDs.World.RockMine, "Assets/Base/Icons/WorldMap/Bauxite.svg", upointsStatsCategoryProto));
        ProductProto orThrow9 = protosDb.GetOrThrow<ProductProto>(Ids.Products.Rock);
        protosDb.Add(new WorldMapMineProto(NewIDs.World.RockMine, Mafi.Core.Prototypes.Proto.CreateStr(NewIDs.World.RockMine, "Rock mine", LocalizationManager.CreateAlreadyLocalizedFormatted(NewIDs.World.RockMine.Value, locStr.Format(orThrow9.Strings.Name))), new ProductQuantity(orThrow9, 6.Quantity()), 20.Seconds(), 0.2.Upoints(), upointsCategory6, new EntityCosts(new AssetValue(cp3.WithQuantity(200).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)), 8), rockMineCostFunc, 16, new WorldMapEntityProto.Gfx("Assets/Base/Products/Icons/Rock.svg", "Assets/Base/Icons/WorldMap/Rock.svg")));
        UpointsCategoryProto upointsCategoryProto4 = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewIDs.World.LimestoneMine, "Assets/Base/Icons/WorldMap/Bauxite.svg", upointsStatsCategoryProto));
        ProductProto orThrow10 = protosDb.GetOrThrow<ProductProto>(Ids.Products.Limestone);
        protosDb.Add(new WorldMapMineProto(NewIDs.World.LimestoneMine, Mafi.Core.Prototypes.Proto.CreateStr(NewIDs.World.LimestoneMine, "Limestone quarry", LocalizationManager.CreateAlreadyLocalizedFormatted(NewIDs.World.LimestoneMine.Value, locStr.Format(orThrow10.Strings.Name))), new ProductQuantity(orThrow10, 6.Quantity()), 20.Seconds(), 0.4.Upoints(), upointsCategoryProto4, new EntityCosts(new AssetValue(cp3.WithQuantity(200).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)), 8), limestoneMineCostFunc, 16, quantityAvailable: withDifficulty(400000.Quantity()), graphics: new WorldMapEntityProto.Gfx("Assets/Base/Products/Icons/Limestone.svg", "Assets/Base/Icons/WorldMap/Limestone.svg")));
        UpointsCategoryProto upointsCategoryProto69 = registrator.PrototypesDb.Add(new UpointsCategoryProto(NewIDs.World.IlmeniteMine, "Assets/COIExtended/Icons/IlmeniteOre.png", upointsStatsCategoryProto));
        ProductProto orThrow69 = protosDb.GetOrThrow<ProductProto>(NewIDs.Products.IlmeniteOre);
        protosDb.Add(new WorldMapMineProto(NewIDs.World.IlmeniteMine, Mafi.Core.Prototypes.Proto.CreateStr(NewIDs.World.IlmeniteMine, "Ilmenite Mine", LocalizationManager.CreateAlreadyLocalizedFormatted(NewIDs.World.IlmeniteMine.Value, locStr.Format(orThrow69.Strings.Name))), new ProductQuantity(orThrow69, 6.Quantity()), 20.Seconds(), 0.4.Upoints(), upointsCategoryProto69, new EntityCosts(new AssetValue(cp4.WithQuantity(200).ScaledBy(gameDifficultyConfig.ConstructionCostsMult)), 8), IlmeniteMineCostFunc, 16, quantityAvailable: withDifficulty(400000.Quantity()), graphics: new WorldMapEntityProto.Gfx("Assets/COIExtended/Icons/IlmeniteOre.png", "Assets/COIExtended/Icons/IlmeniteOre.png")));

        Mafi.Core.Prototypes.Proto.Str settlementStr = Mafi.Core.Prototypes.Proto.CreateStr(new Mafi.Core.Prototypes.Proto.ID("SettlementSmall1"), "Settlement");
        // Adding a new settlement (Settlement1)
        addSettlement(
            NewIDs.World.Settlement1, // ID of the settlement
            100.Percent(), // Befriend cost multiplier

            // Quick trades available in this settlement
            ImmutableArray.Create(
                createTrade(Ids.Products.Wood, 20, Ids.Products.Bricks, 20, 1),
                createTrade(Ids.Products.Wood, 40, Ids.Products.ConcreteSlab, 40, 1),
                createTrade(Ids.Products.Wood, 40, Ids.Products.IronScrap, 48, 1),
                createTrade(Ids.Products.ConstructionParts, 20, Ids.Products.Potato, 20, 1),
                createTrade(Ids.Products.ConstructionParts, 40, Ids.Products.Copper, 40, 1),
                createTrade(Ids.Products.ConstructionParts, 40, Ids.Products.Rubber, 40, 1),
                createTrade(Ids.Products.Diesel, 60, Ids.Products.Coal, 80, 1),
                createTrade(Ids.Products.Coal, 80, Ids.Products.Diesel, 40, 1)
            ),

            // No contracts available in this settlement
            ImmutableArray<ContractProto>.Empty,

            1 // Starting reputation for the settlement
        );

        // Adding a new settlement (Settlement2)
        addSettlement(
            NewIDs.World.Settlement2, // ID of the settlement
            100.Percent(), // Befriend cost multiplier

            // Quick trades available in this settlement
            ImmutableArray.Create(
                createTrade(Ids.Products.ConstructionParts2, 10, Ids.Products.Diesel, 40, 0),
                createTrade(Ids.Products.Rubber, 20, Ids.Products.Wood, 40, 0),
                createTrade(Ids.Products.ConstructionParts, 20, Ids.Products.Coal, 60, 0),
                createTrade(Ids.Products.ConstructionParts3, 20, Ids.Products.Glass, 40, 1),
                createTrade(Ids.Products.ConstructionParts2, 20, Ids.Products.Chicken, 30, 2, 16, ignoreTradeMultipliers: true)
            ),

            // Contracts available in this settlement, specifying products to pay with, quantities, and terms
            ImmutableArray.Create(
                createContract(Ids.Products.Cement, 10, Ids.Products.Coal, 43, 0.11, 0.2, 2),
                createContract(Ids.Products.HouseholdGoods, 10, Ids.Products.Coal, 28, 0.1, 0.2, 2),
                createContract(Ids.Products.Rubber, 10, Ids.Products.Wood, 5, 0.11, 0.2, 2),
                createContract(Ids.Products.HouseholdAppliances, 10, Ids.Products.Wood, 77, 0.11, 0.2, 3),
                createContract(Ids.Products.LabEquipment2, 10, Ids.Products.CopperOre, 49, 0.13, 0.2, 2)
            )
        );

        // Adding a new settlement (Settlement3)
        addSettlement(
            NewIDs.World.Settlement3, // ID of the settlement
            120.Percent(), // Befriend cost multiplier

            // Quick trades available in this settlement
            ImmutableArray.Create(
                createTrade(Ids.Products.Diesel, 100, Ids.Products.Bread, 30, 1),
                createTrade(Ids.Products.Coal, 20, Ids.Products.Sulfur, 40, 1),
                createTrade(Ids.Products.Electronics, 60, Ids.Products.IronOre, 40, 1),
                createTrade(Ids.Products.ConstructionParts3, 80, Ids.Products.Electronics2, 40, 2),
                createTrade(Ids.Products.ConstructionParts3, 60, Ids.Products.MedicalSupplies, 20, 2)
            ),

            // Contracts available in this settlement, specifying products to pay with, quantities, and terms
            ImmutableArray.Create(
                createContract(Ids.Products.ConstructionParts2, 10, Ids.Products.Limestone, 90, 0.12, 0.2, 2),
                createContract(Ids.Products.VehicleParts2, 10, Ids.Products.IronOre, 49, 0.11, 0.3, 2),
                createContract(Ids.Products.HouseholdAppliances, 10, Ids.Products.CopperOre, 54, 0.13, 0.3, 2),
                createContract(Ids.Products.Slag, 540, Ids.Products.SourWater, 218, 0.2, 0.4, 3)
            )
        );

        // Adding a new settlement (Settlement4)
        addSettlement(
            NewIDs.World.Settlement4, // ID of the settlement
            150.Percent(), // Befriend cost multiplier

            // Quick trades available in this settlement
            ImmutableArray.Create(
                createTrade(
                    Ids.Products.Gold, // Product to trade
                    80, // Quantity of product
                    Ids.Products.Microchips, // Trade for product
                    20, // Quantity of trade for product
                    2   // Tier
                )
            ),

            // Contracts available in this settlement, specifying products to pay with, quantities, and terms
            ImmutableArray.Create(
                createContract(Ids.Products.Diesel, 100, Ids.Products.Gold, 8, 0.16, 0.4, 3),
                createContract(Ids.Products.Coal, 120, Ids.Products.Quartz, 120, 0.14, 0.3, 2),
                createContract(Ids.Products.VehicleParts2, 10, Ids.Products.Quartz, 49, 0.14, 0.3, 2),
                createContract(Ids.Products.Sulfur, 10, Ids.Products.Sludge, 45, 0.1, 0.1, 2)
            )
        );

        // Adding a new settlement focused on Fuel
        addSettlement(
            NewIDs.World.SettlementForFuel, // ID of the settlement
            150.Percent(), // Befriend cost multiplier
            ImmutableArray.Empty, // No quick trades available in this settlement

            // Contracts available in this settlement, specifying products to pay with, quantities, and terms
            ImmutableArray.Create(
                createContract(Ids.Products.FoodPack, 48, Ids.Products.CrudeOil, 200, 0.15, 0.3, 1),
                createContract(Ids.Products.VehicleParts2, 10, Ids.Products.CrudeOil, 110, 0.15, 0.3, 2),
                createContract(Ids.Products.Gold, 10, Ids.Products.CrudeOil, 189, 0.17, 0.4, 3),
                createContract(Ids.Products.ConsumerElectronics, 10, Ids.Products.CrudeOil, 347, 0.17, 0.4, 3)
            ),

            0, // Starting reputation for the settlement
            popsAdoptionEnabled: false // Whether pops adoption is enabled
        );
        // Adding a new settlement focused on Uranium
        addSettlement(
            NewIDs.World.SettlementForUranium, // ID of the settlement
            150.Percent(), // Befriend cost multiplier
            ImmutableArray.Empty, // No quick trades available in this settlement

            // Contracts available in this settlement, specifying products to pay with, quantities, and terms
            ImmutableArray.Create(
                createContract(Ids.Products.FoodPack, 48, Ids.Products.UraniumOre, 16, 0.3, 0.3, 1),
                createContract(Ids.Products.Gold, 10, Ids.Products.UraniumOre, 16, 0.4, 0.4, 2),
                createContract(Ids.Products.LabEquipment4, 10, Ids.Products.UraniumOre, 58, 0.4, 0.4, 3)
            ),

            0, // Starting reputation for the settlement
            popsAdoptionEnabled: false // Whether pops adoption is enabled
        );
        // Adding a new settlement
        addSettlement(
            NewIDs.World.Settlement5, // ID of the settlement
            200.Percent(), // Befriend cost multiplier

            // Quick trades available in this settlement
            ImmutableArray.Create(
                createTrade(
                    Ids.Products.Electronics, // Product to trade
                    40, // Quantity of product
                    Ids.Products.Quartz, // Trade for product
                    40, // Quantity of trade for product
                    1   // Tier
                )
            ),

            // Contracts available in this settlement
            ImmutableArray.Create(
                createContract(Ids.Products.Server, 10, Ids.Products.IronOre, 386, 0.12, 0.3, 1),
                createContract(Ids.Products.MedicalSupplies3, 10, Ids.Products.CopperOre, 36, 0.12, 0.2, 2),
                createContract(Ids.Products.LabEquipment3, 10, Ids.Products.Coal, 216, 0.12, 0.2, 2),
                createContract(Ids.Products.SolarCell, 10, Ids.Products.Quartz, 41, 0.12, 0.3, 2),
                createContract(Ids.Products.ConsumerElectronics, 10, Ids.Products.Quartz, 216, 0.12, 0.3, 3),
                createContract(Ids.Products.Server, 10, Ids.Products.GoldOre, 386, 0.12, 0.3, 3)
            ),

            0, // Starting reputation for the settlement
            popsAdoptionEnabled: false // Whether pops adoption is enabled
        );
        // Adding a new settlement
        addSettlement(
                     NewIDs.World.SettlementForIlmenite, // ID of the settlement
                     200.Percent(), // Befriend cost multiplier

                     // Quick trades available in this settlement
                     ImmutableArray.Create(
                         createTrade(
                             Ids.Products.Microchips, // Product to trade
                             40, // Quantity of product
                             Ids.Products.ConstructionParts4, // Trade for product
                             40, // Quantity of trade for product
                             1   // Tier
                         )
                     ),

                     // Contracts available in this settlement
                     ImmutableArray.Create(
                         createContract(Ids.Products.ConstructionParts4, 38, NewIDs.Products.IlmeniteOre, 386, 0.12, 0.3, 1),
                         createContract(Ids.Products.Water, 25, Ids.Products.SourWater, 30, 0.12, 0.2, 2),
                         createContract(Ids.Products.LabEquipment3, 10, Ids.Products.ConstructionParts4, 30, 0.12, 0.2, 2),
                         createContract(Ids.Products.ConstructionParts4, 5, Ids.Products.Rock, 216, 0.12, 0.3, 2),
                         createContract(NewIDs.Products.NitricAcid, 50, Ids.Products.FertilizerChemical2, 216, 0.12, 0.3, 3),
                         createContract(Ids.Products.RetiredWaste, 50, Ids.Products.UraniumOre, 100, 0.12, 0.3, 3)
                     ),

                     1, // Starting reputation for the settlement
                     popsAdoptionEnabled: false // Whether pops adoption is enabled
                 );


        ProductProto orThrow11 = protosDb.GetOrThrow<ProductProto>(Ids.Products.CargoShip);
        ProductProto orThrow12 = protosDb.GetOrThrow<ProductProto>(Ids.Products.ConstructionParts3);
        Quantity quantity = 600.Quantity();


        Mafi.Core.Prototypes.Proto.Str cargoShipStr = Mafi.Core.Prototypes.Proto.CreateStr(NewIDs.World.CargoBuildProgressWreckCost1, "Damaged cargo ship");
        addShipWreck(NewIDs.World.CargoBuildProgressWreckCost1, new AssetValue(orThrow.WithQuantity(240)));
        addShipWreck(NewIDs.World.CargoBuildProgressWreckCost2, new AssetValue(orThrow2.WithQuantity(300), orThrow3.WithQuantity(200)));
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
            return protosDb.Add(new ContractProto(new Mafi.Core.Prototypes.Proto.ID("Contract_" + productToBuy.Value + "_For_" + productToPayWith.Value + "_COIE"), protosDb.GetOrThrow<ProductProto>(productToBuy).WithQuantity(quantity2), protosDb.GetOrThrow<ProductProto>(productToPayWith).WithQuantity(quantityToPayWith), perMonth.Upoints(), per100Quantity.Upoints(), minReputation));
        }

        QuickTradePairProto createTrade(ProductProto.ID productToPayWith, int quantityToPayWith, ProductProto.ID productToBuy, int quantityToBuy, int minReputation, int maxSteps = 16, bool ignoreTradeMultipliers = false)
        {
            return protosDb.Add(new QuickTradePairProto(new Mafi.Core.Prototypes.Proto.ID("Trade_" + productToBuy.Value + "_For_" + productToPayWith.Value + "_COIE"), protosDb.GetOrThrow<ProductProto>(productToBuy).WithQuantity(quantityToBuy), protosDb.GetOrThrow<ProductProto>(productToPayWith).WithQuantity(quantityToPayWith), 0.5.Upoints(), maxSteps, minReputation, 2, 5.Minutes(), 125.Percent(), 100.Percent(), ignoreTradeMultipliers));
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
    }
}