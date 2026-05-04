using System.Collections.Immutable;
using System.Numerics;

namespace GameCube.GFZ.GameData;

public static class MachineDB
{
    public static class MachineName
    {
        public const string RedGazelle = "Red Gazelle";
        public const string WhiteCat = "White Cat";
        public const string GoldenFox = "Golden Fox";
        public const string IronTiger = "Iron Tiger";
        public const string FireStingray = "Fire Stingray";
        public const string WildGoose = "Wild Goose";
        public const string BlueFalcon = "Blue Falcon";
        public const string DeepClaw = "Deep Claw";
        public const string GreatStar = "Great Star";
        public const string LittleWyvern = "Little Wyvern";
        public const string MadWolf = "Mad Wolf";
        public const string SuperPiranha = "Super Piranha";
        public const string DeathAnchor = "Death Anchor";
        public const string AstroRobin = "Astro Robin";
        public const string BigFang = "Big Fang";
        public const string SonicPhantom = "Sonic Phantom";
        public const string GreenPanther = "Green Panther";
        public const string HyperSpeeder = "Hyper Speeder";
        public const string SpaceAngler = "Space Angler";
        public const string KingMeteor = "King Meteor";
        public const string QueenMeteor = "Queen Meteor";
        public const string TwinNoritta = "Twin Noritta";
        public const string NightThunder = "Night thunder";
        public const string WildBoar = "Wild Boar";
        public const string BloodHawk = "Blood Hawk";
        public const string HellHawk = "Hell Hawk";
        public const string WonderWasp = "Wonder Wasp";
        public const string MightyTyphoon = "Mighty Typhoon";
        public const string MightyHurricane = "Mighty Hurricane";
        public const string CrazyBear = "Crazy Bear";
        public const string BlackBull = "Black Bull";
        public const string DarkSchneider = "Dark Schneider";
        public const string FatShark = "Fat Shark";
        public const string CosmicDolphin = "Cosmic Dolphin";
        public const string PinkSpider = "Pink Spider";
        public const string MagicSeagull = "Magic Seagull";
        public const string SilverRat = "Silver Rat";
        public const string SparkMoon = "Spark Moon";
        public const string BunnyFlash = "Bunny Flash";
        public const string GroovyTaxi = "Groovy Taxi";
        public const string RollingTurtle = "Rolling Turtle";
        public const string RainbowPhoenix = "Rainbow Phoenix";
    }


    public static readonly Machine RedGazelle = new()
    {
        MachineIndex = MachineIndex.RedGazelle,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.RedGazelle),
            new(Language.Latin, MachineName.RedGazelle),
        ]),
        MachineNumber = 1,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.MightyGazelle]),
            new(Language.Latin, [PilotDB.PilotNames.MightyGazelle]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.MightyGazelle, new(0f, 0.62f, 1.085f)),
        ]),
    };

    public static readonly Machine WhiteCat = new()
    {
        MachineIndex = MachineIndex.WhiteCat,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.WhiteCat),
            new(Language.Latin, MachineName.WhiteCat),
        ]),
        MachineNumber = 2,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.JodySummer]),
            new(Language.Latin, [PilotDB.PilotNames.JodySummer]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.JodySummer, new(0f, 0.36f, -0.4715f)),
        ]),
    };

    public static readonly Machine GoldenFox = new()
    {
        MachineIndex = MachineIndex.GoldenFox,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.GoldenFox),
            new(Language.Latin, MachineName.GoldenFox),
        ]),
        MachineNumber = 3,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.DrStewart]),
            new(Language.Latin, [PilotDB.PilotNames.DrStewart]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.DrStewart, new(0f, 0.55f, 0.115f)),
        ]),
    };

    public static readonly Machine IronTiger = new()
    {
        MachineIndex = MachineIndex.IronTiger,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.IronTiger),
            new(Language.Latin, MachineName.IronTiger),
        ]),
        MachineNumber = 4,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.Baba]),
            new(Language.Latin, [PilotDB.PilotNames.Baba]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.Baba, new(0f, 0.505f, 0.4f)),
        ]),
    };

    public static readonly Machine FireStingray = new()
    {
        MachineIndex = MachineIndex.FireStingray,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.FireStingray),
            new(Language.Latin, MachineName.FireStingray),
        ]),
        MachineNumber = 5,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.SamuraiGoroh]),
            new(Language.Latin, [PilotDB.PilotNames.SamuraiGoroh]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.SamuraiGoroh, new(0f, 0.525f, -1.01f))
        ]),
    };

    public static readonly Machine WildGoose = new()
    {
        MachineIndex = MachineIndex.WildGoose,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.WildGoose),
            new(Language.Latin, MachineName.WildGoose),
        ]),
        MachineNumber = 6,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.Pico]),
            new(Language.Latin, [PilotDB.PilotNames.Pico]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.Pico, new(0f, 0.63f, -1.29f)),
        ]),
    };

    public static readonly Machine BlueFalcon = new()
    {
        MachineIndex = MachineIndex.BlueFalcon,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
            [
                new(Language.Japanese, MachineName.BlueFalcon),
                new(Language.Latin, MachineName.BlueFalcon),
            ]),
        MachineNumber = 7,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.CaptainFalcon]),
            new(Language.Latin, [PilotDB.PilotNames.CaptainFalcon]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.CaptainFalcon, new(0f, 0.43f, -0.665f)),
        ]),
    };

    public static readonly Machine DeepClaw = new()
    {
        MachineIndex = MachineIndex.DeepClaw,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.DeepClaw),
            new(Language.Latin, MachineName.DeepClaw),
        ]),
        MachineNumber = 8,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.Octman]),
            new(Language.Latin, [PilotDB.PilotNames.Octoman]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.Octoman, new(0f, 0.435f, -0.61f)),
        ]),
    };

    public static readonly Machine GreatStar = new()
    {
        MachineIndex = MachineIndex.GreatStar,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.GreatStar),
            new(Language.Latin, MachineName.GreatStar),
        ]),
        MachineNumber = 9,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.MrEAD]),
            new(Language.Latin, [PilotDB.PilotNames.MrEAD]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.MrEAD, new(0f, 0.825f, -0.31f)),
        ]),
    };

    public static readonly Machine LittleWyvern = new()
    {
        MachineIndex = MachineIndex.LittleWyvern,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.LittleWyvern),
            new(Language.Latin, MachineName.LittleWyvern),
        ]),
        MachineNumber = 10,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.JamesMcCloud]),
            new(Language.Latin, [PilotDB.PilotNames.JamesMcCloud]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new (PilotIndex.JamesMcCloud, new(0f, 0.64f, 0.2f)),
        ]),
    };

    public static readonly Machine MadWolf = new()
    {
        MachineIndex = MachineIndex.MadWolf,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.MadWolf),
            new(Language.Latin, MachineName.MadWolf),
        ]),
        MachineNumber = 11,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.Billy]),
            new(Language.Latin, [PilotDB.PilotNames.Billy]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.Billy, new(0f, 0.62f, -1.02f))
        ]),
    };

    public static readonly Machine SuperPiranha = new()
    {
        MachineIndex = MachineIndex.SuperPiranha,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.SuperPiranha),
            new(Language.Latin, MachineName.SuperPiranha),
        ]),
        MachineNumber = 12,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.KateAlen]),
            new(Language.Latin, [PilotDB.PilotNames.KateAlen]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.KateAlen, new(0f, 0.39f, -0.96f)),
        ]),
    };

    public static readonly Machine DeathAnchor = new()
    {
        MachineIndex = MachineIndex.DeathAnchor,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.DeathAnchor),
            new(Language.Latin, MachineName.DeathAnchor),
        ]),
        MachineNumber = 13,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.Zoda]),
            new(Language.Latin, [PilotDB.PilotNames.Zoda]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.Zoda, new(0f, 0.73f, 0.4625f)),
        ]),
    };

    public static readonly Machine AstroRobin = new()
    {
        MachineIndex = MachineIndex.AstroRobin,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.AstroRobin),
            new(Language.Latin, MachineName.AstroRobin),
        ]),
        MachineNumber = 14,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.JackLevin]),
            new(Language.Latin, [PilotDB.PilotNames.JackLevin]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.JackLevin, new(0f, 0.485f, -0.3f)),
        ]),
    };

    public static readonly Machine BigFang = new()
    {
        MachineIndex = MachineIndex.BigFang,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.BigFang),
            new(Language.Latin, MachineName.BigFang),
        ]),
        MachineNumber = 15,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.BioRex]),
            new(Language.Latin, [PilotDB.PilotNames.BioRex]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.BioRex, new(0f, 0.63f, 0.09f)),
        ]),
    };

    public static readonly Machine SonicPhantom = new()
    {
        MachineIndex = MachineIndex.SonicPhantom,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.SonicPhantom),
            new(Language.Latin, MachineName.SonicPhantom),
        ]),
        MachineNumber = 16,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.TheSkull]),
            new(Language.Latin, [PilotDB.PilotNames.TheSkull]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.TheSkull, new(0f, 0.45f, 0.4975f)),
        ]),
    };

    public static readonly Machine GreenPanther = new()
    {
        MachineIndex = MachineIndex.GreenPanther,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.GreenPanther),
            new(Language.Latin, MachineName.GreenPanther),
        ]),
        MachineNumber = 17,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.AntonioGuster]),
            new(Language.Latin, [PilotDB.PilotNames.AntonioGuster]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.AntonioGuster, new(0f, 0.585f, -0.2075f)),
        ]),
    };

    public static readonly Machine HyperSpeeder = new()
    {
        MachineIndex = MachineIndex.HyperSpeeder,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.HyperSpeeder),
            new(Language.Latin, MachineName.HyperSpeeder),
        ]),
        MachineNumber = 18,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.Beastman]),
            new(Language.Latin, [PilotDB.PilotNames.Beastman]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.Beastman, new(0f, 0.615f, -0.175f)),
        ]),
    };

    public static readonly Machine SpaceAngler = new()
    {
        MachineIndex = MachineIndex.SpaceAngler,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.SpaceAngler),
            new(Language.Latin, MachineName.SpaceAngler),
        ]),
        MachineNumber = 19,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.Leon]),
            new(Language.Latin, [PilotDB.PilotNames.Leon]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.Leon, new(0f, 1f, -0.66f)),
        ]),
    };

    public static readonly Machine KingMeteor = new()
    {
        MachineIndex = MachineIndex.KingMeteor,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.KingMeteor),
            new(Language.Latin, MachineName.KingMeteor),
        ]),
        MachineNumber = 20,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.SuperArrow]),
            new(Language.Latin, [PilotDB.PilotNames.SuperArrow]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.SuperArrow, new(0f, 0.93f, 1.04f)),
        ]),
    };

    public static readonly Machine QueenMeteor = new()
    {
        MachineIndex = MachineIndex.QueenMeteor,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.QueenMeteor),
            new(Language.Latin, MachineName.QueenMeteor),
        ]),
        MachineNumber = 21,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.MrsArrow]),
            new(Language.Latin, [PilotDB.PilotNames.MrsArrow]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.MrsArrow, new(0f, 0.92f, 1.06f)),
        ]),
    };

    public static readonly Machine TwinNoritta = new()
    {
        MachineIndex = MachineIndex.TwinNoritta,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.TwinNoritta),
            new(Language.Latin, MachineName.TwinNoritta),
        ]),
        MachineNumber = 22,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.Shioh, PilotDB.PilotNames.Gomar]),
            new(Language.Latin, [PilotDB.PilotNames.Shioh, PilotDB.PilotNames.Gomar]),

        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.Shioh, new(+0.715f, 0.415f, 0.925f)),
            new(PilotIndex.Gomar, new(-0.715f, 0.345f, 0.805f)),
        ]),
    };

    public static readonly Machine NightThunder = new()
    {
        MachineIndex = MachineIndex.NightThunder,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.NightThunder),
            new(Language.Latin, MachineName.NightThunder),
        ]),
        MachineNumber = 23,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.SilverNeelsen]),
            new(Language.Latin, [PilotDB.PilotNames.SilverNeelsen]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.SilverNeelsen, new(0f, 0.52f, 0.29f)),
        ]),
    };

    public static readonly Machine WildBoar = new()
    {
        MachineIndex = MachineIndex.WildBoar,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.WildBoar),
            new(Language.Latin, MachineName.WildBoar),
        ]),
        MachineNumber = 24,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.MichaelChain]),
            new(Language.Latin, [PilotDB.PilotNames.MichaelChain]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.MichaelChain, new(0f, 0.44f, -1.115f)),
        ]),
    };

    public static readonly Machine BloodHawk = new()
    {
        MachineIndex = MachineIndex.BloodHawk,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.HellHawk),
            new(Language.Latin, MachineName.BloodHawk),
        ]),
        MachineNumber = 25,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.BloodFalcon]),
            new(Language.Latin, [PilotDB.PilotNames.BloodFalcon]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.BloodFalcon, new(0f, 0.375f, -0.27f)),
        ]),
    };

    public static readonly Machine WonderWasp = new()
    {
        MachineIndex = MachineIndex.WonderWasp,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.WonderWasp),
            new(Language.Latin, MachineName.WonderWasp),
        ]),
        MachineNumber = 26,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.JohnTanaka]),
            new(Language.Latin, [PilotDB.PilotNames.JohnTanaka]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.JohnTanaka, new(0f, 0.76f, -0.03f)),
        ]),
    };

    public static readonly Machine MightyTyphoon = new()
    {
        MachineIndex = MachineIndex.MightyTyphoon,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.MightyTyphoon),
            new(Language.Latin, MachineName.MightyTyphoon),
        ]),
        MachineNumber = 27,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.Draq]),
            new(Language.Latin, [PilotDB.PilotNames.Draq]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.Draq, new(0f, 0.39f, -0.02f)),
        ]),
    };

    public static readonly Machine MightyHurricane = new()
    {
        MachineIndex = MachineIndex.MightyHurricane,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.MightyHurricane),
            new(Language.Latin, MachineName.MightyHurricane),
        ]),
        MachineNumber = 28,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.RogerBuster]),
            new(Language.Latin, [PilotDB.PilotNames.RogerBuster]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.RogerBuster, new(0f, 0.73f, 0.035f)),
        ]),
    };

    public static readonly Machine CrazyBear = new()
    {
        MachineIndex = MachineIndex.CrazyBear,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.CrazyBear),
            new(Language.Latin, MachineName.CrazyBear),
        ]),
        MachineNumber = 29,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.DrClash]),
            new(Language.Latin, [PilotDB.PilotNames.DrClash]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.DrClash, new(0f, 0.85f, -0.07f)),
        ]),
    };

    public static readonly Machine BlackBull = new()
    {
        MachineIndex = MachineIndex.BlackBull,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.BlackBull),
            new(Language.Latin, MachineName.BlackBull),
        ]),
        MachineNumber = 30,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.BlackShadow]),
            new(Language.Latin, [PilotDB.PilotNames.BlackShadow]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.BlackShadow, new(0f, 1.455f, 0.81f)),
        ]),
    };

    public static readonly Machine DarkSchneider = new()
    {
        MachineIndex = MachineIndex.DarkSchneider,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.DarkSchneider),
            new(Language.Latin, MachineName.DarkSchneider),
        ]),
        MachineNumber = 0,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.Deathborn]),
            new(Language.Latin, [PilotDB.PilotNames.Deathborn]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.Deathborn, new(0f, 0.8f, -0.065f)),
        ]),
    };

    public static readonly Machine FatShark = new()
    {
        MachineIndex = MachineIndex.FatShark,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.FatShark),
            new(Language.Latin, MachineName.FatShark),
        ]),
        MachineNumber = 31,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.DonGenie]),
            new(Language.Latin, [PilotDB.PilotNames.DonGenie]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.DonGenie, new(0f, 0.65f, -0.19f)),
        ]),
    };

    public static readonly Machine CosmicDolphin = new()
    {
        MachineIndex = MachineIndex.CosmicDolphin,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.CosmicDolphin),
            new(Language.Latin, MachineName.CosmicDolphin),
        ]),
        MachineNumber = 32,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.DigiBoy]),
            new(Language.Latin, [PilotDB.PilotNames.DigiBoy]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.DigiBoy, new(0f, 1.475f, 0.595f)),
        ]),
    };

    public static readonly Machine PinkSpider = new()
    {
        MachineIndex = MachineIndex.PinkSpider,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.PinkSpider),
            new(Language.Latin, MachineName.PinkSpider),
        ]),
        MachineNumber = 33,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.Dai, PilotDB.PilotNames.San, PilotDB.PilotNames.Gen]),
            new(Language.Latin, [PilotDB.PilotNames.Dai, PilotDB.PilotNames.San, PilotDB.PilotNames.Gen]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.Dai, new( 0.00f, 0.85f, -1.72f)),
            new(PilotIndex.San, new(+0.42f, 0.85f, -1.17f)),
            new(PilotIndex.Gen, new(-0.42f, 0.85f, -1.17f)),
        ]),
    };

    public static readonly Machine MagicSeagull = new()
    {
        MachineIndex = MachineIndex.MagicSeagull,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.MagicSeagull),
            new(Language.Latin, MachineName.MagicSeagull),
        ]),
        MachineNumber = 34,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.Spade]),
            new(Language.Latin, [PilotDB.PilotNames.Spade]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.Spade, new(0f, 2f, 0f)),
        ]),
    };

    public static readonly Machine SilverRat = new()
    {
        MachineIndex = MachineIndex.SilverRat,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.SilverRat),
            new(Language.Latin, MachineName.SilverRat),
        ]),
        MachineNumber = 35,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.Daigoroh]),
            new(Language.Latin, [PilotDB.PilotNames.Daigoroh]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.Daigoroh, new(0f, 0.565f, -0.1f)),
        ]),
    };

    public static readonly Machine SparkMoon = new()
    {
        MachineIndex = MachineIndex.SparkMoon,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.SparkMoon),
            new(Language.Latin, MachineName.SparkMoon),
        ]),
        MachineNumber = 36,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.PrinciaRamode]),
            new(Language.Latin, [PilotDB.PilotNames.PrinciaRamode]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.PrinciaRamode, new(0f, 0.65f, -0.725f)),
        ]),
    };

    public static readonly Machine BunnyFlash = new()
    {
        MachineIndex = MachineIndex.BunnyFlash,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.BunnyFlash),
            new(Language.Latin, MachineName.BunnyFlash),
        ]),
        MachineNumber = 37,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.LilyFlyer]),
            new(Language.Latin, [PilotDB.PilotNames.LilyFlyer]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.LilyFlyer, new(0f, 0.7f, -0.92f)),
        ]),
    };

    public static readonly Machine GroovyTaxi = new()
    {
        MachineIndex = MachineIndex.GroovyTaxi,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.GroovyTaxi),
            new(Language.Latin, MachineName.GroovyTaxi),
        ]),
        MachineNumber = 38,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.PJ]),
            new(Language.Latin, [PilotDB.PilotNames.PJ]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.PJ, new(-0.29f, 0.7f, -0.29f)),
        ]),
    };

    public static readonly Machine RollingTurtle = new()
    {
        MachineIndex = MachineIndex.RollingTurtle,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.RollingTurtle),
            new(Language.Latin, MachineName.RollingTurtle),
        ]),
        MachineNumber = 39,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.QQQ]),
            new(Language.Latin, [PilotDB.PilotNames.QQQ]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.QQQ, new(0f, 2f, 0f)),
        ]),
    };

    public static readonly Machine RainbowPhoenix = new()
    {
        MachineIndex = MachineIndex.RainbowPhoenix,
        MachineName = ImmutableDictionary.CreateRange<Language, string>(
        [
            new(Language.Japanese, MachineName.RainbowPhoenix),
            new(Language.Latin, MachineName.RainbowPhoenix),
        ]),
        MachineNumber = 40,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>(
        [
            new(Language.Japanese, [PilotDB.PilotNames.Pheonix]),
            new(Language.Latin, [PilotDB.PilotNames.Pheonix]),
        ]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>(
        [
            new(PilotIndex.Pheonix, new(0f, 0.685f, -0.43f)),
        ]),
    };

    public static readonly Machine None = new()
    {
        MachineIndex = (MachineIndex)byte.MaxValue,
        MachineName = ImmutableDictionary.CreateRange<Language, string>([]),
        MachineNumber = byte.MaxValue,
        PilotNames = ImmutableDictionary.CreateRange<Language, string[]>([]),
        PilotPositions = ImmutableDictionary.CreateRange<PilotIndex, Vector3>([]),
    };


    public static readonly ImmutableArray<Machine> AllMachines =
    [
        RedGazelle,
        WhiteCat,
        GoldenFox,
        IronTiger,
        FireStingray,
        WildGoose,
        BlueFalcon,
        DeepClaw,
        GreatStar,
        LittleWyvern,
        MadWolf,
        SuperPiranha,
        DeathAnchor,
        AstroRobin,
        BigFang,
        SonicPhantom,
        GreenPanther,
        HyperSpeeder,
        SpaceAngler,
        KingMeteor,
        QueenMeteor,
        TwinNoritta,
        NightThunder,
        WildBoar,
        BloodHawk,
        WonderWasp,
        MightyTyphoon,
        MightyHurricane,
        CrazyBear,
        BlackBull,
        DarkSchneider,
        FatShark,
        CosmicDolphin,
        PinkSpider,
        MagicSeagull,
        SilverRat,
        SparkMoon,
        BunnyFlash,
        GroovyTaxi,
        RollingTurtle,
        RainbowPhoenix,
    ];
}
