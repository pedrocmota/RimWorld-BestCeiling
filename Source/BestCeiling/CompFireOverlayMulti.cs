using RimWorld;
using UnityEngine;
using Verse;

namespace CeilingUtilities;

public class CompFireOverlayMulti : CompFireOverlay {
    public new CompProperties_FireOverlayMulti Props => (CompProperties_FireOverlayMulti)props;

    public override void PostDraw() {
        if (refuelableComp != null && !refuelableComp.HasFuel) {
            return;
        }
        Vector3 center = parent.DrawPos;
        center.y += Altitudes.AltInc;
        ResourceBank.FireGraphicMulti.DrawFlames(center, Props.offsets, Props.fireSize, parent.thingIDNumber);
    }
}
