using COIExtended.HarmonyExt;
using COIExtended.Patcher;
using Mafi;
using Mafi.Unity;
using Mafi.Unity.InputControl;
using Mafi.Core.Factory.Transports;
using Mafi.Unity.InputControl.Inspectors;
using Mafi.Unity.UiFramework;
using Mafi.Unity.UiFramework.Components;
using Mafi.Unity.UserInterface;
using System;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;

namespace COIExtended.Game.Patcher {


    internal class StackerWindowPatcher : APatcher<StackerWindowPatcher> {
        public override bool DefaultState => true;
        public override bool Enabled => true;
        private static Type Typ;
        private static int m_distanceToOffset = 0;

        public StackerWindowPatcher() : base("Stacker") {
            Typ = Assembly.Load("Mafi.Unity").GetType("Mafi.Unity.InputControl.Inspectors.Factory.StackerWindowView");
            AddMethod(Typ, "AddCustomItems", this.GetHarmonyMethod("MyPostfix"), true);
        }
        public override void OnInit(DependencyResolver resolver) {

        }
        

    private static void MyPostfix(StaticEntityInspectorBase<Stacker> __instance, ref StackContainer itemContainer) 
        {
            List<string> numberList = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                numberList.Add(i.ToString());
            }

            UiBuilder Builder = __instance.GetFieldRef<ItemDetailWindowView, UiBuilder>("Builder");
            if (Builder is null) return;
           
            var OffsetContainer = Builder.NewStackContainer("OffsetBox")
            .SetStackingDirection(StackContainer.Direction.TopToBottom)
            .SetSizeMode(StackContainer.SizeMode.StaticCenterAligned)
            .SetBackground(Builder.Style.Panel.ItemOverlay)
            .SetInnerPadding(Offset.Zero);               
            
            var OffsetPanel = Builder.NewPanel("OffsetPanelBG").SetBackground(Builder.Style.Panel.ItemOverlay);
            OffsetPanel.PutToRightTopOf(itemContainer, itemContainer.GetSize(), Offset.Top(140f) + Offset.Right(-itemContainer.GetWidth() - 5f));
            //mineTowerPanel.AppendTo(OffsetContainer, size: 25f, Offset.All(0));
            Builder.AddSectionTitle(OffsetContainer, "Offset X", "COIEXTENDED: Set an extra offset for dumping distance.  Look for the green indicator, the prefab will not update.");

            var productDropdown = Builder
                .NewDropdown("ProductDropdown")
                .AddOptions(numberList)
                .OnValueChange(
                                    i =>
                                    {
                                        m_distanceToOffset = i;
                                        ChangeOffsetX(__instance);
                                    }
                              );



            productDropdown.AppendTo(OffsetContainer, new Vector2(85, 30f), ContainerPosition.LeftOrTop);
            //__instance.AddUpdater(this.m_dropDepthPanel.Updater);

            OffsetContainer.PutToRightMiddleOf(itemContainer, new Vector2(95f, 70f), Offset.Top(172f) + Offset.Right(-213f));
   
            
            



        }

        private static void ChangeOffsetX(StaticEntityInspectorBase<Stacker> __instance)
        {
            Stacker toChange = __instance.InvokeGetter<Stacker>("Entity");

            if (toChange == null || m_distanceToOffset <= -1)
                return;

            toChange.SetDumpHeightOffset(new ThicknessTilesI(m_distanceToOffset));
        }
    }
}