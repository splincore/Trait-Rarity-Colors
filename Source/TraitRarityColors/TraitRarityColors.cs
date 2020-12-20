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
                return LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorMystic; // Mystic: Red <color=#FF0000>
            }
            if (rarity <= LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().maxLimitLegendary) // Default: 0.5f
            {
                return LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorLegendary; // Legendary: Orange <color=#FF9900>
            }
            if (rarity <= LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().maxLimitEpic) // Default: 0.9f
            {
                return LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorEpic; // Epic: Purple <color=#cc0099>
            }
            if (rarity <= LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().maxLimitRare) // Default: 1.9f
            {
                return LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorRare; // Rare: Blue <color=#0073e6>
            }
            if (rarity <= LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().maxLimitUncommon) // Default: 2.0f
            {
                return LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorUncommon; // Uncommon: Green <color=#00ff00>
            }
            return LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorCommon; // Common: White <color=#FFFFFF>
        }
    }
}
