using System.Collections.Immutable;
using System.Numerics;

namespace GameCube.GFZ.GameData;

public readonly record struct Machine
{
    public required MachineIndex MachineIndex { get; init; }
    public required byte MachineNumber { get; init; }
    public required ImmutableDictionary<Language, string> MachineName { get; init; }
    public required ImmutableDictionary<Language, string[]> PilotNames { get; init; }
    public required ImmutableDictionary<PilotIndex, Vector3> PilotPositions { get; init; }
}