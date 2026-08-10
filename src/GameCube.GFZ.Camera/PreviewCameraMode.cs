namespace GameCube.GFZ.Camera;

/// <summary>
///     Interpolation mode for <see cref="PreviewCameraShot"/>.
/// </summary>
public enum PreviewCameraMode : System.UInt16
{
    /// <summary>
    ///     A linear interpolation from start to end.
    ///     It is not adjustable.
    /// </summary>
    /// <remarks>
    ///     <see cref="PreviewCameraShot"/>'s From (<see cref="PreviewCameraPoint"/>) is always <see cref="FixedLinear"/>.
    ///     In other words, unused and uses default value.
    /// </remarks>
    FixedLinear = 0,

    /// <summary>
    ///     Unused or stubbed in game code.
    /// </summary>
    //Unknown1 = 1,

    /// <summary>
    ///     A recursive lerp (linear-interpolation) where the previous frame's output is
    ///     used as the next frames input. Uses <see cref="PreviewCameraShot.lerpSpeed"/>.
    /// </summary>
    RecursiveLerp = 2,

    /// <summary>
    ///     Forced Ease In/Out.
    ///     It is not adjustable.
    /// </summary>
    FixedEaseInOut = 3,
}