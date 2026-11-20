using System.Collections.Generic;
using RimWorld;
using Verse;

namespace CeilingUtilities;

public class PlaceWorker_OnlyUnderRoof : PlaceWorker {
    public override AcceptanceReport AllowsPlacing(BuildableDef def, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null) {
        foreach (IntVec3 cell in GenAdj.OccupiedRect(loc, rot, def.Size)) {
            if (!cell.InBounds(map)) {
                return false;
            }
            if (!map.roofGrid.Roofed(cell)) {
                return "WorkPlacer_NeedsRoof".Translate();
            }
            List<Thing> things = map.thingGrid.ThingsListAtFast(cell);
            for (int i = 0; i < things.Count; i++) {
                Thing other = things[i];
                if (other == thingToIgnore || other == thing) {
                    continue;
                }
                if (other is Blueprint || other is Frame) {
                    if (BlocksCeilingFixture(other.def.entityDefToBuild as ThingDef)) {
                        return "WorkPlacer_OverBlueprint".Translate();
                    }
                } else if (other is Building && BlocksCeilingFixture(other.def)) {
                    return "WorkPlacer_OverForbiddenThing".Translate();
                }
            }
        }
        return true;
    }

    private static bool BlocksCeilingFixture(ThingDef def) {
        if (def == null) {
            return false;
        }
        return def.altitudeLayer == AltitudeLayer.MoteOverhead || def.fillPercent >= 1f || def.holdsRoof;
    }
}
