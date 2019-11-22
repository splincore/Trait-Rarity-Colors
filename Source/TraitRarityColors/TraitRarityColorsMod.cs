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
            listingStandard.CheckboxLabeled("Override custom colors (needs restart)", ref traitRarityColorsModSettings.overrideCustomColors, "Override the colors that other mods may apply to their traits (changing this setting needs a restart of the game to be applied!)");
            listingStandard.End();
        }

        public override string SettingsCategory()
        {
            return "Trait Rarity Colors";
        }
    }
}
