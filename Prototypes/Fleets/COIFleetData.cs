using Mafi.Base;
using Mafi.Core.Economy;
using Mafi.Core.Fleet;
using Mafi;
using Mafi.Core.Mods;
using Mafi.Base.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mafi.Base.Prototypes.Fleet;
using Mafi.Core.Prototypes;
using COIExtended.Extensions;
using Mafi.Core.Products;

namespace COIExtended.Prototypes.Fleets;
internal class COIFleetData : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        FleetFuelTankPartProto weakTank;
        registrator.PrototypesDb.TryGetProto(Ids.Fleet.FuelTanks.FuelTankT1, out weakTank);
        weakTank.SetField("AddedFuelCapacity", new Quantity(999));

        FleetEnginePartProto engineT1;
        registrator.PrototypesDb.TryGetProto(Ids.Fleet.Engines.EngineT1, out engineT1);
        engineT1.SetField("DistancePerFuel", 75.ToFix32());

        FleetEnginePartProto engineT2;
        registrator.PrototypesDb.TryGetProto(Ids.Fleet.Engines.EngineT2, out engineT2);
        engineT2.SetField("DistancePerFuel", 125.ToFix32());

        FleetEnginePartProto engineT3;
        registrator.PrototypesDb.TryGetProto(Ids.Fleet.Engines.EngineT3, out engineT3);
        engineT3.SetField("DistancePerFuel", 175.ToFix32());

        
    }
}
