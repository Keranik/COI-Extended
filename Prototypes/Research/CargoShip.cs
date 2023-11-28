using Mafi.Core.Mods;
using Mafi.Core.Research;
using Mafi;

namespace COIExtended.Prototypes.Research;
internal class CargoShip : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        registrator.ResearchNodeProtoBuilder
            .Start("Cargo Ship Research", NewIDs.Research.CargoShipResearch)
            .Description("This research allows you to construct your own cargo ship!")
            .AddProductToUnlock(NewIDs.Virtual.CargoShipProgress)
            .SetCosts(new ResearchCostsTpl(70))
            .AddLayoutEntityToUnlock(NewIDs.Buildings.DryDock)
            .SetGridPosition(new Vector2i(134, 18))
            .AddParents(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(NewIDs.Research.UnlockExpandedStorages))
            .BuildAndAdd();
    }
}
