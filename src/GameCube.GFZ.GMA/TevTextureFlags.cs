namespace GameCube.GFZ.GMA;

[System.Flags]
public enum TevTextureFlags : int
{
    ///////////////////////////////////////////////////////////////////////////////////////////
    /// FORMERLY <see cref="TextureWrapMode"/>

    /// <summary>
    ///     Unused in F-Zero Ax and GX.
    /// </summary>
    /// <remarks>
    ///     Super Monkey Ball decomp: says it's a "view specular" type effect.
    ///     https://github.com/camthesaxman/smb-decomp/blob/5cd6ffccf3f8508231c7cd9cce5b722be4465a2b/src/gma.h#L16
    /// </remarks>
    unused0 = 1 << 0,

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
    ///     
    /// </summary>
    unk6 = 1 << 6, // SMB: DO_EDGE_LOD ?

    /// <summary>
    ///     
    /// </summary>
    unk7 = 1 << 7,

    ///////////////////////////////////////////////////////////////////////////////////////////
    /// FORMERLY <see cref="MipmapSetting"/>

    /// <summary>
    ///     2019/04/03 VERIFIED: Enable (large preview) mipmaps
    /// </summary>
    ENABLE_MIPMAP = 1 << 8,

    /// <summary>
    ///     2019/04/03 THEORY: when only flag, "use custom mip-map"
    ///     See: bg_san_s [39/41] tex [3/8] - RIVER01
    ///     See: bg_big [118/120] tex [1/1] - OCE_OCEAN_C14_B_ltmp2
    ///     See: any recovery pad
    /// </summary>
    UNK_FLAG_1 = 1 << 9, // Working together?
    UNK_FLAG_2 = 1 << 10, // Working together?

    /// <summary>
    ///     2019/04/03 VERIFIED: Enable Mipmap NEAR
    /// </summary>
    FilterNearestNeighbour = 1 << 11,

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
    ///     On many vehicles
    /// </summary>
    unk15 = 1 << 15, // SMB: WORLD_SPECULAR

    ///////////////////////////////////////////////////////////////////////////////////////////
    /// FORMERLY <see cref="TexFlags0x00"/>

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
    ///     Appears to be used whenever tex is for bg reflections
    /// </summary>
    ReflectionMap20 = 1 << 20,

    /// <summary>
    ///     Appears exclusively on vehicles, and (to my best guess) on all TEV layers
    ///     EXCEPT (I'm guessing) not on the auto-assigned fields which show vehicle
    ///     number, name, or which are inside cockpit (screen, seatrest).
    /// </summary>
    VehicleEmblemCanHide = 1 << 21,

    /// <summary>
    ///     Appears to be used whenever tex is for bg reflections
    /// </summary>
    RefelctionMap22 = 1 << 22,

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
