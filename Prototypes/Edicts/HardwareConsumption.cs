using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Mods;
using Mafi.Core.Population.Edicts;
using Mafi.Core.Population;
using Mafi.Core.PropertiesDb;
using Mafi.Core.Prototypes;
using Mafi.Core;
using Mafi.Localization;
using Mafi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace COIExtended.Prototypes.Edicts;
internal class HardwareConsumptionEdict : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        ProtosDb prototypesDb = registrator.PrototypesDb;
        string comment2 = "policy / edict which can enabled by the player in their Captain's office. {0}=15%";
        string comment3 = "policy / edict which can enabled by the player in their Captain's office.";

        Percent percent12 = 25.Percent();
        Percent percent13 = 35.Percent();
        Percent percent14 = 50.Percent();
        Percent percent15 = 20.Percent();
        Debug.Log("Initializing new Edicts");

        EdictCategoryProto edictCategoryProto = prototypesDb.GetOrThrow<EdictCategoryProto>(Ids.EdictCategories.Population);
        string value8 = NewIDs.Edicts.HardwareStoreConsumptionIncrease.Value;

        PopNeedProto orThrow3 = prototypesDb.GetOrThrow<PopNeedProto>(NewIDs.PopNeeds.HardwareStoreNeed);
        LocStr title10 = Loc.Str(NewIDs.Edicts.HardwareStoreConsumptionIncrease + "__name", "More Construction Parts", comment3);
        LocStr2 locStr16 = Loc.Str2(NewIDs.Edicts.HardwareStoreConsumptionIncrease + "__desc", "Construction Parts demand increased by {0}, unity given for it increased by {1}", comment2);

        EdictWithPropertiesProto edictWithPropertiesProto9 = prototypesDb.Add(new EdictWithPropertiesProto(
            NewIDs.Edicts.HardwareStoreConsumptionIncrease,
            Proto.CreateStrFromLocalized(NewIDs.Edicts.HardwareStoreConsumptionIncrease, formatWithNumeral(title10, 0), locStr16.Format(percent12, percent15)),
            edictCategoryProto,
            0.Upoints(),
            createPropsForNeed(orThrow3, percent12, percent15),
            value8,
            Option<EdictProto>.None,
            isGeneratingUnity: true,
            graphics: new EdictProto.Gfx("Assets/Base/Icons/Edicts/UnityIncreased.svg")));

        EdictWithPropertiesProto edictWithPropertiesProto10 = prototypesDb.Add(new EdictWithPropertiesProto(
            NewIDs.Edicts.HardwareStoreConsumptionIncreaseT2,
            Proto.CreateStrFromLocalized(NewIDs.Edicts.HardwareStoreConsumptionIncreaseT2, formatWithNumeral(title10, 1), locStr16.Format(percent12, percent15)),
            edictCategoryProto,
            0.Upoints(),
            createPropsForNeed(orThrow3, percent12, percent15),
            value8,
            edictWithPropertiesProto9,
            isGeneratingUnity: true,
            graphics: new EdictProto.Gfx("Assets/Base/Icons/Edicts/UnityIncreased.svg")));

        prototypesDb.Add(new EdictWithPropertiesProto(
            NewIDs.Edicts.HardwareStoreConsumptionIncreaseT3,
            Proto.CreateStrFromLocalized(NewIDs.Edicts.HardwareStoreConsumptionIncreaseT3, formatWithNumeral(title10, 2), locStr16.Format(percent14, percent15)),
            edictCategoryProto,
            0.Upoints(),
            createPropsForNeed(orThrow3, percent14, percent15),
            value8,
            edictWithPropertiesProto10,
            isGeneratingUnity: true,
            graphics: new EdictProto.Gfx("Assets/Base/Icons/Edicts/UnityIncreased.svg")));

        static ImmutableArray<KeyValuePair<PropertyId<Percent>, Percent>> createPropsForNeed(PopNeedProto popNeed, Percent consumptionDiff, Percent unityDiff)
        {
            return ImmutableArray.Create(Make.Kvp(popNeed.ConsumptionMultiplierProperty.Value, consumptionDiff), Make.Kvp(popNeed.UnityMultiplierProperty.Value, unityDiff));
        }

        static LocStrFormatted formatWithNumeral(LocStr title, int index)
        {
            return new LocStrFormatted($"{title} {EdictProto.ROMAN_NUMERALS[index]}");
        }
    }
}
