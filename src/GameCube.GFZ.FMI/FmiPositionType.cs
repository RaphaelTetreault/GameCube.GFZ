namespace GameCube.GFZ.FMI
{
    /// <summary>
    ///     Specifies what kind of FMI "animation" the position is.
    /// </summary>
    public enum FmiPositionType : uint
    {
        none = 0,
        CustomPartPosition = 8,
        Pilot2Position = 9,
        Pilot3Position = 18,
        AnimationAnalogStick = 25,
        AnimationBoostA = 26,
        AnimationBoostB = 52,
    }
}
