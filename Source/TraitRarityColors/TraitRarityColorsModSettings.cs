using System.Collections.Generic;
using Verse;

namespace TraitRarityColors
{
    public class TraitRarityColorsModSettings : ModSettings
    {
        public bool ignoreCustomTraitColors = true;
        public HashSet<string> traitsToIgnore = new HashSet<string>();

        public float maxLimitMystic = 0.1f;
        public float maxLimitLegendary = 0.5f;
        public float maxLimitEpic = 0.9f;
        public float maxLimitRare = 1.9f;
        public float maxLimitUncommon = 2.0f;
        // common is the rest

        public string colorMystic = "<color=#FF0000>";
        public string colorLegendary = "<color=#FF9900>";
        public string colorEpic = "<color=#cc0099>";
        public string colorRare = "<color=#0073e6>";
        public string colorUncommon = "<color=#00ff00>";
        public string colorCommon = "<color=#FFFFFF>";

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<bool>(ref ignoreCustomTraitColors, "ignoreCustomTraitColors", true);
            Scribe_Collections.Look(ref traitsToIgnore, false, "traitsToIgnore");
            if (traitsToIgnore == null)
            {
                traitsToIgnore = new HashSet<string>();
            }

            Scribe_Values.Look<float>(ref maxLimitMystic, "maxLimitMystic", 0.1f, false);
            Scribe_Values.Look<float>(ref maxLimitLegendary, "maxLimitLegendary", 0.5f, false);
            Scribe_Values.Look<float>(ref maxLimitEpic, "maxLimitEpic", 0.9f, false);
            Scribe_Values.Look<float>(ref maxLimitRare, "maxLimitRare", 1.9f, false);
            Scribe_Values.Look<float>(ref maxLimitUncommon, "maxLimitUncommon", 2.0f, false);

            Scribe_Values.Look<string>(ref colorMystic, "colorMystic", "<color=#FF0000>", false);
            Scribe_Values.Look<string>(ref colorLegendary, "colorLegendary", "<color=#FF9900>", false);
            Scribe_Values.Look<string>(ref colorEpic, "colorEpic", "<color=#cc0099>", false);
            Scribe_Values.Look<string>(ref colorRare, "colorRare", "<color=#0073e6>", false);
            Scribe_Values.Look<string>(ref colorUncommon, "colorUncommon", "<color=#00ff00>", false);
            Scribe_Values.Look<string>(ref colorCommon, "colorCommon", "<color=#FFFFFF>", false);
        }
    }
}
