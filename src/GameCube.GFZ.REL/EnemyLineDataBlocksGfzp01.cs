using System.Collections.Generic;

namespace GameCube.GFZ.REL
{
    /// <summary>
    /// PAL
    /// </summary>
    public class EnemyLineDataBlocksGfzp01 : EnemyLineDataBlocks
    {
        public EnemyLineDataBlocksGfzp01()
        {
            CourseNameAreas.Add(new CustomizableArea(CourseNamesEnglish.Address, CourseNamesEnglish.Size));
            CourseNameAreas.Add(new CustomizableArea(ForbiddenWords.Address, ForbiddenWords.Size));
        }

        public const string kFileHashMD5 = "96398b677d77e2ae1592b695a4bebaca";

        public override GameCode GameCode => GameCode.GFZP01;
        public override string SourceFile => "enemy_line/line__.bin";
        public override string WorkingFile => "enemy_line/line__.rel";
        public override string FileHashMD5 => kFileHashMD5;
        public override DataBlock VenueNames => new DataBlock(0x199940, 0xA0);
        public override DataBlock SlotVenueDefinitions => new DataBlock(0x19A094, 111);
        public override DataBlock CourseNamesEnglish => new DataBlock(0x19A11C, 0x15C);
        public override DataBlock CourseNamesTranslations => throw new System.NotImplementedException("This is absent from the EUR version");
        public override DataBlock CourseSlotDifficulty => new DataBlock(0x1698EC, 111);
        public override DataBlock CourseSlotBgm => new DataBlock(0x16495C, 56);
        public override DataBlock CourseSlotBgmFinalLap => new DataBlock(0x164994, 184);
        public override DataBlock CupCourseLut => new DataBlock(0x1688B0, 0x84);
        public override DataBlock CupCourseLutAssets => new DataBlock(0x168934, 0x84);
        public override DataBlock CupCourseLutUnk => new DataBlock(0x1689B8, 0x84);
        public override DataBlock CourseNameOffsetStructs => new DataBlock(0x201F38, 0x14D0);
        public override DataBlock CourseMinimapParameterStructs => new DataBlock(0x18CF50, 0x508);
        public override DataBlock ForbiddenWords => new DataBlock(0x1BB83C, 0x3E0);
        public override DataBlock AxModeCourseTimers => new DataBlock(0x1B7810, 6);
        public override int CourseNamePointerOffsetBase => 0x16E5A0;
        public override List<CustomizableArea> CourseNameAreas => new List<CustomizableArea>();
        public override DataBlock PilotPositions => new DataBlock(0x1A38F4, 0x210);
        public override DataBlock PilotToMachineLut => new DataBlock(0x168800, 0xA4);

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
