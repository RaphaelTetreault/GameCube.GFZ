using System.ComponentModel;

// TODO: implement the correct indices
// Note: sorted alphabetically
namespace GameCube.GFZ.REL
{
    public enum Venue : byte
    {
        [Description("Aeropolis")]
        Aeropolis,

        [Description("Big Blue")]
        BigBlue,

        [Description("Casino Palace")]
        CasinoPalace,

        [Description("Cosmo Terminal")]
        CosmoTerminal,

        [Description("Fire Field")]
        FireField,

        [Description("Green Plant")]
        GreenPlant,

        [Description("Lightning")]
        Lightning,

        [Description("Null")]
        None,

        [Description("Mute City")]
        MuteCity,

        [Description("Mute City (COM)")]
        MuteCityCOM,

        [Description("Outer Space")]
        OuterSpace,

        [Description("Phantom Road")]
        PhantomRoad,

        [Description("Port Town")]
        PortTown,

        [Description("Sand Ocean")]
        SandOcean,

        // OTHER
        [Description("Win")]
        Win,

        // STORY
        [Description("Big Blue (Story)")]
        StoryBigBlue,

        [Description("Fire Field (Story)")]
        StoryFireField,

        [Description("Mute City (COM) (Story)")]
        StoryMuteCityCOM,

        [Description("Port Town (Story)")]
        StoryPortTown,

        [Description("Sand Ocean (Story)")]
        StorySandOcean,
    }
}
