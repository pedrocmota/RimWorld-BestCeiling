using UnityEngine;
using Verse;

namespace CeilingUtilities;

[StaticConstructorOnStartup]
internal static class ResourceBank {
    public static readonly Texture2D ToggleIcon = ContentFinder<Texture2D>.Get("UI/ShowCeilingFixtures");

    public static readonly Graphic_FlickerMulti FireGraphicMulti = (Graphic_FlickerMulti)GraphicDatabase.Get<Graphic_FlickerMulti>("Things/OwlSpecial/FireTranparent", ShaderDatabase.TransparentPostLight, Vector2.one, Color.white);
}
