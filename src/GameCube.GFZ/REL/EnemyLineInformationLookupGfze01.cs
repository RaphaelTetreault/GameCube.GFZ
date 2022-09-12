using Manifold;
using Manifold.IO;
using System.Collections.Generic;

namespace GameCube.GFZ.REL
{
    /// <summary>
    /// NTSC E
    /// </summary>
    public class EnemyLineInformationLookupGfze01 : EnemyLineInformationLookup
    {
        public override string FileHashMD5 => "a1790e38cbe17510017689088eab5758";
        public override Information VenueNames => new Information(0x197F80, 0xA4);
        public override Information SlotVenueDefinitions => new Information(0x1986D4, 111);
        public override Information CourseNamesEnglish => new Information(0x19875C, 0x15C);
        public override Information CourseNamesTranslations => new Information(0x198A7C, 0x8D8);
        public override Information CourseSlotDifficulty => new Information(0x168958, 111);
        public override Information CourseSlotBgm => new Information(0x163A8C, 56);
        public override Information CourseSlotBgmFinalLap => new Information(0x163AC4, 184);
        public override Information CupCourseLut => new Information(0x167940, 0x84);
        public override Information CupCourseLutAssets => new Information(0x1679C4, 0x84);
        public override Information CupCourseLutUnk => new Information(0x167A48, 0x84);
        public override Information CourseNameOffsetStructs => new Information(0x1F7E28, 0x14D0);
        public override Information CourseMinimapParameterStructs => new Information(0x18B5B0, 0x508);
        public override Information ForbiddenWords => new Information(0x1B0630, 0x3E0);
        public override Information AxModeCourseTimers => new Information(0x1ADBC0, 6);
        public override int CourseNamePointerOffsetBase => 0x16D600;
        public override List<CustomizableArea> CourseNameAreas => new List<CustomizableArea>();
        public override Information PilotPositions => new Information(0x1A19C4, 0x210);
        public override Information PilotToMachineLut => new Information(0x167890, 0xA4); 
        public EnemyLineInformationLookupGfze01()
        {
            CourseNameAreas.Add(new CustomizableArea(CourseNamesEnglish.Address, CourseNamesEnglish.Size));
            CourseNameAreas.Add(new CustomizableArea(CourseNamesTranslations.Address, CourseNamesTranslations.Size));
        }
    }
}
