using Manifold.IO;
using System.Collections.Generic;
using System.Text;

namespace GameCube.GFZ.LineREL
{
    /// <summary>
    /// NTSC E
    /// </summary>
    public class LineInfoGfze01 : LineRelInfo
    {
        public LineInfoGfze01()
        {
            CourseNameAreas.Add(new CustomizableArea(CourseNamesEnglish.Address, CourseNamesEnglish.Size));
            CourseNameAreas.Add(new CustomizableArea(CourseNamesLocalizations.Address, CourseNamesLocalizations.Size));
        }

        public const string kFileHashMD5 = "a1790e38cbe17510017689088eab5758";
        public const string kFileHashMD5_1kb = "";

        public override GameCode GameCode => GameCode.GFZE01;
        public override Encoding TextEncoding => AsciiCString.ascii;
        public override string SourceFile => "enemy_line/line__.bin";
        public override string WorkingFile => "enemy_line/line__.rel";
        public override string FileHashMD5 => kFileHashMD5;
        public override Pointer StringTableBaseAddress => 0x16D600;

        public override ArrayPointer32 VenueNameOffsets => throw new System.NotImplementedException();
        public override ArrayPointer32 VenueNamesEnglishOffsets => throw new System.NotImplementedException();
        public override ArrayPointer32 VenueNamesJapaneseOffsets => throw new System.NotImplementedException();
        public override DataBlock VenueNamesEnglish => new DataBlock(0x197F80, 0xA4);
        public override DataBlock VenueNamesJapanese => throw new System.NotImplementedException();

        public override int CourseNameLanguages => throw new System.NotImplementedException();
        public override ArrayPointer32 CourseNameOffsets => new(0x201F38, 666);
        public override DataBlock CourseNamesEnglish => new DataBlock(0x19875C, 0x15C);
        public override DataBlock CourseNamesLocalizations => new DataBlock(0x198A7C, 0x8D8);

        public override DataBlock CourseVenueIndex => new DataBlock(0x1986D4, 111);
        public override DataBlock CourseDifficulty => new DataBlock(0x168958, 111);
        public override DataBlock CourseBgmIndex => new DataBlock(0x163A8C, 56);
        public override DataBlock CourseBgmFinalLapIndex => new DataBlock(0x163AC4, 184);
        public override DataBlock CupCourseLut => new DataBlock(0x167940, 0x84);
        public override DataBlock CupCourseLutAssets => new DataBlock(0x1679C4, 0x84);
        public override DataBlock CupCourseLutUnk => new DataBlock(0x167A48, 0x84);
        public override DataBlock CourseMinimapParameterStructs => new DataBlock(0x18B5B0, 0x508);
        public override DataBlock ForbiddenWords => new DataBlock(0x1B0630, 0x3E0);
        public override DataBlock AxModeCourseTimers => new DataBlock(0x1ADBC0, 6);
        public override List<CustomizableArea> CourseNameAreas => new List<CustomizableArea>();
        public override DataBlock PilotPositions => new DataBlock(0x1A19C4, 0x210);
        public override DataBlock PilotToMachineLut => new DataBlock(0x167890, 0xA4);

        public override short Salt => unchecked((short)0x180a);
        public override int Key0 => unchecked((int)0x000cd8f3);
        public override int Key1 => unchecked((int)0x9b36bb94);
        public override int Key2 => unchecked((int)0xaf8910be);
        public override int BlockKey0 => unchecked((int)0x9b370000);
        public override short BlockKey1 => unchecked((short)0xbb94);
        public override short BlockKey2 => unchecked((short)0xd8f3);
    }
}
