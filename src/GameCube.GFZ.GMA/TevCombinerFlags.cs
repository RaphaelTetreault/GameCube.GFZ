namespace GameCube.GFZ.GMA;

/// <summary>
/// 
/// </summary>
[System.Flags]
public enum TevCombinerFlags : ushort
{
    /// <summary>
    ///     Receives alpha from previous TEV stage. This is always called
    ///     after <see cref="TevTextureFlags.UseGreyscaleAsAlphaMask"/> is set in the previous TEV stage.
    /// </summary>
    ReceiveAlphaFromLastTevStage = 1 << 0,

    /// <summary>
    /// 
    /// </summary>
    unk1 = 1 << 1,

    /// <summary>
    /// 
    /// </summary>
    unk2 = 1 << 2,

    /// <summary>
    /// 
    /// </summary>
    unk3 = 1 << 3,

    /// <summary>
    /// 
    /// </summary>
    unk4 = 1 << 4,

    /// <summary>
    /// 
    /// </summary>
    unk5 = 1 << 5,

    /// <summary>
    /// 
    /// </summary>
    unk6 = 1 << 6,

    /// <summary>
    /// 
    /// </summary>
    unk7 = 1 << 7,

    /// <summary>
    /// 
    /// </summary>
    unk8 = 1 << 8,

    /// <summary>
    /// 
    /// </summary>
    unk9 = 1 << 9,

    /// <summary>
    /// 
    /// </summary>
    unk10 = 1 << 10,

    /// <summary>
    /// 
    /// </summary>
    unk11 = 1 << 11,

    /// <summary>
    /// 
    /// </summary>
    unk12 = 1 << 12,

    /// <summary>
    /// 
    /// </summary>
    unk13 = 1 << 13,

    /// <summary>
    /// 
    /// </summary>
    unk14 = 1 << 14,

    /// <summary>
    /// 
    /// </summary>
    unk15 = 1 << 15,
}