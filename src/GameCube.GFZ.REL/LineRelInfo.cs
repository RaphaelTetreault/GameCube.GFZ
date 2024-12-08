using Manifold.IO;

namespace GameCube.GFZ.LineREL
{
    public abstract class LineRelInfo
    {
        /// <summary>
        ///     File endianness.
        /// </summary>
        public virtual Endianness Endianness => Endianness.BigEndian;

        /// <summary>
        ///     The game code associated with this file.
        /// </summary>
        public abstract GameCode GameCode { get; }

        /// <summary>
        ///     String encoding. Either ASCII or Shift-JIS.
        /// </summary>
        /// <remarks>
        ///     ASCII should probably should be Windows 1132.
        /// </remarks>
        public abstract System.Text.Encoding TextEncoding { get; } 

        /// <summary>
        ///     
        /// </summary>
        public abstract string SourceFile { get; }

        /// <summary>
        ///     
        /// </summary>
        public abstract string WorkingFile { get; }

        /// <summary>
        ///     TODO: File hash of the archive or decompressed file?
        ///     Probably the former.
        /// </summary>
        public abstract string FileHashMD5 { get; }

        /// <summary>
        ///     Base address of all strings in table. Other offsets defined
        ///     are relative to this address.
        /// </summary>
        public abstract Pointer StringTableBaseAddress { get; }

        // TODO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        /// <summary>
        ///     Address for venue names offsets relative to <see cref="StringTableBaseAddress"/>.
        ///     Logically combines both <see cref="VenueNamesEnglishOffsets"/> and
        ///     <see cref="VenueNamesJapaneseOffsets"/> as one contiguous array.
        /// </summary>
        public abstract ArrayPointer32 VenueNameOffsets { get; }

        /// <summary>
        ///     Address for english venue names offsets relative to <see cref="StringTableBaseAddress"/>.
        /// </summary>
        public abstract ArrayPointer32 VenueNamesEnglishOffsets { get; }

        /// <summary>
        ///     Address for english venue names offsets relative to <see cref="StringTableBaseAddress"/>.
        /// </summary>
        public abstract ArrayPointer32 VenueNamesJapaneseOffsets { get; }
        
        /// <summary>
        ///     Address and byte-length for english venue names.
        /// </summary>
        public abstract DataBlock VenueNamesEnglish { get; }

        /// <summary>
        ///     Address and byte-length for japanese venue names.
        /// </summary>
        public abstract DataBlock VenueNamesJapanese { get; }


        /// <summary>
        ///     How many langagues are defined in the translation tables.
        /// </summary>
        public abstract int CourseNameLanguages { get; } // done

        /// <summary>
        ///     Address for course name offsets relative to <see cref="StringTableBaseAddress"/>.
        /// </summary>
        public abstract ArrayPointer32 CourseNameOffsets { get; }

        /// <summary>
        ///     Data for course names.
        /// </summary>
        /// <remarks>
        ///     Japanese game uses English names.
        /// </remarks>
        public abstract DataBlock CourseNamesEnglish { get; }

        /// <summary>
        ///     Data for course name localizations (non-English european languages).
        /// </summary>
        public abstract DataBlock CourseNamesLocalizations { get; }

        /// <summary>
        ///     Where non-custom "cardata" states are stored used explicitedly
        ///     for the display values on the machine select screen.
        /// </summary>
        public abstract Pointer CarDataMachinesPtr { get; }

        /// <summary>
        ///     The letter ratings for machines. Eg: EAD, ACB, etc.
        /// </summary>
        public abstract Pointer MachineLetterRatingsPtr { get; }

        /// <summary>
        ///     Address of the max speed float constant. When vehicles stay above this
        ///     value for more than 1 frame, speed is set to 0. Value is stored as a double.
        /// </summary>
        public abstract Pointer VehicleMaxSpeedCap9990KmhPtr { get; }

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
