using System.Collections.Frozen;

namespace GameCube.GFZ.GameData
{
    public readonly record struct Venue
    {
        public required VenueID ID { get; init; }
        public required FrozenDictionary<GameCode, string> Name { get; init; }

        public static class Names
        {
            public const string Aeropolis = "Aeropolis";
            public const string BigBlue = "Big Blue";
            public const string CasinoPalace = "Casino Palace";
            public const string CosmoTerminal = "Cosmo Terminal";
            public const string Elevator = "Elevator";
            public const string Fire = "Fire";
            public const string FireField = "Fire Field";
            public const string GreenPlant = "Green Plant";
            public const string Lightning = "Lightning";
            public const string MuteCity = "Mute City";
            public const string MuteCityCom = "Mute City";
            public const string MuteCityGrandPrixPodium = Null;
            public const string Null = "Null";
            public const string OuterSpace = "Outer Space";
            public const string PhantomRoad = "Phantom Road";
            public const string PortTown = "Port Town";
            public const string RainbowRoad = "Rainbow Road";
            public const string SandOcean = "Sand Ocean";
            public const string VegasPalace = "Vegas Palace";
            // Story
            public const string Story = " (Story)";
            public const string BigBlueStory = BigBlue + Story;
            public const string FireStory = Fire + Story; // Fire
            public const string FireFieldStory = FireField + Story; // Fire Field
            public const string MuteCityComStory = MuteCityCom + Story; // Mute City
            public const string PortTownStory = PortTown + Story; // Port Town
            public const string SandOceanStory = SandOcean + Story; // Red Canyon
            // Considerations for above story venue names
            public const string Story1 = MuteCityCom;
            public const string Story2 = "Red Canyon";
            //public const string Story3 = CasinoPalace;
            public const string Story4 = BigBlue;
            //public const string Story5 = Lightning;
            public const string Story6 = PortTown;
            //public const string Story7 = MuteCity;
            public const string Story8 = "Underworld";
            //public const string Story9 = PhantomRoad;
        }

        public static readonly Venue Aeropolis = new()
        {
            ID = VenueID.Aeropolis,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.Aeropolis),
                new (GameCode.GFZJ01, Names.Aeropolis),
                new (GameCode.GFZP01, Names.Aeropolis),
                new (GameCode.GGGE6E, Names.Aeropolis),
            ]),
        };

        public static readonly Venue BigBlue = new()
        {
            ID = VenueID.BigBlue,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.BigBlue),
                new (GameCode.GFZJ01, Names.BigBlue),
                new (GameCode.GFZP01, Names.BigBlue),
                new (GameCode.GGGE6E, Names.BigBlue),
            ]),
        };


        public static readonly Venue BigBlueStory = new()
        {
            ID = VenueID.BigBlueStory,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.BigBlueStory),
                new (GameCode.GFZJ01, Names.BigBlueStory),
                new (GameCode.GFZP01, Names.BigBlueStory),
                new (GameCode.GGGE6E, Names.BigBlueStory),
            ]),
        };

        public static readonly Venue CasinoPalace = new()
        {
            ID = VenueID.CasinoPalace,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.CasinoPalace),
                new (GameCode.GFZJ01, Names.VegasPalace),
                new (GameCode.GFZP01, Names.CasinoPalace),
                new (GameCode.GGGE6E, Names.VegasPalace),
            ]),
        };

        public static readonly Venue CosmoTerminal = new()
        {
            ID = VenueID.CosmoTerminal,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.CosmoTerminal),
                new (GameCode.GFZJ01, Names.CosmoTerminal),
                new (GameCode.GFZP01, Names.CosmoTerminal),
                new (GameCode.GGGE6E, Names.Elevator),
            ]),
        };

        public static readonly Venue FireField = new()
        {
            ID = VenueID.FireField,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.FireField),
                new (GameCode.GFZJ01, Names.FireField),
                new (GameCode.GFZP01, Names.FireField),
                new (GameCode.GGGE6E, Names.Fire),
            ]),
        };

        public static readonly Venue FireFieldStory = new()
        {
            ID = VenueID.FireFieldStory,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.FireFieldStory),
                new (GameCode.GFZJ01, Names.FireFieldStory),
                new (GameCode.GFZP01, Names.FireFieldStory),
                new (GameCode.GGGE6E, Names.FireStory),
            ]),
        };

        public static readonly Venue GreenPlant = new()
        {
            ID = VenueID.GreenPlant,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.GreenPlant),
                new (GameCode.GFZJ01, Names.GreenPlant),
                new (GameCode.GFZP01, Names.GreenPlant),
                new (GameCode.GGGE6E, Names.GreenPlant),
            ]),
        };

        public static readonly Venue Lightning = new()
        {
            ID = VenueID.Lightning,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.Lightning),
                new (GameCode.GFZJ01, Names.Lightning),
                new (GameCode.GFZP01, Names.Lightning),
                new (GameCode.GGGE6E, Names.Lightning),
            ]),
        };

        public static readonly Venue MuteCity = new()
        {
            ID = VenueID.MuteCity,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.MuteCity),
                new (GameCode.GFZJ01, Names.MuteCity),
                new (GameCode.GFZP01, Names.MuteCity),
                new (GameCode.GGGE6E, Names.MuteCity),
            ]),
        };

        public static readonly Venue MuteCityCom = new()
        {
            ID = VenueID.MuteCityCom,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.MuteCityCom),
                new (GameCode.GFZJ01, Names.MuteCityCom),
                new (GameCode.GFZP01, Names.MuteCityCom),
                new (GameCode.GGGE6E, Names.MuteCityCom),
            ]),
        };

        public static readonly Venue MuteCityComStory = new()
        {
            ID = VenueID.MuteCityComStory,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.MuteCityComStory),
                new (GameCode.GFZJ01, Names.MuteCityComStory),
                new (GameCode.GFZP01, Names.MuteCityComStory),
                new (GameCode.GGGE6E, Names.MuteCityComStory),
            ]),
        };

        public static readonly Venue MuteCityGrandPrixPodium = new()
        {
            ID = VenueID.MuteCityGrandPrixPodium,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.MuteCityGrandPrixPodium),
                new (GameCode.GFZJ01, Names.MuteCityGrandPrixPodium),
                new (GameCode.GFZP01, Names.MuteCityGrandPrixPodium),
                new (GameCode.GGGE6E, Names.MuteCityGrandPrixPodium),
            ]),
        };

        public static readonly Venue Null = new()
        {
            ID = VenueID.None,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.Null),
                new (GameCode.GFZJ01, Names.Null),
                new (GameCode.GFZP01, Names.Null),
                new (GameCode.GGGE6E, Names.Null),
            ]),
        };

        public static readonly Venue OuterSpace = new()
        {
            ID = VenueID.OuterSpace,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.OuterSpace),
                new (GameCode.GFZJ01, Names.OuterSpace),
                new (GameCode.GFZP01, Names.OuterSpace),
                new (GameCode.GGGE6E, Names.OuterSpace),
            ]),
        };

        public static readonly Venue PhantomRoad = new()
        {
            ID = VenueID.PhantomRoad,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.PhantomRoad),
                new (GameCode.GFZJ01, Names.PhantomRoad),
                new (GameCode.GFZP01, Names.PhantomRoad),
                new (GameCode.GGGE6E, Names.RainbowRoad),
            ]),
        };

        public static readonly Venue PortTown = new()
        {
            ID = VenueID.PortTown,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.PortTown),
                new (GameCode.GFZJ01, Names.PortTown),
                new (GameCode.GFZP01, Names.PortTown),
                new (GameCode.GGGE6E, Names.PortTown),
            ]),
        };

        public static readonly Venue PortTownStory = new()
        {
            ID = VenueID.PortTownStory,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.PortTownStory),
                new (GameCode.GFZJ01, Names.PortTownStory),
                new (GameCode.GFZP01, Names.PortTownStory),
                new (GameCode.GGGE6E, Names.PortTownStory),
            ]),
        };

        public static readonly Venue SandOcean = new()
        {
            ID = VenueID.SandOcean,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.SandOcean),
                new (GameCode.GFZJ01, Names.SandOcean),
                new (GameCode.GFZP01, Names.SandOcean),
                new (GameCode.GGGE6E, Names.SandOcean),
            ]),
        };

        public static readonly Venue SandOceanStory = new()
        {
            ID = VenueID.SandOceanStory,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, Names.SandOceanStory),
                new (GameCode.GFZJ01, Names.SandOceanStory),
                new (GameCode.GFZP01, Names.SandOceanStory),
                new (GameCode.GGGE6E, Names.SandOceanStory),
            ]),
        };

    }
}
