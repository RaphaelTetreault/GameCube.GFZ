using System;
using System.Collections.Immutable;

namespace GameCube.GFZ.GameData;

/// <summary>
///     Represents a Grand Prix and Time Attack cup.
/// </summary>
public readonly record struct Cup
{
    private const int MaxCourseCount = 6;

    public required CupIndex CupIndex { get; init; }
    public required Course[] Courses { get; init; }
    public required ImmutableDictionary<Language, string> Name { get; init; }

    public Cup()
    {
        Courses = new Course[MaxCourseCount];
    }

    public Cup(params Course[] courses)
    {
        if (courses.Length > MaxCourseCount)
        {
            string msg = $"A {nameof(Cup)} has a maximum of {MaxCourseCount} courses.";
            throw new ArgumentException(msg);
        }

        Courses = new Course[MaxCourseCount];
        courses.CopyTo(Courses, 0);
    }
}
