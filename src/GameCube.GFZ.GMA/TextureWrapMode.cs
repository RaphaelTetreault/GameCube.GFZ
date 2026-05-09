namespace GameCube.GFZ.GMA;

/// <summary>
/// 
/// </summary>
[System.Flags]
public enum TextureWrapMode : byte
{
    /// <summary>
    ///     
    /// </summary>
    unk0 = 1 << 0,

    /// <summary>
    ///     
    /// </summary>
    UseNBT = 1 << 1,

    /// <summary>
    ///      
    /// </summary>
    RepeatX = 1 << 2,

    /// <summary>
    ///     
    /// </summary>
    MirrorX = 1 << 3,

    /// <summary>
    ///     
    /// </summary>
    RepeatY = 1 << 4,

    /// <summary>
    ///     
    /// </summary>
    MirrorY = 1 << 5,

    /// <summary>
    ///     
    /// </summary>
    unk6 = 1 << 6,

    /// <summary>
    ///     
    /// </summary>
    unk7 = 1 << 7,
}