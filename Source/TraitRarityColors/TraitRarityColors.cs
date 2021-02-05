using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace TraitRarityColors
{
    [StaticConstructorOnStartup]
    internal static class TraitRarityColors
    {
        static TraitRarityColors()
        {
            HashSet<string> traitsToIgnore = new HashSet<string>();
            foreach (TraitDef traitDef in DefDatabase<TraitDef>.AllDefsListForReading)
            {
                foreach (TraitDegreeData traitDegreeData in traitDef.degreeDatas)
                {
                    if (traitDegreeData.label.Contains("<color=#"))
                    {
                        traitsToIgnore.Add(traitDegreeData.label);
                    }
                }
            }
            LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().traitsToIgnore = traitsToIgnore;
            RefreshColors();
        }

        public static int GetCountForColor(string color)
        {
            RefreshColors();
            return DefDatabase<TraitDef>.AllDefsListForReading.SelectMany(t => t.degreeDatas).Where(d => d.label.Contains(color)).Count();
        }

        public static void IncreaseTierFor(string traitLabel)
        {
            Dictionary<string, int> traitTiers = LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().traitTiers;
            string cleanLabel = traitLabel.Remove(0, 15).Replace("</color>", "");
            traitTiers[cleanLabel] = Math.Max(traitTiers[cleanLabel] - 1, 1);
            LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().traitTiers = traitTiers;
        }

        public static void LowerTierFor(string traitLabel)
        {
            Dictionary<string, int> traitTiers = LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().traitTiers;
            string cleanLabel = traitLabel.Remove(0, 15).Replace("</color>", "");
            traitTiers[cleanLabel] = Math.Min(traitTiers[cleanLabel] + 1, 6);
            LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().traitTiers = traitTiers;
        }

        private static void RefreshColors()
        {
            bool ignoreCustomTraitColors = LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().ignoreCustomTraitColors;
            if (LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().useCustomTraitTiers) ignoreCustomTraitColors = true;
            HashSet<string> traitsToIgnore = LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().traitsToIgnore;
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
                    else if (ignoreCustomTraitColors || !traitsToIgnore.Contains(traitDegreeData.label))
                    {
                        traitDegreeData.label = traitDegreeData.label.Remove(0, 15).Replace("</color>", "");
                        traitDegreeData.label = color + traitDegreeData.label.CapitalizeFirst() + "</color>";
                    }
                }
            }

            if (LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().useCustomTraitTiers)
            {
                Dictionary<string, int> traitTiers = LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().traitTiers;
                foreach (TraitDef traitDef in DefDatabase<TraitDef>.AllDefsListForReading)
                {
                    foreach (TraitDegreeData traitDegreeData in traitDef.degreeDatas)
                    {
                        string cleanLabel = traitDegreeData.label.Remove(0, 15).Replace("</color>", "");
                        if (traitTiers.ContainsKey(cleanLabel))
                        {
                            traitDegreeData.label = GetColorForTier(traitTiers[cleanLabel]) + cleanLabel.CapitalizeFirst() + "</color>";
                        }
                        else
                        {
                            int tier = GetTierForColor(traitDegreeData.label.Substring(0, 15));
                            traitTiers.Add(cleanLabel, tier);
                            traitDegreeData.label = GetColorForTier(traitTiers[cleanLabel]) + cleanLabel.CapitalizeFirst() + "</color>";
                        }
                    }
                }
                LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().traitTiers = traitTiers;
            }
        }
        public static int GetTierForColor(string color)
        {
            if (color == LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorMystic)
            {
                return 1;
            }
            if (color == LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorLegendary)
            {
                return 2;
            }
            if (color == LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorEpic)
            {
                return 3;
            }
            if (color == LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorRare)
            {
                return 4;
            }
            if (color == LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorUncommon)
            {
                return 5;
            }
            return 6;
        }

        public static string GetColorForTier(int tier)
        {
            switch(tier)
            {
                case 1:
                    return LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorMystic;
                case 2:
                    return LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorLegendary;
                case 3:
                    return LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorEpic;
                case 4:
                    return LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorRare;
                case 5:
                    return LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorUncommon;
                case 6:
                    return LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorCommon;
                default:
                    return "<color=#FFFFFF>";
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
