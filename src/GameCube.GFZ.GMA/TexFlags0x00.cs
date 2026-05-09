namespace GameCube.GFZ.GMA;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Notes:
///     Combinations: 4&5 (x1986), 1&3 (x7), 1&4 (x1)
///     Flags used: 1, 3, 4, 5, 6
///     See: https://github.com/camthesaxman/smb-decomp/blob/master/src/gma.h
///     However, looks like the games differ here.
/// </remarks>
[System.Flags]
public enum TexFlags0x00 : ushort
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     Unused in GFZ, from SMB.
    /// </remarks>
    unused0 = 1 << 0,

    /// <summary>
    ///     Based on st24 models, uv scroll. Scroll values stored in TextureScroll class
    ///     attached to SceneObjectDynamic. TODO: find how scrolls are indexed.
    /// </summary>
    ENABLE_UV_SCROLL = 1 << 1,

    /// <summary>
    ///     
    /// </summary>
    /// <remarks>
    ///     Always comes with <see cref="NICHE_PARAM3"/>, but the inverse is not true.
    ///     Only on 2 models inside ./common/operation.gma and /operation_us.gma.
    ///     Models are for F-Zero AX attract screen tutorial animations.
    ///     ARROW is the (colorized) yellow arrow for boost / paddle tutorials.
    ///     MARU is the (colorized) yellow circle for the GC memcard tutorial.
    /// </remarks>
    NICHE_PARAM2 = 1 << 2,

    /// <summary>
    ///     
    /// </summary>
    /// <remarks>
    ///     Only used for AX attract mode animated yellow UI (ARROW and MARU inside
    ///     of ./common/operation.gma and /operation_us.gma) or for Aeropolis Dragon
    ///     Slopes guide_light* models (blue scrolling markers for long jumps).
    /// </remarks>
    NICHE_PARAM3 = 1 << 3,

    /// <summary>
    ///     Appears to be used whenever tex is for bg reflections
    /// </summary>
    REFLECTION_MAP1 = 1 << 4,

    /// <summary>
    ///     Appears exclusively on vehicles, and (to my best guess) on all TEV layers
    ///     EXCEPT (I'm guessing) not on the auto-assigned fields which show vehicle
    ///     number, name, or which are inside cockpit (screen, seatrest).
    /// </summary>
    VEHICLE_EMBLEM = 1 << 5,

    /// <summary>
    ///     Appears to be used whenever tex is for bg reflections
    /// </summary>
    REFLECTION_MAP2 = 1 << 6,

    // 2026-05-08: Unused! I generated a spreadsheet for all AX/GX games, and these
    // didn't come up anywhere.
    unused7 = 1 << 7,
    unused8 = 1 << 8,
    unused9 = 1 << 9,
    unused10 = 1 << 10,
    unused11 = 1 << 11,
    unused12 = 1 << 12,
    unused13 = 1 << 13,
    unused14 = 1 << 14,
    unused15 = 1 << 15,
}