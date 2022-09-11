using Manifold;
using Manifold.IO;
using System.Collections.Generic;

namespace GameCube.GFZ.REL
{
    /// <summary>
    /// PAL
    /// </summary>
    public class EnemyLineInformationLookupGfzp01 : EnemyLineInformationLookup
    {
        public override string FileHashMD5 => "96398b677d77e2ae1592b695a4bebaca";
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
        public EnemyLineInformationLookupGfzp01()
        {
            CourseNameAreas.Add(new CustomizableArea(CourseNamesEnglish.Address, CourseNamesEnglish.Size));
            CourseNameAreas.Add(new CustomizableArea(ForbiddenWords.Address, ForbiddenWords.Size));
        }
    }
}
