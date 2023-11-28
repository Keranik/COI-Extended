using Mafi.Core.Mods;
using Mafi.Base;
using Mafi.Core.Research;
using Mafi;

namespace COIExtended.Prototypes.Research;
internal class FishingDockResearch : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        registrator.ResearchNodeProtoBuilder
                .Start("Fishing Dock", NewIDs.Research.UnlockFishingDock)
                .Description("Increase the happiness of your population by providing them with seafood.")
                .AddMachineToUnlock(NewIDs.Machines.FishingDock, false)
                .SetCosts(new ResearchCostsTpl(65))
                .SetGridPosition(new Vector2i(116, 37))
                .AddParents(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.ResearchLab5))
                .BuildAndAdd();
    }
}