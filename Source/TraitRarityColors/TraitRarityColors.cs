using RimWorld;
using System.Linq;
using Verse;

namespace TraitRarityColors
{
    [StaticConstructorOnStartup]
    internal static class TraitRarityColors
    {
        static TraitRarityColors()
        {
            RefreshColors();
        }

        public static int GetCountForColor(string color)
        {
            RefreshColors();
            return DefDatabase<TraitDef>.AllDefsListForReading.SelectMany(t => t.degreeDatas).Where(d => d.label.Contains(color)).Count();
        }

        private static void RefreshColors()
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
                    else
                    {
                        traitDegreeData.label = traitDegreeData.label.Remove(0, 15).Replace("</color>", "");
                        traitDegreeData.label = color + traitDegreeData.label.CapitalizeFirst() + "</color>";
                    }
                }
            }
        }

        public static string GetColorForRarity(float rarity)
        {

            if (rarity <= LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().maxLimitMystic) // Default: 0.1f
            {
                return "<color=#FF0000>"; // Mystic: Red
            }
            if (rarity <= LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().maxLimitLegendary) // Default: 0.5f
            {
                return "<color=#FF9900>"; // Legendary: Orange
            }
            if (rarity <= LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().maxLimitEpic) // Default: 0.9f
            {
                return "<color=#cc0099>"; // Epic: Purple
            }
            if (rarity <= LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().maxLimitRare) // Default: 1.9f
            {
                return "<color=#0073e6>"; // Rare: Blue
            }
            if (rarity <= LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().maxLimitUncommon) // Default: 2.0f
            {
                return "<color=#00ff00>"; // Uncommon: Green
            }
            return "<color=#FFFFFF>"; // Common: White
        }
    }
}
