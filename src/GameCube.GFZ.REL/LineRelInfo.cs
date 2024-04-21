using Manifold.IO;

namespace GameCube.GFZ.LineREL
{
    public abstract class LineRelInfo
    {
        public virtual Endianness Endianness => Endianness.BigEndian;

        public abstract GameCode GameCode { get; }
        public abstract System.Text.Encoding TextEncoding { get; } 
        public abstract string SourceFile { get; }
        public abstract string WorkingFile { get; }
        public abstract string FileHashMD5 { get; }
        public abstract Pointer StringTableBaseAddress { get; }

        // TODO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public abstract ArrayPointer32 VenueNameOffsets { get; }
        public abstract ArrayPointer32 VenueNamesEnglishOffsets { get; }
        public abstract ArrayPointer32 VenueNamesJapaneseOffsets { get; }
        public abstract DataBlock VenueNamesEnglish { get; }
        public abstract DataBlock VenueNamesJapanese { get; }

        // Done
        public abstract int CourseNameLanguages { get; }
        public abstract ArrayPointer32 CourseNameOffsets { get; }
        public abstract DataBlock CourseNamesEnglish { get; }
        public abstract DataBlock CourseNamesLocalizations { get; }

        public abstract Pointer CarDataMachinesPtr { get; }
        /// <summary>
        ///     The letter ratings for machines. Eg: EAD, ACB, etc.
        /// </summary>
        public abstract Pointer MachineLetterRatingsPtr { get; }

        public abstract DataBlock CourseVenueIndex { get; }         // done
        public abstract DataBlock CourseDifficulty { get; }         // done
        public abstract DataBlock CourseBgmIndex { get; }           // done
        public abstract DataBlock CourseBgmFinalLapIndex { get; }   // done
        public abstract DataBlock CupCourseLut { get; }             // TODO: index in cup
        public abstract DataBlock CupCourseLutAssets { get; }       // TODO: gma/tpl loading index
        public abstract DataBlock CupCourseLutUnk { get; }          // TODO: unknown, but related
        public abstract DataBlock CourseMinimapParameterStructs { get; } // for editor
        public abstract DataBlock ForbiddenWords { get; }           // 
        public abstract DataBlock AxModeCourseTimers { get; }       // 
        public abstract DataBlock PilotPositions { get; }           // 
        public abstract DataBlock PilotToMachineLut { get; }        //

        // internal only
        public abstract short Salt { get; }
        public abstract int Key0 { get; }
        public abstract int Key1 { get; }
        public abstract int Key2 { get; }
        public abstract int BlockKey0 { get; }
        public abstract short BlockKey1 { get; }
        public abstract short BlockKey2 { get; }
    }
}
