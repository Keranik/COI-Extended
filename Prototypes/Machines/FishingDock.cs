using Mafi.Core.Mods;
using System;
using Mafi.Core.Prototypes;
using Mafi.Base.Prototypes.Machines;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Terrain;
using Mafi.Localization;
using Mafi;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace COIExtended.Prototypes.Machines;
internal class FishingDock : IModData
{
    public void RegisterData(ProtoRegistrator registrator)
    {
        
        EntityCosts costs = NewCosts.Machines.FishingDock.MapToEntityCosts(registrator);
        EntityLayout layout = new EntityLayoutParser(registrator.PrototypesDb).ParseLayoutOrThrow(new EntityLayoutParams(null, new CustomLayoutToken[3]
        {
            new CustomLayoutToken("~0!", (EntityLayoutParams p, int h) => new LayoutTokenSpec(-10, h, LayoutTileConstraint.Ocean)),
            new CustomLayoutToken("{0!", delegate(EntityLayoutParams p, int h)
            {
                int? maxTerrainHeight = 0;
                return new LayoutTokenSpec(-10, h, LayoutTileConstraint.None, null, null, maxTerrainHeight);
            }),
            new CustomLayoutToken("~~~", (EntityLayoutParams p, int h) => new LayoutTokenSpec(-12, -10, LayoutTileConstraint.Ocean))
                        }), "~2!{2!{2!{2!                                    ",
                            "~2!{2!{2!{2!                                    ",
                            "~2!{2!{2!{2!                                    ",
                            "~2!{2!{2!{2!{2!{2!{2!{4!{4!{4!{4!{4!{4!(4)(4)   ",
                            "~2!{2!{2!{2!{2!{2!{2!{4!{4!{4!{4!{4!{4!(4)(4)<~A",
                            "~2!{2!{2!{2!{2!{2!{2!{4!{4!{4!{4!{4!{4!(4)(4)   ",
                            "~2!{2!{2!{2!{2!{2!{2!{4!{4!{4!{4!{4!{4!(4)(4)   ",
                            "~2!{2!{2!{2!{2!{2!{2!{4!{4!{4!{4!{4!{4!(4)(4)   ",
                            "~2!{2!{2!{2!{2!{2!{2!{4!{4!{4!{4!{4!{4!(4)(4)   ",
                            "~2!{2!{2!{2!{2!{2!{2!{4!{4!{4!{4!{4!{4!(4)(4)   ",
                            "~2!{2!{2!{2!{2!{2!{2!{4!{4!{4!{4!{4!{4!(4)(4)>#Z",
                            "~2!{2!{2!{2!{2!{2!{2!{4!{4!{4!{4!{4!{4!(4)(4)   ",
                            "~2!{2!{2!{2!                                    ",
                            "~2!{2!{2!{2!                                    ",
                            "~2!{2!{2!{2!                                    ");
        Proto.Str strings = Proto.CreateStr(NewIDs.Machines.FishingDock, "Fishing Dock", "A Fishing Dock allows you to harvest natural resources from the Ocean.");
        RelTile3f prefabOffset = new RelTile3f(-2, 0, 1);

        MachineProto.Gfx dockGFX = new MachineProto.Gfx("Assets/COIExtended/Buildings/FishingDock/FishingDock.prefab", registrator.GetCategoriesProtos(new Proto.ID[]
        {
                Ids.ToolbarCategories.MachinesFood
        }), prefabOffset, "Assets/COIExtended/Buildings/FishingDock/FishingDock.png", default, default, default, false, false, null, null, default, null);

        MachineProto fishingDock = new MachineProto(NewIDs.Machines.FishingDock, strings, layout, costs, default, default, null, default, default, null, dockGFX, null, default, false, null, null);
        registrator.PrototypesDb.Add(fishingDock);
               

            registrator.RecipeProtoBuilder.Start("Fishing", NewIDs.Recipes.FishingDock, NewIDs.Machines.FishingDock)
            .AddInput(4, Ids.Products.MeatTrimmings, "A")
            .SetDuration(60.Seconds())
            .AddOutput(24, NewIDs.Products.FreshFish, "Z")
            .BuildAndAdd();

            registrator.RecipeProtoBuilder.Start("Fishing No Bait", NewIDs.Recipes.FishingDockNoBait, NewIDs.Machines.FishingDock)
            .SetDuration(60.Seconds())
            .AddOutput(6, NewIDs.Products.FreshFish, "Z")
            .BuildAndAdd();

    }

}
