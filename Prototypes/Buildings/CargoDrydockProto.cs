using System;
using System.Collections.Generic;
using Mafi;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;


namespace COIExtended.Prototypes.Buildings;
public class CargoDrydockProto : LayoutEntityProto, IProto, IProtoWithPowerConsumption, IProtoWithComputingConsumption
{
    public readonly int? EmissionIntensity;
    public override Type EntityType => typeof(CargoDrydock);

    public Electricity ElectricityConsumed { get; private set;  }

    public Computing ComputingConsumed { get; private set; }
    public Option<ProductProto> InputTitanium;
    public Option<ProductProto> InputElectronics3;
    public Option<ProductProto> InputConstructionParts4;
    public Option<ProductProto> ShipProgressItem;

    public CargoDrydockProto(ID id, Str strings, EntityLayout layout, EntityCosts costs, Option<ProductProto> inputTitanium, Option<ProductProto> inputElectronics3, Option<ProductProto> inputCP4, Option<ProductProto> shipProgressItem, Gfx graphics,  Electricity consumedPowerPerTick, Computing computingConsumed = default(Computing), Upoints? boostCost = null,  IEnumerable<Tag> tags = null)
    : base(id, strings, layout, costs, graphics, null, null, false, true, false, false, tags)
    {
        InputTitanium = inputTitanium;
        InputElectronics3 = inputElectronics3;
        InputConstructionParts4 = inputCP4;
        ElectricityConsumed = consumedPowerPerTick;
        ComputingConsumed = computingConsumed;
        ShipProgressItem = shipProgressItem;
    }


}
