namespace GameCube.GFZ.Camera;

/// <summary>
///     Interpolation mode for <see cref="CameraPan"/>.
/// </summary>
public enum CameraPanInterpolation : short
{
    /// <summary>
    /// Used for shots where there is no lerping of any kind.
    /// </summary>
    Linear = 0,

    /// <summary>
    /// 
    /// </summary>
    EaseOut = 2,

    /// <summary>
    /// 
    /// </summary>
    EaseInOut = 3,
}