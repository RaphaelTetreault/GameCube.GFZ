using System.ComponentModel;

namespace GameCube.GFZ.GeneralGameData
{
    // adv_ac?
    // Wings For My Way -ver.AX- (AX Advertise)

    public enum Bgm
    {
        [Description("(None)")]
        none = 0,

        [Description("Wings For My Way (GX Advertise)")]
        adv_gc = 1,

        [Description("Night Of Big Blue (Story #4)")]
        bblue = 2,

        [Description("Raise a curtain (Card Check)")]
        cardcheck = 3,

        [Description("Shotgun Kiss (Vegas Palace)")]
        casino = 4,

        [Description("Shotgun Kiss (Vegas Palace) B")]
        casino_b = 5,

        [Description("Short Pre-View (Course View 1)")]
        course_view = 6,

        [Description("Long Pre-View (Course View 2)")]
        course_view_ac = 7,

        [Description("Feather (Customize)")]
        customize = 8,

        [Description("One Ahead System (Cosmo Terminal)")]
        elev = 9,

        [Description("One Ahead System (Cosmo Terminal) B")]
        elev_b = 10,

        [Description("Feel Our Pain (Fire Field)")]
        fire = 11,

        [Description("Feel Our Pain (Fire Field) B")]
        fire_b = 12,

        [Description("Planet Colors (Green Plant)")]
        forest = 13,

        [Description("Planet Colors (Green Plant)")]
        forest_b = 14,

        [Description("Your Garage (Shop)")]
        garage = 15,

        [Description("Finish to Go (Finish)")]
        goal = 16,

        [Description("")]
        hyosho_ac = 17,

        [Description("F-ZERO TV Opening (Interview In)")]
        interview = 18,

        [Description("F-ZERO TV Ending (Interview Out)")]
        interview_out = 19,

        [Description("Osc-Sync Carnival (Lightning)")]
        lightning = 20,

        [Description("Osc-Sync Carnival (Lightning) B")]
        lightning_b = 21,

        [Description("Paper Engine (Outer Space)")]
        meteor = 22,

        [Description("Paper Engine (Outer Space) B")]
        meteor_b = 23,

        [Description("2sec (Mission Clear)")]
        missionclear = 24,

        [Description("For The Glory -feat. Mute City's Theme- (Mute City)")]
        mutecity = 25,

        [Description("For The Glory -feat. Mute City's Theme- (Mute City) B")]
        mutecity_b = 26,

        [Description("Infinite Blue (Big Blue)")]
        ocean = 27,

        [Description("Infinite Blue (Big Blue) B")]
        ocean_b = 28,

        [Description("Flags (Flag Open)")]
        openflag = 29,

        [Description("No Time (Game Over)")]
        over = 30,

        [Description("Stereo Signal (Pilot Point Result)")]
        point = 31,

        [Description("Refresh Time (Character's Profile)")]
        profile = 32,

        [Description("Like a Snake (Port Town)")]
        ptown = 33,

        [Description("Like a Snake (Port Town) B")]
        ptown_b = 34,

        [Description("DIZZY (Phantom Road)")]
        rainbow = 35,

        [Description("DIZZY (Phantom Road) B")]
        rainbow_b = 36,

        [Description("Cover Of Red Canyon's Theme (Story #2)")]
        redcanyon = 37,

        [Description("Brain Cleaner (Replay)")]
        replay = 38,

        [Description("The Fall (Retire)")]
        retire = 39,

        [Description("Respect To \"RESULT THEME OF F-ZERO\" (Staff Roll)")]
        roll = 40,

        [Description("8 Guitars (Sand Ocean)")]
        sand = 41,

        [Description("8 Guitars (Sand Ocean) B")]
        sand_b = 42,

        [Description("Cover Of Big Blue's Theme (Item Song 2)")]
        secret_bb = 43,

        [Description("Cover Of Mute City's Theme (Item Song 1)")]
        secret_mc = 44,

        [Description("As you choose \"3rd\" (Main Selector)")]
        selector = 45,

        [Description("As you choose \"3rd\" (Selector)")]
        selector_yobi = 46,

        [Description("Time For Kill (Story #6)")]
        story7 = 47,

        [Description("Emperor Breath (Story #8)")]
        story9 = 48,

        [Description("U-Rays (Tutorial)")]
        storyend = 49,

        [Description("ZEN (Aeropolis)")]
        tower = 50,

        [Description("ZEN (Aeropolis) B")]
        tower_b = 51,

        //[Description("")]
        //unused52 = 52,

        [Description("Hurrah for the Champion (Winning Run)")]
        winingrun = 53,

        //[Description("")]
        //unused54 = 54,

        [Description()] // in GX, not on CD?
        yobi2 = 55,

        [Description("ANTONIO GUSTER")]
        antonio_t = 56,

        [Description("THE SKULL")]
        arbingordon_t = 57,

        [Description("Mrs. ARROW")]
        arrowm_t = 58,

        [Description("SUPER ARROW")]
        arrows_t = 59,

        [Description("BABA")]
        baba_t = 60,

        [Description("BEASTMAN")]
        beastman_t = 61,

        [Description("BILLY")]
        billy_t = 62,

        [Description("BIO REX")]
        biorex_t = 63,

        [Description("BLACK SHADOW")]
        blackshadow_t = 64,

        [Description("BLOOD FALCON")]
        bloodfalcon_t = 65,

        [Description("CAPTAIN FALCON")]
        captainfalcon_t = 66,

        [Description("Dr. CLASH")]
        clash_t = 67,

        [Description("DAIGOROH")]
        daigoroh_t = 68,

        [Description("DAI SAN GEN")]
        daisangen_t = 69,

        [Description("DEATHBORN")]
        deathbone_t = 70,

        [Description("DIGI-BOY")]
        digiboy_t = 71,

        [Description("DON GENIE")]
        don_t = 72,

        [Description("DRAQ")]
        draq_t = 73,

        [Description("Mr. EAD")]
        ead_t = 74,

        [Description("MIGHTY GAZELLE")]
        gazelle_t = 75,

        [Description("GOMAR & SHIOH")]
        gommer_t = 76,

        [Description("JACK LEVIN")]
        jacklevin_t = 77,

        [Description("JAMES MCCLOUD")]
        jamesmcloud_t = 78,

        [Description("JODY SUMMER")]
        jodysummer_t = 79,

        [Description("KATE ALEN")]
        kate_t = 80,

        [Description("LEON")]
        leon_t = 81,

        [Description("LILY FLYER")]
        lily_t = 82,

        [Description("MICHAEL CHAIN")]
        michaelchain_t = 83,

        [Description("OCTOMAN")]
        octman_t = 84,

        [Description("PICO")]
        pico_t = 85,

        [Description("PJ")]
        pj_t = 86,

        [Description("PRINCIA RAMODE")]
        prisia_t = 87,

        [Description("QQQ")]
        qqq_t = 88,

        [Description("ROGER BUSTER")]
        rogerbuster_t = 89,

        [Description("SAMURAI GOROH")]
        samuraigoroh_t = 90,

        [Description("PHOENIX")]
        sharock_t = 91,

        [Description("SILVER NEELSEN")]
        silverneelsen_t = 92,

        [Description("SPADE")]
        spade_t = 93,

        [Description("Dr. STEWART")]
        stewart_t = 94,

        [Description("JOHN TANAKA")]
        tanaka_t = 95,

        [Description("ZODA")]
        zoda_t = 96,

        // Metadata
        last_id = 96,

        [Description("(Random song)")]
        random = 255,
    }

}
