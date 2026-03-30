using System;
using System.Collections.Frozen;

namespace GameCube.GFZ.GameData
{
    public readonly record struct Course
    {
        public required byte CourseIndex { get; init; }
        public required Venue Venue { get; init; }
        public required FrozenDictionary<GameCode, string> Name { get; init; }


        public readonly string DisplayText(GameCode gameCode)
        {
            string venue = Venue.Name[gameCode];
            string course = this.Name[gameCode];
            return $"{venue} [{course}]";
        }


        public static class NamesEN
        {
            // Ruby Cup
            public const string TwistRoad = "Twist Road";
            public const string SplitOval = "Split Oval";
            public const string SurfaceSlide = "Surface Slide";
            public const string LoopCross = "Loop Cross";
            public const string Multiplex = "Multiplex";
            // Sapphire Cup
            public const string DriftHighway = "Drift Highway";
            public const string AeroDive = "Aero Dive";
            public const string MobiusRing = "Mobius Ring";
            public const string LongPipe = "Long Pipe";
            public const string SerialGaps = "Serial Gaps";
            // Emerald Cup
            public const string CylinderKnot = "Cylinder Knot";
            public const string Intersection = "Intersection";
            public const string DoubleBranches = "Double Branches";
            public const string HalfPipe = "Half Pipe";
            public const string Ordeal = "Ordeal";
            // Diamond Cup
            public const string Trident = "Trident";
            public const string LateralShift = "Lateral Shift";
            public const string Undulation = "Undulation";
            public const string DragonSlope = "Dragon Slope";
            public const string SlimLineSlits = "Slim-Line Slits";
            // AX Cup
            public const string ScrewDrive = "Screw Drive";
            public const string MeteorStream = "Meteor Stream";
            public const string CylinderWave = "Cylinder Wave";
            public const string ThunderRoad = "Thunder Road";
            public const string Spiral = "Spiral";
            public const string SonicOval = "Sonic Oval";
            // Story Mode
            public const string Story1 = "Chapter 1  Captain Falcon Trains";
            public const string Story2 = "Chapter 2  Goroh: The Vengeful Samurai";
            public const string Story3 = "Chapter 3  High Stakes in Mute City";
            public const string Story4 = "Chapter 4  Challenge of the Bloody Chain";
            public const string Story5 = "Chapter 5  Save Jody!";
            public const string Story6 = "Chapter 6  Black Shadow's Trap";
            public const string Story7 = "Chapter 7  The F-Zero Grand Prix";
            public const string Story8 = "Chapter 8  Secrets of the Champion Belt";
            public const string Story9 = "Chapter 9  Finale: Enter the Creators";
            public const string Story1Alt = "Virtual Circuit"; // In fze.story.rel
            // Other
            public const string Null = "---";
            public const string GrandPrixPodium = Null;
            public const string VictoryLap = Null;

        }
        public static class NamesJP
        {
            // Ruby Cup
            public const string TwistRoad = "ツイストロード";
            public const string SplitOval = "スプリットオーバル";
            public const string SurfaceSlide = "サーフェススライド";
            public const string LoopCross = "ループクロス";
            public const string Multiplex = "マルチプレックス";
            // Sapphire Cup
            public const string DriftHighway = "ドリフトハイウェイ";
            public const string AeroDive = "エアロダイブ";
            public const string MobiusRing = "メビウスリング";
            public const string LongPipe = "ロングパイプ";
            public const string SerialGaps = "シリアルギャップ";
            // Emerald Cup
            public const string CylinderKnot = "シリンダーノット";
            public const string Intersection = "インターセクション";
            public const string DoubleBranches = "ダブルブランチ";
            public const string HalfPipe = "ハーフパイプ";
            public const string Ordeal = "オーディール";
            // Diamond Cup
            public const string Trident = "トライデント";
            public const string LateralShift = "ラテラルシフト";
            public const string Undulation = "アンデュレーション";
            public const string DragonSlope = "ドラゴンスロープ";
            public const string SlimLineSlits = "スリムラインスリット";
            // AX Cup
            public const string ScrewDrive = "スクリュードライブ";
            public const string MeteorStream = "メテオストリーム";
            public const string CylinderWave = "シリンダーウェーブ";
            public const string ThunderRoad = "サンダーロード";
            public const string Spiral = "スパイラル";
            public const string SonicOval = "ソニックオーバル";
            // Story Mode
            public const string Story1 = "第１話　キャプテンファルコンの特訓";
            public const string Story2 = "第２話　復習のサムライゴロー";
            public const string Story3 = "第３話　ミュートシティで大勝負";
            public const string Story4 = "第４話　宇宙暴走族の挑戦";
            public const string Story5 = "第５話　ジョディを救え";
            public const string Story6 = "第６話　ブラックシャドーの罠";
            public const string Story7 = "第７話　F-ZERO グランプリ";
            public const string Story8 = "第８話　チャンピオンベルトの秘密";
            public const string Story9 = "第９話　創造主との最終決戦";
            // Other
            public const string Null = "---";
            public const string GrandPrixPodium = Null;
            public const string VictoryLap = Null;
        }

        public static readonly Course SandOcean_ScrewDrive = new()
        {
            CourseIndex = 0,
            Venue = Venue.SandOcean,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.ScrewDrive),
            ]),
        };

        public static readonly Course MuteCity_TwistRoad = new()
        {
            CourseIndex = 1,
            Venue = Venue.MuteCity,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.TwistRoad),
                new (GameCode.GFZJ01, NamesJP.TwistRoad),
                new (GameCode.GFZP01, NamesEN.TwistRoad),
                new (GameCode.GGGE6E, NamesEN.TwistRoad),
            ]),
        };

        public static readonly Course MuteCity_SerialGaps = new()
        {
            CourseIndex = 3,
            Venue = Venue.MuteCity,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.SerialGaps),
                new (GameCode.GFZJ01, NamesJP.SerialGaps),
                new (GameCode.GFZP01, NamesEN.SerialGaps),
                new (GameCode.GGGE6E, NamesEN.SerialGaps),
            ]),
        };

        public static readonly Course Aeropolis_Multiplex = new()
        {
            CourseIndex = 5,
            Venue = Venue.Aeropolis,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Multiplex),
                new (GameCode.GFZJ01, NamesJP.Multiplex),
                new (GameCode.GFZP01, NamesEN.Multiplex),
                new (GameCode.GGGE6E, NamesEN.Multiplex),
            ]),
        };

        public static readonly Course PortTown_AeroDive = new()
        {
            CourseIndex = 7,
            Venue = Venue.PortTown,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.AeroDive),
                new (GameCode.GFZJ01, NamesJP.AeroDive),
                new (GameCode.GFZP01, NamesEN.AeroDive),
                new (GameCode.GGGE6E, NamesEN.AeroDive),
            ]),
        };

        public static readonly Course Lightning_LoopCross = new()
        {
            CourseIndex = 8,
            Venue = Venue.Lightning,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.LoopCross),
                new (GameCode.GFZJ01, NamesJP.LoopCross),
                new (GameCode.GFZP01, NamesEN.LoopCross),
                new (GameCode.GGGE6E, NamesEN.LoopCross),
            ]),
        };

        public static readonly Course Lightning_HalfPipe = new()
        {
            CourseIndex = 9,
            Venue = Venue.Lightning,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.HalfPipe),
                new (GameCode.GFZJ01, NamesJP.HalfPipe),
                new (GameCode.GFZP01, NamesEN.HalfPipe),
                new (GameCode.GGGE6E, NamesEN.HalfPipe),
            ]),
        };

        public static readonly Course GreenPlant_Intersection = new()
        {
            CourseIndex = 10,
            Venue = Venue.GreenPlant,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Intersection),
                new (GameCode.GFZJ01, NamesJP.Intersection),
                new (GameCode.GFZP01, NamesEN.Intersection),
                new (GameCode.GGGE6E, NamesEN.Intersection),
            ]),
        };

        public static readonly Course GreenPlant_MobiusRing = new()
        {
            CourseIndex = 11,
            Venue = Venue.GreenPlant,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.MobiusRing),
                new (GameCode.GFZJ01, NamesJP.MobiusRing),
                new (GameCode.GFZP01, NamesEN.MobiusRing),
                new (GameCode.GGGE6E, NamesEN.MobiusRing),
            ]),
        };

        public static readonly Course PortTown_LongPipe = new()
        {
            CourseIndex = 13,
            Venue = Venue.PortTown,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.LongPipe),
                new (GameCode.GFZJ01, NamesJP.LongPipe),
                new (GameCode.GFZP01, NamesEN.LongPipe),
                new (GameCode.GGGE6E, NamesEN.LongPipe),
            ]),
        };

        public static readonly Course BigBlue_DriftHighway = new()
        {
            CourseIndex = 14,
            Venue = Venue.BigBlue,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.DriftHighway),
                new (GameCode.GFZJ01, NamesJP.DriftHighway),
                new (GameCode.GFZP01, NamesEN.DriftHighway),
                new (GameCode.GGGE6E, NamesEN.DriftHighway),
            ]),
        };

        public static readonly Course FireField_CylinderKnot = new()
        {
            CourseIndex = 15,
            Venue = Venue.FireField,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.CylinderKnot),
                new (GameCode.GFZJ01, NamesJP.CylinderKnot),
                new (GameCode.GFZP01, NamesEN.CylinderKnot),
                new (GameCode.GGGE6E, NamesEN.CylinderKnot),
            ]),
        };

        public static readonly Course CasinoPalace_SplitOval = new()
        {
            CourseIndex = 16,
            Venue = Venue.CasinoPalace,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.SplitOval),
                new (GameCode.GFZJ01, NamesJP.SplitOval),
                new (GameCode.GFZP01, NamesEN.SplitOval),
                new (GameCode.GGGE6E, NamesEN.SplitOval),
            ]),
        };

        public static readonly Course FireField_Undulation = new()
        {
            CourseIndex = 17,
            Venue = Venue.FireField,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Undulation),
                new (GameCode.GFZJ01, NamesJP.Undulation),
                new (GameCode.GFZP01, NamesEN.Undulation),
                new (GameCode.GGGE6E, NamesEN.Undulation),
            ]),
        };

        public static readonly Course Aeropolis_DragonSlope = new()
        {
            CourseIndex = 21,
            Venue = Venue.Aeropolis,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.DragonSlope),
                new (GameCode.GFZJ01, NamesJP.DragonSlope),
                new (GameCode.GFZP01, NamesEN.DragonSlope),
                new (GameCode.GGGE6E, NamesEN.DragonSlope),
            ]),
        };

        public static readonly Course CosmoTerminal_Trident = new()
        {
            CourseIndex = 24,
            Venue = Venue.CosmoTerminal,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Trident),
                new (GameCode.GFZJ01, NamesJP.Trident),
                new (GameCode.GFZP01, NamesEN.Trident),
                new (GameCode.GGGE6E, NamesEN.Trident),
            ]),
        };

        public static readonly Course SandOcean_LateralShift = new()
        {
            CourseIndex = 25,
            Venue = Venue.SandOcean,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.LateralShift),
                new (GameCode.GFZJ01, NamesJP.LateralShift),
                new (GameCode.GFZP01, NamesEN.LateralShift),
                new (GameCode.GGGE6E, NamesEN.LateralShift),
            ]),
        };

        public static readonly Course SandOcean_SurfaceSlide = new()
        {
            CourseIndex = 26,
            Venue = Venue.SandOcean,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.SurfaceSlide),
                new (GameCode.GFZJ01, NamesJP.SurfaceSlide),
                new (GameCode.GFZP01, NamesEN.SurfaceSlide),
                new (GameCode.GGGE6E, NamesEN.SurfaceSlide),
            ]),
        };

        public static readonly Course BigBlue_Ordeal = new()
        {
            CourseIndex = 27,
            Venue = Venue.BigBlue,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Ordeal),
                new (GameCode.GFZJ01, NamesJP.Ordeal),
                new (GameCode.GFZP01, NamesEN.Ordeal),
                new (GameCode.GGGE6E, NamesEN.Ordeal),
            ]),
        };

        public static readonly Course PhantomRoad_SlimLineSlits = new()
        {
            CourseIndex = 28,
            Venue = Venue.PhantomRoad,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.SlimLineSlits),
                new (GameCode.GFZJ01, NamesJP.SlimLineSlits),
                new (GameCode.GFZP01, NamesEN.SlimLineSlits),
                new (GameCode.GGGE6E, NamesEN.SlimLineSlits),
            ]),
        };

        public static readonly Course CasinoPalace_DoubleBranches = new()
        {
            CourseIndex = 29,
            Venue = Venue.CasinoPalace,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.DoubleBranches),
                new (GameCode.GFZJ01, NamesJP.DoubleBranches),
                new (GameCode.GFZP01, NamesEN.DoubleBranches),
                new (GameCode.GGGE6E, NamesEN.DoubleBranches),
            ]),
        };

        public static readonly Course Aeropolis_ScrewDrive = new()
        {
            CourseIndex = 31,
            Venue = Venue.Aeropolis,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.ScrewDrive),
                new (GameCode.GFZJ01, NamesJP.ScrewDrive),
                new (GameCode.GFZP01, NamesEN.ScrewDrive),
                new (GameCode.GGGE6E, NamesEN.ScrewDrive),
            ]),
        };

        public static readonly Course OuterSpace_MeteorStream = new()
        {
            CourseIndex = 32,
            Venue = Venue.OuterSpace,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.MeteorStream),
                new (GameCode.GFZJ01, NamesJP.MeteorStream),
                new (GameCode.GFZP01, NamesEN.MeteorStream),
                new (GameCode.GGGE6E, NamesEN.MeteorStream),
            ]),
        };

        public static readonly Course PortTown_CylinderWave = new()
        {
            CourseIndex = 33,
            Venue = Venue.PortTown,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.CylinderWave),
                new (GameCode.GFZJ01, NamesJP.CylinderWave),
                new (GameCode.GFZP01, NamesEN.CylinderWave),
                new (GameCode.GGGE6E, NamesEN.CylinderWave),
            ]),
        };

        public static readonly Course Lightning_ThunderRoad = new()
        {
            CourseIndex = 34,
            Venue = Venue.Lightning,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.ThunderRoad),
                new (GameCode.GFZJ01, NamesJP.ThunderRoad),
                new (GameCode.GFZP01, NamesEN.ThunderRoad),
                new (GameCode.GGGE6E, NamesEN.ThunderRoad),
            ]),
        };

        public static readonly Course GreenPlant_Spiral = new()
        {
            CourseIndex = 35,
            Venue = Venue.GreenPlant,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Spiral),
                new (GameCode.GFZJ01, NamesJP.Spiral),
                new (GameCode.GFZP01, NamesEN.Spiral),
                new (GameCode.GGGE6E, NamesEN.Spiral),
            ]),
        };

        public static readonly Course MuteCity_SonicOval = new()
        {
            CourseIndex = 36,
            Venue = Venue.MuteCityCom,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.SonicOval),
                new (GameCode.GFZJ01, NamesJP.SonicOval),
                new (GameCode.GFZP01, NamesEN.SonicOval),
                new (GameCode.GGGE6E, NamesEN.SonicOval),
            ]),
        };

        public static readonly Course Story1 = new()
        {
            CourseIndex = 37,
            Venue = Venue.MuteCityComStory,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Story1),
                new (GameCode.GFZJ01, NamesJP.Story1),
                new (GameCode.GFZP01, NamesEN.Story1),
                new (GameCode.GGGE6E, NamesEN.Story1),
            ]),
        };

        public static readonly Course Story2 = new()
        {
            CourseIndex = 38,
            Venue = Venue.SandOceanStory,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Story2),
                new (GameCode.GFZJ01, NamesJP.Story2),
                new (GameCode.GFZP01, NamesEN.Story2),
                new (GameCode.GGGE6E, NamesEN.Story2),
            ]),
        };

        public static readonly Course Story3 = new()
        {
            CourseIndex = 39,
            Venue = Venue.CasinoPalace,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Story3),
                new (GameCode.GFZJ01, NamesJP.Story3),
                new (GameCode.GFZP01, NamesEN.Story3),
                new (GameCode.GGGE6E, NamesEN.Story3),
            ]),
        };

        public static readonly Course Story4 = new()
        {
            CourseIndex = 40,
            Venue = Venue.BigBlueStory,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Story4),
                new (GameCode.GFZJ01, NamesJP.Story4),
                new (GameCode.GFZP01, NamesEN.Story4),
                new (GameCode.GGGE6E, NamesEN.Story4),
            ]),
        };

        public static readonly Course Story5 = new()
        {
            CourseIndex = 41,
            Venue = Venue.Lightning,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Story5),
                new (GameCode.GFZJ01, NamesJP.Story5),
                new (GameCode.GFZP01, NamesEN.Story5),
                new (GameCode.GGGE6E, NamesEN.Story5),
            ]),
        };

        public static readonly Course Story6 = new()
        {
            CourseIndex = 42,
            Venue = Venue.PortTownStory,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Story6),
                new (GameCode.GFZJ01, NamesJP.Story6),
                new (GameCode.GFZP01, NamesEN.Story6),
                new (GameCode.GGGE6E, NamesEN.Story6),
            ]),
        };

        public static readonly Course Story7 = new()
        {
            CourseIndex = 43,
            Venue = Venue.MuteCity,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Story7),
                new (GameCode.GFZJ01, NamesJP.Story7),
                new (GameCode.GFZP01, NamesEN.Story7),
                new (GameCode.GGGE6E, NamesEN.Story7),
            ]),
        };

        public static readonly Course Story8 = new()
        {
            CourseIndex = 44,
            Venue = Venue.FireFieldStory,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Story8),
                new (GameCode.GFZJ01, NamesJP.Story8),
                new (GameCode.GFZP01, NamesEN.Story8),
                new (GameCode.GGGE6E, NamesEN.Story8),
            ]),
        };

        public static readonly Course Story9 = new()
        {
            CourseIndex = 45,
            Venue = Venue.PhantomRoad,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Story9),
                new (GameCode.GFZJ01, NamesJP.Story9),
                new (GameCode.GFZP01, NamesEN.Story9),
                new (GameCode.GGGE6E, NamesEN.Story9),
            ]),
        };

        public static readonly Course GrandPrixPodium = new()
        {
            CourseIndex = 49,
            Venue = Venue.MuteCityGrandPrixPodium,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Null),
                new (GameCode.GFZJ01, NamesJP.Null),
                new (GameCode.GFZP01, NamesEN.Null),
                new (GameCode.GGGE6E, NamesEN.Null),
            ]),
        };

        public static readonly Course VictoryLap = new()
        {
            CourseIndex = 50,
            Venue = Venue.MuteCityGrandPrixPodium,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Null),
                new (GameCode.GFZJ01, NamesJP.Null),
                new (GameCode.GFZP01, NamesEN.Null),
                new (GameCode.GGGE6E, NamesEN.Null),
            ]),
        };

        public static readonly Course Test_SurfaceSlide = new()
        {
            CourseIndex = 72,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.Story9),
            ]),
        };

        public static readonly Course Test_LoopCross = new()
        {
            CourseIndex = 77,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.LoopCross),
            ]),
        };

        public static readonly Course Test_MeteorStream = new()
        {
            CourseIndex = 86,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.MeteorStream),
            ]),
        };

        public static readonly Course Test_CylinderWave = new()
        {
            CourseIndex = 87,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.CylinderWave),
            ]),
        };

        public static readonly Course Test_LongPipe = new()
        {
            CourseIndex = 90,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.LongPipe),
            ]),
        };

        public static readonly Course Test_Story2 = new()
        {
            CourseIndex = 91,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.Story2),
            ]),
        };


        public static readonly Course Test_Story3 = new()
        {
            CourseIndex = 92,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.Story3),
            ]),
        };


        public static readonly Course Test_Story4 = new()
        {
            CourseIndex = 93,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.Story4),
            ]),
        };


        public static readonly Course Test_Story5 = new()
        {
            CourseIndex = 94,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.Story5),
            ]),
        };


        public static readonly Course Test_Story6 = new()
        {
            CourseIndex = 95,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.Story6),
            ]),
        };


        public static readonly Course Test_Story7 = new()
        {
            CourseIndex = 96,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.Story7),
            ]),
        };


        public static readonly Course Test_Story8 = new()
        {
            CourseIndex = 97,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.Story8),
            ]),
        };

        public static readonly Course Test_Story9 = new()
        {
            CourseIndex = 98,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.Story9),
            ]),
        };

        public static readonly Course Test_TwistRoadOld = new()
        {
            CourseIndex = 101,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.TwistRoad),
            ]),
        };

        public static readonly Course Test_TwistRoad = new()
        {
            CourseIndex = 102,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.TwistRoad),
            ]),
        };

        public static readonly Course Test_Multiplex = new()
        {
            CourseIndex = 103,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.Multiplex),
            ]),
        };

        public static readonly Course Test_Intersection = new()
        {
            CourseIndex = 104,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.Intersection),
            ]),
        };

        public static readonly Course Test_Undulation = new()
        {
            CourseIndex = 105,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.Undulation),
            ]),
        };

        public static readonly Course Test_DriftHighway = new()
        {
            CourseIndex = 107,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.DriftHighway),
            ]),
        };

        public static readonly Course Test_AeroDive = new()
        {
            CourseIndex = 108,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.AeroDive),
            ]),
        };

        public static readonly Course Test_LateralShift = new()
        {
            CourseIndex = 110,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GGGE6E, NamesEN.LateralShift),
            ]),
        };

        public static readonly Course Null = new()
        {
            CourseIndex = 255,
            Venue = Venue.Null,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.Null),
                new (GameCode.GFZJ01, NamesJP.Null),
                new (GameCode.GFZP01, NamesEN.Null),
                new (GameCode.GGGE6E, NamesEN.Null),
            ]),
        };

        public static readonly Course[] DefaultCourses =
        [
            // 0
            SandOcean_ScrewDrive,
            MuteCity_TwistRoad,
            Null,
            MuteCity_SerialGaps,
            Null,
            Aeropolis_Multiplex,
            Null,
            PortTown_AeroDive,
            Lightning_LoopCross,
            Lightning_HalfPipe,
            // 10
            GreenPlant_Intersection,
            GreenPlant_MobiusRing,
            Null,
            PortTown_LongPipe,
            BigBlue_DriftHighway,
            FireField_CylinderKnot,
            CasinoPalace_SplitOval,
            FireField_Undulation,
            Null,
            Null,
            // 20
            Null,
            Aeropolis_DragonSlope,
            Null,
            Null,
            CosmoTerminal_Trident,
            SandOcean_LateralShift,
            SandOcean_SurfaceSlide,
            BigBlue_Ordeal,
            PhantomRoad_SlimLineSlits,
            CasinoPalace_DoubleBranches,
            // 30
            Null,
            Aeropolis_ScrewDrive,
            OuterSpace_MeteorStream,
            PortTown_CylinderWave,
            Lightning_ThunderRoad,
            GreenPlant_Spiral,
            MuteCity_SonicOval,
            Story1,
            Story2,
            Story3,
            // 40
            Story4,
            Story5,
            Story6,
            Story7,
            Story8,
            Story9,
            Null,
            Null,
            Null,
            GrandPrixPodium,
            // 50
            VictoryLap,
            Null,
            Null,
            Null,
            Null,
            Null,
            Null,
            Null,
            Null,
            Null,
            // 60
            Null,
            Null,
            Null,
            Null,
            Null,
            Null,
            Null,
            Null,
            Null,
            Null,
            // 70
            Null,
            Null,
            Test_SurfaceSlide,
            Null,
            Null,
            Null,
            Null,
            Test_LoopCross,
            Null,
            Null,
            // 80
            Null,
            Null,
            Null,
            Null,
            Null,
            Null,
            Test_MeteorStream,
            Test_CylinderWave,
            Null,
            Null,
            // 90
            Test_LongPipe,
            Test_Story2,
            Test_Story3,
            Test_Story4,
            Test_Story5,
            Test_Story6,
            Test_Story7,
            Test_Story8,
            Test_Story9,
            Null,
            // 100
            Null,
            Test_TwistRoadOld,
            Test_TwistRoad,
            Test_Multiplex,
            Test_Intersection,
            Test_Undulation,
            Null,
            Test_DriftHighway,
            Test_AeroDive,
            Null,
            // 110
            Test_LateralShift,
        ];

        public static void UnitTest()
        {
            foreach (var gameCode in Enum.GetValues<GameCode>())
            {
                if (gameCode == GameCode.GFZJ8P)
                    continue;

                Console.WriteLine(gameCode);
                for (int i = 0; i < GameDataConsts.MaxStageIndex; i++)
                {
                    Course c = DefaultCourses[i];
                    if (c != Null)
                    {
                        if (i != c.CourseIndex)
                            throw new System.Exception("Wrong index match!");
                    }

                    // Catch few cases where only AX defines track, so GameCode is not present for course name
                    if (!c.Name.ContainsKey(gameCode))
                        c = Null;
                    // Print
                    Console.WriteLine($"{gameCode} {i,3} - Stage: {c.CourseIndex,3}, {c.Venue.Name[gameCode]} [{c.Name[gameCode]}]");
                }
                Console.WriteLine();
            }
        }
    }
}
