using System.ComponentModel;

namespace GameCube.GFZ.REL
{
    public enum Venue : byte
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
        Com,

        [Description("Mute City (Story)")]
        MuteCityStory,

        [Description("Phantom Road")]
        PhantomRoad,

        [Description("WIN")]
        Win,
    }
}
