using Mafi.Core.Mods;
using Mafi.Base;
using Mafi.Core.Products;
using COIExtended.Extensions;

namespace COIExtended.Prototypes.Modifications;
internal class FluidStorage : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        // We could probably not use harmony and just make our own prototypes which we should
        // but this is here now and it works, so we will know to come back to it because it is
        // in the modifications directory.
        FluidProductProto productChange;
        registrator.PrototypesDb.TryGetProto(Ids.Products.CoreFuel, out productChange);
        productChange.SetField("IsStorable", true);
        registrator.PrototypesDb.TryGetProto(Ids.Products.CoreFuelDirty, out productChange);
        productChange.SetField("IsStorable", true);
        registrator.PrototypesDb.TryGetProto(Ids.Products.SteamSp, out productChange);
        productChange.SetField("IsStorable", true);
        registrator.PrototypesDb.TryGetProto(Ids.Products.SteamHi, out productChange);
        productChange.SetField("IsStorable", true);
        registrator.PrototypesDb.TryGetProto(Ids.Products.SteamLo, out productChange);
        productChange.SetField("IsStorable", true);
        registrator.PrototypesDb.TryGetProto(Ids.Products.SteamDepleted, out productChange);
        productChange.SetField("IsStorable", true);
        registrator.PrototypesDb.TryGetProto(Ids.Products.Exhaust, out productChange);
        productChange.SetField("IsStorable", true);
    }

}
