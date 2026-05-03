using System.Collections.Immutable;

namespace GameCube.GFZ.GameData;

/// <summary>
///     Represents a course.
/// </summary>
public readonly record struct Course
{
    public const ushort UnassignedCourseIndex = 0xFFFF;

    public Course()
    {
    }

    public required ushort CourseIndex { get; init; } = UnassignedCourseIndex;
    public required ImmutableDictionary<GameCode, string> Name { get; init; }
    public byte StarDifficultyRating { get; init; }
    public required Venue Venue { get; init; }

    public readonly string DisplayText(GameCode gameCode)
    {
        string venue = Venue.Name[gameCode];
        string course = this.Name[gameCode];
        return $"{venue} [{course}]";
    }
}
