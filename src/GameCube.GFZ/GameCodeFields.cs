namespace GameCube.GFZ;

/// <summary>
///     Enum representing all bit fields for <see cref="GameCode"/>.
/// </summary>
[System.Flags]
public enum GameCodeFields
{
    // Games
    GX = 1 << 0,
    AX = 1 << 1,
    MaskGame = AX | GX,

    // Regions
    Japan = 1 << 2,
    NorthAmerica = 1 << 3,
    Europe = 1 << 4,
    MaskRegion = Japan | NorthAmerica | Europe,
}
