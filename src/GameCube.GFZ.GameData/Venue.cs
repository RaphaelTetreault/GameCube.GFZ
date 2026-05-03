using System.Collections.Immutable;

namespace GameCube.GFZ.GameData;

/// <summary>
///     Represents a course venue.
/// </summary>
public readonly record struct Venue
{
    public required ImmutableDictionary<GameCode, string> Name { get; init; }
    public required VenueIndex VenueIndex { get; init; }
}
