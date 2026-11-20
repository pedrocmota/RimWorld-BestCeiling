using HarmonyLib;
using RimWorld;
using Verse;

namespace CeilingUtilities;

[HarmonyPatch(typeof(DynamicDrawManager), nameof(DynamicDrawManager.RegisterDrawable))]
internal static class Patch_DynamicDrawManager_RegisterDrawable {
    private static bool Prefix(Thing t) {
        return !CeilingFixtureVisibility.ShouldBlockDrawRegistration(t);
    }
}

[HarmonyPatch(typeof(PlaySettings), nameof(PlaySettings.DoPlaySettingsGlobalControls))]
internal static class Patch_PlaySettings_DoPlaySettingsGlobalControls {
    private static void Postfix(WidgetRow row, bool worldView) {
        if (worldView || row == null || !ModSettings_CeilingUtilities.useDrawFixturesToggle) {
            return;
        }
        bool visible = CeilingFixtureVisibility.Visible;
        row.ToggleableIcon(ref visible, ResourceBank.ToggleIcon, "Owl_ToggleFixtures".Translate(), SoundDefOf.Mouseover_ButtonToggle);
        if (visible != CeilingFixtureVisibility.Visible) {
            CeilingFixtureVisibility.SetVisible(visible);
            Mod_CeilingUtilities.Instance?.WriteSettings();
        }
    }
}

[HarmonyPatch(typeof(PowerNetGraphics), nameof(PowerNetGraphics.PrintWirePieceConnecting))]
internal static class Patch_PowerNetGraphics_PrintWirePieceConnecting {
    private static bool Prefix(Thing A) {
        return A == null || !CeilingUtilitiesUtility.IsCeilingFixture(A.def);
    }
}
