namespace GameCube.GFZ.GameData;

/// <summary>
///     DataBase of default game <see cref="BgmMusic"/>s.
/// </summary>
public static class BgmMusicDB
{
    public static readonly BgmMusic AdvertiseAX = new()
    {
        BgmIndex = BgmIndex.adv_ac,
        OfficialName = "Wings For My Way - ver.AX - (AX Advertise)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic AdvertiseGX = new()
    {
        BgmIndex = BgmIndex.adv_gc,
        OfficialName = "Wings For My Way (GX Advertise)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic NightOfBigBlue = new()
    {
        BgmIndex = BgmIndex.bblue,
        OfficialName = "Night Of Big Blue (Story #4)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic CardCheck = new()
    {
        BgmIndex = BgmIndex.cardcheck,
        OfficialName = "Raise a curtain (Card Check)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic CasinoPalace = new()
    {
        BgmIndex = BgmIndex.casino,
        BgmFinalLapIndex = BgmIndex.casino_b,
        BgmFinalLapLoopOffset = 0x0F00,
        OfficialName = "Shotgun Kiss (Vegas Palace)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic CoursePreviewGX = new()
    {
        BgmIndex = BgmIndex.course_view,
        OfficialName = "Long Pre-View (Course View 2)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic CoursePreviewAX = new()
    {
        BgmIndex = BgmIndex.course_view_ac,
        OfficialName = "Short Pre-View (Course View 1)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic Customize = new()
    {
        BgmIndex = BgmIndex.customize,
        OfficialName = "Feather (Customize)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic CosmoTerminal = new()
    {
        BgmIndex = BgmIndex.elev,
        BgmFinalLapIndex = BgmIndex.elev_b,
        BgmFinalLapLoopOffset = 0x0E00,
        OfficialName = "One Ahead System (Cosmo Terminal)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic FireField = new()
    {
        BgmIndex = BgmIndex.fire,
        BgmFinalLapIndex = BgmIndex.fire_b,
        BgmFinalLapLoopOffset = 0x0C00,
        OfficialName = "Feel Our Pain (Fire Field)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic GreenPlant = new()
    {
        BgmIndex = BgmIndex.forest,
        BgmFinalLapIndex = BgmIndex.forest_b,
        BgmFinalLapLoopOffset = 0x0900,
        OfficialName = "Planet Colors (Green Plant)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic Garage = new()
    {
        BgmIndex = BgmIndex.garage,
        OfficialName = "Your Garage (Shop)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic RaceFinish = new()
    {
        BgmIndex = BgmIndex.goal,
        OfficialName = "Finish to Go (Finish)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic WinningRunAX = new()
    {
        BgmIndex = BgmIndex.hyosho_ac,
        OfficialName = "CAPTAIN FALCON",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic InterviewIn = new()
    {
        BgmIndex = BgmIndex.interview,
        OfficialName = "F-ZERO TV Opening (Interview In)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic InterviewOut = new()
    {
        BgmIndex = BgmIndex.interview_out,
        OfficialName = "F-ZERO TV Ending (Interview Out)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic Lightning = new()
    {
        BgmIndex = BgmIndex.lightning,
        BgmFinalLapIndex = BgmIndex.lightning_b,
        BgmFinalLapLoopOffset = 0x0700,
        OfficialName = "Osc-Sync Carnival (Lightning)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic OuterSpace = new()
    {
        BgmIndex = BgmIndex.meteor,
        BgmFinalLapIndex = BgmIndex.meteor_b,
        BgmFinalLapLoopOffset = 0x0500,
        OfficialName = "Paper Engine (Outer Space)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic MissionClear = new()
    {
        BgmIndex = BgmIndex.missionclear,
        OfficialName = "2sec (Mission Clear)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic MuteCity = new()
    {
        BgmIndex = BgmIndex.mutecity,
        BgmFinalLapIndex = BgmIndex.mutecity_b,
        BgmFinalLapLoopOffset = 0x0600,
        OfficialName = "For The Glory -feat. Mute City's Theme- (Mute City)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic BigBlue = new()
    {
        BgmIndex = BgmIndex.ocean,
        BgmFinalLapIndex = BgmIndex.ocean_b,
        BgmFinalLapLoopOffset = 0x0D00,
        OfficialName = "Infinite Blue (Big Blue)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic FlagOpen = new()
    {
        BgmIndex = BgmIndex.openflag,
        OfficialName = "Flags (Flag Open)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic GameOver = new()
    {
        BgmIndex = BgmIndex.over,
        OfficialName = "No Time (Game Over)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic PilotPointResult = new()
    {
        BgmIndex = BgmIndex.point,
        OfficialName = "Stereo Signal (Pilot Point Result)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic CharactersProfile = new()
    {
        BgmIndex = BgmIndex.profile,
        OfficialName = "Refresh Time (Character's Profile)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic PortTown = new()
    {
        BgmIndex = BgmIndex.ptown,
        BgmFinalLapIndex = BgmIndex.ptown_b,
        BgmFinalLapLoopOffset = 0x0800,
        OfficialName = "Like a Snake (Port Town)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic PhantomRoad = new()
    {
        BgmIndex = BgmIndex.rainbow,
        BgmFinalLapIndex = BgmIndex.rainbow_b,
        BgmFinalLapLoopOffset = 0x0B00,
        OfficialName = "DIZZY (Phantom Road)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic RedCanyon = new()
    {
        BgmIndex = BgmIndex.redcanyon,
        OfficialName = "Cover Of Red Canyon's Theme (Story #2)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic Replay = new()
    {
        BgmIndex = BgmIndex.replay,
        OfficialName = "Brain Cleaner (Replay)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic Retire = new()
    {
        BgmIndex = BgmIndex.retire,
        OfficialName = "The Fall (Retire)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic StaffRoll = new()
    {
        BgmIndex = BgmIndex.roll,
        OfficialName = "Respect To \"RESULT THEME OF F-ZERO\" (Staff Roll)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic SandOcean = new()
    {
        BgmIndex = BgmIndex.sand,
        BgmFinalLapIndex = BgmIndex.sand_b,
        BgmFinalLapLoopOffset = 0x0A00,
        OfficialName = "8 Guitars (Sand Ocean)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic BigBlueSecret = new()
    {
        BgmIndex = BgmIndex.secret_bb,
        OfficialName = "Cover Of Big Blue's Theme (Item Song 2)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic MuteCitySecret = new()
    {
        BgmIndex = BgmIndex.secret_mc,
        OfficialName = "Cover Of Mute City's Theme (Item Song 1)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic MainSelector = new()
    {
        BgmIndex = BgmIndex.selector,
        OfficialName = "As you choose \"3rd\" (Main Selector)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic Selector = new()
    {
        BgmIndex = BgmIndex.selector_yobi,
        OfficialName = "As you choose \"3rd\" (Selector)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic Story6 = new()
    {
        BgmIndex = BgmIndex.story7,
        OfficialName = "Time For Kill (Story #6)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic Story8 = new()
    {
        BgmIndex = BgmIndex.story9,
        OfficialName = "Emperor Breath (Story #8)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic StoryStaffRoll = new()
    {
        BgmIndex = BgmIndex.storyend,
        OfficialName = "TODO",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic Aeropolis = new()
    {
        BgmIndex = BgmIndex.tower,
        BgmFinalLapIndex = BgmIndex.tower_b,
        BgmFinalLapLoopOffset = 0x0400,
        OfficialName = "ZEN (Aeropolis)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic Tutorial = new()
    {
        BgmIndex = BgmIndex.tutorial,
        OfficialName = "U-Rays (Tutorial)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic WinningRunGX = new()
    {
        BgmIndex = BgmIndex.winingrun,
        OfficialName = "Hurrah for the Champion (Winning Run)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic Unused = new()
    {
        BgmIndex = BgmIndex.metadata_unused,
        OfficialName = "(unused)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic Options = new()
    {
        BgmIndex = BgmIndex.yobi2,
        OfficialName = "Step 70's (Options)",
        Group = BgmGroup.Disc2_AdxBgm,
    };

    public static readonly BgmMusic Theme_AntonioGuster = new()
    {
        BgmIndex = BgmIndex.antonio_t,
        OfficialName = "ANTONIO GUSTER",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_TheSkull = new()
    {
        BgmIndex = BgmIndex.arbingordon_t,
        OfficialName = "THE SKULL",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_MrsArrow = new()
    {
        BgmIndex = BgmIndex.arrowm_t,
        OfficialName = "Mrs. ARROW",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_SuperArrow = new()
    {
        BgmIndex = BgmIndex.arrows_t,
        OfficialName = "SUPER ARROW",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_Baba = new()
    {
        BgmIndex = BgmIndex.baba_t,
        OfficialName = "BABA",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_Beastman = new()
    {
        BgmIndex = BgmIndex.beastman_t,
        OfficialName = "BEASTMAN",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_Billy = new()
    {
        BgmIndex = BgmIndex.billy_t,
        OfficialName = "Billy",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_BioRex = new()
    {
        BgmIndex = BgmIndex.biorex_t,
        OfficialName = "BIO REX",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_BlackShadow = new()
    {
        BgmIndex = BgmIndex.blackshadow_t,
        OfficialName = "BLACK SHADOW",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_BloodFalcon = new()
    {
        BgmIndex = BgmIndex.bloodfalcon_t,
        OfficialName = "BLOOD FALCON",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_CaptainFalcon = new()
    {
        BgmIndex = BgmIndex.captainfalcon_t,
        OfficialName = "CAPTAIN FALCON",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_DrClash = new()
    {
        BgmIndex = BgmIndex.clash_t,
        OfficialName = "Dr. CLASH",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_Daigoroh = new()
    {
        BgmIndex = BgmIndex.daigoroh_t,
        OfficialName = "DAIGOROH",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_DaiSanGen = new()
    {
        BgmIndex = BgmIndex.daisangen_t,
        OfficialName = "DAI SAN GEN",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_Deathborn = new()
    {
        BgmIndex = BgmIndex.deathbone_t,
        OfficialName = "DEATHBORN",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_DigiBoy = new()
    {
        BgmIndex = BgmIndex.digiboy_t,
        OfficialName = "DIGI-BOY",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_DonGenie = new()
    {
        BgmIndex = BgmIndex.don_t,
        OfficialName = "DON GENIE",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_Draq = new()
    {
        BgmIndex = BgmIndex.draq_t,
        OfficialName = "DRAQ",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_MrEAD = new()
    {
        BgmIndex = BgmIndex.ead_t,
        OfficialName = "Mr. EAD",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_MightyGazelle = new()
    {
        BgmIndex = BgmIndex.gazelle_t,
        OfficialName = "MIGHTY GAZELLE",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_GomarAndShioh = new()
    {
        BgmIndex = BgmIndex.gommer_t,
        OfficialName = "GOMAR & SHIOH",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_JackLevin = new()
    {
        BgmIndex = BgmIndex.jacklevin_t,
        OfficialName = "JACK LEVIN",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_JamesMcCloud = new()
    {
        BgmIndex = BgmIndex.jamesmcloud_t,
        OfficialName = "JAMES MCCLOUD",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_JodySummer = new()
    {
        BgmIndex = BgmIndex.jodysummer_t,
        OfficialName = "JODY SUMMER",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_KateAlen = new()
    {
        BgmIndex = BgmIndex.kate_t,
        OfficialName = "KATE ALEN",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_Leon = new()
    {
        BgmIndex = BgmIndex.leon_t,
        OfficialName = "LEON",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_LilyFlyer = new()
    {
        BgmIndex = BgmIndex.lily_t,
        OfficialName = "LILY FLYER",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_MichaelChain = new()
    {
        BgmIndex = BgmIndex.michaelchain_t,
        OfficialName = "MICHAEL CHAIN",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_Octoman = new()
    {
        BgmIndex = BgmIndex.octman_t,
        OfficialName = "OCTOMAN",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_Pico = new()
    {
        BgmIndex = BgmIndex.pico_t,
        OfficialName = "PICO",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_PJ = new()
    {
        BgmIndex = BgmIndex.pj_t,
        OfficialName = "PJ",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_PrinciaRamode = new()
    {
        BgmIndex = BgmIndex.prisia_t,
        OfficialName = "PRINCIA RAMODE",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_QQQ = new()
    {
        BgmIndex = BgmIndex.qqq_t,
        OfficialName = "QQQ",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_RogerBuster = new()
    {
        BgmIndex = BgmIndex.rogerbuster_t,
        OfficialName = "ROGER BUSTER",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_SamuraiGoroh = new()
    {
        BgmIndex = BgmIndex.samuraigoroh_t,
        OfficialName = "SAMURAI GOROH",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_Phoenix = new()
    {
        BgmIndex = BgmIndex.sharock_t, // Sherlock
        OfficialName = "PHOENIX",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_SilverNeelsen = new()
    {
        BgmIndex = BgmIndex.silverneelsen_t,
        OfficialName = "SILVER NEELSEN",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_Spade = new()
    {
        BgmIndex = BgmIndex.spade_t,
        OfficialName = "SPADE",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_DrStewart = new()
    {
        BgmIndex = BgmIndex.stewart_t,
        OfficialName = "Dr. STEWART",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_JohnTanaka = new()
    {
        BgmIndex = BgmIndex.tanaka_t,
        OfficialName = "JOHN TANAKA",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Theme_Zoda = new()
    {
        BgmIndex = BgmIndex.zoda_t,
        OfficialName = "ZODA",
        Group = BgmGroup.Disc1_CharBgm,
    };

    public static readonly BgmMusic Random = new()
    {
        BgmIndex = BgmIndex.metadata_random,
        OfficialName = "(random song)",
        Group = BgmGroup.None,
    };

    public static readonly BgmMusic[] AdxBgm =
    [
        AdvertiseAX,        // 0
        AdvertiseGX,        // 1
        NightOfBigBlue,     // 2
        CardCheck,          // 3
        CasinoPalace,       // 4, 5
        CoursePreviewGX,    // 6
        CoursePreviewAX,    // 7
        Customize,          // 8
        CosmoTerminal,      // 9, 10
        FireField,          // 11, 12
        GreenPlant,         // 13, 14
        Garage,             // 15
        RaceFinish,         // 16
        WinningRunAX,       // 17
        InterviewIn,        // 18
        InterviewOut,       // 19
        Lightning,          // 20, 21
        OuterSpace,         // 22, 23
        MissionClear,       // 24
        MuteCity,           // 25, 26
        BigBlue,            // 27, 28
        FlagOpen,           // 29
        GameOver,           // 30
        PilotPointResult,   // 31
        CharactersProfile,  // 32
        PortTown,           // 33, 34
        PhantomRoad,        // 35, 36
        RedCanyon,          // 37
        Replay,             // 38
        Retire,             // 39
        StaffRoll,          // 40
        SandOcean,          // 41, 42
        BigBlueSecret,      // 43
        MuteCitySecret,     // 44
        MainSelector,       // 45
        Selector,           // 46
        Story6,             // 47
        Story8,             // 48
        StoryStaffRoll,     // 49
        Aeropolis,          // 50, 51
        Tutorial,           // 52
        WinningRunGX,       // 53
        Unused,             // 54
        Options,            // 55
    ];

    public static readonly BgmMusic[] CharBgm =
    [
        Theme_AntonioGuster,// 56
        Theme_TheSkull,     // 57
        Theme_MrsArrow,     // 58
        Theme_SuperArrow,   // 59
        Theme_Baba,         // 60 -
        Theme_Beastman,     // 61
        Theme_Billy,        // 62
        Theme_BioRex,       // 63
        Theme_BlackShadow,  // 64
        Theme_BloodFalcon,  // 65
        Theme_CaptainFalcon,// 66
        Theme_DrClash,      // 67    
        Theme_Daigoroh,     // 68
        Theme_DaiSanGen,    // 69
        Theme_Deathborn,    // 70 -
        Theme_DigiBoy,      // 71
        Theme_DonGenie,     // 72
        Theme_Draq,         // 73
        Theme_MrEAD,        // 74
        Theme_MightyGazelle,// 75
        Theme_GomarAndShioh,// 76
        Theme_JackLevin,    // 77
        Theme_JamesMcCloud, // 78
        Theme_JodySummer,   // 79
        Theme_KateAlen,     // 80 -
        Theme_Leon,         // 81
        Theme_LilyFlyer,    // 82
        Theme_MichaelChain, // 83
        Theme_Octoman,      // 84
        Theme_Pico,         // 85
        Theme_PJ,           // 86
        Theme_PrinciaRamode,// 87
        Theme_QQQ,          // 88
        Theme_RogerBuster,  // 89
        Theme_SamuraiGoroh, // 90 -
        Theme_Phoenix,      // 91
        Theme_SilverNeelsen,// 92
        Theme_Spade,        // 93
        Theme_DrStewart,    // 94
        Theme_JohnTanaka,   // 95
        Theme_Zoda,         // 96
    ];

    public static bool IsBgmIndexInvalid(BgmIndex bgmIndex)
    {
        bool isInvalid =
            bgmIndex >= BgmIndex.metadata_invalid_id_start &&
            bgmIndex <= BgmIndex.metadata_invalid_id_end;
        return isInvalid;
    }

    public static bool IsBgmIndexInvalid(byte bgmIndex)
        => IsBgmIndexInvalid((BgmIndex)bgmIndex);

    public static void ThrowIfBgmIndexInvalid(BgmIndex bgmIndex)
    {
        if (IsBgmIndexInvalid(bgmIndex))
        {
            int min = (int)BgmIndex.adv_ac;
            int max = (int)BgmIndex.metadata_invalid_id_start - 1;
            int last = (int)BgmIndex.metadata_random;
            string msg = $"{nameof(BgmIndex)} must be between {min} and {max}, or be exactly {last}.";
            throw new System.ArgumentException(msg);
        }
    }

    public static void ThrowIfBgmIndexInvalid(byte bgmIndex)
        => ThrowIfBgmIndexInvalid((BgmIndex)bgmIndex);

    /// <summary>
    ///     Get the associated final lap BGM loop point offset
    ///     for <paramref name="bgmFinalLapIndex"/>.
    /// </summary>
    /// <param name="bgmFinalLapIndex">The BGM song index.</param>
    /// <returns>
    ///     The relevant 16-bit offset to the correct loop point data.
    /// </returns>
    public static ushort GetBgmLoopPointOffset(BgmIndex bgmFinalLapIndex)
    {
        return bgmFinalLapIndex switch
        {
            BgmIndex.tower_b     => 0x0400,// Aeropolis
            BgmIndex.meteor_b    => 0x0500,// Meteor Stream
            BgmIndex.mutecity_b  => 0x0600,// Mute City
            BgmIndex.lightning_b => 0x0700,// Lightning
            BgmIndex.ptown_b     => 0x0800,// Port Town
            BgmIndex.forest_b    => 0x0900,// Green Plant
            BgmIndex.sand_b      => 0x0A00,// Sand Ocean
            BgmIndex.rainbow_b   => 0x0B00,// Phantom Road
            BgmIndex.fire_b      => 0x0C00,// Fire Field
            BgmIndex.ocean_b     => 0x0D00,// Big Blue
            BgmIndex.elev_b      => 0x0E00,// Cosmo Terminal
            BgmIndex.casino_b    => 0x0F00,// Casino Palace
            BgmIndex.metadata_random => 0xFFFF,// No BGM
            _ => 0xFFFF,
        };
    }

    /// <summary>
    ///     Get the associated final lap BGM loop point offset
    ///     for <paramref name="bgmFinalLapIndex"/>.
    /// </summary>
    /// <param name="bgmFinalLapIndex"></param>
    /// <returns>
    ///     The relevant 16-bit offset to the correct loop point data.
    /// </returns>
    public static ushort GetBgmLoopPointOffset(byte bgmFinalLapIndex)
    {
        ushort value = GetBgmLoopPointOffset((BgmIndex)bgmFinalLapIndex);
        return value;
    }

}
