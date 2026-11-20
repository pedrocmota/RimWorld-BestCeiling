using System.Collections.Generic;
using Verse;

namespace CeilingUtilities;

public static class CeilingFixtureVisibility {
    public static bool Visible => ModSettings_CeilingUtilities.drawFixtures;

    public static bool ShouldBlockDrawRegistration(Thing thing) {
        return !ModSettings_CeilingUtilities.drawFixtures && thing != null && CeilingUtilitiesUtility.IsCeilingFixture(thing.def);
    }

    public static void SetVisible(bool visible) {
        if (ModSettings_CeilingUtilities.drawFixtures == visible) {
            return;
        }
        ModSettings_CeilingUtilities.drawFixtures = visible;
        RefreshAllMaps();
    }

    public static void RefreshAllMaps() {
        if (Current.ProgramState != ProgramState.Playing) {
            return;
        }
        List<Map> maps = Find.Maps;
        if (maps == null) {
            return;
        }
        for (int i = 0; i < maps.Count; i++) {
            RefreshMap(maps[i]);
        }
    }

    private static void RefreshMap(Map map) {
        DynamicDrawManager drawManager = map?.dynamicDrawManager;
        if (drawManager == null) {
            return;
        }
        bool visible = Visible;
        ThingDef[] fixtures = CeilingUtilitiesUtility.ceilingFixtures;
        for (int i = 0; i < fixtures.Length; i++) {
            ThingDef def = fixtures[i];
            if (def.drawerType == DrawerType.None || def.drawerType == DrawerType.MapMeshOnly) {
                continue;
            }
            List<Thing> things = map.listerThings.ThingsOfDef(def);
            for (int j = 0; j < things.Count; j++) {
                Thing thing = things[j];
                drawManager.DeRegisterDrawable(thing);
                if (visible) {
                    drawManager.RegisterDrawable(thing);
                }
            }
        }
    }
}
