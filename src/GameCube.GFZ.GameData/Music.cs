using System;
using System.Collections.Generic;
using System.Text;

namespace GameCube.GFZ.GameData;

public readonly record struct Music()
{
    public required BgmIndex BgmIndex { get; init; }
    public BgmIndex BgmFinalLapIndex { get; init; } = BgmIndex.random; // I don't like this.
    public ushort BgmFinalLapLoopOffset { get; init; } = 0xFFFF;
    public required string OfficialName { get; init; }

    public string FileName => BgmIndex.ToString();
}

public static class MusicDB
{
    public static readonly Music AdvertiseAX = new()
    {
        BgmIndex = BgmIndex.adv_ac,
        OfficialName = "Wings For My Way - ver.AX - (AX Advertise)",
    };

    public static readonly Music AdvertiseGX = new()
    {
        BgmIndex = BgmIndex.adv_gc,
        OfficialName = "Wings For My Way (GX Advertise)",
    };

    public static readonly Music NightOfBigBlue = new()
    {
        BgmIndex = BgmIndex.bblue,
        OfficialName = "Night Of Big Blue (Story #4)",
    };

    public static readonly Music CardCheck = new()
    {
        BgmIndex = BgmIndex.cardcheck,
        OfficialName = "Raise a curtain (Card Check)",
    };

    public static readonly Music CasinoPalace = new()
    {
        BgmIndex = BgmIndex.casino,
        BgmFinalLapIndex = BgmIndex.casino_b,
        BgmFinalLapLoopOffset = 0x0F00,
        OfficialName = "Shotgun Kiss (Vegas Palace)",
    };

    public static readonly Music CoursePreviewGX = new()
    {
        BgmIndex = BgmIndex.course_view,
        OfficialName = "Long Pre-View (Course View 2)",
    };

    public static readonly Music CoursePreviewAX = new()
    {
        BgmIndex = BgmIndex.course_view_ac,
        OfficialName = "Short Pre-View (Course View 1)",
    };

    public static readonly Music Customize = new()
    {
        BgmIndex = BgmIndex.customize,
        OfficialName = "Feather (Customize)",
    };

    public static readonly Music CosmoTerminal = new()
    {
        BgmIndex = BgmIndex.elev,
        BgmFinalLapIndex = BgmIndex.elev_b,
        BgmFinalLapLoopOffset = 0x0E00,
        OfficialName = "One Ahead System (Cosmo Terminal)",
    };

    public static readonly Music FireField = new()
    {
        BgmIndex = BgmIndex.fire,
        BgmFinalLapIndex = BgmIndex.fire_b,
        BgmFinalLapLoopOffset = 0x0C00,
        OfficialName = "Feel Our Pain (Fire Field)",
    };

    public static readonly Music GreenPlant = new()
    {
        BgmIndex = BgmIndex.forest,
        BgmFinalLapIndex = BgmIndex.forest_b,
        BgmFinalLapLoopOffset = 0x0900,
        OfficialName = "Planet Colors (Green Plant)",
    };

    public static readonly Music Garage = new()
    {
        BgmIndex = BgmIndex.garage,
        OfficialName = "Your Garage (Shop)",
    };

    public static readonly Music RaceFinish = new()
    {
        BgmIndex = BgmIndex.goal,
        OfficialName = "Finish to Go (Finish)",
    };

    public static readonly Music WinningRunAX = new()
    {
        BgmIndex = BgmIndex.hyosho_ac,
        OfficialName = "CAPTAIN FALCON",
    };

    public static readonly Music InterviewIn = new()
    {
        BgmIndex = BgmIndex.interview,
        OfficialName = "F-ZERO TV Opening (Interview In)",
    };

    public static readonly Music InterviewOut = new()
    {
        BgmIndex = BgmIndex.interview_out,
        OfficialName = "F-ZERO TV Ending (Interview Out)",
    };

    public static readonly Music Lightning = new()
    {
        BgmIndex = BgmIndex.lightning,
        BgmFinalLapIndex = BgmIndex.lightning_b,
        BgmFinalLapLoopOffset = 0x0700,
        OfficialName = "Osc-Sync Carnival (Lightning)",
    };

    public static readonly Music MeteorStream = new()
    {
        BgmIndex = BgmIndex.meteor,
        BgmFinalLapIndex = BgmIndex.meteor_b,
        BgmFinalLapLoopOffset = 0x0500,
        OfficialName = "Paper Engine (Outer Space)",
    };

    public static readonly Music MissionClear = new()
    {
        BgmIndex = BgmIndex.missionclear,
        OfficialName = "2sec (Mission Clear)",
    };

    public static readonly Music MuteCity = new()
    {
        BgmIndex = BgmIndex.mutecity,
        BgmFinalLapIndex = BgmIndex.mutecity_b,
        BgmFinalLapLoopOffset = 0x0600,
        OfficialName = "For The Glory -feat. Mute City's Theme- (Mute City)",
    };

    public static readonly Music BigBlue = new()
    {
        BgmIndex = BgmIndex.ocean,
        BgmFinalLapIndex = BgmIndex.ocean_b,
        BgmFinalLapLoopOffset = 0x0D00,
        OfficialName = "Infinite Blue (Big Blue)",
    };

    public static readonly Music FlagOpen = new()
    {
        BgmIndex = BgmIndex.openflag,
        OfficialName = "Flags (Flag Open)",
    };

    public static readonly Music GameOver = new()
    {
        BgmIndex = BgmIndex.over,
        OfficialName = "No Time (Game Over)",
    };

    public static readonly Music PilotPointResult = new()
    {
        BgmIndex = BgmIndex.point,
        OfficialName = "Stereo Signal (Pilot Point Result)",
    };

    public static readonly Music CharactersProfile = new()
    {
        BgmIndex = BgmIndex.profile,
        OfficialName = "Refresh Time (Character's Profile)",
    };

    public static readonly Music PortTown = new()
    {
        BgmIndex = BgmIndex.ptown,
        BgmFinalLapIndex = BgmIndex.ptown_b,
        BgmFinalLapLoopOffset = 0x0800,
        OfficialName = "Like a Snake (Port Town)",
    };

    public static readonly Music PhantomRoad = new()
    {
        BgmIndex = BgmIndex.rainbow,
        BgmFinalLapIndex = BgmIndex.rainbow_b,
        BgmFinalLapLoopOffset = 0x0B00,
        OfficialName = "DIZZY (Phantom Road)",
    };

    public static readonly Music RedCanyon = new ()
    {
        BgmIndex = BgmIndex.redcanyon,
        OfficialName = "Cover Of Red Canyon's Theme (Story #2)",
    };

    public static readonly Music Replay = new ()
    {
        BgmIndex = BgmIndex.replay,
        OfficialName = "Brain Cleaner (Replay)",
    };

    public static readonly Music Retire = new ()
    {
        BgmIndex = BgmIndex.retire,
        OfficialName = "The Fall (Retire)",
    };

    public static readonly Music StaffRoll = new ()
    {
        BgmIndex = BgmIndex.roll,
        OfficialName = "Respect To \"RESULT THEME OF F-ZERO\" (Staff Roll)",
    };

    public static readonly Music SandOcean = new ()
    {
        BgmIndex = BgmIndex.sand,
        BgmFinalLapIndex = BgmIndex.sand_b,
        BgmFinalLapLoopOffset = 0x0A00,
        OfficialName = "8 Guitars (Sand Ocean)",
    };

    public static readonly Music BigBlueSecret = new ()
    {
        BgmIndex = BgmIndex.secret_bb,
        OfficialName = "Cover Of Big Blue's Theme (Item Song 2)",
    };

    public static readonly Music MuteCitySecret = new ()
    {
        BgmIndex = BgmIndex.secret_mc,
        OfficialName = "Cover Of Mute City's Theme (Item Song 1)",
    };

    public static readonly Music MainSelector = new ()
    {
        BgmIndex = BgmIndex.selector,
        OfficialName = "As you choose \"3rd\" (Main Selector)",
    };

    public static readonly Music Selector = new ()
    {
        BgmIndex = BgmIndex.selector_yobi,
        OfficialName = "As you choose \"3rd\" (Selector)",
    };

    public static readonly Music Story6 = new ()
    {
        BgmIndex = BgmIndex.story7,
        OfficialName = "Time For Kill (Story #6)",
    };

    public static readonly Music Story8 = new ()
    {
        BgmIndex = BgmIndex.story9,
        OfficialName = "Emperor Breath (Story #8)",
    };

    public static readonly Music StoryStaffRoll = new()
    {
        BgmIndex = BgmIndex.storyend,
        OfficialName = "TODO",
    };

    public static readonly Music Aeropolis = new ()
    {
        BgmIndex = BgmIndex.tower,
        BgmFinalLapIndex = BgmIndex.tower_b,
        BgmFinalLapLoopOffset = 0x0400,
        OfficialName = "ZEN (Aeropolis)",
    };

    public static readonly Music Tutorial = new ()
    {
        BgmIndex = BgmIndex.tutorial,
        OfficialName = "U-Rays (Tutorial)",
    };

    public static readonly Music WinningRunGX = new ()
    {
        BgmIndex = BgmIndex.winingrun,
        OfficialName = "Hurrah for the Champion (Winning Run)",
    };

    public static readonly Music Unused = new ()
    {
        BgmIndex = BgmIndex.unused,
        OfficialName = "(unused)",
    };

    public static readonly Music Options = new ()
    {
        BgmIndex = BgmIndex.yobi2,
        OfficialName = "Step 70's (Options)",
    };

    public static readonly Music Theme_AntonioGuster = new ()
    {
        BgmIndex = BgmIndex.antonio_t,
        OfficialName = "ANTONIO GUSTER",
    };

    public static readonly Music Theme_TheSkull = new ()
    {
        BgmIndex = BgmIndex.arbingordon_t,
        OfficialName = "THE SKULL",
    };

    public static readonly Music Theme_MrsArrow = new ()
    {
        BgmIndex = BgmIndex.arrowm_t,
        OfficialName = "Mrs. ARROW",
    };

    public static readonly Music Theme_SuperArrow = new()
    {
        BgmIndex = BgmIndex.arrows_t,
        OfficialName = "SUPER ARROW",
    };

    public static readonly Music Theme_Baba = new()
    {
        BgmIndex = BgmIndex.baba_t,
        OfficialName = "BABA",
    };

    public static readonly Music Theme_Beastman = new()
    {
        BgmIndex = BgmIndex.beastman_t,
        OfficialName = "BEASTMAN",
    };

    public static readonly Music Theme_Billy = new()
    {
        BgmIndex = BgmIndex.billy_t,
        OfficialName = "Billy",
    };

    public static readonly Music Theme_BioRex = new()
    {
        BgmIndex = BgmIndex.biorex_t,
        OfficialName = "BIO REX",
    };

    public static readonly Music Theme_BlackShadow = new()
    {
        BgmIndex = BgmIndex.blackshadow_t,
        OfficialName = "BLACK SHADOW",
    };

    public static readonly Music Theme_BloodFalcon = new()
    {
        BgmIndex = BgmIndex.bloodfalcon_t,
        OfficialName = "BLOOD FALCON",
    };

    public static readonly Music Theme_CaptinaFalcon = new()
    {
        BgmIndex = BgmIndex.captainfalcon_t,
        OfficialName = "CAPTAIN FALCON",
    };

    public static readonly Music Theme_DrClash = new()
    {
        BgmIndex = BgmIndex.clash_t,
        OfficialName = "Dr. CLASH",
    };

    public static readonly Music Theme_Daigoroh = new()
    {
        BgmIndex = BgmIndex.daigoroh_t,
        OfficialName = "DAIGOROH",
    };

    public static readonly Music Theme_DaiSanGen = new()
    {
        BgmIndex = BgmIndex.daisangen_t,
        OfficialName = "DAI SAN GEN",
    };

    public static readonly Music Theme_Deathborn = new()
    {
        BgmIndex = BgmIndex.deathbone_t,
        OfficialName = "DEATHBORN",
    };

    public static readonly Music Theme_DigiBoy = new()
    {
        BgmIndex = BgmIndex.digiboy_t,
        OfficialName = "DIGI-BOY",
    };

    public static readonly Music Theme_DonGenie = new()
    {
        BgmIndex = BgmIndex.don_t,
        OfficialName = "DON GENIE",
    };

    public static readonly Music Theme_Draq = new()
    {
        BgmIndex = BgmIndex.draq_t,
        OfficialName = "DRAQ",
    };

    public static readonly Music Theme_MrEAD = new()
    {
        BgmIndex = BgmIndex.ead_t,
        OfficialName = "Mr. EAD",
    };

    public static readonly Music Theme_MightyGazelle = new()
    {
        BgmIndex = BgmIndex.gazelle_t,
        OfficialName = "MIGHTY GAZELLE",
    };

    public static readonly Music Theme_GomarAndShioh = new()
    {
        BgmIndex = BgmIndex.gommer_t,
        OfficialName = "GOMAR & SHIOH",
    };

    public static readonly Music Theme_JackLevin = new()
    {
        BgmIndex = BgmIndex.jacklevin_t,
        OfficialName = "JACK LEVIN",
    };

    public static readonly Music Theme_JamesMcCloud = new()
    {
        BgmIndex = BgmIndex.jamesmcloud_t,
        OfficialName = "JAMES MCCLOUD",
    };

    public static readonly Music Theme_JodySummer = new()
    {
        BgmIndex = BgmIndex.jodysummer_t,
        OfficialName = "JODY SUMMER",
    };

    public static readonly Music Theme_KateAlen = new()
    {
        BgmIndex = BgmIndex.kate_t,
        OfficialName = "KATE ALEN",
    };

    public static readonly Music Theme_Leon = new()
    {
        BgmIndex = BgmIndex.leon_t,
        OfficialName = "LEON",
    };

    public static readonly Music Theme_LilyFlyer = new()
    {
        BgmIndex = BgmIndex.lily_t,
        OfficialName = "LILY FLYER",
    };

    public static readonly Music Theme_MichaelChain = new()
    {
        BgmIndex = BgmIndex.michaelchain_t,
        OfficialName = "MICHAEL CHAIN",
    };

    public static readonly Music Theme_Octoman = new()
    {
        BgmIndex = BgmIndex.octman_t,
        OfficialName = "OCTOMAN",
    };

    public static readonly Music Theme_Pico = new()
    {
        BgmIndex = BgmIndex.pico_t,
        OfficialName = "PICO",
    };

    public static readonly Music Theme_PJ = new()
    {
        BgmIndex = BgmIndex.pj_t,
        OfficialName = "PJ",
    };

    public static readonly Music Theme_QQQ = new()
    {
        BgmIndex = BgmIndex.qqq_t,
        OfficialName = "QQQ",
    };

    public static readonly Music Theme_RogerBuster = new()
    {
        BgmIndex = BgmIndex.rogerbuster_t,
        OfficialName = "ROGER BUSTER",
    };

    public static readonly Music Theme_SamuraiGoroh = new()
    {
        BgmIndex = BgmIndex.samuraigoroh_t,
        OfficialName = "SAMURAI GOROH",
    };

    public static readonly Music Theme_Phoenix = new()
    {
        BgmIndex = BgmIndex.sharock_t, // Sherlock
        OfficialName = "PHOENIX",
    };

    public static readonly Music Theme_SilverNeelsen = new()
    {
        BgmIndex = BgmIndex.silverneelsen_t,
        OfficialName = "SILVER NEELSEN",
    };

    public static readonly Music Theme_Spade = new()
    {
        BgmIndex = BgmIndex.spade_t,
        OfficialName = "SPADE",
    };

    public static readonly Music Theme_DrStewart = new()
    {
        BgmIndex = BgmIndex.stewart_t,
        OfficialName = "Dr. STEWART",
    };

    public static readonly Music Theme_JohnTanaka = new()
    {
        BgmIndex = BgmIndex.tanaka_t,
        OfficialName = "JOHN TANAKA",
    };

    public static readonly Music Theme_Zoda = new()
    {
        BgmIndex = BgmIndex.zoda_t,
        OfficialName = "ZODA",
    };

    public static readonly Music Random = new()
    {
        BgmIndex = BgmIndex.random,
        OfficialName = "(random song)",
    };
}