using System.Linq;
using UnityEngine;
using Verse;

namespace TraitRarityColors
{
    static class TraitRarityOverlayDrawer
    {
        public static void TraitRarityStatsOnGUI()
        {
            if (Event.current.type != EventType.Repaint || !TraitRarityOverlayDrawer.ShouldShowWindowNow())
            {
                return;
            }
            CellRect currentViewRect = Find.CameraDriver.CurrentViewRect;
            foreach (Pawn pawn in Find.CurrentMap.mapPawns.AllPawnsSpawned)
            {
                if (pawn.RaceProps.Humanlike && currentViewRect.Contains(pawn.Position) && !pawn.Position.Fogged(Find.CurrentMap)) TraitRarityOverlayDrawer.DrawInfowWindow(pawn);
            }
        }

        public static bool ShouldShowWindowNow()
        {
            if (!TraitRarityUISetting.showTraitUI)
            {
                return false;
            }
            return true;
        }

        public static void DrawInfowWindow(Pawn pawn)
        {
            string text = pawn.LabelCap + ": " + string.Join(", ", pawn.story.traits.allTraits.Select(t => t.CurrentData.label));
            float cellSizePixels = Find.CameraDriver.CellSizePixels;
            Text.Font = GameFont.Small;
            Vector2 vector = new Vector2(cellSizePixels, cellSizePixels);
            Vector2 vectorText = Text.CalcSize(text);
            Rect rect = new Rect(0f, 0f, vectorText.x + 36f, Text.CalcHeight(text, vectorText.x) + 18f);
            Vector2 vector2 = pawn.DrawPos.MapToUIPosition();
            rect.x = vector2.x - vector.x / 2f;
            rect.y = vector2.y + vector.x / 2f;
            
            Find.WindowStack.ImmediateWindow(1751884355 + pawn.thingIDNumber, rect, WindowLayer.GameUI, delegate
            {
                TraitRarityOverlayDrawer.FillWindow(rect, text);
            }, true, false, 1f);
        }

        public static void FillWindow(Rect windowRect, string text)
        {
            Text.Font = GameFont.Small;
            float num = 9f;            
            Rect rect = new Rect(18f, num, windowRect.width - 36f, windowRect.height - 18f);
            GUI.color = Color.white;
            Widgets.Label(rect, text);
        }
    }
}
