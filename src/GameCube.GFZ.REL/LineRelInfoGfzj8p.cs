using Manifold.IO;
using System.Collections.Generic;
using System.Text;

namespace GameCube.GFZ.LineREL
{
    /// <summary>
    /// AX
    /// </summary>
    public class LineRelInfoGfzj8p : LineRelInfo
    {
        public LineRelInfoGfzj8p()
        {
            CourseNameAreas.Add(new CustomizableArea(CourseNamesEnglish.Address, CourseNamesEnglish.Size));
            CourseNameAreas.Add(new CustomizableArea(CourseNamesLocalizations.Address, CourseNamesLocalizations.Size));
        }

        // TODO: const for file hash
        public override GameCode GameCode => GameCode.GFZJ8P;
        public override Encoding TextEncoding => ShiftJisCString.shiftJis;
        public override string SourceFile => "../sys/main.dol"; //...?
        public override string WorkingFile => "../sys/main.dol";
        public override string FileHashMD5 => throw new System.NotImplementedException();

        public override ArrayPointer32 VenueNameOffsets => throw new System.NotImplementedException();
        public override ArrayPointer32 VenueNamesEnglishOffsets => throw new System.NotImplementedException();
        public override ArrayPointer32 VenueNamesJapaneseOffsets => throw new System.NotImplementedException();
        public override DataBlock VenueNamesEnglish => new DataBlock(0x21AE54, 0x8C);
        public override DataBlock VenueNamesJapanese => throw new System.NotImplementedException();

        public override int CourseNameLanguages => throw new System.NotImplementedException();
        public override ArrayPointer32 CourseNameOffsets => new(0x201F38, 666);
        public override DataBlock CourseNamesEnglish => new DataBlock(0x21B474, 0x140);
        public override DataBlock CourseNamesLocalizations => new DataBlock(0x21B770, 0x8D8);

        public override Pointer CarDataMachinesPtr => throw new System.NotImplementedException();

        public override DataBlock CourseVenueIndex => new DataBlock(0x21B3EC, 111);
        public override DataBlock CourseDifficulty => throw new System.NotImplementedException("This is absent from the AX version");
        public override DataBlock CourseBgmIndex => new DataBlock(0x20E3F0, 56);
        public override DataBlock CourseBgmFinalLapIndex => new DataBlock(0x20E484, 184);
        public override DataBlock CupCourseLut => new DataBlock(0x20FB64, 0x84);
        public override DataBlock CupCourseLutAssets => new DataBlock(0x20FBE8, 0x84);
        public override DataBlock CupCourseLutUnk => new DataBlock(0x20FC6C, 0x84);
        public override DataBlock CourseMinimapParameterStructs => new DataBlock(0, 0x508);
        public override DataBlock ForbiddenWords => throw new System.NotImplementedException("This is absent from the AX version");
        public override DataBlock AxModeCourseTimers => new DataBlock(0x3390C8, 6);
        public override Pointer StringTableBaseAddress => 0;
        public override List<CustomizableArea> CourseNameAreas => new List<CustomizableArea>();
        public override DataBlock PilotPositions => new DataBlock(0x230004, 0x210);
        public override DataBlock PilotToMachineLut => new DataBlock(0x20FAC0, 0xA4);

        public override short Salt => throw new System.NotImplementedException();
        public override int Key0 => throw new System.NotImplementedException();
        public override int Key1 => throw new System.NotImplementedException();
        public override int Key2 => throw new System.NotImplementedException();
        public override int BlockKey0 => throw new System.NotImplementedException();
        public override short BlockKey1 => throw new System.NotImplementedException();
        public override short BlockKey2 => throw new System.NotImplementedException();
    }
}
