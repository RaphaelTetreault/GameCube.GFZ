using Manifold.IO;
using System.Collections.Generic;
using System.Text;

namespace GameCube.GFZ.LineREL
{
    /// <summary>
    /// NTSC J
    /// </summary>
    public class LineRelInfoGfzj01 : LineRelInfo
    {
        public const string kFileHashMD5 = "f8947b6cec19af95f96fb9d11670ebdd";

        public override GameCode GameCode => GameCode.GFZJ01;
        public override Encoding TextEncoding => ShiftJisCString.shiftJis;
        public override string SourceFile => "enemy_line/line__.bin";
        public override string WorkingFile => "enemy_line/line__.rel";
        public override string FileHashMD5 => kFileHashMD5;

        // Where strings begin in the REL
        public override Pointer StringTableBaseAddress => 0x16A180;

        // VENUE NAMES
        public override ArrayPointer32 VenueNameOffsets => new(0x1F1C2C, 42); // ENG followed by JPN
        public override ArrayPointer32 VenueNamesEnglishOffsets => new(0x1F1C2C, 22);
        public override ArrayPointer32 VenueNamesJapaneseOffsets => new(0x1F1CDC, 20);
        public override DataBlock VenueNamesEnglish => new(0x194A60, 0xA4);
        public override DataBlock VenueNamesJapanese => new(0x194B5C, 0xD8);

        // COURSE NAMES
        public override int CourseNameLanguages => 6;
        public override ArrayPointer32 CourseNameOffsets => new(0x1F286C, 666); // 111 * 6
        public override DataBlock CourseNamesEnglish => new(0x19523C, 0x15C); // English strings
        public override DataBlock CourseNamesLocalizations => new(0x19555C, 0x8D8); // Other localization strings

        // CARDATA
        public override Pointer CarDataMachinesPtr => 0x192140;
        public override Pointer MachineLetterRatingsPtr => 0x1AA3F8;

        public override DataBlock CourseVenueIndex => new(0x1951B4, 111);
        public override DataBlock CourseDifficulty => new(0x165460, 111);
        public override DataBlock CourseBgmIndex => new(0x1607B4, 56);
        public override DataBlock CourseBgmFinalLapIndex => new(0x1607EC, 184);
        public override DataBlock CupCourseLut => new(0x164548, 0x84);
        public override DataBlock CupCourseLutAssets => new(0x1645CC, 0x84);
        public override DataBlock CupCourseLutUnk => new(0x164650, 0x84);
        public override DataBlock CourseMinimapParameterStructs => new(0x188098, 0x508);
        public override DataBlock ForbiddenWords => new(0x1ABA60, 0x3E0);
        public override DataBlock AxModeCourseTimers => new(0x1A9390, 6);
        public override DataBlock PilotPositions => new(0x19E49C, 0x210);
        public override DataBlock PilotToMachineLut => new(0x164498, 0xA4);

        public override short Salt => unchecked((short)0x0ce0);
        public override int Key0 => unchecked((int)0x0004f107);
        public override int Key1 => unchecked((int)0xb5fb6483);
        public override int Key2 => unchecked((int)0xdeaddead);
        public override int BlockKey0 => unchecked((int)0xb5fb0000);
        public override short BlockKey1 => unchecked((short)0x6483);
        public override short BlockKey2 => unchecked((short)0xf107);
    }
}
