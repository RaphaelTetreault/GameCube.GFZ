namespace GameCube.GFZ.GameData;

/// <summary>
///     Enum of all cups in order present in the game.
/// </summary>
public enum CupIndex : byte
{
    /// <summary>
    ///     Debug "All Cup".
    /// </summary>
    AllCup,

    /// <summary>
    ///     F-Zero GX's Ruby Cup.
    /// </summary>
    RubyCup,

    /// <summary>
    ///     F-Zero GX's Sapphire Cup.
    /// </summary>
    SapphireCup,

    /// <summary>
    ///     F-Zero GX's Emerald Cup.
    /// </summary>
    EmeraldCup,

    /// <summary>
    ///     F-Zero GX's Diamond Cup.
    /// </summary>
    DiamondCup,

    /// <summary>
    ///     F-Zero GX's AX Cup.
    /// </summary>
    AXCup,

    /// <summary>
    ///     F-Zero AX's single-race grand prix cup.
    /// </summary>
    ACCup,

    /// <summary>
    ///     Appears to have been an AX test cup.
    /// </summary>
    AC,

    /// <summary>
    ///     World Hobby Fair demo cup.
    /// </summary>
    /// <remarks>
    ///     Despite the name, this appears to have been an AX test cup.
    /// </remarks>
    WHF,

    /// <summary>
    ///     E3 single-race grand prix demo cup.
    /// </summary>
    E3_SingleRaceGP,

    /// <summary>
    ///     E3 versus mode demo cup.
    /// </summary>
    E3_Versus,
}
