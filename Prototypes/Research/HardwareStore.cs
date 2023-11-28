using Mafi.Core.Mods;
using Mafi.Base;
using Mafi.Core.Research;
using Mafi;

namespace COIExtended.Prototypes.Research;
internal class HardwareStoreResearch : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        registrator.ResearchNodeProtoBuilder
                .Start("Hardware Store", NewIDs.Research.UnlockHardwareStore)
                .Description("Increase the happiness of your population by providing them with basic supplies to customize their homes.")
                .AddLayoutEntityToUnlock(NewIDs.Buildings.SettlementHardwareStore, false)
                .AddEdictToUnlock(NewIDs.Edicts.HardwareStoreConsumptionIncrease)
                .AddEdictToUnlock(NewIDs.Edicts.HardwareStoreConsumptionIncreaseT2)
                .AddEdictToUnlock(NewIDs.Edicts.HardwareStoreConsumptionIncreaseT3)
                .AddProductIcon(Ids.Products.ConstructionParts4)
                .SetCosts(new ResearchCostsTpl(65))
                .SetGridPosition(new Vector2i(120, 11))
                .AddParents(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.VehicleCapIncrease6))
                .BuildAndAdd();
    }
}