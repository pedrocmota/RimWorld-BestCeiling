using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace CeilingUtilities;

public class Graphic_FlickerMulti : Graphic_Flicker {
    private const int TicksPerFrame = 15;

    public void DrawFlames(Vector3 center, List<Vector3> offsets, float size, int seed) {
        if (offsets == null || subGraphics == null || subGraphics.Length == 0) {
            return;
        }
        int frameCount = subGraphics.Length;
        uint ticks = unchecked((uint)(Find.TickManager.TicksGame + (seed ^ 0x80FD52)));
        int frame = (int)(ticks / TicksPerFrame % (uint)frameCount);
        Vector3 scale = new Vector3(size, 1f, size);
        for (int i = 0; i < offsets.Count; i++) {
            Vector3 offset = offsets[i];
            Vector3 pos = new Vector3(center.x + offset.x, center.y, center.z + offset.z);
            Matrix4x4 matrix = Matrix4x4.TRS(pos, Quaternion.identity, scale);
            Material material = subGraphics[(frame + i) % frameCount].MatSingle;
            Graphics.DrawMesh(MeshPool.plane10, matrix, material, 0);
        }
    }
}
