namespace GameCube.GFZ;

/// <summary>
///     Enum representing all known game codes for F-Zero GX and AX.
/// </summary>
public enum GameCode
{
    /// <summary>
    ///     GFZ: <u>G</u>ameCube <u>F</u>-<u>Z</u>ero.
    ///     E: <u>E</u>nglish / North America.
    ///     01: Nintendo publisher code.
    /// </summary>
    GFZE01 = GameCodeFields.GX + GameCodeFields.NorthAmerica,

    /// <summary>
    ///     GFZ: <u>G</u>ameCube <u>F</u>-<u>Z</u>ero.
    ///     J: <u>J</u>apanese/ Japan.
    ///     01: Nintendo publisher code.
    /// </summary>
    GFZJ01 = GameCodeFields.GX + GameCodeFields.Japan,

    /// <summary>
    ///     GFZ: <u>G</u>ameCube <u>F</u>-<u>Z</u>ero.
    ///     P: <u>P</u>AL / Europe.
    ///     01: Nintendo publisher code.
    /// </summary>
    GFZP01 = GameCodeFields.GX + GameCodeFields.Europe,

    /// <summary>
    ///     GFZ: <u>G</u>ameCube <u>F</u>-<u>Z</u>ero.
    ///     E: <u>E</u>nglish / North America.
    ///     8P: SEGA publisher code.
    /// </summary>
    /// <remarks>
    ///     Game code in header of GDROM ISO, but boot.bin contains the true
    ///     Game code GGGE6E. This game, despite the E region, does also
    ///     contain Japanese text.
    /// </remarks>
    GFZJ8P = GameCodeFields.AX + GameCodeFields.Japan,

    /// <summary>
    ///     F-Zero AX ID according to GDROM associated boot.bin.
    /// </summary>
    /// <remarks>
    ///     The official ID recognized by Dolphin and crediar.
    /// </remarks>
    GGGE6E = GameCodeFields.AX + GameCodeFields.NorthAmerica,
}
