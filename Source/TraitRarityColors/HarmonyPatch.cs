using Harmony;
using RimWorld;
using Verse;

namespace TraitRarityColors
{
    [StaticConstructorOnStartup]
    static class HarmonyPatch
    {
        static HarmonyPatch()
        {
            var harmony = HarmonyInstance.Create("rimworld.carnysenpai.traitraritycolors");
            harmony.Patch(AccessTools.Method(typeof(Trait), "get_Label"), null, new HarmonyMethod(typeof(HarmonyPatch).GetMethod("Get_Label_PostFix")), null);
        }

        [HarmonyPostfix]
        public static void Get_Label_PostFix(ref string __result)
        {
            if (__result.Contains("<color=#")) __result = __result.Remove(__result.IndexOf("<color=#"), 15).Replace("</color>", "");
        }
    }
}
