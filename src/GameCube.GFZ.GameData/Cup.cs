using System;

namespace GameCube.GFZ.GameData
{
    public readonly record struct Cup
    {
        private const int MaxCourseCount = 6;

        public required CupIndex CupIndex { get; init; }
        public required Course[] Courses { get; init; }

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
            ]
        };

        public static readonly Cup AllCup_FunctionalValues = new()
        {
            CupIndex = CupIndex.AllCup,
            Courses = Course.DefaultCourses,
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
            ]
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
            ]
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
            ]
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
            ]
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
            ]
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
            ]
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
            ]
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
            ]
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
            ]
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
            ]
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
