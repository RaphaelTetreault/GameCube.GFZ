using System.Collections.Immutable;

namespace GameCube.GFZ.GameData;

public readonly record struct Pilot
{
    public required PilotIndex PilotIndex { get; init; }
    public required ImmutableDictionary<Language, string> PilotName { get; init; }
    public required Machine Machine { get; init; }

    // Other forwarding?
    public byte PilotNumber => Machine.MachineNumber;
}
