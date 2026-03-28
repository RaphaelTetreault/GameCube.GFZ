using GameCube.GFZ.GameData;
using Manifold;
using Manifold.IO;
using System.IO;
using System.Text.RegularExpressions;

namespace GameCube.GFZ.Stage;

/// <summary>
///     File wrapper for <see cref="Scene"/>.
/// </summary>
public class SceneFile : BinaryFileWrapper<Scene>
{
    // CONSTANTS
    public const Endianness endianness = Endianness.BigEndian;
    /// <summary>
    ///     The file extension for Scene (COLI_COURSE##). There is none.
    /// </summary>
    public const string fileExtension = "";


    // PROPERTIES
    public override Endianness Endianness { get; set; } = endianness;
    public override string FileExtension { get; set; } = fileExtension;
    public override string FileName { get; set; } = string.Empty;

    /// <summary>
    ///     The course's author.
    /// </summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>
    ///     The course's name.
    /// </summary>
    public string CourseName { get; set; } = string.Empty;

    /// <summary>
    ///     How large the file is in bytes.
    /// </summary>
    public int FileSize { get; private set; }

    public string FileFormatDescription => Value.IsFormatGX ? "GX" : "AX";

    /// <summary>
    ///     The course index as indicated by the file name COLI_COURSE## where ## is the index.
    /// </summary>
    public int CourseIndex { get; set; }

    public string CourseDescription => ((CourseIndexAX)CourseIndex).GetDescription();

    /// <summary>
    ///     The venue for this course.
    /// </summary>
    public VenueID Venue { get; set; }

    public string VenueDescription => CourseDatabase.GetDefaultVenueID(CourseIndex).GetDescription();

    /// <summary>
    ///     Gets the venue's name
    /// </summary>
    public string VenueName => EnumExtensions.GetDescription(Venue);


    // CONSTRUCTORS
    public SceneFile() : base() { }
    public SceneFile(string inputPath) : base(inputPath)
    {
        // CAPTURE METADATA
        using var reader = new EndianBinaryReader(File.OpenRead(inputPath), Endianness);
        FileSize = (int)reader.BaseStream.Length;

        bool hasFileName = !string.IsNullOrWhiteSpace(FileName);
        if (hasFileName)
        {
            // Store the stage index, can solve venue and course name from this using hashes
            var matchDigits = Regex.Match(FileName, ConstRegex.MatchIntegers);
            CourseIndex = int.Parse(matchDigits.Value);
        }

        // TODO: use file hash + DB instead of hardcoded guesses.
        Venue = CourseDatabase.GetDefaultVenueID(CourseIndex);
        CourseName = CourseDatabase.GetDefaultCourseNameAX(CourseIndex);
        //Author = "Amusement Vision";
    }


}