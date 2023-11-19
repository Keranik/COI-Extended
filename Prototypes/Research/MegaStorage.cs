using Mafi.Core.Mods;
using Mafi.Base;
using Mafi.Core.Research;
using Mafi;

namespace COIExtended.Prototypes.Research;
internal class MegaStorage : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        registrator.ResearchNodeProtoBuilder
                .Start("Mega Storage", NewIDs.Research.UnlockExpandedStorages)
                .Description("Even more options for storage with more IO support.")
                .AddLayoutEntityToUnlock(NewIDs.Buildings.StorageFluidT5, false)
                .AddLayoutEntityToUnlock(NewIDs.Buildings.StorageUnitT5, false)
                .AddLayoutEntityToUnlock(NewIDs.Buildings.StorageLooseT5, false)
                .SetCosts(new ResearchCostsTpl(69))
                .SetGridPosition(new Vector2i(130, 18))
                .AddParents(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.ConsumerElectronics))
                .BuildAndAdd();
    }
}
