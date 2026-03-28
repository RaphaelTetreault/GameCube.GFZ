using System.Collections.Frozen;

namespace GameCube.GFZ.GameData
{
    public readonly record struct Course
    {
        public required byte CourseIndex { get; init; }
        public required Venue Venue { get; init; }
        public required FrozenDictionary<GameCode, string> Name { get; init; }


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
            public const string Story1 = "Story 1 - Captain Falcon Trains";
            public const string Story2 = "Story 2 - Goroh: The Vengeful Samurai";
            public const string Story3 = "Story 3 - High Stakes in Mute City";
            public const string Story4 = "Story 4 - Challenge of the Bloody Chain";
            public const string Story5 = "Story 5 - Save Jody!";
            public const string Story6 = "Story 6 - Black Shadow's Trap";
            public const string Story7 = "Story 7 - The F-Zero Grand Prix";
            public const string Story8 = "Story 8 - Secrets of the Champion Belt";
            public const string Story9 = "Story 9 - Finale: Enter the Creators";
            // Other
            public const string Null = "---";
        }
        public static class NamesJP
        {
            // Ruby Cup
            public const string TwistRoad = "ツイストロード";
            public const string SplitOval = "スプリットオーバル";
            public const string SurfaceSlide = "サーフェイススライド";
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
            public const string Ordeal = "オルディール";
            // Diamond Cup
            public const string Trident = "トライデント";
            public const string LateralShift = "ラテラルシフト";
            public const string Undulation = "アンジュレーション";
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
            public const string Story1 = "Story 1 - ";
            public const string Story2 = "Story 2 - ";
            public const string Story3 = "Story 3 - ";
            public const string Story4 = "Story 4 - ";
            public const string Story5 = "Story 5 - ";
            public const string Story6 = "Story 6 - ";
            public const string Story7 = "Story 7 - ";
            public const string Story8 = "Story 8 - ";
            public const string Story9 = "Story 9 - ";
            // Other
            public const string Null = "---";
        }

        public static readonly Course MuteCity_TwistRoad = new()
        {
            CourseIndex = 1,
            Venue = Venue.MuteCity,
            Name = FrozenDictionary.Create<GameCode, string>(
            [
                new (GameCode.GFZE01, NamesEN.TwistRoad),
                new (GameCode.GFZJ01, NamesJP.TwistRoad),
                new (GameCode.GFZJ8P, NamesEN.TwistRoad), //?
                new (GameCode.GFZP01, NamesEN.TwistRoad),
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
                new (GameCode.GFZJ8P, NamesEN.SerialGaps), //?
                new (GameCode.GFZP01, NamesEN.SerialGaps),
            ]),
        };

        // TODO:    Finish GX stages
        //          Review AX to see what it actually calls index50+ stages

    }
}
