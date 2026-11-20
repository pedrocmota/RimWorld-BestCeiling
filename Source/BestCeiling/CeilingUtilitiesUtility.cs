using System.Collections.Generic;
using Verse;

namespace CeilingUtilities;

[StaticConstructorOnStartup]
public static class CeilingUtilitiesUtility {
    public static readonly ThingDef[] ceilingFixtures;

    private static readonly HashSet<ThingDef> ceilingFixtureSet;

    static CeilingUtilitiesUtility() {
        List<ThingDef> found = new List<ThingDef>();
        List<ThingDef> allDefs = DefDatabase<ThingDef>.AllDefsListForReading;
        for (int i = 0; i < allDefs.Count; i++) {
            ThingDef def = allDefs[i];
            if (def.HasModExtension<CeilingFixture>()) {
                found.Add(def);
            }
        }
        ceilingFixtures = found.ToArray();
        ceilingFixtureSet = new HashSet<ThingDef>(found);
    }

    public static bool IsCeilingFixture(ThingDef def) {
        return def != null && ceilingFixtureSet.Contains(def);
    }
}
