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
            listingStandard.Label("<color=#FF0000>Mystic traits:</color> " + TraitRarityColors.GetCountForColor("#FF0000"), -1f, "Per default mystic traits are not in the vanilla game and are inteded for ultra rare traits, that other mods may add.");
            listingStandard.Gap(listingStandard.verticalSpacing);
            Rect rectMystic = listingStandard.GetRect(22f);
            traitRarityColorsModSettings.maxLimitMystic = Widgets.HorizontalSlider(rectMystic, traitRarityColorsModSettings.maxLimitMystic, 0f, 3f, false, "<color=#FF0000>Max commonality:</color> " + (traitRarityColorsModSettings.maxLimitMystic).ToString(), "0", "3", 0.1f);

            listingStandard.Gap(10f);
            listingStandard.Label("<color=#FF9900>Legendary traits:</color> " + TraitRarityColors.GetCountForColor("#FF9900"));
            listingStandard.Gap(listingStandard.verticalSpacing);
            Rect rectLegendary = listingStandard.GetRect(22f);
            traitRarityColorsModSettings.maxLimitLegendary = Widgets.HorizontalSlider(rectLegendary, traitRarityColorsModSettings.maxLimitLegendary, 0f, 3f, false, "<color=#FF9900>Max commonality:</color> " + (traitRarityColorsModSettings.maxLimitLegendary).ToString(), "0", "3", 0.1f);

            listingStandard.Gap(10f);
            listingStandard.Label("<color=#cc0099>Epic traits:</color> " + TraitRarityColors.GetCountForColor("#cc0099"));
            listingStandard.Gap(listingStandard.verticalSpacing);
            Rect rectEpic = listingStandard.GetRect(22f);
            traitRarityColorsModSettings.maxLimitEpic = Widgets.HorizontalSlider(rectEpic, traitRarityColorsModSettings.maxLimitEpic, 0f, 3f, false, "<color=#cc0099>Max commonality:</color> " + (traitRarityColorsModSettings.maxLimitEpic).ToString(), "0", "3", 0.1f);

            listingStandard.Gap(10f);
            listingStandard.Label("<color=#0073e6>Rare traits:</color> " + TraitRarityColors.GetCountForColor("#0073e6"));
            listingStandard.Gap(listingStandard.verticalSpacing);
            Rect rectRare = listingStandard.GetRect(22f);
            traitRarityColorsModSettings.maxLimitRare = Widgets.HorizontalSlider(rectRare, traitRarityColorsModSettings.maxLimitRare, 0f, 3f, false, "<color=#0073e6>Max commonality:</color> " + (traitRarityColorsModSettings.maxLimitRare).ToString(), "0", "3", 0.1f);

            listingStandard.Gap(10f);
            listingStandard.Label("<color=#00ff00>Uncommon traits:</color> " + TraitRarityColors.GetCountForColor("#00ff00"));
            listingStandard.Gap(listingStandard.verticalSpacing);
            Rect rectUncommon = listingStandard.GetRect(22f);
            traitRarityColorsModSettings.maxLimitUncommon = Widgets.HorizontalSlider(rectUncommon, traitRarityColorsModSettings.maxLimitUncommon, 0f, 3f, false, "<color=#00ff00>Max commonality:</color> " + (traitRarityColorsModSettings.maxLimitUncommon).ToString(), "0", "3", 0.1f);

            listingStandard.Gap(10f);
            listingStandard.Label("<color=#FFFFFF>Common traits:</color> " + TraitRarityColors.GetCountForColor("#FFFFFF"));

            listingStandard.Gap(10f);
            if (listingStandard.ButtonText("Reset to default"))
            {
                traitRarityColorsModSettings.maxLimitMystic = 0.1f;
                traitRarityColorsModSettings.maxLimitLegendary = 0.5f;
                traitRarityColorsModSettings.maxLimitEpic = 0.9f;
                traitRarityColorsModSettings.maxLimitRare = 1.9f;
                traitRarityColorsModSettings.maxLimitUncommon = 2.0f;
            }
            
            listingStandard.End();
        }

        public override string SettingsCategory()
        {
            return "Trait Rarity Colors";
        }
    }
}
