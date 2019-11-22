using RimWorld;
using Verse;

namespace TraitRarityColors
{
    [StaticConstructorOnStartup]
    internal static class TraitRarityColors
    {
        static TraitRarityColors()
        {
            foreach (TraitDef traitDef in DefDatabase<TraitDef>.AllDefsListForReading)
            {
                foreach (TraitDegreeData traitDegreeData in traitDef.degreeDatas)
                {
                    string color = GetColorForRarity(traitDef.GetGenderSpecificCommonality(Gender.None));
                    if (traitDef.GetGenderSpecificCommonality(Gender.Female) != traitDef.GetGenderSpecificCommonality(Gender.Male))
                    {
                        color = GetColorForRarity((traitDef.GetGenderSpecificCommonality(Gender.Female) + traitDef.GetGenderSpecificCommonality(Gender.Male)) / 2f);
                    }
                    if (!traitDegreeData.label.Contains("<color=#"))
                    {
                        traitDegreeData.label = color + traitDegreeData.label.CapitalizeFirst() + "</color>";
                    }
                    else if (LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().overrideCustomColors)
                    {
                        traitDegreeData.label = traitDegreeData.label.Remove(0, 15).Replace("</color>", "");
                        traitDegreeData.label = color + traitDegreeData.label.CapitalizeFirst() + "</color>";
                    }
                }
            }
        }

        public static string GetColorForRarity(float rarity)
        {

            if (rarity <= 0.1f)
            {
                return "<color=#FF0000>"; // Red
            }
            if (rarity <= 0.5f)
            {
                return "<color=#FF9900>"; // Orange
            }
            if (rarity <= 0.9f)
            {
                return "<color=#CC00FF>"; //Purple
            }
            if (rarity <= 1.9f)
            {
                return "<color=#0066FF>"; //Blue
            }
            if (rarity <= 2.0f)
            {
                return "<color=#00CC00>"; //Green
            }
            return "<color=#FFFFFF>"; //White
        }
    }
}
