namespace GameCube.GFZ.GameData;

/// <summary>
///     Game language.
/// </summary>
public enum Language
{
    Japanese = 1 << 0,

    English = 1 << 1,
    Deutsch = 1 << 2,
    Français = 1 << 3,
    Español = 1 << 4,
    Italiano = 1 << 5,

    Latin = English | Deutsch | Français | Español | Italiano,
}
