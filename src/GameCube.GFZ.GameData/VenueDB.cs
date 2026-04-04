using System;
using System.Collections.Frozen;

namespace GameCube.GFZ.GameData;

/// <summary>
///     DataBase of default game <see cref="Venue"/>s.
/// </summary>
public static class VenueDB
{
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
        public const string LightningStory = Lightning + Story;
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
        VenueIndex = VenueIndex.Aeropolis,
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
        VenueIndex = VenueIndex.BigBlue,
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
        VenueIndex = VenueIndex.BigBlueStory,
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
        VenueIndex = VenueIndex.CasinoPalace,
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
        VenueIndex = VenueIndex.CosmoTerminal,
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
        VenueIndex = VenueIndex.FireField,
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
        VenueIndex = VenueIndex.FireFieldStory,
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
        VenueIndex = VenueIndex.GreenPlant,
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
        VenueIndex = VenueIndex.Lightning,
        Name = FrozenDictionary.Create<GameCode, string>(
        [
            new (GameCode.GFZE01, Names.Lightning),
            new (GameCode.GFZJ01, Names.Lightning),
            new (GameCode.GFZP01, Names.Lightning),
            new (GameCode.GGGE6E, Names.Lightning),
        ]),
    };

    public static readonly Venue LightningStory = new()
    {
        VenueIndex = VenueIndex.LightningStory,
        Name = FrozenDictionary.Create<GameCode, string>(
        [
            new (GameCode.GFZE01, Names.LightningStory),
            new (GameCode.GFZJ01, Names.LightningStory),
            new (GameCode.GFZP01, Names.LightningStory),
            new (GameCode.GGGE6E, Names.LightningStory),
        ]),
    };

    public static readonly Venue MuteCity = new()
    {
        VenueIndex = VenueIndex.MuteCity,
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
        VenueIndex = VenueIndex.MuteCityCom,
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
        VenueIndex = VenueIndex.MuteCityComStory,
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
        VenueIndex = VenueIndex.MuteCityGrandPrixPodium,
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
        VenueIndex = VenueIndex.None,
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
        VenueIndex = VenueIndex.OuterSpace,
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
        VenueIndex = VenueIndex.PhantomRoad,
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
        VenueIndex = VenueIndex.PortTown,
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
        VenueIndex = VenueIndex.PortTownStory,
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
        VenueIndex = VenueIndex.SandOcean,
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
        VenueIndex = VenueIndex.SandOceanStory,
        Name = FrozenDictionary.Create<GameCode, string>(
        [
            new (GameCode.GFZE01, Names.SandOceanStory),
            new (GameCode.GFZJ01, Names.SandOceanStory),
            new (GameCode.GFZP01, Names.SandOceanStory),
            new (GameCode.GGGE6E, Names.SandOceanStory),
        ]),
    };


    public static readonly Venue[] DefaultVenues =
    [
        Null,
        MuteCity,
        PortTown,
        PortTownStory,
        BigBlue,
        // 5
        BigBlueStory,
        Lightning,
        LightningStory,
        SandOcean,
        SandOceanStory,
        // 10
        GreenPlant,
        FireField,
        FireFieldStory,
        CasinoPalace,
        OuterSpace,
        // 15
        Aeropolis,
        CosmoTerminal,
        MuteCityCom,
        MuteCityComStory,
        PhantomRoad,
        // 20
        MuteCityGrandPrixPodium,
    ];

    /// <summary>
    ///     Maps <see cref="VenueIndex"/> to <see cref="BgmMusic"/>.
    /// </summary>
    /// <remarks>
    ///     Returned array of <see cref="BgmMusic"/> can be empty or contain an alternative song (Mute City, Big Blue).
    /// </remarks>
    public static readonly FrozenDictionary<VenueIndex, BgmMusic[]> VenueBgmMusic = FrozenDictionary.Create<VenueIndex, BgmMusic[]>(
    [
        new (VenueIndex.None, []),
        new (VenueIndex.MuteCity, [BgmMusicDB.MuteCity, BgmMusicDB.MuteCitySecret]),
        new (VenueIndex.PortTown, [BgmMusicDB.PortTown]),
        new (VenueIndex.PortTownStory, [BgmMusicDB.Story6]),
        new (VenueIndex.BigBlue, [BgmMusicDB.BigBlue, BgmMusicDB.BigBlueSecret]),
        new (VenueIndex.BigBlueStory, [BgmMusicDB.NightOfBigBlue]),
        new (VenueIndex.Lightning, [BgmMusicDB.Lightning]),
        new (VenueIndex.LightningStory, []),
        new (VenueIndex.SandOcean, [BgmMusicDB.SandOcean]),
        new (VenueIndex.SandOceanStory, [BgmMusicDB.RedCanyon]),
        new (VenueIndex.GreenPlant, [BgmMusicDB.GreenPlant]),
        new (VenueIndex.FireField, [BgmMusicDB.FireField]),
        new (VenueIndex.FireFieldStory, [BgmMusicDB.Story8]),
        new (VenueIndex.CasinoPalace, [BgmMusicDB.CasinoPalace]),
        new (VenueIndex.OuterSpace, [BgmMusicDB.OuterSpace]),
        new (VenueIndex.Aeropolis, [BgmMusicDB.Aeropolis]),
        new (VenueIndex.CosmoTerminal, [BgmMusicDB.CosmoTerminal]),
        new (VenueIndex.MuteCityCom, [BgmMusicDB.MuteCity, BgmMusicDB.MuteCitySecret]),
        new (VenueIndex.MuteCityComStory, [BgmMusicDB.MuteCity]),
        new (VenueIndex.PhantomRoad, [BgmMusicDB.PhantomRoad]),
        new (VenueIndex.MuteCityGrandPrixPodium, [BgmMusicDB.WinningRunGX]),
    ]);

    public static void UnitTest()
    {
        foreach (var gameCode in Enum.GetValues<GameCode>())
        {
            if (gameCode == GameCode.GFZJ8P)
                continue;

            Console.WriteLine(gameCode);
            for (int i = 0; i < GameDataConsts.MaxVenueIndex; i++)
            {
                Venue v = DefaultVenues[i];
                if (v != Null)
                {
                    if (i != (int)v.VenueIndex)
                        throw new Exception("Wrong index match!");
                }

                // Catch few cases where only AX defines track, so GameCode is not present for course name
                if (!v.Name.ContainsKey(gameCode))
                    v = Null;
                // Print
                Console.WriteLine($"{gameCode} {i,3} - Stage: {v.VenueIndex,3}, {v.Name[gameCode]}");
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    ///         Get default venue for stage ID <paramref name="index"/>.
    /// </summary>
    /// <param name="index">The stage ID index (0-110).</param>
    /// <returns>
    ///     
    /// </returns>
    public static VenueIndex GetDefaultVenueID(int index)
    {
        return index switch
        {
            00 => VenueIndex.SandOcean,    // AX test for Sand Ocean [Screw Drive]
            01 => VenueIndex.MuteCity,     // Mute City [Twist Road]
            02 => VenueIndex.MuteCity,
            03 => VenueIndex.MuteCity,     // Mute City [Serial Gaps]
            04 => VenueIndex.MuteCity,
            05 => VenueIndex.Aeropolis,    // Aeropolis [Multiplex]
            06 => VenueIndex.PortTown,
            07 => VenueIndex.PortTown,     // Port Town [Aero Dive]
            08 => VenueIndex.Lightning,    // Lightning [Loop Cross]
            09 => VenueIndex.Lightning,    // Lightning [Half-Pipe]
            10 => VenueIndex.GreenPlant,   // Green Plant [Intersection]
            11 => VenueIndex.GreenPlant,   // Green Plant [Mobius Ring]
            12 => VenueIndex.Lightning,
            13 => VenueIndex.PortTown,     // Port Town [Long Pipe]
            14 => VenueIndex.BigBlue,      // Big Blue [Drift Highway]
            15 => VenueIndex.FireField,    // Fire Field [Cylinder Knot]
            16 => VenueIndex.CasinoPalace, // Casino Palace [Split Oval]
            17 => VenueIndex.FireField,    // Fire Field [Undulation]
            18 => VenueIndex.FireField,
            19 => VenueIndex.OuterSpace,
            20 => VenueIndex.OuterSpace,
            21 => VenueIndex.Aeropolis,    // Aeropolis [Dragon Slope]
            22 => VenueIndex.CosmoTerminal,
            23 => VenueIndex.Lightning,
            24 => VenueIndex.CosmoTerminal,// Cosmo Terminal [Trident]
            25 => VenueIndex.SandOcean,    // Sand Ocean [Lateral Shift]
            26 => VenueIndex.SandOcean,    // Sand Ocean [Surface Slide]
            27 => VenueIndex.BigBlue,      // Big Blue [Ordeal]
            28 => VenueIndex.PhantomRoad,  // Phantom Road [Slim-line Slits]
            29 => VenueIndex.CasinoPalace, // Casino Palace [Double Branches]
            30 => VenueIndex.SandOcean,
            31 => VenueIndex.Aeropolis,    // Aeropolis [Screw Drive]
            32 => VenueIndex.OuterSpace,   // Outer Space [Meteor Stream]
            33 => VenueIndex.PortTown,     // Port Town [Cylinder Wave]
            34 => VenueIndex.Lightning,    // Lightning [Thunder Road]
            35 => VenueIndex.GreenPlant,   // Green Plant [Sprial]
            36 => VenueIndex.MuteCityCom,  // Mute City [Sonic Oval]
            37 => VenueIndex.MuteCityComStory, // Chapter 1  Captain Falcon Trains
            38 => VenueIndex.SandOceanStory,   // Chapter 2  Goroh: The Vengeful Samurai
            39 => VenueIndex.CasinoPalace,     // Chapter 3  High Stakes in Mute City
            40 => VenueIndex.BigBlueStory,     // Chapter 4  Challenge of the Bloody Chain
            41 => VenueIndex.Lightning,        // Chapter 5  Save Jody Summer!
            42 => VenueIndex.PortTownStory,    // Chapter 6  Black Shadow's Trap
            43 => VenueIndex.MuteCity,         // Chapter 7  The F-Zero Grand Prix
            44 => VenueIndex.FireFieldStory,   // Chapter 8  Secrets of the Champion Belt
            45 => VenueIndex.PhantomRoad,      // Chapter 9  Finale: Enter The Creators

            49 => VenueIndex.MuteCityGrandPrixPodium, // Grand Prix Podium
            50 => VenueIndex.MuteCityGrandPrixPodium, // Victory Lap

            _ => VenueIndex.None, // All other indices are unset
        };
    }

    public static string GetVenueBackgroundName(VenueIndex venueID, GameCode gameCode) => venueID switch
    {
        VenueIndex.Aeropolis => "tow",   // Tower = Aeropolis
        VenueIndex.BigBlue => "big",
        VenueIndex.BigBlueStory => "big_s",
        VenueIndex.CasinoPalace => "cas",   // Vegas Palace = Casino Palace
        VenueIndex.CosmoTerminal => "ele",   // Elevator = Cosmo Terminal
        VenueIndex.FireField => "fir",
        VenueIndex.FireFieldStory => "fir_s",
        VenueIndex.GreenPlant => "for",   // Forest = Green Plant
        VenueIndex.Lightning => "lig",
        VenueIndex.LightningStory => throw new ArgumentException($"No known file match for {(int)venueID} {venueID}"),
        VenueIndex.MuteCity => "mut",
        VenueIndex.MuteCityCom => "com",   // Com for Combo? Mute City + Casino Palace.
        VenueIndex.MuteCityComStory => "com_s",
        VenueIndex.MuteCityGrandPrixPodium => IsGX(gameCode) ? "win_gx" : IsAX(gameCode) ? "win" : throw GetGameCodeNotAxOrGxException(gameCode),
        VenueIndex.OuterSpace => "met",   // Meteor = Outer Space
        VenueIndex.PhantomRoad => "rai",   // Rainbow Road = Phantom Road
        VenueIndex.PortTown => "por",
        VenueIndex.PortTownStory => "por_s",
        VenueIndex.SandOcean => "san",
        VenueIndex.SandOceanStory => "san_s",
        _ => throw GetVenueIDInvalidException(venueID),
    };

    public static string GetDefaultVenueName(GameCode gameCode, int courseIndex)
    {
        Course course = CourseDB.DefaultCourses[courseIndex];
        Venue venue = course.Venue;
        string name = venue.Name[gameCode];
        return name;
    }

    public static string GetDefaultVenueName(GameCode gameCode, VenueIndex venueID)
    {
        Venue venue = DefaultVenues[(int)venueID];
        string name = venue.Name[gameCode];
        return name;
    }

    // TODO: consider moving this into a utlity class for GameCode, GameCodeFields, AvGame, etc.
#pragma warning disable CA2248 // Provide correct 'enum' argument to 'Enum.HasFlag'
    private static bool IsAX(GameCode gameCode) => gameCode.HasFlag(GameCodeFlags.AX);
    private static bool IsGX(GameCode gameCode) => gameCode.HasFlag(GameCodeFlags.GX);
#pragma warning restore CA2248 // Provide correct 'enum' argument to 'Enum.HasFlag'

    private static ArgumentException GetGameCodeNotAxOrGxException(GameCode gameCode)
    {
        string msg = $"Invalid {nameof(GameCode)} {gameCode}. No flags for AX or GX defined.";
        return new ArgumentException(msg);
    }

    private static ArgumentException GetVenueIDInvalidException(VenueIndex venueID)
    {
        string msg = $"Invalid {nameof(VenueIndex)} value {venueID} ({(int)venueID}).";
        return new ArgumentException(msg);
    }

}
