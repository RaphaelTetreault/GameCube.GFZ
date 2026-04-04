using System.Text;

namespace GameCube.GFZ.GameData;

/// <summary>
///     "Tuple" of <see cref="GameData.Language"/> and <see cref="System.Text.Encoding"/>.
/// </summary>
public readonly record struct LanguageEncoding
{
    public required Language Language { get; init; }
    public required Encoding Encoding { get; init; }
}
