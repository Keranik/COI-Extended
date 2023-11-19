using HarmonyLib;
using Mafi.Unity.UiFramework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mafi.Unity.InputControl.Inspectors.Factory;
using System.Reflection;
using COIExtended.HarmonyExt;

namespace COIExtended.UIFixes;
internal class StackerUIFix
{
    public StackerWindowFix() : base("Stacker")
    {
        Typ = Assembly.Load("Mafi.Unity").GetType("Mafi.Unity.InputControl.Inspectors.Factory.StackerWindowView");
        AddMethod(Typ, "AddCustomItems", this.GetHarmonyMethod("MyPostfix"), true);
    }
}
