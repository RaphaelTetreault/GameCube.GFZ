using System.Collections.Frozen;

namespace GameCube.GFZ.GameData;

/// <summary>
///     Represents a course.
/// </summary>
public readonly record struct Course
{
    public required ushort CourseIndex { get; init; }
    public required FrozenDictionary<GameCode, string> Name { get; init; }
    public required Venue Venue { get; init; }

    public readonly string DisplayText(GameCode gameCode)
    {
        string venue = Venue.Name[gameCode];
        string course = this.Name[gameCode];
        return $"{venue} [{course}]";
    }
}
