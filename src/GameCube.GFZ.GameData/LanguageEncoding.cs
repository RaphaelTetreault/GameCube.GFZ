using GameCube.Common;
using System.Text;

namespace GameCube.GFZ.GameData;

public readonly record struct LanguageEncoding
{
    public required Language Language { get; init; }
    public required Encoding Encoding { get; init; }


    public static readonly LanguageEncoding Japanese = new()
    {
        Language = Language.Japanese,
        Encoding = TextEncoding.ShiftJIS,
    };

    public static readonly LanguageEncoding Latin = new()
    {
        Language = Language.Latin,
        Encoding = TextEncoding.Windows1252,
    };

    public static readonly LanguageEncoding English = Latin with { Language = Language.English };
    public static readonly LanguageEncoding Deutsch = Latin with { Language = Language.Deutsch };
    public static readonly LanguageEncoding Français = Latin with { Language = Language.Français };
    public static readonly LanguageEncoding Español = Latin with { Language = Language.Español };
    public static readonly LanguageEncoding Italiano = Latin with { Language = Language.Italiano };
}
