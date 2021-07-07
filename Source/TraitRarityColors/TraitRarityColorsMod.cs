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
            listingStandard.Label("TraitRarityWarningLabel".Translate(), -1f, "TraitRarityWarningDescription".Translate());

            listingStandard.Gap(10f);
            listingStandard.CheckboxLabeled("TraitRarityCustomTierLabel".Translate(), ref traitRarityColorsModSettings.useCustomTraitTiers, "TraitRarityCustomTierDescription".Translate());
            if (traitRarityColorsModSettings.useCustomTraitTiers)
            {
                listingStandard.Gap(10f);
                if (listingStandard.ButtonText("TraitRarityCustomTierConfigurationLabel".Translate()))
                {
                    Find.WindowStack.Add(new TraitRarityColorCustomTierWindow());
                }
            }
            if (!traitRarityColorsModSettings.useCustomTraitTiers)
            {
                listingStandard.Gap(2f);
                listingStandard.CheckboxLabeled("TraitRarityOverwriteLabel".Translate(), ref traitRarityColorsModSettings.ignoreCustomTraitColors, "TraitRarityCustomTierConfigurationDescription".Translate());
            }
            else
            {
                traitRarityColorsModSettings.ignoreCustomTraitColors = true;
            }

            // Mystic
            listingStandard.Gap(10f);
            if (listingStandard.ButtonTextLabeled(string.Format("{0}{1}</color>: {2}", traitRarityColorsModSettings.colorMystic, "TraitRarityCountMystic".Translate(), TraitRarityColors.GetCountForColor(traitRarityColorsModSettings.colorMystic)), "TraitRarityChangeColorMystic".Translate()))
            {
                Find.WindowStack.Add(new TraitRarityColorChangeWindow("mystic", traitRarityColorsModSettings.colorMystic, "<color=#FF0000>"));
            }
            if (!traitRarityColorsModSettings.useCustomTraitTiers)
            {
                listingStandard.Gap(5f);
                Rect rectMystic = listingStandard.GetRect(22f);
                traitRarityColorsModSettings.maxLimitMystic = Widgets.HorizontalSlider(rectMystic, traitRarityColorsModSettings.maxLimitMystic, 0f, 3f, false, string.Format("{0}{1}</color>: {2}", traitRarityColorsModSettings.colorMystic, "TraitRarityMaxMommonality".Translate(), traitRarityColorsModSettings.maxLimitMystic.ToString()), "0", "3", 0.1f);
            }

            // Legendary
            listingStandard.Gap(10f);
            if (listingStandard.ButtonTextLabeled(string.Format("{0}{1}</color>: {2}", traitRarityColorsModSettings.colorLegendary , "TraitRarityCountLegendary".Translate(), TraitRarityColors.GetCountForColor(traitRarityColorsModSettings.colorLegendary)), "TraitRarityChangeColorLegendary".Translate()))
            {
                Find.WindowStack.Add(new TraitRarityColorChangeWindow("legendary", traitRarityColorsModSettings.colorLegendary, "<color=#FF9900>"));
            }
            if (!traitRarityColorsModSettings.useCustomTraitTiers)
            {
                listingStandard.Gap(5f);
                Rect rectLegendary = listingStandard.GetRect(22f);
                traitRarityColorsModSettings.maxLimitLegendary = Widgets.HorizontalSlider(rectLegendary, traitRarityColorsModSettings.maxLimitLegendary, 0f, 3f, false, string.Format("{0}{1}</color>: {2}", traitRarityColorsModSettings.colorLegendary, "TraitRarityMaxMommonality".Translate(), traitRarityColorsModSettings.maxLimitLegendary.ToString()), "0", "3", 0.1f);
            }
            // Epic
            listingStandard.Gap(10f);
            if (listingStandard.ButtonTextLabeled(string.Format("{0}{1}</color>: {2}", traitRarityColorsModSettings.colorEpic, "TraitRarityCountEpic".Translate(), TraitRarityColors.GetCountForColor(traitRarityColorsModSettings.colorEpic)), "TraitRarityChangeColorEpic".Translate()))
            {
                Find.WindowStack.Add(new TraitRarityColorChangeWindow("epic", traitRarityColorsModSettings.colorEpic, "<color=#cc0099>"));
            }
            if (!traitRarityColorsModSettings.useCustomTraitTiers)
            {
                listingStandard.Gap(5f);
                Rect rectEpic = listingStandard.GetRect(22f);
                traitRarityColorsModSettings.maxLimitEpic = Widgets.HorizontalSlider(rectEpic, traitRarityColorsModSettings.maxLimitEpic, 0f, 3f, false, string.Format("{0}{1}</color>: {2}", traitRarityColorsModSettings.colorEpic, "TraitRarityMaxMommonality".Translate(), traitRarityColorsModSettings.maxLimitEpic.ToString()), "0", "3", 0.1f);
            }
            // Rare
            listingStandard.Gap(10f);
            if (listingStandard.ButtonTextLabeled(string.Format("{0}{1}</color>: {2}", traitRarityColorsModSettings.colorRare, "TraitRarityCountRare".Translate(), TraitRarityColors.GetCountForColor(traitRarityColorsModSettings.colorRare)), "TraitRarityChangeColorRare".Translate()))
            {
                Find.WindowStack.Add(new TraitRarityColorChangeWindow("rare", traitRarityColorsModSettings.colorRare, "<color=#0073e6>"));
            }
            if (!traitRarityColorsModSettings.useCustomTraitTiers)
            {            
                listingStandard.Gap(5f);
                Rect rectRare = listingStandard.GetRect(22f);
                traitRarityColorsModSettings.maxLimitRare = Widgets.HorizontalSlider(rectRare, traitRarityColorsModSettings.maxLimitRare, 0f, 3f, false, string.Format("{0}{1}</color>: {2}", traitRarityColorsModSettings.colorRare, "TraitRarityMaxMommonality".Translate(), traitRarityColorsModSettings.maxLimitRare.ToString()), "0", "3", 0.1f);
            }
            // Uncommon
            listingStandard.Gap(10f);
            if (listingStandard.ButtonTextLabeled(string.Format("{0}{1}</color>: {2}", traitRarityColorsModSettings.colorUncommon, "TraitRarityCountUncommon".Translate(), TraitRarityColors.GetCountForColor(traitRarityColorsModSettings.colorUncommon)), "TraitRarityChangeColorUncommon".Translate()))
            {
                Find.WindowStack.Add(new TraitRarityColorChangeWindow("uncommon", traitRarityColorsModSettings.colorUncommon, "<color=#00ff00>"));
            }
            if (!traitRarityColorsModSettings.useCustomTraitTiers)
            {            
                listingStandard.Gap(5f);
                Rect rectUncommon = listingStandard.GetRect(22f);
                traitRarityColorsModSettings.maxLimitUncommon = Widgets.HorizontalSlider(rectUncommon, traitRarityColorsModSettings.maxLimitUncommon, 0f, 3f, false, string.Format("{0}{1}</color>: {2}", traitRarityColorsModSettings.colorUncommon, "TraitRarityMaxMommonality".Translate(), traitRarityColorsModSettings.maxLimitUncommon.ToString()), "0", "3", 0.1f);
            }
            // Common
            listingStandard.Gap(10f);
            if (listingStandard.ButtonTextLabeled(string.Format("{0}{1}</color>: {2}", traitRarityColorsModSettings.colorCommon, "TraitRarityCountCommon".Translate(), TraitRarityColors.GetCountForColor(traitRarityColorsModSettings.colorCommon)), "TraitRarityChangeColorCommon".Translate()))
            {
                Find.WindowStack.Add(new TraitRarityColorChangeWindow("common", traitRarityColorsModSettings.colorCommon, "<color=#FFFFFF>"));
            }

            if (!traitRarityColorsModSettings.useCustomTraitTiers)
            {
                listingStandard.Gap(20);
                if (listingStandard.ButtonText("TraitRarityResetLimitsLabel".Translate()))
                {
                    traitRarityColorsModSettings.maxLimitMystic = 0.1f;
                    traitRarityColorsModSettings.maxLimitLegendary = 0.5f;
                    traitRarityColorsModSettings.maxLimitEpic = 0.9f;
                    traitRarityColorsModSettings.maxLimitRare = 1.9f;
                    traitRarityColorsModSettings.maxLimitUncommon = 2.0f;
                }
            }
            
            listingStandard.Gap(10f);
            if (listingStandard.ButtonText("TraitRarityResetColorsLabel".Translate()))
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
