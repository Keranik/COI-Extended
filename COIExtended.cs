using Mafi;
using Mafi.Base;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Entities;
using Mafi.Core.Game;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using System;
using ResNodeID = Mafi.Core.Research.ResearchNodeProto.ID;
using MachineID = Mafi.Core.Factory.Machines.MachineProto.ID;
using RecipeID = Mafi.Core.Factory.Recipes.RecipeProto.ID;
using TransportID = Mafi.Core.Factory.Transports.TransportProto.ID;
using ProductID = Mafi.Core.Products.ProductProto.ID;
using Mafi.Core.Products;
using COIExtended.Prototypes.Buildings;
using Mafi.Core.Entities.Static.Layout;
using COIExtended.UI;
using UnityEngine.UIElements;

namespace COIExtended
{
    public sealed class COIExtended : IMod
    {    
        public static Version ModVersion = new Version(0, 2, 0);
        public string Name => "COI Extended";
        public int Version => 1;
        public bool IsUiOnly => false;

        public COIExtended(CoreMod coreMod, BaseMod baseMod)
        {

            // You can use Log class for logging. These will be written to the log file
            // and can be also displayed in the in-game console with command `also_log_to_console`.
            Log.Info("ExampleMod: constructed");
        }

        public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
        {

        }
       
        public void ChangeConfigs(Lyst<IConfig> configs)
        {
        }

        public void RegisterPrototypes(ProtoRegistrator registrator)
        {
            registrator.RegisterAllProducts();
            registrator.RegisterData<Prototypes.Products.Countable>();
            registrator.RegisterData<Prototypes.Products.Loose>();
            registrator.RegisterData<Prototypes.Products.Virtual>();
            registrator.RegisterData<Prototypes.Products.Fluid>();
            registrator.RegisterData<Prototypes.Products.Molten>();
            registrator.RegisterData<Prototypes.Machines.SeawaterPump>();
            registrator.RegisterData<Prototypes.Machines.Waterwell>();
            registrator.RegisterData<Prototypes.Machines.SettlingTank>();
            registrator.RegisterData<Prototypes.Machines.LiquidDumps>();
            registrator.RegisterData<Prototypes.Machines.AirSeparator>();
            registrator.RegisterData<Prototypes.Transports.StackerLong>();
            registrator.RegisterData<Prototypes.Transports.StackerT2>();
            registrator.RegisterData<Prototypes.Buildings.DryDock>();
            registrator.RegisterData<Prototypes.Modifications.FluidStorage>();
            registrator.RegisterData<Prototypes.Storages.StoragesT5>();
            registrator.RegisterData<Prototypes.Research.LiquidDump>();
            registrator.RegisterData<Prototypes.Research.AdvancedPumping>();
            registrator.RegisterData<Prototypes.Research.MegaStorage>();
            registrator.RegisterData<Prototypes.Research.Nitrogen>();
            registrator.RegisterData<Prototypes.Recipes.Titanium>();
            registrator.RegisterData<Prototypes.Research.Titanium>();
            registrator.RegisterData<Prototypes.Research.CargoShip>();
            registrator.RegisterData<Prototypes.Machines.ChemicalPlant>();
            registrator.RegisterData<Prototypes.Research.ChemicalPlantT3>();
            registrator.RegisterData<Prototypes.Machines.ScrubberT2>();
            registrator.RegisterData<Prototypes.Research.PhotoOxidation>();            
            registrator.RegisterData<Prototypes.World.Locations>();
            
        }
        public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool wasLoaded)
        {
            //depBuilder.RegisterDependency<ShipConstructionManager>()
            //.AsSelf() // Registers ShipConstructionManager as itself for injection.
            //.AsAllInterfaces(); // Registers ShipConstructionManager for all interfaces it implements.
            //depBuilder.RegisterDependency<CargoDrydockWindowView>()
            //.AsSelf();
            //depBuilder.RegisterDependency<CargoDrydockWindowController>()
            //.AsSelf() // Registers ShipConstructionManager as itself for injection.
            //.AsAllInterfaces(); // Registers ShipConstructionManager for all interfaces it implements.
            //depBuilder.RegisterDependency<CargoDrydockInspector>()
           //.AsSelf() // Registers CargoDrydockInspector as itself for injection.
           //.AsAllInterfaces(); // Registers CargoDrydockInspector for all interfaces it implements.  



        }

        private void InitializePatchers(DependencyResolver resolver)
        {


        }
    }

    public static class NewIDs
    {
        public partial class Buildings()
        {
            public static readonly MachineID StorageFluidT5 = Ids.Machines.CreateId("StorageFluidT5");
            public static readonly MachineID StorageLooseT5 = Ids.Machines.CreateId("StorageLooseT5");
            public static readonly MachineID StorageUnitT5 = Ids.Machines.CreateId("StorageUnitT5");
            public static readonly LayoutEntityProto.ID DryDock = new LayoutEntityProto.ID("DryDock");
        }
        public partial class World()
        {
            public static readonly EntityProto.ID OilRigCost1 = new EntityProto.ID("COIE_OilRigCost1");
            public static readonly EntityProto.ID OilRigCost2 = new EntityProto.ID("COIE_OilRigCost2");
            public static readonly EntityProto.ID OilRigCost3 = new EntityProto.ID("COIE_OilRigCost3");
            public static readonly EntityProto.ID WaterWell = new EntityProto.ID("COIE_WaterWell");
            public static readonly EntityProto.ID QuartzMine = new EntityProto.ID("COIE_QuartzMine");
            public static readonly EntityProto.ID SulfurMine = new EntityProto.ID("COIE_SulfurMine");
            public static readonly EntityProto.ID CoalMine = new EntityProto.ID("COIE_CoalMine");
            public static readonly EntityProto.ID UraniumMine = new EntityProto.ID("COIE_UraniumMine");
            public static readonly EntityProto.ID RockMine = new EntityProto.ID("COIE_RockMine");
            public static readonly EntityProto.ID LimestoneMine = new EntityProto.ID("COIE_LimestoneMine");
            public static readonly EntityProto.ID IlmeniteMine = new EntityProto.ID("COIE_IlmeniteMine");
            public static readonly EntityProto.ID Settlement1 = new EntityProto.ID("COIE_Settlement1");
            public static readonly EntityProto.ID Settlement2 = new EntityProto.ID("COIE_Settlement2");
            public static readonly EntityProto.ID Settlement3 = new EntityProto.ID("COIE_Settlement3");
            public static readonly EntityProto.ID Settlement4 = new EntityProto.ID("COIE_Settlement4");
            public static readonly EntityProto.ID Settlement5 = new EntityProto.ID("COIE_Settlement5");
            public static readonly EntityProto.ID SettlementForFuel = new EntityProto.ID("COIE_Settlement6");
            public static readonly EntityProto.ID SettlementForUranium = new EntityProto.ID("COIE_Settlement7");
            public static readonly EntityProto.ID SettlementForIlmenite = new EntityProto.ID("COIE_IlmeniteSettlement");
            public static readonly EntityProto.ID SettlementForShips = new EntityProto.ID("COIE_SettlementForShips");
            public static readonly EntityProto.ID CargoBuildProgressWreckCost1 = new EntityProto.ID("COIE_CargoBuildProgressWreckCost1");
            public static readonly EntityProto.ID CargoBuildProgressWreckCost2 = new EntityProto.ID("COIE_CargoBuildProgressWreckCost2");
        }

        public partial class Research
        {
            public static readonly ResNodeID UnlockExpandedStorages = Ids.Research.CreateId("UnlockExpandedStorages");
            public static readonly ResNodeID UnlockLargeWasteDumps = Ids.Research.CreateId("UnlockLargeLiquidDumps");
            public static readonly ResNodeID AdvancedPumping = Ids.Research.CreateId("AdvancedPumping");
            public static readonly ResNodeID NitrogenManipulation = Ids.Research.CreateId("NitrogenManipulation");
            public static readonly ResNodeID ImprovedPhotocatalysis = Ids.Research.CreateId("Improved Photocatalysis");
            public static readonly ResNodeID IlmeniteOreProcessing = Ids.Research.CreateId("IlmeniteOreProcessing");
            public static readonly ResNodeID CargoShipResearch = Ids.Research.CreateId("CargoShipResearch");
            public static readonly ResNodeID ChemicalPlantT3 = Ids.Research.CreateId("ChemicalPlantT3");
            public static readonly ResNodeID PhotoOxidation = Ids.Research.CreateId("PhotoOxidation");
        }

        public partial class Machines
        {
            
            public static readonly MachineID LargeLiquidDump = Ids.Machines.CreateId("LargeLiquidDump");
            public static readonly MachineID LandWaterPumpT2 = Ids.Machines.CreateId("LandWaterPumpT2");
            public static readonly MachineID OceanWaterPumpT2 = Ids.Machines.CreateId("OceanWaterPumpT2");
            public static readonly MachineID OceanWaterPumpTallT2 = Ids.Machines.CreateId("OceanWaterPumpTallT2");
            public static readonly MachineID AirSeparatorT2 = Ids.Machines.CreateId("AirSeparatorT2");
            public static readonly MachineID SettlingTankII = Ids.Machines.CreateId("SettlingTankII");
            public static readonly MachineID ChemicalPlantIII = Ids.Machines.CreateId("ChemicalPlantIII");
            public static readonly MachineID ScrubberT2 = Ids.Machines.CreateId("ScrubberT2");

        }

        public partial class Recipes
        {
            public static readonly RecipeID LargeWaterDumping = Ids.Recipes.CreateId("LargeWaterDumping");
            public static readonly RecipeID LargeSeaWaterDumping = Ids.Recipes.CreateId("LargeSeaWaterDumping");
            public static readonly RecipeID LargeBrineDumping = Ids.Recipes.CreateId("LargeBrineDumping");
            public static readonly RecipeID LargeWasteWaterDumping = Ids.Recipes.CreateId("LargeWasteWaterDumping");
            public static readonly RecipeID LargeSourWaterDumping = Ids.Recipes.CreateId("LargeSourWaterDumping");
            public static readonly RecipeID LargeAcidDumping = Ids.Recipes.CreateId("LargeAcidDumping");
            public static readonly RecipeID LargeToxicDumping = Ids.Recipes.CreateId("LargeToxicDumping");
            public static readonly RecipeID LargeFert1Dumping = Ids.Recipes.CreateId("LargeFert1Dumping");
            public static readonly RecipeID LargeFert2Dumping = Ids.Recipes.CreateId("LargeFert2Dumping");
            public static readonly RecipeID LargeFert3Dumping = Ids.Recipes.CreateId("LargeFert3Dumping");
            public static readonly RecipeID LargeLandWaterPumping = Ids.Recipes.CreateId("LargeLandWaterPumping");
            public static readonly RecipeID LargeSeaWaterPumping = Ids.Recipes.CreateId("LargeSeaWaterPumping");
            public static readonly RecipeID LargeSeaWaterPumpingTall = Ids.Recipes.CreateId("LargeSeaWaterPumpingTall");
            public static readonly RecipeID AirSeparationT2 = Ids.Recipes.CreateId("AirSeparationT2");
            public static readonly RecipeID NitrogenDioxide = Ids.Recipes.CreateId("NitrogenDioxideRecipe");
            public static readonly RecipeID NitricAcidRecipe = Ids.Recipes.CreateId("NitricAcidRecipe");
            public static readonly RecipeID NitricAcidElectrolysis = Ids.Recipes.CreateId("NitricAcidElectrolysis");
            public static readonly RecipeID NitricAcidGlassMix = Ids.Recipes.CreateId("NitricAcidGlassMix");
            public static readonly RecipeID NitricAcidGoldSettling = Ids.Recipes.CreateId("NitricAcidGoldSettling");
            public static readonly RecipeID NitricAcidUraniumSettling = Ids.Recipes.CreateId("NitricAcidUraniumSettling");
            public static readonly RecipeID NitricAcidHFSettling = Ids.Recipes.CreateId("NitricAcidHFSettling");
            public static readonly RecipeID IlmeniteSettling = Ids.Recipes.CreateId("IlmeniteSettling");
            public static readonly RecipeID IlmeniteCrushing = Ids.Recipes.CreateId("IlmeniteCrushing");
            public static readonly RecipeID MoltenTitanium = Ids.Recipes.CreateId("MoltenTitanium");
            public static readonly RecipeID ImpureTitanium = Ids.Recipes.CreateId("ImpureTitanium");
            public static readonly RecipeID TitaniumElectrolysis = Ids.Recipes.CreateId("TitaniumElectrolysis");
            public static readonly RecipeID GoldSettlingII = Ids.Recipes.CreateId("GoldSettlingII");
            public static readonly RecipeID UraniumLeachingII = Ids.Recipes.CreateId("UraniumLeachingII");
            public static readonly RecipeID FlourideLeachingII = Ids.Recipes.CreateId("FlourideLeachingII");
            public static readonly RecipeID NitricAcidGoldSettlingII = Ids.Recipes.CreateId("NitricAcidGoldSettlingII");
            public static readonly RecipeID NitricAcidUraniumSettlingII = Ids.Recipes.CreateId("NitricAcidUraniumSettlingII");
            public static readonly RecipeID NitricAcidHFSettlingII = Ids.Recipes.CreateId("NitricAcidHFSettlingII");
            // Chemical Plant 3
            public static readonly RecipeID FertilizerProductionT3 = Ids.Recipes.CreateId("FertilizerProductionT3");
            public static readonly RecipeID AmmoniaSynthesisT3 = Ids.Recipes.CreateId("AmmoniaSynthesisT3");
            public static readonly RecipeID FuelGasSynthesisT3 = Ids.Recipes.CreateId("FuelGasSynthesisT3");
            public static readonly RecipeID GraphiteProductionT3 = Ids.Recipes.CreateId("GraphiteProductionT3");
            public static readonly RecipeID DisinfectantProductionT3 = Ids.Recipes.CreateId("DisinfectantProductionT3");
            public static readonly RecipeID AnestheticsProductionT3 = Ids.Recipes.CreateId("AnestheticsProductionT3");
            public static readonly RecipeID MorphineProductionT3 = Ids.Recipes.CreateId("MorphineProductionT3");
            public static readonly RecipeID TitaniumDioxide = Ids.Recipes.CreateId("TitaniumDioxide");
            public static readonly RecipeID PhotoOxiScrub = Ids.Recipes.CreateId("PhotoOxiScrub");



        }

        public partial class Transports
        {
            public static readonly TransportID StackerT2 = new TransportID("StackerT2");
            public static readonly TransportID StackerLong = new TransportID("StackerLong");
        }

        public partial class Products
        {
            // New Fluid Products
            public static readonly ProductID NitrogenDioxide = Ids.Products.CreateId("NitrogenDioxide");
            public static readonly ProductID NitricAcid = Ids.Products.CreateId("NitricAcid");
            public static readonly ProductID TitaniumDioxide = Ids.Products.CreateId("TitaniumDioxide");
            // New Loose Products
            public static readonly ProductID IlmeniteOre = Ids.Products.CreateId("IlmeniteOre");
            public static readonly ProductID IlmeniteOreCrushed = Ids.Products.CreateId("IlmeniteOreCrushed");
            public static readonly ProductID TitaniumOre = Ids.Products.CreateId("TitaniumOre");
            public static readonly ProductID IlmeniteOreConcentrate = Ids.Products.CreateId("IlmeniteConcentrate");
            // New Molten Products
            public static readonly ProductID MoltenTitanium = Ids.Products.CreateId("MoltenTitanium");
            // New Unit Products
            public static readonly ProductID ImpureTitanium = Ids.Products.CreateId("ImpureTitanium");
            public static readonly ProductID TitaniumIngots = Ids.Products.CreateId("TitaniumIngots");


            
 

        }

        public partial class Virtual
        {
            public static readonly ProductID CargoShipProgress = Ids.Products.CreateVirtualId("CargoShipProgress");
        }
    }
    public static class NewCosts
    {
        public static class Buildings
        {
            public static EntityCostsTpl DryDock => Build.CP4(500).Workers(200).MaintenanceT3(12);
        }

        public static class CargoShip
        {
            
        }

        public static class Machines
        {
            public static EntityCostsTpl OceanWaterPumpT2 => Build.CP2(75).Workers(2).MaintenanceT1(4);
            public static EntityCostsTpl OceanWaterPumpTallT2 => Build.CP2(75).Workers(2).MaintenanceT1(8);
            public static EntityCostsTpl ChemicalPlantT3 => Build.CP4(60).Workers(14).MaintenanceT3(2);
            public static EntityCostsTpl ScrubberT2 => Build.CP4(60).Workers(14).MaintenanceT3(4);

            public static EntityCostsTpl LandWaterPumpT2 => Build.CP2(60).Workers(4).MaintenanceT2(2).Priority(8);
        }

        public static class Rockets
        {

        }

        public static class Transports
        {
            
        }

        public static class Vehicles
        {
            
        }

        public static EntityCostsTpl.Builder Build => new EntityCostsTpl.Builder();
    }

    
}