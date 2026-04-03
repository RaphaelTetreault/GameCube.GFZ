using System.Collections.Frozen;

namespace GameCube.GFZ.GameData;

/// <summary>
///     Represents a course venue.
/// </summary>
public readonly record struct Venue
{
    public required FrozenDictionary<GameCode, string> Name { get; init; }
    public required VenueIndex VenueIndex { get; init; }
}
