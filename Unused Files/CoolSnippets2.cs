using Mafi.Core.World.Entities;
using Mafi.Unity.UiFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COIExtended;
internal class CoolSnippets
{

    public void RevealAllVillages()
    {
        List<WorldMapVillage> lyst = m_entitiesManager.GetAllEntitiesOfType<WorldMapVillage>().ToList();
        Debug.Log("Starting Reveal All Villages");
        foreach (WorldMapVillage village in lyst)
        {
            if (village != null)
            {
                Debug.Log("Looping thru villages");
                var listPopsAvailable = village.PopsAvailable;

                if (listPopsAvailable >= 0)
                {
                    Builder.NewTxt(listPopsAvailable.ToString())
                    .AppendTo(_debugWindow, 12, Offset.Zero, false);
                }
            }

        }
    }

    foreach (WorldMapLocation worldMapLocation in enumerable)
				{

         List<WorldMapVillage> lyst = m_entitiesManager.GetAllEntitiesOfType<WorldMapVillage>().ToList();
    Debug.Log("Starting Reveal All Villages");
            foreach (WorldMapVillage village in lyst)
            {
                if (village != null)
                {
                    Debug.Log("Looping thru villages");
                    var result = village.IsOwnedByPlayer;
    var torf = string.Format("Result: {0}", result ? "True" : "False");
    Txt txt = Builder
        .NewTxt("DoesThisMatter")
        .EnableRichText()
        .SetTextStyle(Builder.Style.Global.Text)
        .SetText(new LocStrFormatted(torf))
        .SetAlignment(TextAnchor.MiddleRight)
        .AppendTo(_debugWindow, null, Offset.Right(5f), false);

}

            }


}
IEnumerable<WorldMapEntityProto> allProtos = m_entityManager.GetAllEntitiesOfType<WorldMapEntityProto>();