using Mafi.Base;
using Mafi.Core.Entities;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Mods;
using Mafi.Localization;
using Mafi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COIExtended.Prototypes.Machines;
internal class ScrubberT2 : IModData 
{
   public void RegisterData(ProtoRegistrator registrator)
        {
            LocStr1 locStr = Loc.Str1(NewIDs.Machines.ScrubberT2.ToString() + "__desc", "A photocatalytic machine is a device that utilizes light-activated catalysts to initiate and accelerate chemical reactions, often for environmental applications like air and water purification.", "{0} is a number, used like for instance '75%'");
            MachineProto machine = registrator.MachineProtoBuilder.Start("Exhaust Scrubber II", NewIDs.Machines.ScrubberT2).Description(LocalizationManager.CreateAlreadyLocalizedStr(NewIDs.Machines.ScrubberT2.ToString() + "_formatted", locStr.Format(75.ToString()).Value)).SetCost(NewCosts.Machines.ScrubberT2)
                .SetElectricityConsumption(400.Kw())
                .SetCategories(Ids.ToolbarCategories.Waste)
                .SetLayout("      [2][3][3][3][3][9][3]      ", "A@>[2][3][3][3][3][3][9][3]      ", "B@>[2][3][3][3][3][5][5][5]      ", "C@>[2][3][3][3][3][5][5][5][1]>@Y", "D@>[2][3][3][3][3][5][5][5]      ", "      [2][3][3][3][3][3]         ", "               v~Z   v@X         ")
                .AddParticleParams(ParticlesParams.Loop("Smoke", useUtilizationOnAlpha: true))
                .SetPrefabPath("Assets/COIExtended/Buildings/ScrubberT2/ScrubberT2.prefab")
                .SetMachineSound("Assets/Base/Machines/MetalWorks/FiltrationStation/FiltrationStationSound.prefab")
                .SetCustomIconPath("Assets/Unity/Generated/Icons/LayoutEntity/ExhaustScrubber.png")
                .EnableSemiInstancedRendering()
                .BuildAndAdd();
            registrator.RecipeProtoBuilder.Start("Photocatalytic Oxidation", NewIDs.Recipes.PhotoOxiScrub, machine)
            .AddInput(75, Ids.Products.Exhaust, "D")
            .AddInput(6, NewIDs.Products.TitaniumDioxide, "B")
            .SetDuration(10.Seconds())
            .AddOutput(48, Ids.Products.CarbonDioxide, "X")
            .AddOutput(3, Ids.Products.ToxicSlurry, "Y")
            .BuildAndAdd();
        }
    
}
