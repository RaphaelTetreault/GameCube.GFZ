namespace GameCube.GFZ.FMI
{
    /// <summary>
    ///     Specifies what the FMI position is for.
    /// </summary>
    public enum FmiPositionType : uint
    {
        none = 0,                   // Bit positions... flags?
        CustomPartPosition = 8,     // 001000, __ __ 8 _ _ _
        Pilot2Position = 9,         // 001001, __ __ 8 _ _ 1
        Pilot3Position = 18,        // 010010, __ 16 _ _ 2 _
        AnimationAnalogStick = 25,  // 011001, __ 16 8 _ _ 1
        AnimationBoostA = 26,       // 011010, __ 16 8 _ 2 _
        AnimationBoostB = 52,       // 110100, 32 16 _ 4 _ _
    }
}
