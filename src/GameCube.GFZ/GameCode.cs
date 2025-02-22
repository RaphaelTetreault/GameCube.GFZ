namespace GameCube.GFZ;

/// <summary>
///     Enum representing all known game codes for F-Zero GX and AX.
/// </summary>
public enum GameCode
{
    GFZE01 = GameCodeFields.GX + GameCodeFields.NorthAmerica,
    GFZJ01 = GameCodeFields.GX + GameCodeFields.Japan,
    GFZP01 = GameCodeFields.GX + GameCodeFields.Europe,
    GFZJ8P = GameCodeFields.AX + GameCodeFields.Japan,
}
