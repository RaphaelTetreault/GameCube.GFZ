using System;

namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Notes:
    /// Combinations: 4&5 (x1986), 1&3 (x7), 1&4 (x1)
    /// Flags used: 1, 3, 4, 5, 6
    /// See: https://github.com/camthesaxman/smb-decomp/blob/master/src/gma.h
    /// Hoowever, looks like the games differ here.
    /// </remarks>
    [Flags]
    public enum TexFlags0x00 : ushort
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Unused in GFZ, from SMB.
        /// </remarks>
        unused0 = 1 << 0,

        /// <summary>
        /// Based on st24 models, uv scroll. Scroll values stored in TextureScroll class
        /// attached to SceneObjectDynamic. TODO: find how scrolls are indexed.
        /// </summary>
        ENABLE_UV_SCROLL = 1 << 1,

        /// <summary>
        /// 
        /// </summary>
        unk2 = 1 << 2,

        /// <summary>
        /// 7 occurences total. (st21,lz.gma, [75,76,77/130] guide_light*, [1/6])
        /// </summary>
        unk3 = 1 << 3,

        /// <summary>
        /// Appears to be used whenever tex is for bg reflections
        /// </summary>
        unk4 = 1 << 4,

        /// <summary>
        /// ..?
        /// </summary>
        unk5 = 1 << 5,

        /// <summary>
        /// Appears to be used whenever tex is for bg reflections
        /// </summary>
        unk6 = 1 << 6,

        /// <summary>
        /// Unused
        /// </summary>
        unused7 = 1 << 7,

        // Checking to see if these are used
        unk8 = 1 << 8,
        unk9 = 1 << 9,
        unk10 = 1 << 10,
        unk11 = 1 << 11,
        unk12 = 1 << 12,
        unk13 = 1 << 13,
        unk14 = 1 << 14,
        unk15 = 1 << 15,
    }
}