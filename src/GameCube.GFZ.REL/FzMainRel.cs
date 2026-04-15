using Manifold.IO;

namespace GameCube.GFZ.REL;

/// <summary>
///     Defines a number of useful locations and areas for patching fz*.main.rel
/// </summary>
public readonly record struct FzMainRel
{
    public FzMainRel()
    {
    }

    /// <summary>
    ///     The game code associated with this file.
    /// </summary>
    public GameCode GameCode { get; init; }

    /// <summary>
    ///     File endianness.
    /// </summary>
    public static Endianness Endianness => FzMainCrypter.Endianness;

    /// <summary>
    ///     
    /// </summary>
    public string SourceFile { get; init; } = string.Empty;

    /// <summary>
    ///     
    /// </summary>
    public string WorkingFile { get; init; } = string.Empty;

    /// <summary>
    ///     TODO: File hash of the archive or decompressed file?
    ///     Probably the former.
    /// </summary>
    public string FileHashMD5 { get; init; } = string.Empty;

    /// <summary>
    ///     Base address of all strings in table. Other offsets defined
    ///     are relative to this address.
    /// </summary>
    public Pointer StringTableBaseAddress { get; init; }


    /// <summary>
    ///     Address for venue names offsets relative to <see cref="StringTableBaseAddress"/>.
    ///     Logically combines both <see cref="VenueNamesEnglishOffsets"/> and
    ///     <see cref="VenueNamesJapaneseOffsets"/> as one contiguous array.
    /// </summary>
    public ArrayPointer32 VenueNameOffsets { get; init; }

    /// <summary>
    ///     Address for english venue names offsets relative to <see cref="StringTableBaseAddress"/>.
    /// </summary>
    public ArrayPointer32 VenueNamesEnglishOffsets { get; init; }

    /// <summary>
    ///     Address for english venue names offsets relative to <see cref="StringTableBaseAddress"/>.
    /// </summary>
    public ArrayPointer32 VenueNamesJapaneseOffsets { get; init; }
    
    /// <summary>
    ///     Address and byte-length for english venue names.
    /// </summary>
    public DataBlock VenueNamesEnglish { get; init; }

    /// <summary>
    ///     Address and byte-length for japanese venue names.
    /// </summary>
    public DataBlock VenueNamesJapanese { get; init; }


    /// <summary>
    ///     How many langagues are defined in the translation tables.
    /// </summary>
    public int CourseNameLanguages { get; init; } // done

    /// <summary>
    ///     Address for course name offsets relative to <see cref="StringTableBaseAddress"/>.
    /// </summary>
    public ArrayPointer32 CourseNameOffsets { get; init; }

    /// <summary>
    ///     Data for course names.
    /// </summary>
    /// <remarks>
    ///     Japanese game uses English names.
    /// </remarks>
    public DataBlock CourseNamesEnglish { get; init; }

    /// <summary>
    ///     Data for course name localizations (non-English european languages).
    /// </summary>
    /// <remarks>
    ///     AX, E, J order: GER, FRE, SPA, ITA, JPN (interleaved).
    ///     P order: JPN (only).
    /// </remarks>
    public DataBlock CourseNamesLocalizations { get; init; }

    /// <summary>
    ///     Where non-custom "cardata" states are stored used explicitedly
    ///     for the display values on the machine select screen.
    /// </summary>
    public Pointer CarDataMachinesPtr { get; init; }

    /// <summary>
    ///     The letter ratings for machines. Eg: EAD, ACB, etc.
    /// </summary>
    public Pointer MachineLetterRatingsPtr { get; init; }

    /// <summary>
    ///     Address of the max speed float constant. When vehicles stay above this
    ///     value for more than 1 frame, speed is set to 0. Value is stored as a double.
    /// </summary>
    public Pointer VehicleMaxSpeedCap9990KmhPtr { get; init; }

    /// <summary>
    ///     Index which correlates stage index to venue.
    /// </summary>
    public DataBlock CourseVenueIndex { get; init; }         // done

    /// <summary>
    ///     Dificulty rating byte for each stage index.
    /// </summary>
    public DataBlock CourseDifficulty { get; init; }         // done

    /// <summary>
    ///     Index which correlates stage index to BGM.
    /// </summary>
    public DataBlock CourseBgmIndex { get; init; }           // done

    /// <summary>
    ///     Index which correlates stage index to final lap BGM.
    /// </summary>
    public DataBlock CourseBgmFinalLapIndex { get; init; }   // done

    /// <summary>
    ///     Look-Up-Table which maps the 6 cup entries to stages indexes
    ///     for the purpose of loading the COLI_COURSE## file.
    /// </summary>
    public DataBlock CupCourseLut { get; init; }             // TODO: index in cup

    /// <summary>
    ///     Look-Up-Table which maps the 6 cup entries to stages indexes
    ///     for the purpose of loading in the GMA and TPL assets.
    /// </summary>
    public DataBlock CupCourseLutAssets { get; init; }       // TODO: gma/tpl loading index

    /// <summary>
    ///     Look-Up-Table which maps the 6 cup entries to stages indexes.
    ///     Purpose unknown.
    /// </summary>
    public DataBlock CupCourseLutUnk { get; init; }          // TODO: unknown, but related

    /// <summary>
    ///     Stage minimap projections.
    /// </summary>
    public DataBlock CourseMinimapParameterStructs { get; init; }

    /// <summary>
    ///     List of banned/censored words.
    /// </summary>
    /// <remarks>
    ///     Use to invalidate values during name entry.
    /// </remarks>
    public DataBlock ForbiddenWords { get; init; }

    /// <summary>
    ///     List of banned/censored words.
    /// </summary>
    public DataBlock AxModeCourseTimers { get; init; }

    /// <summary>
    ///     Positions for pilot seating in their vehicle.
    /// </summary>
    public DataBlock PilotPositions { get; init; }

    /// <summary>
    ///     Map which translates pilot index into machine index.
    /// </summary>
    public DataBlock PilotToMachineLut { get; init; }


    public FzMainCrypter Crypter { get; init; }
}
