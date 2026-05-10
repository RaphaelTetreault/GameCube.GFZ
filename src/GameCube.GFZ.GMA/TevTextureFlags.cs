namespace GameCube.GFZ.GMA;

[System.Flags]
public enum TevTextureFlags : int
{
    /// <summary>
    ///     
    /// </summary>
    /// <remarks>
    ///     Super Monkey Ball decomp: says it's a "view specular" type effect.
    ///     https://github.com/camthesaxman/smb-decomp/blob/5cd6ffccf3f8508231c7cd9cce5b722be4465a2b/src/gma.h#L16
    ///     It does tend to come up with
    /// </remarks>
    ViewSpecular = 1 << 0,

    /// <summary>
    ///     Use Normal, Binormal, and Tangent data for texturing.
    /// </summary>
    UseNBT1 = 1 << 1,

    /// <summary>
    ///      
    /// </summary>
    WrapModeRepeatX = 1 << 2,

    /// <summary>
    ///     
    /// </summary>
    WrapModeMirrorX = 1 << 3,

    /// <summary>
    ///     
    /// </summary>
    WrapModeRepeatY = 1 << 4,

    /// <summary>
    ///     
    /// </summary>
    WrapModeMirrorY = 1 << 5,

    /// <summary>
    ///     Level of detail (LOD) at triangle edge boundaries?
    /// </summary>
    /// <remarks>
    ///     Very close relationship with this on and Aniso being Aniso4.
    /// </remarks>
    EnableEdgeLOD = 1 << 6, // SMB: DO_EDGE_LOD ?

    //////////////////////////////////////////////////////////////////////////////////////////////
    // NOTES
    //     When any of next 4 mipmap bits set, texture debug shows [XXXXX & MIPMAP LINEAR, XXXXX].
    //     When any of next 4 mipmap bits unset, texture debug shows [XXXXX & MIPMAP NEAR, XXXXX].

    /// <summary>
    ///     
    /// </summary>
    /// <remarks>
    ///     
    /// </remarks>
    Mipmap7 = 1 << 7,

    /// <summary>
    ///     
    /// </summary>
    Mipmap8 = 1 << 8,

    /// <summary>
    ///     
    /// </summary>
    Mipmap9 = 1 << 9,

    /// <summary>
    ///     
    /// </summary>
    Mipmap10 = 1 << 10,

    /// <summary>
    ///     Uses Nearest Neighbour to sample the texture.
    /// </summary>
    /// <remarks>
    ///     Shows up as [NEAR & MIPMAP XXXXX, NEAR] in debug menu.
    ///     When set, XXXXX = NEAR, nearest neighbour, not filtering.
    ///     When unset, XXXXX = LINEAR, linear filtering.
    /// </remarks>
    SampleNearestNeighbour = 1 << 11,

    /// <summary>
    ///     
    /// </summary>
    /// <remarks>
    ///     Height map? Blend? (they are greyscale)
    ///     Low occurences: 188 for tracks and st2 boulders
    /// </remarks>
    UseNBT12 = 1 << 12,

    /// <summary>
    ///     Every time this is set, the next TEV layer has <see cref="TevCombinerFlags.ReceiveAlphaFromLastTevStage"/>
    ///     enabled. TODO: assert in code.
    /// </summary>
    UseGreyscaleAsAlphaMask = 1 << 13,

    /// <summary>
    ///     
    /// </summary>
    /// <remarks>
    ///     Total occurences = 3. Only MCSO, on a single geometry set C##_ROAD, where
    ///     C## is C36 (MCSO), C37 (Story 1 MCSO), and C50 (victory lap MCSO).
    /// </remarks>
    NicheParam14 = 1 << 14,

    /// <summary>
    ///     Reflection map type thing, but with black-is-transparent images (all that I checked).
    /// </summary>
    /// <remarks>
    ///     Used more than <see cref="SpecularReflection20"/>
    ///     but less than <see cref="SpecularReflection22"/>.
    /// </remarks>
    SpecularReflectionAdditive = 1 << 15,

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     Unused in GFZ, from SMB.
    /// </remarks>
    unused16 = 1 << 16,

    /// <summary>
    ///     Lets (indicates?) TEV Layer be scrolled by <see cref="GameCube.GFZ.Stage.TextureScroll"/>.
    /// </summary>
    /// <remarks>
    ///     REAL OLD COMMENT:
    ///     Based on st24 models, uv scroll. Scroll values stored in TextureScroll class
    ///     attached to SceneObjectDynamic. TODO: find how scrolls are indexed.
    /// </remarks>
    ScrollUV = 1 << 17,

    /// <summary>
    ///     
    /// </summary>
    /// <remarks>
    ///     Always comes with <see cref="NicheParam19"/>, but the inverse is not true.
    ///     Only on 2 models inside ./common/operation.gma and /operation_us.gma.
    ///     Models are for F-Zero AX attract screen tutorial animations.
    ///     ARROW is the (colorized) yellow arrow for boost / paddle tutorials.
    ///     MARU is the (colorized) yellow circle for the GC memcard tutorial.
    /// </remarks>
    NicheParam18 = 1 << 18,

    /// <summary>
    ///     
    /// </summary>
    /// <remarks>
    ///     Only used for AX attract mode animated yellow UI (ARROW and MARU inside
    ///     of ./common/operation.gma and /operation_us.gma) or for Aeropolis Dragon
    ///     Slopes guide_light* models (blue scrolling markers for long jumps).
    /// </remarks>
    NicheParam19 = 1 << 19,

    /// <summary>
    ///     Appears to be used whenever tex is for bg reflections.
    /// </summary>
    /// <remarks>
    ///     Most common specular reflection, less harsh compared to <see cref="SpecularReflection22"/>?
    /// </remarks>
    SpecularReflection20 = 1 << 20,

    /// <summary>
    ///     Appears exclusively on vehicles, and (to my best guess) on all TEV layers
    ///     EXCEPT (I'm guessing) not on the auto-assigned fields which show vehicle
    ///     number, name, or which are inside cockpit (screen, seatrest).
    /// </summary>
    VehicleEmblemCanHide = 1 << 21,

    /// <summary>
    ///     Appears to be used whenever tex is for bg reflections
    /// </summary>
    /// <remarks>
    ///     Least common specular reflection, more harsh compared to <see cref="SpecularReflection20"/>?
    /// </remarks>
    SpecularReflection22 = 1 << 22,

    // 2026-05-08: Unused! I generated a spreadsheet for all AX/GX games, and these
    // didn't come up anywhere.
    unused23 = 1 << 23,
    unused24 = 1 << 24,
    unused25 = 1 << 25,
    unused26 = 1 << 26,
    unused27 = 1 << 27,
    unused28 = 1 << 28,
    unused29 = 1 << 29,
    unused30 = 1 << 30,
    unused31 = 1 << 31,
}
