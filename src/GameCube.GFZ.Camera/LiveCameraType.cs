namespace GameCube.GFZ.Camera;

public enum LiveCameraType : System.UInt32
{
    /// <summary>
    /// RED
    ///     Follows target.
    ///     Zoom in and out infinetely.
    ///     No movement.
    /// </summary>
    FollowZoomInOutNoMove = 0x00000000,

    /// <summary>
    /// CYAN
    ///     Follows target.
    ///     Zoom in only.
    ///     Move from-to.
    /// </summary>
    FollowZoomInOnly = 0x01000000,

    /// <summary>
    /// GREEN
    ///     Follows target.
    ///     Zoom in and out only once.
    ///     Move from-to.
    /// </summary>
    FollowZoomInOutOnce = 0x02000000,

    /// <summary>
    /// MAGENTA
    ///     Does not lookat vehicle.
    ///     Is fixed, pos-from lookat pos-to.
    ///     Is rockable if vehicle is close enough / fast enough.
    /// </summary>
    FixedCameraRockable = 0x03000000,

    /// <summary>
    /// BLUE
    ///     Follows target.
    ///     Zoom in and out only once.
    ///     Move from-to but also towards target freely (off rails).
    ///     Maybe only moves once at end.
    /// </summary>
    FollowZoomInOutOnceAndMoveTowards = 0x04000000,

    /// <summary>
    /// YELLOW
    ///     No zoom.
    /// </summary>
    FollowOnly = 0x05000000,
}
