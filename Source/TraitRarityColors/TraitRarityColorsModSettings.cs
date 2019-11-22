using Verse;

namespace TraitRarityColors
{
    public class TraitRarityColorsModSettings : ModSettings
    {
        public bool overrideCustomColors = true;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<bool>(ref overrideCustomColors, "overrideCustomColors", true, false);
        }
    }
}
