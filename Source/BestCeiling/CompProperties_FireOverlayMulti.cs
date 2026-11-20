using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace CeilingUtilities;

public class CompProperties_FireOverlayMulti : CompProperties_FireOverlay {
    public List<Vector3> offsets;

    public CompProperties_FireOverlayMulti() {
        compClass = typeof(CompFireOverlayMulti);
    }

    public override void DrawGhost(IntVec3 center, Rot4 rot, ThingDef thingDef, Color ghostCol, AltitudeLayer drawAltitude, Thing thing = null) {
    }

    public override IEnumerable<string> ConfigErrors(ThingDef parentDef) {
        foreach (string error in base.ConfigErrors(parentDef)) {
            yield return error;
        }
        if (offsets.NullOrEmpty()) {
            yield return "CompProperties_FireOverlayMulti has no offsets, no flames will be drawn.";
        }
    }
}
