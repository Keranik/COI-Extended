using Mafi.Localization;
using Mafi.Unity.UiFramework.Components;
using Mafi.Unity.UiFramework;
using Mafi;
using System.Security.AccessControl;
using System;
using UnityEngine; // Make sure to include the appropriate namespace
using static HarmonyLib.Code;
using static Unity.VectorGraphics.VectorUtils;

public static class Utilities
{
    public static Txt CreateAndAppendTxt(StackContainer windowToAddTo, string textToAdd, Offset adjustments)
    {
        Txt txt = Builder
            .NewTxt("Auto-Generated")
            .EnableRichText()
            .SetTextStyle(Builder.Style.Global.Text)
            .SetText(new LocStrFormatted(torf))
            .SetAlignment(alignment)
            .AppendTo(debugWindow, null, Offset.Right(5f), false);

        return txt;
    }
}

