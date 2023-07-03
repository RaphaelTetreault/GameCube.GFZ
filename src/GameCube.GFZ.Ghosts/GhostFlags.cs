using System;

namespace GameCube.GFZ.Ghosts
{
    /// <summary>
    ///     Guess: interpolation mode
    /// </summary>
    [Flags]
    public enum GhostFlags : byte
    {
        Unk0 = 1 << 0, //  1
        Unk1 = 1 << 1, //  2
        Unk2 = 1 << 2, //  4
        Unk3 = 1 << 3, //  8
        Unk4 = 1 << 4, // 16
        All = Unk0 | Unk1 | Unk2 | Unk3 | Unk4,
        // Seen: 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 16, 22
    }
}
