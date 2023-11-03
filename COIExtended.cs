using FluidChanges;
using Mafi;
using Mafi.Collections;
using Mafi.Core.Game;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using System;
using ResNodeID = Mafi.Core.Research.ResearchNodeProto.ID;
using MachineID = Mafi.Core.Factory.Machines.MachineProto.ID;
using Mafi.Base;


namespace COIExtended
{

    public sealed class COIExtended : IMod
    {

        public static Version ModVersion = new Version(0, 0, 1);
        public string Name => "COI Extended";
        public int Version => 1;
        public bool IsUiOnly => false;
        public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
        {
            cargoShipPatcher.runPatch();
            //slidersPatcher.runPatch();
            //farmPatcher.runPatch();
        }

        public partial class Research
        {
                public static readonly ResNodeID UnlockExpandedStorages = Ids.Research.CreateId("UnlockExpandedStorages");
                public static readonly MachineID StorageFluidT5 = Ids.Machines.CreateId("StorageFluidT5");
                public static readonly MachineID StorageLooseT5 = Ids.Machines.CreateId("StorageLooseT5");
                public static readonly MachineID StorageUnitT5 = Ids.Machines.CreateId("StorageUnitT5");
        }
        public void ChangeConfigs(Lyst<IConfig> configs)
        {
        }

        public void RegisterPrototypes(ProtoRegistrator registrator)
        {
            ModCFG.LoadSettings(); 
            FluidChangePH.Instance.StartUp(registrator);
            
        }
        public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool wasLoaded)
        {
        }

    }
}