using GameCube.Common;
using System.Collections.Frozen;
using System.Text;

namespace GameCube.GFZ.GameData;

/// <summary>
///     DataBase of default game <see cref="GameData.LanguageEncoding"/>s.
/// </summary>
public static class LanguageEncodingDB
{
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

    public static readonly FrozenDictionary<Language, Encoding> LanguageEncoding = FrozenDictionary.Create<Language, Encoding>
    ([
        new (Language.Japanese, TextEncoding.ShiftJIS),
        new (Language.English, TextEncoding.Windows1252),
        new (Language.Deutsch, TextEncoding.Windows1252),
        new (Language.Français, TextEncoding.Windows1252),
        new (Language.Español, TextEncoding.Windows1252),
        new (Language.Italiano, TextEncoding.Windows1252),
        new (Language.Latin, TextEncoding.Windows1252),
    ]);
}
