using Manifold.IO;

namespace GameCube.GFZ.REL;

/// <summary>
///     DataBase for <see cref="FzMainRel"/>
/// </summary>
public static class FzMainRelDB
{
    /// <summary>
    ///     Get <see cref="FzMainRel"/> data related to <paramref name="gameCode"/>.
    /// </summary>
    /// <param name="gameCode"></param>
    /// <returns>
    ///     <see cref="FzMainRel"/> data related to <paramref name="gameCode"/>.
    /// </returns>
    /// <exception cref="System.ArgumentException">
    ///     Raised if unable to map <paramref name="gameCode"/>.
    /// </exception>
    public static FzMainRel Get(GameCode gameCode)
    {
        return gameCode switch
        {
            GameCode.GFZJ01 => GFZJ01,
            GameCode.GFZE01 => GFZE01,
            GameCode.GFZP01 => GFZP01,
            GameCode.GFZJ8P or
            GameCode.GGGE6E => GGGE6E,
            _ => throw new System.ArgumentException($"Invalid game code {gameCode}"),
        };
    }

    /// <summary>
    ///     JP / Japanese F-Zero GX
    /// </summary>
    public static readonly FzMainRel GFZJ01 = new()
    {
        GameCode = GameCode.GFZE01,
        SourceFile = "enemy_line/line__.bin",
        WorkingFile = "enemy_line/line__.rel",
        FileHashMD5 = "f8947b6cec19af95f96fb9d11670ebdd",
        StringTableBaseAddress = 0x16A180,
        Crypter = FzMainCrypterDB.Japanese,

        // VENUE NAMES
        VenueNameOffsets = new(0x1F1C2C, 42), // ENG followed by JPN
        VenueNamesEnglishOffsets = new(0x1F1C2C, 22),
        VenueNamesJapaneseOffsets = new(0x1F1CDC, 20),
        VenueNamesEnglish = new(0x194A60, 0xA4),
        VenueNamesJapanese = new(0x194B5C, 0xD8),

        // COURSE NAMES
        CourseNameLanguages = 6,
        CourseNameLocalizationsStartIndex = 1,
        CourseNameOffsets = new(0x1F286C, 666), // 111 * 6
        CourseNamesEnglish = new(0x19523C, 0x15C), // English strings
        CourseNamesLocalizations = new(0x19555C, 0x8D8), // Other localization strings

        // CARDATA
        CarDataMachinesPtr = 0x192140,
        MachineLetterRatingsPtr = 0x1AA3F8,
        VehicleMaxSpeedCap9990KmhPtr = 0x15d040,

        CourseVenueIndex = new(0x1951B4, 111),
        CourseDifficulty = new(0x165460, 111),
        CourseBgmIndex = new(0x1607B4, 56),
        CourseBgmFinalLapIndex = new(0x1607EC, 184),
        CupCourseLut = new(0x164548, 0x84),
        CupCourseLutAssets = new(0x1645CC, 0x84),
        CupCourseLutUnk = new(0x164650, 0x84),
        CourseMinimapParameterStructs = new(0x188098, 0x508),
        ForbiddenWords = new(0x1ABA60, 0x3E0),
        AxModeCourseTimers = new(0x1A9390, 6),
        PilotPositions = new(0x19E49C, 0x210),
        PilotToMachineLut = new(0x164498, 0xA4),
    };

    /// <summary>
    ///     NA / English F-Zero GX
    /// </summary>
    public static readonly FzMainRel GFZE01 = new()
    {
        GameCode = GameCode.GFZE01,
        SourceFile = "enemy_line/line__.bin",
        WorkingFile = "enemy_line/line__.rel",
        FileHashMD5 = "a1790e38cbe17510017689088eab5758",
        StringTableBaseAddress = 0x16D600,
        Crypter = FzMainCrypterDB.Latin,

        VenueNameOffsets = new(0x1F71DC, 42),
        VenueNamesEnglishOffsets = new(0x1F71DC, 22), // Strings at: 197F80
        VenueNamesJapaneseOffsets = new(0x1F728C, 20), // Strings at: 19807C
        VenueNamesEnglish = new(0x197F80, 0xA4),
        VenueNamesJapanese = new(0x19807C, 0xD8),

        CourseNameLanguages = 6, // ENG, GER, FRE, SPA, ITA, JPN
        CourseNameLocalizationsStartIndex = 1,
        CourseNameOffsets = new(0x1F7E1C, 666),
        CourseNamesEnglish = new(0x19875C, 0x15C),
        CourseNamesLocalizations = new(0x198A7C, 0x8D8),

        CarDataMachinesPtr = 0x195660,
        MachineLetterRatingsPtr = 0x1AECB8,
        VehicleMaxSpeedCap9990KmhPtr = 0x160230,

        CourseVenueIndex = new(0x1986D4, 111),
        CourseDifficulty = new(0x168958, 111),
        CourseBgmIndex = new(0x163A8C, 56),
        CourseBgmFinalLapIndex = new(0x163AC4, 184),
        CupCourseLut = new(0x167940, 0x84),
        CupCourseLutAssets = new(0x1679C4, 0x84),
        CupCourseLutUnk = new(0x167A48, 0x84),
        CourseMinimapParameterStructs = new(0x18B5B0, 0x508),
        ForbiddenWords = new(0x1B0630, 0x3E0),
        AxModeCourseTimers = new(0x1ADBC0, 6),
        PilotPositions = new(0x1A19C4, 0x210),
        PilotToMachineLut = new(0x167890, 0xA4),
    };

    /// <summary>
    ///     PAL / European F-Zero GX
    /// </summary>
    public static readonly FzMainRel GFZP01 = new()
    {
        GameCode = GameCode.GFZP01,
        SourceFile = "enemy_line/line__.bin",
        WorkingFile = "enemy_line/line__.rel",
        FileHashMD5 = "96398b677d77e2ae1592b695a4bebaca",
        StringTableBaseAddress = 0x16E5A0,
        Crypter = FzMainCrypterDB.Latin,

        VenueNameOffsets = new(0x201664, 42),
        VenueNamesEnglishOffsets = new(0x201664, 22), // Strings at: 199940
        VenueNamesJapaneseOffsets = new(0x201714, 20), // Strings at: 199A3C
        VenueNamesEnglish = new(0x199940, 0xA0),
        VenueNamesJapanese = new(0x199A3C, 0xD8),

        CourseNameLanguages = 6,
        CourseNameLocalizationsStartIndex = 111,
        //CourseNameOffsets = new(0x201F34, 666), // 111 English in linear order, then 111 * 6 cycling ENG, GER, FRE, SPA, ITA, JPN
        //CourseNameOffsets = new(0x201F34 + 0x08 * 111, 666), // 111 English in linear order, then 111 * 6 cycling ENG, GER, FRE, SPA, ITA, JPN
        CourseNameOffsets = new(0x201F34, 777), // 111 English in linear order, then 111 * 6 cycling ENG, GER, FRE, SPA, ITA, JPN
        CourseNamesEnglish = new(0x19A11C, 0x15C),
        CourseNamesLocalizations = new(0x19A434, 0x1E8), // Japanese only (no GER, FRE, SPA, ITA)

        CarDataMachinesPtr = 0x195648,
        MachineLetterRatingsPtr = 0x1B8E20,
        VehicleMaxSpeedCap9990KmhPtr = 0x160e10,

        CourseVenueIndex = new(0x19A094, 111),
        CourseDifficulty = new(0x1698EC, 111),
        CourseBgmIndex = new(0x16495C, 56),
        CourseBgmFinalLapIndex = new(0x164994, 184),
        CupCourseLut = new(0x1688B0, 0x84),
        CupCourseLutAssets = new(0x168934, 0x84),
        CupCourseLutUnk = new(0x1689B8, 0x84),
        CourseMinimapParameterStructs = new(0x18CF50, 0x508),
        ForbiddenWords = new(0x1BB83C, 0x3E0),
        AxModeCourseTimers = new(0x1B7810, 6),
        PilotPositions = new(0x1A38F4, 0x210),
        PilotToMachineLut = new(0x168800, 0xA4),
    };

    /// <summary>
    ///     F-Zero AX
    /// </summary>
    public static readonly FzMainRel GGGE6E = new()
    {
        GameCode = GameCode.GGGE6E,
        SourceFile = "../sys/main.dol", //...?
        WorkingFile = "../sys/main.dol",
        FileHashMD5 = null, // Not computed
        StringTableBaseAddress = Pointer.Null, // Nothing comfirmed yet
        Crypter = FzMainCrypterDB.None, // TODO: make sure this doesn't affect anything if *crypting.

        VenueNameOffsets = ArrayPointer32.Null, // Nothing comfirmed yet
        VenueNamesEnglishOffsets = ArrayPointer32.Null, // Nothing comfirmed yet
        VenueNamesJapaneseOffsets = ArrayPointer32.Null, // Nothing comfirmed yet
        VenueNamesEnglish = new(0x21AE54, 0x8C),
        VenueNamesJapanese = DataBlock.Null, // Nothing comfirmed yet

        CourseNameLanguages = -1, // Nothing comfirmed yet
        CourseNameOffsets = new(0x201F38, 666),
        CourseNamesEnglish = new(0x21B474, 0x140),
        CourseNamesLocalizations = new(0x21B770, 0x8D8),

        CarDataMachinesPtr = Pointer.Null, // Nothing comfirmed yet
        MachineLetterRatingsPtr = Pointer.Null, // Nothing comfirmed yet
        VehicleMaxSpeedCap9990KmhPtr = 0x33ab20,

        CourseVenueIndex = new(0x21B3EC, 111),
        CourseDifficulty = DataBlock.Null, // Nothing comfirmed yet
        CourseBgmIndex = new(0x20E3F0, 56),
        CourseBgmFinalLapIndex = new(0x20E484, 184),
        CupCourseLut = new(0x20FB64, 0x84),
        CupCourseLutAssets = new(0x20FBE8, 0x84),
        CupCourseLutUnk = new(0x20FC6C, 0x84),
        CourseMinimapParameterStructs = new(0, 0x508),
        ForbiddenWords = DataBlock.Null, // Nothing comfirmed yet
        AxModeCourseTimers = new(0x3390C8, 6),
        PilotPositions = new(0x230004, 0x210),
        PilotToMachineLut = new(0x20FAC0, 0xA4),
    };
}
