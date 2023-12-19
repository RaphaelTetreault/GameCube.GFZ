using Manifold.IO;
using System.Collections.Generic;
using System.Text;

namespace GameCube.GFZ.LineREL
{
    /// <summary>
    /// PAL
    /// </summary>
    public class LineRelInfoGfzp01 : LineRelInfo
    {
        public LineRelInfoGfzp01()
        {
            CourseNameAreas.Add(new CustomizableArea(CourseNamesEnglish.Address, CourseNamesEnglish.Size));
            CourseNameAreas.Add(new CustomizableArea(ForbiddenWords.Address, ForbiddenWords.Size));
        }

        public const string kFileHashMD5 = "96398b677d77e2ae1592b695a4bebaca";

        public override GameCode GameCode => GameCode.GFZP01;
        public override Encoding TextEncoding => AsciiCString.ascii;
        public override string SourceFile => "enemy_line/line__.bin";
        public override string WorkingFile => "enemy_line/line__.rel";
        public override string FileHashMD5 => kFileHashMD5;
        public override Pointer StringTableBaseAddress => 0x16E5A0;

        public override ArrayPointer32 VenueNameOffsets => throw new System.NotImplementedException();
        public override ArrayPointer32 VenueNamesEnglishOffsets => throw new System.NotImplementedException();
        public override ArrayPointer32 VenueNamesJapaneseOffsets => throw new System.NotImplementedException();
        public override DataBlock VenueNamesEnglish => new DataBlock(0x199940, 0xA0);
        public override DataBlock VenueNamesJapanese => throw new System.NotImplementedException();

        public override int CourseNameLanguages => throw new System.NotImplementedException();
        public override ArrayPointer32 CourseNameOffsets => new(0x201F38, 666);
        public override DataBlock CourseNamesEnglish => new(0x19A11C, 0x15C);
        public override DataBlock CourseNamesLocalizations => throw new System.NotImplementedException("This is absent from the EUR version");

        public override Pointer CarDataMachinesPtr => throw new System.NotImplementedException();
        public override Pointer MachineLetterRatingsPtr => 0x1B8E20;

        public override DataBlock CourseVenueIndex => new DataBlock(0x19A094, 111);
        public override DataBlock CourseDifficulty => new DataBlock(0x1698EC, 111);
        public override DataBlock CourseBgmIndex => new DataBlock(0x16495C, 56);
        public override DataBlock CourseBgmFinalLapIndex => new DataBlock(0x164994, 184);
        public override DataBlock CupCourseLut => new DataBlock(0x1688B0, 0x84);
        public override DataBlock CupCourseLutAssets => new DataBlock(0x168934, 0x84);
        public override DataBlock CupCourseLutUnk => new DataBlock(0x1689B8, 0x84);
        public override DataBlock CourseMinimapParameterStructs => new DataBlock(0x18CF50, 0x508);
        public override DataBlock ForbiddenWords => new DataBlock(0x1BB83C, 0x3E0);
        public override DataBlock AxModeCourseTimers => new DataBlock(0x1B7810, 6);
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
