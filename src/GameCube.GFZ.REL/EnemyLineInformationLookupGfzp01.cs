using System.Collections.Generic;

namespace GameCube.GFZ.REL
{
    /// <summary>
    /// PAL
    /// </summary>
    public class EnemyLineInformationLookupGfzp01 : EnemyLineInformationLookup
    {
        public EnemyLineInformationLookupGfzp01()
        {
            CourseNameAreas.Add(new CustomizableArea(CourseNamesEnglish.Address, CourseNamesEnglish.Size));
            CourseNameAreas.Add(new CustomizableArea(ForbiddenWords.Address, ForbiddenWords.Size));
        }

        public const string kFileHashMD5 = "96398b677d77e2ae1592b695a4bebaca";

        public override EnemyLineInformation.GameCode GameCode => EnemyLineInformation.GameCode.GX_P;
        public override string SourceFile => "enemy_line/line__.bin";
        public override string WorkingFile => "enemy_line/line__.rel";
        public override string FileHashMD5 => kFileHashMD5;
        public override Information VenueNames => new Information(0x199940, 0xA0);
        public override Information SlotVenueDefinitions => new Information(0x19A094, 111);
        public override Information CourseNamesEnglish => new Information(0x19A11C, 0x15C);
        public override Information CourseNamesTranslations => throw new System.NotImplementedException("This is absent from the EUR version");
        public override Information CourseSlotDifficulty => new Information(0x1698EC, 111);
        public override Information CourseSlotBgm => new Information(0x16495C, 56);
        public override Information CourseSlotBgmFinalLap => new Information(0x164994, 184);
        public override Information CupCourseLut => new Information(0x1688B0, 0x84);
        public override Information CupCourseLutAssets => new Information(0x168934, 0x84);
        public override Information CupCourseLutUnk => new Information(0x1689B8, 0x84);
        public override Information CourseNameOffsetStructs => new Information(0x201F38, 0x14D0);
        public override Information CourseMinimapParameterStructs => new Information(0x18CF50, 0x508);
        public override Information ForbiddenWords => new Information(0x1BB83C, 0x3E0);
        public override Information AxModeCourseTimers => new Information(0x1B7810, 6);
        public override int CourseNamePointerOffsetBase => 0x16E5A0;
        public override List<CustomizableArea> CourseNameAreas => new List<CustomizableArea>();
        public override Information PilotPositions => new Information(0x1A38F4, 0x210);
        public override Information PilotToMachineLut => new Information(0x168800, 0xA4);

        // Same as GFZE01
        public override short Salt => unchecked((short)0x180a);
        public override int Key0 => unchecked((int)0x000cd8f3);
        public override int Key1 => unchecked((int)0x9b36bb94);
        public override int Key2 => unchecked((int)0xaf8910be);
        public override int BlockKey0 => unchecked((int)0x9b370000);
        public override short BlockKey1 => unchecked((short)0xbb94);
        public override short BlockKey2 => unchecked((short)0xd8f3);
    }
}
