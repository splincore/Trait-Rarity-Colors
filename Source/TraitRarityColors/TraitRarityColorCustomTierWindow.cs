using UnityEngine;
using Verse;
using RimWorld;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TraitRarityColors
{
    public class TraitRarityColorCustomTierWindow : Window
    {
        public override Vector2 InitialSize
        {
            get
            {
                return this.WindowSize;
            }
        }
        protected Vector2 WindowSize = new Vector2(600f, 800f); // Border Size is 18
        private Vector2 scrollPosition = Vector2.zero;
		private Dictionary<string, string> tipStrings = new Dictionary<string, string>();
        public TraitRarityColorCustomTierWindow()
        {
            
		}

        public override void DoWindowContents(Rect inRect)
        {
            Text.Anchor = TextAnchor.MiddleCenter;
            Rect mysticRect = new Rect(inRect.width * (5f / 6f), 0, inRect.width / 6, 48);
            Widgets.Label(mysticRect.TopHalf(), string.Format("{0}{1}</color>", LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorMystic, "TraitRarityMystic".Translate()));
            Widgets.Label(mysticRect.BottomHalf(), TraitRarityColors.GetCountForColor(LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorMystic).ToString());
            Rect legendaryRect = new Rect(inRect.width * (4f / 6f), 0, inRect.width / 6, 48);
            Widgets.Label(legendaryRect.TopHalf(), string.Format("{0}{1}</color>", LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorLegendary, "TraitRarityLegendary".Translate()));
            Widgets.Label(legendaryRect.BottomHalf(), TraitRarityColors.GetCountForColor(LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorLegendary).ToString());
            Rect epicRect = new Rect(inRect.width * (3f / 6f), 0, inRect.width / 6, 48);
            Widgets.Label(epicRect.TopHalf(), string.Format("{0}{1}</color>", LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorEpic, "TraitRarityEpic".Translate()));
            Widgets.Label(epicRect.BottomHalf(), TraitRarityColors.GetCountForColor(LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorEpic).ToString());
            Rect rareRect = new Rect(inRect.width * (2f / 6f), 0, inRect.width / 6, 48);
            Widgets.Label(rareRect.TopHalf(), string.Format("{0}{1}</color>", LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorRare, "TraitRarityRare".Translate()));
            Widgets.Label(rareRect.BottomHalf(), TraitRarityColors.GetCountForColor(LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorRare).ToString());
            Rect uncommonRect = new Rect(inRect.width * (1f / 6f), 0, inRect.width / 6, 48);
            Widgets.Label(uncommonRect.TopHalf(), string.Format("{0}{1}</color>", LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorUncommon, "TraitRarityUncommon".Translate()));
            Widgets.Label(uncommonRect.BottomHalf(), TraitRarityColors.GetCountForColor(LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorUncommon).ToString());
            Rect commonRect = new Rect(inRect.width * (0f / 6f), 0, inRect.width / 6, 48);
            Widgets.Label(commonRect.TopHalf(), string.Format("{0}{1}</color>", LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorCommon, "TraitRarityCommon".Translate()));
            Widgets.Label(commonRect.BottomHalf(), TraitRarityColors.GetCountForColor(LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorCommon).ToString());
            Text.Anchor = TextAnchor.UpperLeft;

            Widgets.DrawLineHorizontal(0f, 52f, inRect.width);
            Rect rect = inRect.AtZero().ContractedBy(7f);
            rect.position = new Vector2(rect.x, rect.y + 56f);
            rect.height -= 56; // 48 distance from top with labels and line with 4 above/below
            rect.height -= 49f; // distance from bottom with buttons

            float viewRectHeight = 0;
            foreach (TraitDef traitDef in DefDatabase<TraitDef>.AllDefsListForReading)
            {
                foreach (TraitDegreeData traitDegreeData in traitDef.degreeDatas)
                {
                    viewRectHeight += 30; // height per trait option
                }
            }
            Rect viewRect = rect.AtZero();
            viewRect.height = viewRectHeight;
            viewRect.width -= GUI.skin.verticalScrollbar.fixedWidth;
            viewRect.width -= GUI.skin.verticalScrollbar.margin.left;
            scrollPosition = GUI.BeginScrollView(rect, scrollPosition, viewRect, false, true);
            int currentY = 0;
            foreach (TraitDef traitDef in DefDatabase<TraitDef>.AllDefsListForReading)
            {
                foreach (TraitDegreeData traitDegreeData in traitDef.degreeDatas)
                {
                    Rect traitRect = new Rect(0, currentY, viewRect.width, 30);
                    Rect labelRect = new Rect(traitRect.width / 4, currentY, traitRect.width / 2, 30);
                    Text.Anchor = TextAnchor.MiddleCenter;
                    Widgets.Label(labelRect, traitDegreeData.label);
                    Text.Anchor = TextAnchor.UpperLeft;
                    TooltipHandler.TipRegion(labelRect, TipString(traitDef, traitDegreeData));
                    Rect buttonRect = traitRect.RightHalf();
                    if (Widgets.ButtonText(traitRect.LeftHalf().LeftHalf(), "TraitRarityLowerTierLabel".Translate())) TraitRarityColors.LowerTierFor(traitDegreeData.label);
                    if (Widgets.ButtonText(traitRect.RightHalf().RightHalf(), "TraitRarityLowerHigherLabel".Translate())) TraitRarityColors.IncreaseTierFor(traitDegreeData.label);
                    currentY += 30;
                }
            }
            GUI.EndScrollView();
            Vector2 buttonSize = new Vector2(120f, 40f);
            float positionButtonY = InitialSize.y - buttonSize.y - 36f;
            float positionOk = InitialSize.x / 2 - buttonSize.x - 5f - 18f;
            float positionReset = positionOk + buttonSize.x + 10f;
            Rect rectOk = new Rect(positionOk, positionButtonY, buttonSize.x, buttonSize.y);
            Rect rectReset = new Rect(positionReset, positionButtonY, buttonSize.x, buttonSize.y);

            if (Widgets.ButtonText(rectOk, "OK".Translate()))
            {
                this.Close(true);
                Event.current.Use();
            }
            if (Widgets.ButtonText(rectReset, "TraitRarityResetButtonLabel".Translate()))
            {
                LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().traitTiers = new Dictionary<string, int>();
            }
        }

        public string TipString(TraitDef traitDef, TraitDegreeData traitDegreeData)
        {
			string traitKey = traitDef.defName + "_" + traitDegreeData.degree.ToString();
			if (tipStrings.ContainsKey(traitKey)) return tipStrings[traitKey];
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(traitDegreeData.description);
			bool flag = traitDegreeData.skillGains.Count > 0;
			bool flag2 = GetPermaThoughts(traitDef, traitDegreeData).Any<ThoughtDef>();
			bool flag3 = traitDegreeData.statOffsets != null;
			bool flag4 = traitDegreeData.statFactors != null;
			if (flag || flag2 || flag3 || flag4)
			{
				stringBuilder.AppendLine();
			}
			if (flag)
			{
				foreach (KeyValuePair<SkillDef, int> keyValuePair in traitDegreeData.skillGains)
				{
					if (keyValuePair.Value != 0)
					{
						string value = "    " + keyValuePair.Key.skillLabel.CapitalizeFirst() + ":   " + keyValuePair.Value.ToString("+##;-##");
						stringBuilder.AppendLine(value);
					}
				}
			}
			if (flag2)
			{
				foreach (ThoughtDef thoughtDef in GetPermaThoughts(traitDef, traitDegreeData))
				{
					stringBuilder.AppendLine("    " + "PermanentMoodEffect".Translate() + " " + thoughtDef.stages[0].baseMoodEffect.ToStringByStyle(ToStringStyle.Integer, ToStringNumberSense.Offset));
				}
			}
			if (flag3)
			{
				for (int i = 0; i < traitDegreeData.statOffsets.Count; i++)
				{
					StatModifier statModifier = traitDegreeData.statOffsets[i];
					string valueToStringAsOffset = statModifier.ValueToStringAsOffset;
					string value2 = "    " + statModifier.stat.LabelCap + " " + valueToStringAsOffset;
					stringBuilder.AppendLine(value2);
				}
			}
			if (flag4)
			{
				for (int j = 0; j < traitDegreeData.statFactors.Count; j++)
				{
					StatModifier statModifier2 = traitDegreeData.statFactors[j];
					string toStringAsFactor = statModifier2.ToStringAsFactor;
					string value3 = "    " + statModifier2.stat.LabelCap + " " + toStringAsFactor;
					stringBuilder.AppendLine(value3);
				}
			}
			if (traitDegreeData.hungerRateFactor != 1f)
			{
				string t = traitDegreeData.hungerRateFactor.ToStringByStyle(ToStringStyle.PercentOne, ToStringNumberSense.Factor);
				string value4 = "    " + "HungerRate".Translate() + " " + t;
				stringBuilder.AppendLine(value4);
			}
			if (ModsConfig.RoyaltyActive)
			{
				List<MeditationFocusDef> allowedMeditationFocusTypes = traitDegreeData.allowedMeditationFocusTypes;
				if (!allowedMeditationFocusTypes.NullOrEmpty<MeditationFocusDef>())
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendLine("EnablesMeditationFocusType".Translate().Colorize(ColoredText.TipSectionTitleColor) + ":\n" + (from f in allowedMeditationFocusTypes select f.LabelCap.Resolve()).ToLineList("  - ", false));
				}
			}
			if (ModsConfig.IdeologyActive)
			{
				List<IssueDef> affectedIssues = traitDegreeData.GetAffectedIssues(traitDef);
				if (affectedIssues.Count != 0)
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendLine("OverridesSomePrecepts".Translate().Colorize(ColoredText.TipSectionTitleColor) + ":\n" + (from x in affectedIssues select x.LabelCap.Resolve()).ToLineList("  - ", false));
				}
				List<MemeDef> affectedMemes = traitDegreeData.GetAffectedMemes(traitDef, true);
				if (affectedMemes.Count > 0)
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendLine("AgreeableMemes".Translate().Colorize(ColoredText.TipSectionTitleColor) + ":\n" + (from x in affectedMemes select x.LabelCap.Resolve()).ToLineList("  - ", false));
				}
				List<MemeDef> affectedMemes2 = traitDegreeData.GetAffectedMemes(traitDef, false);
				if (affectedMemes2.Count > 0)
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendLine("DisagreeableMemes".Translate().Colorize(ColoredText.TipSectionTitleColor) + ":\n" + (from x in affectedMemes2 select x.LabelCap.Resolve()).ToLineList("  - ", false));
				}
			}
			if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '\n')
			{
				if (stringBuilder.Length > 1 && stringBuilder[stringBuilder.Length - 2] == '\r')
				{
					stringBuilder.Remove(stringBuilder.Length - 2, 2);
				}
				else
				{
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
				}
			}
			string tipString = stringBuilder.ToString();
			tipStrings.Add(traitKey, tipString);
			return tipString;
		}

		private IEnumerable<ThoughtDef> GetPermaThoughts(TraitDef traitDef, TraitDegreeData traitDegreeData)
		{
			List<ThoughtDef> allThoughts = DefDatabase<ThoughtDef>.AllDefsListForReading;
			int num;
			for (int i = 0; i < allThoughts.Count; i = num + 1)
			{
				if (allThoughts[i].IsSituational && allThoughts[i].Worker is ThoughtWorker_AlwaysActive && allThoughts[i].requiredTraits != null && allThoughts[i].requiredTraits.Contains(traitDef) && (!allThoughts[i].RequiresSpecificTraitsDegree || allThoughts[i].requiredTraitsDegree == traitDegreeData.degree))
				{
					yield return allThoughts[i];
				}
				num = i;
			}
			yield break;
		}
	}
}
