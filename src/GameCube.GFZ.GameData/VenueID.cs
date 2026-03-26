using System.ComponentModel;

namespace GameCube.GFZ.GameData;

/// <summary>
///     Venue index (internal venue index).
/// </summary>
/// <remarks>
///     Description attribute contains display name of each venue.
/// </remarks>
public enum VenueID : byte
{
    [Description("Null")]
    None,

    [Description("Mute City")]
    MuteCity,

    [Description("Port Town")]
    PortTown,

    [Description("Port Town (Story)")]
    PortTownStory,

    [Description("Big Blue")]
    BigBlue,

    [Description("Big Blue (Story)")]
    BigBlueStory,

    [Description("Lightning")]
    Lightning,

    [Description("Lightning (Story)")]
    LightningStory,

    [Description("Sand Ocean")]
    SandOcean,

    [Description("Sand Ocean (Story)")]
    SandOceanStory,

    [Description("Green Plant")]
    GreenPlant,

    [Description("Fire Field")]
    FireField,

    [Description("Fire Field (Story)")]
    FireFieldStory,

    [Description("Casino Palace")]
    CasinoPalace,

    [Description("Outer Space")]
    OuterSpace,

    [Description("Aeropolis")]
    Aeropolis,

    [Description("Cosmo Terminal")]
    CosmoTerminal,

    [Description("Mute City COM")]
    MuteCityCom,

    [Description("Mute City (Story)")]
    MuteCityComStory,

    [Description("Phantom Road")]
    PhantomRoad,

    [Description("Mute City (Grand Prix Podium)")]
    MuteCityGrandPrixPodium,
}
