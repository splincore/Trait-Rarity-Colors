using Verse;

namespace TraitRarityColors
{
    public class TraitRarityColorsModSettings : ModSettings
    {
        public float maxLimitMystic = 0.1f;
        public float maxLimitLegendary = 0.5f;
        public float maxLimitEpic = 0.9f;
        public float maxLimitRare = 1.9f;
        public float maxLimitUncommon = 2.0f;
        // common is the rest

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<float>(ref maxLimitMystic, "maxLimitMystic", 0.1f, false);
            Scribe_Values.Look<float>(ref maxLimitLegendary, "maxLimitLegendary", 0.5f, false);
            Scribe_Values.Look<float>(ref maxLimitEpic, "maxLimitEpic", 0.9f, false);
            Scribe_Values.Look<float>(ref maxLimitRare, "maxLimitRare", 1.9f, false);
            Scribe_Values.Look<float>(ref maxLimitUncommon, "maxLimitUncommon", 2.0f, false);
        }
    }
}
