using UnityEngine;
using Verse;

namespace TraitRarityColors
{
    public class TraitRarityColorsMod : Mod
    {
        TraitRarityColorsModSettings traitRarityColorsModSettings;

        public TraitRarityColorsMod(ModContentPack content) : base(content)
        {
            this.traitRarityColorsModSettings = GetSettings<TraitRarityColorsModSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Label("Changing anything needs a restart of the game to be applied!");

            listingStandard.Gap(10f);
            listingStandard.CheckboxLabeled("Use custom trait tiers", ref traitRarityColorsModSettings.useCustomTraitTiers, "If enabled for teh first time, the default tier for each trait is its current rarity");
            if (traitRarityColorsModSettings.useCustomTraitTiers)
            {
                listingStandard.Gap(10f);
                if (listingStandard.ButtonText("Configure custom trait tiers..."))
                {
                    Find.WindowStack.Add(new TraitRarityColorCustomTierWindow());
                }
            }
            if (!traitRarityColorsModSettings.useCustomTraitTiers)
            {
                listingStandard.Gap(2f);
                listingStandard.CheckboxLabeled("Overwrite trait colors from other mods", ref traitRarityColorsModSettings.ignoreCustomTraitColors, "Not compatible with the custom trait tier option");
            }
            else
            {
                traitRarityColorsModSettings.ignoreCustomTraitColors = true;
            }

            // Mystic
            listingStandard.Gap(10f);
            if (listingStandard.ButtonTextLabeled(traitRarityColorsModSettings.colorMystic + "Mystic traits:</color> " + TraitRarityColors.GetCountForColor(traitRarityColorsModSettings.colorMystic), "Change mystic color"))
            {
                Find.WindowStack.Add(new TraitRarityColorChangeWindow("mystic", traitRarityColorsModSettings.colorMystic, "<color=#FF0000>"));
            }
            if (!traitRarityColorsModSettings.useCustomTraitTiers)
            {
                listingStandard.Gap(5f);
                Rect rectMystic = listingStandard.GetRect(22f);
                traitRarityColorsModSettings.maxLimitMystic = Widgets.HorizontalSlider(rectMystic, traitRarityColorsModSettings.maxLimitMystic, 0f, 3f, false, traitRarityColorsModSettings.colorMystic + "Max commonality:</color> " + (traitRarityColorsModSettings.maxLimitMystic).ToString(), "0", "3", 0.1f);
            }

            // Legendary
            listingStandard.Gap(10f);
            if (listingStandard.ButtonTextLabeled(traitRarityColorsModSettings.colorLegendary + "Legendary traits:</color> " + TraitRarityColors.GetCountForColor(traitRarityColorsModSettings.colorLegendary), "Change legendary color"))
            {
                Find.WindowStack.Add(new TraitRarityColorChangeWindow("legendary", traitRarityColorsModSettings.colorLegendary, "<color=#FF9900>"));
            }
            if (!traitRarityColorsModSettings.useCustomTraitTiers)
            {
                listingStandard.Gap(5f);
                Rect rectLegendary = listingStandard.GetRect(22f);
                traitRarityColorsModSettings.maxLimitLegendary = Widgets.HorizontalSlider(rectLegendary, traitRarityColorsModSettings.maxLimitLegendary, 0f, 3f, false, traitRarityColorsModSettings.colorLegendary + "Max commonality:</color> " + (traitRarityColorsModSettings.maxLimitLegendary).ToString(), "0", "3", 0.1f);
            }
            // Epic
            listingStandard.Gap(10f);
            if (listingStandard.ButtonTextLabeled(traitRarityColorsModSettings.colorEpic +"Epic traits:</color> " + TraitRarityColors.GetCountForColor(traitRarityColorsModSettings.colorEpic), "Change epic color"))
            {
                Find.WindowStack.Add(new TraitRarityColorChangeWindow("epic", traitRarityColorsModSettings.colorEpic, "<color=#cc0099>"));
            }
            if (!traitRarityColorsModSettings.useCustomTraitTiers)
            {
                listingStandard.Gap(5f);
                Rect rectEpic = listingStandard.GetRect(22f);
                traitRarityColorsModSettings.maxLimitEpic = Widgets.HorizontalSlider(rectEpic, traitRarityColorsModSettings.maxLimitEpic, 0f, 3f, false, traitRarityColorsModSettings.colorEpic + "Max commonality:</color> " + (traitRarityColorsModSettings.maxLimitEpic).ToString(), "0", "3", 0.1f);
            }
            // Rare
            listingStandard.Gap(10f);
            if (listingStandard.ButtonTextLabeled(traitRarityColorsModSettings.colorRare + "Rare traits:</color> " + TraitRarityColors.GetCountForColor(traitRarityColorsModSettings.colorRare), "Change rare color"))
            {
                Find.WindowStack.Add(new TraitRarityColorChangeWindow("rare", traitRarityColorsModSettings.colorRare, "<color=#0073e6>"));
            }
            if (!traitRarityColorsModSettings.useCustomTraitTiers)
            {            
                listingStandard.Gap(5f);
                Rect rectRare = listingStandard.GetRect(22f);
                traitRarityColorsModSettings.maxLimitRare = Widgets.HorizontalSlider(rectRare, traitRarityColorsModSettings.maxLimitRare, 0f, 3f, false, traitRarityColorsModSettings.colorRare + "Max commonality:</color> " + (traitRarityColorsModSettings.maxLimitRare).ToString(), "0", "3", 0.1f);
            }
            // Uncommon
            listingStandard.Gap(10f);
            if (listingStandard.ButtonTextLabeled(traitRarityColorsModSettings.colorUncommon + "Uncommon traits:</color> " + TraitRarityColors.GetCountForColor(traitRarityColorsModSettings.colorUncommon), "Change uncommon color"))
            {
                Find.WindowStack.Add(new TraitRarityColorChangeWindow("uncommon", traitRarityColorsModSettings.colorUncommon, "<color=#00ff00>"));
            }
            if (!traitRarityColorsModSettings.useCustomTraitTiers)
            {            
                listingStandard.Gap(5f);
                Rect rectUncommon = listingStandard.GetRect(22f);
                traitRarityColorsModSettings.maxLimitUncommon = Widgets.HorizontalSlider(rectUncommon, traitRarityColorsModSettings.maxLimitUncommon, 0f, 3f, false, traitRarityColorsModSettings.colorUncommon + "Max commonality:</color> " + (traitRarityColorsModSettings.maxLimitUncommon).ToString(), "0", "3", 0.1f);
            }
            // Common
            listingStandard.Gap(10f);
            if (listingStandard.ButtonTextLabeled(traitRarityColorsModSettings.colorCommon + "Common traits:</color> " + TraitRarityColors.GetCountForColor(traitRarityColorsModSettings.colorCommon), "Change common color"))
            {
                Find.WindowStack.Add(new TraitRarityColorChangeWindow("common", traitRarityColorsModSettings.colorCommon, "<color=#FFFFFF>"));
            }

            if (!traitRarityColorsModSettings.useCustomTraitTiers)
            {
                listingStandard.Gap(20);
                if (listingStandard.ButtonText("Reset limits to default"))
                {
                    traitRarityColorsModSettings.maxLimitMystic = 0.1f;
                    traitRarityColorsModSettings.maxLimitLegendary = 0.5f;
                    traitRarityColorsModSettings.maxLimitEpic = 0.9f;
                    traitRarityColorsModSettings.maxLimitRare = 1.9f;
                    traitRarityColorsModSettings.maxLimitUncommon = 2.0f;
                }
            }
            
            listingStandard.Gap(10f);
            if (listingStandard.ButtonText("Reset colors to default"))
            {
                traitRarityColorsModSettings.colorMystic = "<color=#FF0000>";
                traitRarityColorsModSettings.colorLegendary = "<color=#FF9900>";
                traitRarityColorsModSettings.colorEpic = "<color=#cc0099>";
                traitRarityColorsModSettings.colorRare = "<color=#0073e6>";
                traitRarityColorsModSettings.colorUncommon = "<color=#00ff00>";
                traitRarityColorsModSettings.colorCommon = "<color=#FFFFFF>";
            }

            listingStandard.End();
        }

        public override string SettingsCategory()
        {
            return "Trait Rarity Colors";
        }

        private Vector2 scrollPosition = Vector2.zero;
    }
}
