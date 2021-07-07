using UnityEngine;
using Verse;
using RimWorld;
using System.Collections.Generic;

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
                    TooltipHandler.TipRegion(labelRect, traitDegreeData.description);
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
    }
}
