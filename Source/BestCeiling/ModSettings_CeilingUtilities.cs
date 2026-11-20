using Verse;

namespace CeilingUtilities;

public class ModSettings_CeilingUtilities : ModSettings {
    public static bool drawFixtures = true;

    public static bool useDrawFixturesToggle = true;

    public override void ExposeData() {
        base.ExposeData();
        Scribe_Values.Look(ref drawFixtures, "drawFixtures", defaultValue: true);
        Scribe_Values.Look(ref useDrawFixturesToggle, "useDrawFixturesToggle", defaultValue: true);
    }
}
