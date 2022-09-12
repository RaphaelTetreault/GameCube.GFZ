using Manifold;
using Manifold.IO;
using System.Collections.Generic;

namespace GameCube.GFZ.REL
{
    public abstract class EnemyLineInformationLookup
    {
        /// <summary>
        /// This comment is inherited :)
        /// </summary>
        public abstract string FileHashMD5 { get; }
        public abstract Information VenueNames { get; }
        public abstract Information SlotVenueDefinitions { get; }
        public abstract Information CourseNamesEnglish { get; }
        public abstract Information CourseNamesTranslations { get; }
        public abstract Information CourseSlotDifficulty { get; }
        public abstract Information CourseSlotBgm { get; }
        public abstract Information CourseSlotBgmFinalLap { get; }
        public abstract Information CupCourseLut { get; }
        public abstract Information CupCourseLutAssets { get; }
        public abstract Information CupCourseLutUnk { get; }
        public abstract Information CourseNameOffsetStructs{ get; }
        public abstract Information CourseMinimapParameterStructs { get; }
        public abstract Information ForbiddenWords { get; }
        public abstract Information AxModeCourseTimers { get; }
        public abstract int CourseNamePointerOffsetBase { get; }
        public abstract List<CustomizableArea> CourseNameAreas { get; }
        public abstract Information PilotPositions { get; }
        public abstract Information PilotToMachineLut { get; }
        //public EnemyLineInformationLookup(){}
    }
}
