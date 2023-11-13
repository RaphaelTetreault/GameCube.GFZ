using Manifold.IO;
using System.Collections.Generic;

namespace GameCube.GFZ.REL
{
    public abstract class EnemyLineDataBlocks
    {
        public virtual Endianness Endianness => Endianness.BigEndian;

        public abstract GameCode GameCode { get; }
        public abstract string SourceFile { get; }
        public abstract string WorkingFile { get; }
        public abstract string FileHashMD5 { get; }
        public abstract DataBlock VenueNames { get; }
        public abstract DataBlock SlotVenueDefinitions { get; }
        public abstract DataBlock CourseNamesEnglish { get; }
        public abstract DataBlock CourseNamesTranslations { get; }
        public abstract DataBlock CourseSlotDifficulty { get; }
        public abstract DataBlock CourseSlotBgm { get; }
        public abstract DataBlock CourseSlotBgmFinalLap { get; }
        public abstract DataBlock CupCourseLut { get; }
        public abstract DataBlock CupCourseLutAssets { get; }
        public abstract DataBlock CupCourseLutUnk { get; }
        public abstract DataBlock CourseNameOffsetStructs{ get; }
        public abstract DataBlock CourseMinimapParameterStructs { get; }
        public abstract DataBlock ForbiddenWords { get; }
        public abstract DataBlock AxModeCourseTimers { get; }
        public abstract int CourseNamePointerOffsetBase { get; }
        public abstract List<CustomizableArea> CourseNameAreas { get; }
        public abstract DataBlock PilotPositions { get; }
        public abstract DataBlock PilotToMachineLut { get; }
        public abstract short Salt { get; }
        public abstract int Key0 { get; }
        public abstract int Key1 { get; }
        public abstract int Key2 { get; }
        public abstract int BlockKey0 { get; }
        public abstract short BlockKey1 { get; }
        public abstract short BlockKey2 { get; }
    }
}
