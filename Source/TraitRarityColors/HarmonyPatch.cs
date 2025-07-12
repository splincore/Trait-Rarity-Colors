using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace TraitRarityColors
{
    [StaticConstructorOnStartup]
    static class HarmonyPatch
    {
        static HarmonyPatch()
        {
            var harmony = new Harmony("rimworld.carnysenpai.traitraritycolors");
            harmony.Patch(AccessTools.Method(typeof(Trait), "get_Label"), null, new HarmonyMethod(typeof(HarmonyPatch).GetMethod("Get_Label_PostFix")), null);
            harmony.Patch(AccessTools.Method(typeof(PlaySettings), "DoPlaySettingsGlobalControls"), null, new HarmonyMethod(typeof(HarmonyPatch).GetMethod("DoPlaySettingsGlobalControls_PostFix")), null);
            harmony.Patch(AccessTools.Method(typeof(PlaySettings), "ExposeData"), null, new HarmonyMethod(typeof(HarmonyPatch).GetMethod("ExposeData_PostFix")), null);
            harmony.Patch(AccessTools.Method(typeof(MapInterface), "MapInterfaceOnGUI_AfterMainTabs"), null, new HarmonyMethod(typeof(HarmonyPatch).GetMethod("MapInterfaceOnGUI_AfterMainTabs_PostFix")), null);
        }

        [HarmonyPostfix]
        public static void Get_Label_PostFix(ref string __result)
        {
            if (__result.Contains("<color=#")) __result = __result.Remove(__result.IndexOf("<color=#"), 15).Replace("</color>", "");
        }

        [HarmonyPostfix]
        public static void DoPlaySettingsGlobalControls_PostFix(ref WidgetRow row, bool worldView)
        {
            if (!worldView)
            {
                row.ToggleableIcon(ref TraitRarityUISetting.showTraitUI, TraitTexButton.ShowTraits, "ShowTraitRarityOverlayToggleButton".Translate(), SoundDefOf.Mouseover_ButtonToggle, null);
            }
        }

        [HarmonyPostfix]
        public static void ExposeData_PostFix()
        {
            Scribe_Values.Look<bool>(ref TraitRarityUISetting.showTraitUI, "showTraitRarityOverlay", false, false);
        }

        [HarmonyPostfix]
        public static void MapInterfaceOnGUI_AfterMainTabs_PostFix()
        {
            if (Find.CurrentMap == null)
            {
                return;
            }
            if (!WorldRendererUtility.WorldRendered && !Find.UIRoot.screenshotMode.FiltersCurrentEvent)
            {
                TraitRarityOverlayDrawer.TraitRarityStatsOnGUI();
            }
        }
    }
}
