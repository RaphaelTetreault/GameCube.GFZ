using System.Collections.Frozen;

namespace GameCube.GFZ.GameData;

/// <summary>
///     DataBase of default game <see cref="Cup"/>s.
/// </summary>
public static class CupDB
{
    public static readonly Cup AllCup_ActualValues = new()
    {
        CupIndex = CupIndex.AllCup,
        Courses =
        [
            CourseDB.Null,
            CourseDB.Null,
            CourseDB.Null,
            CourseDB.Null,
            CourseDB.Null,
            CourseDB.Null,
        ],
        Name = FrozenDictionary.Create<Language, string>(
        [
            new (Language.English,  "ALL Cup"),
            new (Language.Deutsch,  "ALL Cup GER"),
            new (Language.Français, "ALL Cup FRA"),
            new (Language.Español,  "ALL Cup SPA"),
            new (Language.Italiano, "ALL Cup ITA"),
            new (Language.Japanese, "ぜんせんたくカップ"),
        ]),
    };

    public static readonly Cup AllCup_FunctionalValues = new()
    {
        CupIndex = CupIndex.AllCup,
        Courses = CourseDB.DefaultCourses,
        Name = AllCup_ActualValues.Name,
    };

    public static readonly Cup RubyCup = new()
    {
        CupIndex = CupIndex.RubyCup,
        Courses = [
            CourseDB.MuteCity_TwistRoad,
            CourseDB.CasinoPalace_SplitOval,
            CourseDB.SandOcean_SurfaceSlide,
            CourseDB.Lightning_LoopCross,
            CourseDB.Aeropolis_Multiplex,
            CourseDB.Null,
        ],
        Name = FrozenDictionary.Create<Language, string>(
        [
            new (Language.English,  "Ruby Cup"),
            new (Language.Deutsch,  "Rubin-Cup"),
            new (Language.Français, "Coupe Rubis"),
            new (Language.Español,  "Copa Rubí"),
            new (Language.Italiano, "Coppa Rubino"),
            new (Language.Japanese, "ルビーカップ"),
        ]),
    };

    public static readonly Cup SapphireCup = new()
    {
        CupIndex = CupIndex.SapphireCup,
        Courses = [
            CourseDB.BigBlue_DriftHighway,
            CourseDB.PortTown_AeroDive,
            CourseDB.GreenPlant_MobiusRing,
            CourseDB.PortTown_LongPipe,
            CourseDB.MuteCity_SerialGaps,
            CourseDB.Null,
        ],
        Name = FrozenDictionary.Create<Language, string>(
        [
            new (Language.English,  "Sapphire Cup"),
            new (Language.Deutsch,  "Saphir-Cup"),
            new (Language.Français, "Coupe Saphir"),
            new (Language.Español,  "Copa Zafiro"),
            new (Language.Italiano, "Coppa Zaffiro"),
            new (Language.Japanese, "サファイアカップ"),
        ]),
    };

    public static readonly Cup EmeraldCup = new()
    {
        CupIndex = CupIndex.EmeraldCup,
        Courses = [
            CourseDB.FireField_CylinderKnot,
            CourseDB.GreenPlant_Intersection,
            CourseDB.CasinoPalace_DoubleBranches,
            CourseDB.Lightning_HalfPipe,
            CourseDB.BigBlue_Ordeal,
            CourseDB.Null,
        ],
        Name = FrozenDictionary.Create<Language, string>(
        [
            new (Language.English,  "Emerald Cup"),
            new (Language.Deutsch,  "Smaragd-Cup"),
            new (Language.Français, "Coupe Emeraude"),
            new (Language.Español,  "Copa Esmeralda"),
            new (Language.Italiano, "Coppa Smeraldo"),
            new (Language.Japanese, "エメラルドカップ"),
        ]),
    };

    public static readonly Cup DiamondCup = new()
    {
        CupIndex = CupIndex.DiamondCup,
        Courses = [
            CourseDB.CosmoTerminal_Trident,
            CourseDB.SandOcean_LateralShift,
            CourseDB.FireField_Undulation,
            CourseDB.Aeropolis_DragonSlope,
            CourseDB.PhantomRoad_SlimLineSlits,
            CourseDB.Null,
        ],
        Name = FrozenDictionary.Create<Language, string>(
        [
            new (Language.English,  "Diamond Cup"),
            new (Language.Deutsch,  "Diamant-Cup"),
            new (Language.Français, "Coupe Diamant"),
            new (Language.Español,  "Copa Diamante"),
            new (Language.Italiano, "Coppa Diamante"),
            new (Language.Japanese, "ダイヤモンドカップ"),
        ]),
    };

    public static readonly Cup AXCup = new()
    {
        CupIndex = CupIndex.AXCup,
        Courses = [
            CourseDB.Aeropolis_ScrewDrive,
            CourseDB.OuterSpace_MeteorStream,
            CourseDB.PortTown_CylinderWave,
            CourseDB.Lightning_ThunderRoad,
            CourseDB.GreenPlant_Spiral,
            CourseDB.MuteCity_SonicOval,
        ],
        Name = FrozenDictionary.Create<Language, string>(
        [
            new (Language.English,  "AX Cup"),
            new (Language.Deutsch,  "AX-Cup"),
            new (Language.Français, "Coupe AX"),
            new (Language.Español,  "Copa AX"),
            new (Language.Italiano, "Coppa AX"),
            new (Language.Japanese, "AXカップ"),
        ]),
    };

    public static readonly Cup ACCup = new()
    {
        CupIndex = CupIndex.ACCup,
        Courses = [
            CourseDB.MuteCity_SonicOval,
            CourseDB.Aeropolis_ScrewDrive,
            CourseDB.OuterSpace_MeteorStream,
            CourseDB.PortTown_CylinderWave,
            CourseDB.Lightning_ThunderRoad,
            CourseDB.GreenPlant_Spiral,
        ],
        Name = FrozenDictionary.Create<Language, string>(
        [
            new (Language.English,  "Ac Cup"),
            new (Language.Deutsch,  "Ac Cup GER"),
            new (Language.Français, "Ac Cup FRA"),
            new (Language.Español,  "Ac Cup SPA"),
            new (Language.Italiano, "Ac Cup ITA"),
            new (Language.Japanese, "アーケードカップ80"), // Yeah, didn't think about WTF is going on here. See below.
            new (Language.Japanese, "アーケードカップ50"), // ...
        ]),
    };

    public static readonly Cup AC = new()
    {
        CupIndex = CupIndex.AC,
        Courses = [
            CourseDB.Null with { CourseIndex = 82 },
            CourseDB.Null with { CourseIndex = 56 },
            CourseDB.Null with { CourseIndex = 57 },
            CourseDB.Null with { CourseIndex = 58 },
            CourseDB.Null with { CourseIndex = 59 },
            CourseDB.Null with { CourseIndex = 60 },
        ],
        Name = FrozenDictionary.Create<Language, string>(
        [
            new (Language.English,  "Ac"),
            new (Language.Deutsch,  "Ac GER"),
            new (Language.Français, "Ac FRA"),
            new (Language.Español,  "Ac SPA"),
            new (Language.Italiano, "Ac ITA"),
            new (Language.Japanese, "アーケードカップ30"), // Yeah, didn't think about WTF is going on here either. See above.
        ]),
    };

    public static readonly Cup WHF = new()
    {
        CupIndex = CupIndex.WHF,
        Courses = [
            CourseDB.Null with { CourseIndex = 81 },
            CourseDB.Null with { CourseIndex = 85 },
            CourseDB.Null with { CourseIndex = 86 },
            CourseDB.Null with { CourseIndex = 87 },
            CourseDB.Null with { CourseIndex = 88 },
            CourseDB.Null with { CourseIndex = 89 },
        ],
        Name = FrozenDictionary.Create<Language, string>(
        [
            new (Language.English,  "Whf Cup"),
            new (Language.Deutsch,  "Whf Cup GER"),
            new (Language.Français, "Whf Cup FRA"),
            new (Language.Español,  "Whf Cup SPA"),
            new (Language.Italiano, "Whf Cup ITA"),
            new (Language.Japanese, "WHFカップ"),
        ]),
    };

    public static readonly Cup E3SingleRaceGP = new()
    {
        CupIndex = CupIndex.E3_SingleRaceGP,
        Courses = [
            CourseDB.MuteCity_TwistRoad,
            CourseDB.PortTown_LongPipe,
            CourseDB.GreenPlant_Intersection,
            CourseDB.PortTown_AeroDive,
            CourseDB.Null,
            CourseDB.Null,
        ],
        Name = FrozenDictionary.Create<Language, string>(
        [
            new (Language.English,  "E3-0"),
            new (Language.Japanese, "E3 1コースレース用"),
        ]),
    };

    public static readonly Cup E3Versus = new()
    {
        CupIndex = CupIndex.E3_Versus,
        Courses = [
            CourseDB.CasinoPalace_SplitOval,
            CourseDB.Null,
            CourseDB.Null,
            CourseDB.Null,
            CourseDB.Null,
            CourseDB.Null,
        ],
        Name = FrozenDictionary.Create<Language, string>(
        [
            new (Language.English,  "E3-1"),
            new (Language.Japanese, "E3 VS用"),
        ]),
    };

    public static readonly Cup[] DefaultCupsAX =
    [
        ACCup,
    ];
    public static readonly Cup[] DefaultCupsGX =
    [
        RubyCup,
        SapphireCup,
        EmeraldCup,
        DiamondCup,
        AXCup,
    ];
    public static readonly Cup[] DefaultCups =
    [
        AllCup_FunctionalValues,
        RubyCup,
        SapphireCup,
        EmeraldCup,
        DiamondCup,
        AXCup,
        ACCup,
        AC,
        WHF,
        E3SingleRaceGP,
        E3Versus,
    ];
}
