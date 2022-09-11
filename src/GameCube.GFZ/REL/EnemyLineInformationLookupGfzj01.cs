using Manifold;
using Manifold.IO;
using System.Collections.Generic;

namespace GameCube.GFZ.REL
{
    /// <summary>
    /// NTSC J
    /// </summary>
    public class EnemyLineInformationLookupGfzj01 : EnemyLineInformationLookup
    {
        public override string FileHashMD5 => "f8947b6cec19af95f96fb9d11670ebdd";
        public override Information VenueNames => new Information(0x194A60, 0xA0);
        public override Information SlotVenueDefinitions => new Information(0x1951B4, 111);
        public override Information CourseNamesEnglish => new Information(0x19523C, 0x15C);
        public override Information CourseNamesTranslations => new Information(0x19555C, 0x8D8);
        public override Information CourseSlotDifficulty => new Information(0x165460, 111);
        public override Information CourseSlotBgm => new Information(0x1607B4, 56);
        public override Information CourseSlotBgmFinalLap => new Information(0x1607EC, 184);
        public override Information CupCourseLut => new Information(0x164548, 0x84);
        public override Information CupCourseLutAssets => new Information(0x1645CC, 0x84);
        public override Information CupCourseLutUnk => new Information(0x164650, 0x84);
        public override Information CourseNameOffsetStructs => new Information(0x1F7E20, 0x14D0);
        public override Information CourseMinimapParameterStructs => new Information(0x188098, 0x508);
        public override Information ForbiddenWords => new Information(0x1ABA60, 0x3E0);
        public override Information AxModeCourseTimers => new Information(0x1A9390, 6);
        public override int CourseNamePointerOffsetBase => 0x16A180;
        public override List<CustomizableArea> CourseNameAreas => new List<CustomizableArea>();
        public EnemyLineInformationLookupGfzj01()
        {
            CourseNameAreas.Add(new CustomizableArea(CourseNamesEnglish.Address, CourseNamesEnglish.Size));
            CourseNameAreas.Add(new CustomizableArea(CourseNamesTranslations.Address, CourseNamesTranslations.Size));
        }
    }
}
