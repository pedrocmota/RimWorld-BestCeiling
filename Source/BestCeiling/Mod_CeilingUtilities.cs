using HarmonyLib;
using UnityEngine;
using Verse;

namespace CeilingUtilities;

public class Mod_CeilingUtilities : Mod {
    public static Mod_CeilingUtilities Instance { get; private set; }

    public Mod_CeilingUtilities(ModContentPack content) : base(content) {
        Instance = this;
        GetSettings<ModSettings_CeilingUtilities>();
        new Harmony(content.PackageIdPlayerFacing).PatchAll(typeof(Mod_CeilingUtilities).Assembly);
    }

    public override void DoSettingsWindowContents(Rect inRect) {
        Listing_Standard listing = new Listing_Standard();
        listing.Begin(inRect);

        bool visible = CeilingFixtureVisibility.Visible;
        listing.CheckboxLabeled("Utilities_drawFixtures".Translate(), ref visible, "Utilities_drawFixturesDesc".Translate());
        if (visible != CeilingFixtureVisibility.Visible) {
            CeilingFixtureVisibility.SetVisible(visible);
        }

        listing.CheckboxLabeled("Utilities_useDrawFixturesToggle".Translate(), ref ModSettings_CeilingUtilities.useDrawFixturesToggle, "Utilities_useDrawFixturesToggleDesc".Translate());

        listing.End();
    }

    public override string SettingsCategory() {
        return Content?.Name ?? "BestCeiling";
    }
}
