namespace GameCube.GFZ.GMA;

/// <summary>
/// 
/// </summary>
[System.Flags]
public enum TevCombinerFlags : ushort
{
    /// <summary>
    ///     
    /// </summary>
    None = 0,

    /// <summary>
    ///     Receives alpha from previous TEV stage. This is always called
    ///     after <see cref="TevTextureFlags.UseGreyscaleAsAlphaMask"/> is set in the previous TEV stage.
    /// </summary>
    ReceiveAlphaFromLastTevStage = 1 << 0,

    /// <summary>
    /// 
    /// </summary>
    unused1 = 1 << 1,

    /// <summary>
    /// 
    /// </summary>
    unused2 = 1 << 2,

    /// <summary>
    /// 
    /// </summary>
    unused3 = 1 << 3,

    /// <summary>
    ///     Use last color?
    /// </summary>
    unk4 = 1 << 4,

    /// <summary>
    ///     Use last color?
    /// </summary>
    unk5 = 1 << 5,

    /// <summary>
    /// 
    /// </summary>
    unused6 = 1 << 6,

    /// <summary>
    /// 
    /// </summary>
    unused7 = 1 << 7,

    /// <summary>
    ///     2026-05-09: Appears related to alpha or multiply textures.
    /// </summary>
    unk8 = 1 << 8,

    /// <summary>
    ///     2026-05-09: Appears related to alpha or multiply textures.
    /// </summary>
    unk9 = 1 << 9,

    /// <summary>
    /// 
    /// </summary>
    unused10 = 1 << 10,

    /// <summary>
    /// 
    /// </summary>
    unused11 = 1 << 11,

    /// <summary>
    /// 
    /// </summary>
    unused12 = 1 << 12,

    /// <summary>
    /// 
    /// </summary>
    unused13 = 1 << 13,

    /// <summary>
    /// 
    /// </summary>
    unused14 = 1 << 14,

    /// <summary>
    /// 
    /// </summary>
    unused15 = 1 << 15,
}