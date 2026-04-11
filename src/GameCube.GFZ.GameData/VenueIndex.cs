namespace GameCube.GFZ.GameData;

/// <summary>
///     Venue index (internal venue index).
/// </summary>
public enum VenueIndex : byte
{
    None,
    MuteCity,
    PortTown,
    PortTownStory,
    BigBlue,
    BigBlueStory,
    Lightning,
    LightningStory,
    SandOcean,
    SandOceanStory,
    GreenPlant,
    FireField,
    FireFieldStory,
    CasinoPalace,
    OuterSpace,
    Aeropolis,
    CosmoTerminal,
    MuteCityCom,
    MuteCityComStory,
    PhantomRoad,
    MuteCityGrandPrixPodium,
}

public static class VenueIndexExtensions
{
    extension(VenueIndex venueIndex)
    {
        public byte Byte => (byte)venueIndex;        
    }
}