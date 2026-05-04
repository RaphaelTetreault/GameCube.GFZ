using System.Collections.Immutable;

namespace GameCube.GFZ.GameData;

internal class PilotDB
{
    public static class PilotNames
    {
        public const string MightyGazelle = "Mighty Gazelle";
        public const string JodySummer = "Jody Summer";
        public const string DrStewart = "Dr. Stewart";
        public const string Baba = "Baba";
        public const string SamuraiGoroh = "Samurai Goroh";
        public const string Pico = "Pico";
        public const string CaptainFalcon = "Captain Falcon";
        public const string Octman = "Octman";
        public const string Octoman = "Octoman";
        public const string MrEAD = "Mr. EAD";
        public const string JamesMcCloud = "James McCloud";
        public const string Billy = "Billy";
        public const string KateAlen = "Kate Alen";
        public const string Zoda = "Zoda";
        public const string JackLevin = "Jack Levin";
        public const string BioRex = "Bio Rex";
        public const string TheSkull = "The Skull";
        public const string AntonioGuster = "Antonio Guster";
        public const string Beastman = "Beastman";
        public const string Leon = "Leon";
        public const string SuperArrow = "Super Arrow";
        public const string MrsArrow = "Mrs. Arrow";
        public const string GomarAndShioh = "Gomar & Shioh";
        public const string Gomar = "Gomar";
        public const string Shioh = "Shioh";
        public const string SilverNeelsen = "Silver Neelson";
        public const string MichaelChain = "Michael Chain";
        public const string BloodFalcon = "Blood Falcon";
        public const string JohnTanaka = "John Tanaka";
        public const string Draq = "Draq";
        public const string RogerBuster = "Roger Buster";
        public const string DrClash = "Dr. Clash";
        public const string BlackShadow = "Black Shadow";
        public const string Deathborn = "Deathborn";
        public const string DonGenie = "Don Genie";
        public const string DigiBoy = "Digi-boy";
        public const string DaiSanGen = "Dai San Gen";
        public const string Dai = "Dai";
        public const string San = "San";
        public const string Gen = "Gen";
        public const string Spade = "Spade";
        public const string Daigoroh = "Daigoroh";
        public const string PrinciaRamode = "Princia";
        public const string LilyFlyer = "Lily";
        public const string PJ = "PJ";
        public const string QQQ = "QQQ";
        public const string Pheonix = "Pheonix";
        public const string MrZero = "Mr. Zero";
    }

    public static readonly Pilot MightyGazelle = new()
    {
        Machine = MachineDB.RedGazelle,
        PilotIndex = PilotIndex.MightyGazelle,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.MightyGazelle),
            new(Language.Latin, PilotNames.MightyGazelle),
        ]),
    };

    public static readonly Pilot JodySummer = new()
    {
        Machine = MachineDB.WhiteCat,
        PilotIndex = PilotIndex.JodySummer,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.JodySummer),
            new(Language.Latin, PilotNames.JodySummer),
        ]),
    };

    public static readonly Pilot DrStewart = new()
    {
        Machine = MachineDB.GoldenFox,
        PilotIndex = PilotIndex.DrStewart,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.DrStewart),
            new(Language.Latin, PilotNames.DrStewart),
        ]),
    };

    public static readonly Pilot Baba = new()
    {
        Machine = MachineDB.IronTiger,
        PilotIndex = PilotIndex.Baba,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Baba),
            new(Language.Latin, PilotNames.Baba),
        ]),
    };

    public static readonly Pilot SamuraiGoroh = new()
    {
        Machine = MachineDB.FireStingray,
        PilotIndex = PilotIndex.SamuraiGoroh,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.SamuraiGoroh),
            new(Language.Latin, PilotNames.SamuraiGoroh),
        ]),
    };

    public static readonly Pilot Pico = new()
    {
        Machine = MachineDB.WildGoose,
        PilotIndex = PilotIndex.Pico,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Pico),
            new(Language.Latin, PilotNames.Pico),
        ]),
    };

    public static readonly Pilot CaptainFalcon = new()
    {
        Machine = MachineDB.BlueFalcon,
        PilotIndex = PilotIndex.CaptainFalcon,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
    [
        new(Language.Japanese, PilotNames.CaptainFalcon),
            new(Language.Latin, PilotNames.CaptainFalcon),
        ]),
    };

    public static readonly Pilot Octoman = new()
    {
        Machine = MachineDB.DeepClaw,
        PilotIndex = PilotIndex.Octoman,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Octman),
            new(Language.Latin, PilotNames.Octoman),
        ]),
    };

    public static readonly Pilot MrEAD = new()
    {
        Machine = MachineDB.GreatStar,
        PilotIndex = PilotIndex.MrEAD,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.MrEAD),
            new(Language.Latin, PilotNames.MrEAD),
        ]),
    };

    public static readonly Pilot JamesMcCloud = new()
    {
        Machine = MachineDB.LittleWyvern,
        PilotIndex = PilotIndex.JamesMcCloud,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.JamesMcCloud),
            new(Language.Latin, PilotNames.JamesMcCloud),
        ]),
    };

    public static readonly Pilot Billy = new()
    {
        Machine = MachineDB.MadWolf,
        PilotIndex = PilotIndex.Billy,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Billy),
            new(Language.Latin, PilotNames.Billy),
        ]),
    };

    public static readonly Pilot KateAlen = new()
    {
        Machine = MachineDB.SuperPiranha,
        PilotIndex = PilotIndex.KateAlen,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.KateAlen),
            new(Language.Latin, PilotNames.KateAlen),
        ]),

    };

    public static readonly Pilot Zoda = new()
    {
        Machine = MachineDB.DeathAnchor,
        PilotIndex = PilotIndex.Zoda,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Zoda),
            new(Language.Latin, PilotNames.Zoda),
        ]),
    };

    public static readonly Pilot JackLevin = new()
    {
        Machine = MachineDB.AstroRobin,
        PilotIndex = PilotIndex.JackLevin,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.JackLevin),
            new(Language.Latin, PilotNames.JackLevin),
        ]),
    };

    public static readonly Pilot BioRex = new()
    {
        Machine = MachineDB.BigFang,
        PilotIndex = PilotIndex.BioRex,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.BioRex),
            new(Language.Latin, PilotNames.BioRex),
        ]),
    };

    public static readonly Pilot TheSkull = new()
    {
        Machine = MachineDB.SonicPhantom,
        PilotIndex = PilotIndex.TheSkull,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.TheSkull),
            new(Language.Latin, PilotNames.TheSkull),
        ]),
    };

    public static readonly Pilot AntonioGuster = new()
    {
        Machine = MachineDB.GreenPanther,
        PilotIndex = PilotIndex.AntonioGuster,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.AntonioGuster),
            new(Language.Latin, PilotNames.AntonioGuster),
        ]),
    };

    public static readonly Pilot Beastman = new()
    {
        Machine = MachineDB.HyperSpeeder,
        PilotIndex = PilotIndex.Beastman,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Beastman),
            new(Language.Latin, PilotNames.Beastman),
        ]),
    };

    public static readonly Pilot Leon = new()
    {
        Machine = MachineDB.SpaceAngler,
        PilotIndex = PilotIndex.Leon,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Leon),
            new(Language.Latin, PilotNames.Leon),
        ]),
    };

    public static readonly Pilot SuperArrow = new()
    {
        Machine = MachineDB.KingMeteor,
        PilotIndex = PilotIndex.SuperArrow,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.SuperArrow),
            new(Language.Latin, PilotNames.SuperArrow),
        ]),
    };

    public static readonly Pilot MrsArrow = new()
    {
        Machine = MachineDB.QueenMeteor,
        PilotIndex = PilotIndex.MrsArrow,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.MrsArrow),
            new(Language.Latin, PilotNames.MrsArrow),
        ]),
    };

    public static readonly Pilot GomarAndShioh = new()
    {
        Machine = MachineDB.TwinNoritta,
        PilotIndex = PilotIndex.Shioh,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.GomarAndShioh),
            new(Language.Latin, PilotNames.GomarAndShioh),
        ]),
    };

    public static readonly Pilot SilverNeelsen = new()
    {
        Machine = MachineDB.NightThunder,
        PilotIndex = PilotIndex.SilverNeelsen,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.SilverNeelsen),
            new(Language.Latin, PilotNames.SilverNeelsen),
        ]),
    };

    public static readonly Pilot MichaelChain = new()
    {
        Machine = MachineDB.WildBoar,
        PilotIndex = PilotIndex.MichaelChain,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.MichaelChain),
            new(Language.Latin, PilotNames.MichaelChain),
        ]),
    };

    public static readonly Pilot BloodFalcon = new()
    {
        Machine = MachineDB.BloodHawk,
        PilotIndex = PilotIndex.BloodFalcon,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.BloodFalcon),
            new(Language.Latin, PilotNames.BloodFalcon),
        ]),
    };

    public static readonly Pilot JohnTanaka = new()
    {
        Machine = MachineDB.WonderWasp,
        PilotIndex = PilotIndex.JohnTanaka,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.JohnTanaka),
            new(Language.Latin, PilotNames.JohnTanaka),
        ]),
    };

    public static readonly Pilot Draq = new()
    {
        Machine = MachineDB.MightyTyphoon,
        PilotIndex = PilotIndex.Draq,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Draq),
            new(Language.Latin, PilotNames.Draq),
        ]),
    };

    public static readonly Pilot RogerBuster = new()
    {
        Machine = MachineDB.MightyHurricane,
        PilotIndex = PilotIndex.RogerBuster,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.RogerBuster),
            new(Language.Latin, PilotNames.RogerBuster),
        ]),
    };

    public static readonly Pilot DrClash = new()
    {
        Machine = MachineDB.CrazyBear,
        PilotIndex = PilotIndex.DrClash,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.DrClash),
            new(Language.Latin, PilotNames.DrClash),
        ]),
    };

    public static readonly Pilot BlackShadow = new()
    {
        Machine = MachineDB.BlackBull,
        PilotIndex = PilotIndex.BlackShadow,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.BlackShadow),
            new(Language.Latin, PilotNames.BlackShadow),
        ]),
    };

    public static readonly Pilot Deathborn = new()
    {
        Machine = MachineDB.DarkSchneider,
        PilotIndex = PilotIndex.Deathborn,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Deathborn),
            new(Language.Latin, PilotNames.Deathborn),
        ]),
    };

    public static readonly Pilot DonGenie = new()
    {
        Machine = MachineDB.FatShark,
        PilotIndex = PilotIndex.DonGenie,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.DonGenie),
            new(Language.Latin, PilotNames.DonGenie),
        ]),
    };

    public static readonly Pilot DigiBoy = new()
    {
        Machine = MachineDB.CosmicDolphin,
        PilotIndex = PilotIndex.DigiBoy,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.DigiBoy),
            new(Language.Latin, PilotNames.DigiBoy),
        ]),
    };

    public static readonly Pilot DaiSanGen = new()
    {
        Machine = MachineDB.PinkSpider,
        PilotIndex = PilotIndex.Dai,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.DaiSanGen),
            new(Language.Latin, PilotNames.DaiSanGen),
        ]),
    };

    public static readonly Pilot Spade = new()
    {
        Machine = MachineDB.MagicSeagull,
        PilotIndex = PilotIndex.Spade,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Spade),
            new(Language.Latin, PilotNames.Spade),
        ]),
    };

    public static readonly Pilot DaiGoroh = new()
    {
        Machine = MachineDB.SilverRat,
        PilotIndex = PilotIndex.Daigoroh,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Daigoroh),
            new(Language.Latin, PilotNames.Daigoroh),
        ]),
    };

    public static readonly Pilot PrinciaRamode = new()
    {
        Machine = MachineDB.SparkMoon,
        PilotIndex = PilotIndex.PrinciaRamode,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.PrinciaRamode),
            new(Language.Latin, PilotNames.PrinciaRamode),
        ]),
    };

    public static readonly Pilot LilyFlyer = new()
    {
        Machine = MachineDB.BunnyFlash,
        PilotIndex = PilotIndex.LilyFlyer,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.LilyFlyer),
            new(Language.Latin, PilotNames.LilyFlyer),
        ]),
    };

    public static readonly Pilot PJ = new()
    {
        Machine = MachineDB.GroovyTaxi,
        PilotIndex = PilotIndex.PJ,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.PJ),
            new(Language.Latin, PilotNames.PJ),
        ]),
    };

    public static readonly Pilot QQQ = new()
    {
        Machine = MachineDB.RollingTurtle,
        PilotIndex = PilotIndex.QQQ,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.QQQ),
            new(Language.Latin, PilotNames.QQQ),
        ]),
    };

    public static readonly Pilot Pheonix = new()
    {
        Machine = MachineDB.RainbowPhoenix,
        PilotIndex = PilotIndex.Pheonix,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Pheonix),
            new(Language.Latin, PilotNames.Pheonix),
        ]),
    };

    public static readonly Pilot Shioh = new()
    {
        Machine = MachineDB.TwinNoritta,
        PilotIndex = PilotIndex.Shioh,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Shioh),
            new(Language.Latin, PilotNames.Shioh),
        ]),
    };

    public static readonly Pilot Gomar = new()
    {
        Machine = MachineDB.TwinNoritta,
        PilotIndex = PilotIndex.Gomar,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Gomar),
            new(Language.Latin, PilotNames.Gomar),
        ]),
    };

    public static readonly Pilot Dai = new()
    {
        Machine = MachineDB.PinkSpider,
        PilotIndex = PilotIndex.Dai,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Dai),
            new(Language.Latin, PilotNames.Dai),
        ]),
    };

    public static readonly Pilot San = new()
    {
        Machine = MachineDB.PinkSpider,
        PilotIndex = PilotIndex.San,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.San),
            new(Language.Latin, PilotNames.San),
        ]),
    };

    public static readonly Pilot Gen = new()
    {
        Machine = MachineDB.PinkSpider,
        PilotIndex = PilotIndex.Gen,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.Gen),
            new(Language.Latin, PilotNames.Gen),
        ]),
    };

    public static readonly Pilot MrZero = new()
    {
        Machine = MachineDB.None,
        PilotIndex = PilotIndex.MrZero,
        PilotName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, PilotNames.MrZero),
            new(Language.Latin, PilotNames.MrZero),
        ]),
    };


    public static readonly ImmutableArray<Pilot> AllRacers =
    [
        MightyGazelle,
        JodySummer,
        DrStewart,
        Baba,
        SamuraiGoroh,
        Pico,
        CaptainFalcon,
        Octoman,
        MrEAD,
        JamesMcCloud,
        Billy,
        KateAlen,
        Zoda,
        JackLevin,
        BioRex,
        TheSkull,
        AntonioGuster,
        Beastman,
        Leon,
        SuperArrow,
        MrsArrow,
        GomarAndShioh,
        SilverNeelsen,
        MichaelChain,
        BloodFalcon,
        JohnTanaka,
        Draq,
        RogerBuster,
        DrClash,
        BlackShadow,
        Deathborn,
        DonGenie,
        DigiBoy,
        DaiSanGen,
        Spade,
        DaiGoroh,
        PrinciaRamode,
        LilyFlyer,
        PJ,
        QQQ,
        Pheonix,
    ];

    public static readonly ImmutableArray<Pilot> AllPilots =
    [
        MightyGazelle,
        JodySummer,
        DrStewart,
        Baba,
        SamuraiGoroh,
        Pico,
        CaptainFalcon,
        Octoman,
        MrEAD,
        JamesMcCloud,
        Billy,
        KateAlen,
        Zoda,
        JackLevin,
        BioRex,
        TheSkull,
        AntonioGuster,
        Beastman,
        Leon,
        SuperArrow,
        MrsArrow,
        Shioh,
        SilverNeelsen,
        MichaelChain,
        BloodFalcon,
        JohnTanaka,
        Draq,
        RogerBuster,
        DrClash,
        BlackShadow,
        Deathborn,
        DonGenie,
        DigiBoy,
        Dai,
        Spade,
        DaiGoroh,
        PrinciaRamode,
        LilyFlyer,
        PJ,
        QQQ,
        Pheonix,
        //
        Gomar,
        San,
        Gen,
        MrZero,
    ];

}
