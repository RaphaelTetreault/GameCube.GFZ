using System.Collections.Generic;

namespace GameCube.GFZ.REL
{
    /// <summary>
    /// AX
    /// </summary>
    public class MainDolInformationLookupGfzj8p : EnemyLineInformationLookup
    {
        public MainDolInformationLookupGfzj8p()
        {
            CourseNameAreas.Add(new CustomizableArea(CourseNamesEnglish.Address, CourseNamesEnglish.Size));
            CourseNameAreas.Add(new CustomizableArea(CourseNamesTranslations.Address, CourseNamesTranslations.Size));
        }

        // TODO: const for file hash

        public override EnemyLineInformation.GameCode GameCode => EnemyLineInformation.GameCode.AX;
        public override string SourceFile => "../sys/main.dol"; //...?
        public override string WorkingFile => "../sys/main.dol";
        public override string FileHashMD5 => throw new System.NotImplementedException();
        public override Information VenueNames => new Information(0x21AE54, 0x8C);
        public override Information SlotVenueDefinitions => new Information(0x21B3EC, 111);
        public override Information CourseNamesEnglish => new Information(0x21B474, 0x140);
        public override Information CourseNamesTranslations => new Information(0x21B770, 0x8D8);
        public override Information CourseSlotDifficulty => throw new System.NotImplementedException("This is absent from the AX version");
        public override Information CourseSlotBgm => new Information(0x20E3F0, 56);
        public override Information CourseSlotBgmFinalLap => new Information(0x20E484, 184);
        public override Information CupCourseLut => new Information(0x20FB64, 0x84);
        public override Information CupCourseLutAssets => new Information(0x20FBE8, 0x84);
        public override Information CupCourseLutUnk => new Information(0x20FC6C, 0x84);
        public override Information CourseNameOffsetStructs => throw new System.NotImplementedException("This is absent from the AX version");
        public override Information CourseMinimapParameterStructs => new Information(0, 0x508);
        public override Information ForbiddenWords => throw new System.NotImplementedException("This is absent from the AX version");
        public override Information AxModeCourseTimers => new Information(0x3390C8, 6);
        public override int CourseNamePointerOffsetBase => 0;
        public override List<CustomizableArea> CourseNameAreas => new List<CustomizableArea>();
        public override Information PilotPositions => new Information(0x230004, 0x210);
        public override Information PilotToMachineLut => new Information(0x20FAC0, 0xA4);

        public override short Salt => throw new System.NotImplementedException();
        public override int Key0 => throw new System.NotImplementedException();
        public override int Key1 => throw new System.NotImplementedException();
        public override int Key2 => throw new System.NotImplementedException();
        public override int BlockKey0 => throw new System.NotImplementedException();
        public override short BlockKey1 => throw new System.NotImplementedException();
        public override short BlockKey2 => throw new System.NotImplementedException();
    }
}
