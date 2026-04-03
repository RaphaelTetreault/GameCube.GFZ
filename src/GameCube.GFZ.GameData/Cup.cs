using System;
using System.Collections.Frozen;

namespace GameCube.GFZ.GameData
{
    public readonly record struct Cup
    {
        private const int MaxCourseCount = 6;

        public required CupIndex CupIndex { get; init; }
        public required Course[] Courses { get; init; }
        public required FrozenDictionary<Language, string> Name { get; init; }

        public Cup()
        {
            Courses = new Course[MaxCourseCount];
        }

        public Cup(params Course[] courses)
        {
            if (courses.Length > MaxCourseCount)
            {
                string msg = $"A {nameof(Cup)} has a maximum of {MaxCourseCount} courses.";
                throw new ArgumentException(msg);
            }

            Courses = new Course[MaxCourseCount];
            courses.CopyTo(Courses, 0);
        }

        public static readonly Cup AllCup_ActualValues = new()
        {
            CupIndex = CupIndex.AllCup,
            Courses =
            [
                Course.Null,
                Course.Null,
                Course.Null,
                Course.Null,
                Course.Null,
                Course.Null,
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
            Courses = Course.DefaultCourses,
            Name = AllCup_ActualValues.Name,
        };

        public static readonly Cup RubyCup = new()
        {
            CupIndex = CupIndex.RubyCup,
            Courses = [
                Course.MuteCity_TwistRoad,
                Course.CasinoPalace_SplitOval,
                Course.SandOcean_SurfaceSlide,
                Course.Lightning_LoopCross,
                Course.Aeropolis_Multiplex,
                Course.Null,
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
                Course.BigBlue_DriftHighway,
                Course.PortTown_AeroDive,
                Course.GreenPlant_MobiusRing,
                Course.PortTown_LongPipe,
                Course.MuteCity_SerialGaps,
                Course.Null,
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
                Course.FireField_CylinderKnot,
                Course.GreenPlant_Intersection,
                Course.CasinoPalace_DoubleBranches,
                Course.Lightning_LoopCross,
                Course.BigBlue_Ordeal,
                Course.Null,
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
                Course.CosmoTerminal_Trident,
                Course.SandOcean_LateralShift,
                Course.FireField_Undulation,
                Course.Aeropolis_DragonSlope,
                Course.PhantomRoad_SlimLineSlits,
                Course.Null,
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
                Course.Aeropolis_ScrewDrive,
                Course.OuterSpace_MeteorStream,
                Course.PortTown_CylinderWave,
                Course.Lightning_ThunderRoad,
                Course.GreenPlant_Spiral,
                Course.MuteCity_SonicOval,
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
                Course.MuteCity_SonicOval,
                Course.Aeropolis_ScrewDrive,
                Course.OuterSpace_MeteorStream,
                Course.PortTown_CylinderWave,
                Course.Lightning_ThunderRoad,
                Course.GreenPlant_Spiral,
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
                Course.Null with { CourseIndex = 82 },
                Course.Null with { CourseIndex = 56 },
                Course.Null with { CourseIndex = 57 },
                Course.Null with { CourseIndex = 58 },
                Course.Null with { CourseIndex = 59 },
                Course.Null with { CourseIndex = 60 },
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
                Course.Null with { CourseIndex = 81 },
                Course.Null with { CourseIndex = 85 },
                Course.Null with { CourseIndex = 86 },
                Course.Null with { CourseIndex = 87 },
                Course.Null with { CourseIndex = 88 },
                Course.Null with { CourseIndex = 89 },
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
                Course.MuteCity_TwistRoad,
                Course.PortTown_LongPipe,
                Course.GreenPlant_Intersection,
                Course.PortTown_AeroDive,
                Course.Null,
                Course.Null,
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
                Course.CasinoPalace_SplitOval,
                Course.Null,
                Course.Null,
                Course.Null,
                Course.Null,
                Course.Null,
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
}
