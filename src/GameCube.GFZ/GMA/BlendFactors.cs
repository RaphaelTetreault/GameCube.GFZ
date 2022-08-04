namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 2019/04/23 - All GFZ values 0, 17, 18, 20, 36, 48
    /// From SMB decomp: 0xF bitmask for src blend factor, 0xF0 for dst blend factor
    /// </remarks>
    [System.Flags]
    public enum BlendFactors : uint
    {
        /// <summary>
        /// 
        /// </summary>
        unk0 = 1 << 0, // 0x01

        /// <summary>
        /// 
        /// </summary>
        unk1 = 1 << 1, // 0x02

        /// <summary>
        /// 
        /// </summary>
        unk2_UseBlendMode = 1 << 2, // 0x04

        /// <summary>
        /// 
        /// </summary>
        unk4_UseBlendMode = 1 << 4, // 0x16

        /// <summary>
        /// 
        /// </summary>
        unk5 = 1 << 5, // 0x32
    }
}