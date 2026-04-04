using System.ComponentModel;

namespace GameCube.GFZ.GameData;

/// <summary>
///     <u>B</u>ack<u>G</u>round <u>M</u>usic index.
/// </summary>
public enum BgmIndex : byte
{
    /// <summary>
    ///     AX advertise
    /// </summary>
    adv_ac = 0,

    /// <summary>
    ///     GX advertise
    /// </summary>
    adv_gc = 1,

    /// <summary>
    ///     Night of Big Blue
    /// </summary>
    bblue = 2,

    /// <summary>
    ///     AX magcard check
    /// </summary>
    cardcheck = 3,

    /// <summary>
    ///     Casino Palace
    /// </summary>
    casino = 4,

    /// <summary>
    ///     Casino Palace final lap
    /// </summary>
    casino_b = 5,

    /// <summary>
    ///     GX long course preview
    /// </summary>
    course_view = 6,

    /// <summary>
    ///     AX short course preview
    /// </summary>
    course_view_ac = 7,

    /// <summary>
    ///     Vehicle and emblem customize
    /// </summary>
    customize = 8,

    /// <summary>
    ///     Cosmo Terminal
    /// </summary>
    elev = 9,

    /// <summary>
    ///     Cosmo Terminal final lap
    /// </summary>
    elev_b = 10,

    /// <summary>
    ///     Fire Field
    /// </summary>
    fire = 11,

    /// <summary>
    ///     Fire Field final lap
    /// </summary>
    fire_b = 12,

    /// <summary>
    ///     Green Plant
    /// </summary>
    forest = 13,

    /// <summary>
    ///     Green Plant final lap
    /// </summary>
    forest_b = 14,

    /// <summary>
    ///     Shop
    /// </summary>
    garage = 15,

    /// <summary>
    ///     Finish
    /// </summary>
    goal = 16,

    /// <summary>
    ///     AX winning run, same as <see cref="captainfalcon_t"/>
    /// </summary>
    hyosho_ac = 17,

    /// <summary>
    ///     Interview intro
    /// </summary>
    interview = 18,

    /// <summary>
    ///     Interview outro
    /// </summary>
    interview_out = 19,

    /// <summary>
    ///     Lightning
    /// </summary>
    lightning = 20,

    /// <summary>
    ///     Lightning final lap
    /// </summary>
    lightning_b = 21,

    /// <summary>
    ///     Meteor Stream
    /// </summary>
    meteor = 22,

    /// <summary>
    ///     Meteor Stream final lap
    /// </summary>
    meteor_b = 23,

    /// <summary>
    ///     Mission clear
    /// </summary>
    missionclear = 24,

    /// <summary>
    ///     Mute City
    /// </summary>
    mutecity = 25,

    /// <summary>
    ///     Mute City final lap
    /// </summary>
    mutecity_b = 26,

    /// <summary>
    ///     Big Blue
    /// </summary>
    ocean = 27,

    /// <summary>
    ///     Big Blue final lap
    /// </summary>
    ocean_b = 28,

    /// <summary>
    ///     AX link versus?
    /// </summary>
    openflag = 29,

    /// <summary>
    ///     Game Over
    /// </summary>
    over = 30,

    /// <summary>
    ///     Pilot Point Result
    /// </summary>
    point = 31,

    /// <summary>
    ///     Character Profile
    /// </summary>
    profile = 32,

    /// <summary>
    ///     Port Town
    /// </summary>
    ptown = 33,

    /// <summary>
    ///     Port Town final lap
    /// </summary>
    ptown_b = 34,

    /// <summary>
    ///     Phantom Road
    /// </summary>
    rainbow = 35,

    /// <summary>
    ///     Phantom Road final lap
    /// </summary>
    rainbow_b = 36,

    /// <summary>
    ///     Red Canyon (Chapter 2)
    /// </summary>
    redcanyon = 37,

    /// <summary>
    ///     Replay
    /// </summary>
    replay = 38,

    /// <summary>
    ///     Retire
    /// </summary>
    retire = 39,

    /// <summary>
    ///     Staff Roll (Master, Grand Prix)
    /// </summary>
    roll = 40,

    /// <summary>
    ///     Sand Ocean
    /// </summary>
    sand = 41,

    /// <summary>
    ///     Sand Ocean final lap
    /// </summary>
    sand_b = 42,

    /// <summary>
    ///     Big Blue (Alt)
    /// </summary>
    secret_bb = 43,

    /// <summary>
    ///     Mute City (Alt)
    /// </summary>
    secret_mc = 44,

    /// <summary>
    ///     Main Menu
    /// </summary>
    selector = 45,

    /// <summary>
    ///     Submenu
    /// </summary>
    selector_yobi = 46,

    /// <summary>
    ///     Chapter 6
    /// </summary>
    story7 = 47,

    /// <summary>
    ///     Chapter 8
    /// </summary>
    story9 = 48,

    /// <summary>
    ///     Staff Roll (Story Mode)
    /// </summary>
    storyend = 49,

    /// <summary>
    ///     Aeropolis
    /// </summary>
    tower = 50,

    /// <summary>
    ///     Aeropolis final lap
    /// </summary>
    tower_b = 51,

    /// <summary>
    ///     AX advertise tutorial
    /// </summary>
    tutorial = 52,

    /// <summary>
    ///     GX winning run / victory lap
    /// </summary>
    winingrun = 53,

    /// <summary>
    ///     Unused. "yobi"?
    /// </summary>
    metadata_unused = 54,

    /// <summary>
    ///     Options
    /// </summary>
    yobi2 = 55,

    /// <summary>
    ///     Antonio Guster's theme
    /// </summary>
    antonio_t = 56,

    /// <summary>
    ///     The Skull's theme
    /// </summary>
    arbingordon_t = 57,

    /// <summary>
    ///     Mrs. Arrow's theme
    /// </summary>
    arrowm_t = 58,

    /// <summary>
    ///     Super Arrow's theme
    /// </summary>
    arrows_t = 59,

    /// <summary>
    ///     Baba's theme
    /// </summary>
    baba_t = 60,

    /// <summary>
    ///     Beastman's theme
    /// </summary>
    beastman_t = 61,

    /// <summary>
    ///     Billy's theme
    /// </summary>
    billy_t = 62,

    /// <summary>
    ///     Bio Rex's theme
    /// </summary>
    biorex_t = 63,

    /// <summary>
    ///     Black Shadow's theme
    /// </summary>
    blackshadow_t = 64,

    /// <summary>
    ///     Blood Falcon's theme
    /// </summary>
    bloodfalcon_t = 65,

    /// <summary>
    ///     Captain Falcon's theme
    /// </summary>
    captainfalcon_t = 66,

    /// <summary>
    ///     Dr. Clash's theme
    /// </summary>
    clash_t = 67,

    /// <summary>
    ///     Daigoroh's theme
    /// </summary>
    daigoroh_t = 68,

    /// <summary>
    ///     Dai San Gen's theme
    /// </summary>
    daisangen_t = 69,

    /// <summary>
    ///     Deathborn's theme
    /// </summary>
    ///<remarks>
    ///     death<i>bone</i>_t is <u>not</u> a typo
    /// </remarks>
    deathbone_t = 70,

    /// <summary>
    ///     Digi-Boy's theme
    /// </summary>
    digiboy_t = 71,

    /// <summary>
    ///     Don Genie's theme
    /// </summary>
    don_t = 72,

    /// <summary>
    ///     Draq's theme
    /// </summary>
    draq_t = 73,

    /// <summary>
    ///     Mr. EAD's theme
    /// </summary>
    ead_t = 74,

    /// <summary>
    ///     Mighty Gazelle's theme
    /// </summary>
    gazelle_t = 75,

    /// <summary>
    ///     Gomar and Shioh's theme
    /// </summary>
    gommer_t = 76,

    /// <summary>
    ///     Jack Levin's theme
    /// </summary>
    jacklevin_t = 77,

    /// <summary>
    ///     James McCloud's theme
    /// </summary>
    jamesmcloud_t = 78,

    /// <summary>
    ///     Jody Summer's theme
    /// </summary>
    jodysummer_t = 79,

    /// <summary>
    ///     Kate Alen's theme
    /// </summary>
    kate_t = 80,

    /// <summary>
    ///     Leon's theme
    /// </summary>
    leon_t = 81,

    /// <summary>
    ///     Lily Flyer's theme
    /// </summary>
    lily_t = 82,

    /// <summary>
    ///     Michael Chain's theme
    /// </summary>
    michaelchain_t = 83,

    /// <summary>
    ///     Octoman's theme
    /// </summary>
    octman_t = 84,

    /// <summary>
    ///     Pico's theme
    /// </summary>
    pico_t = 85,

    /// <summary>
    ///     PJ's theme
    /// </summary>
    pj_t = 86,

    /// <summary>
    ///     Princia Romade's theme
    /// </summary>
    prisia_t = 87,

    /// <summary>
    ///     QQQ's theme
    /// </summary>
    qqq_t = 88,

    /// <summary>
    ///     Roger Buster's theme
    /// </summary>
    rogerbuster_t = 89,

    /// <summary>
    ///     Samurai's theme
    /// </summary>
    samuraigoroh_t = 90,

    /// <summary>
    ///     Pheonix's theme
    /// </summary>
    sharock_t = 91,

    /// <summary>
    ///     Silver Neelsen's theme
    /// </summary>
    silverneelsen_t = 92,

    /// <summary>
    ///     Spade's theme
    /// </summary>
    spade_t = 93,

    /// <summary>
    ///     Dr. Stewart's theme
    /// </summary>
    stewart_t = 94,

    /// <summary>
    ///     John Tanaka's theme
    /// </summary>
    tanaka_t = 95,

    /// <summary>
    ///     Zoda's theme
    /// </summary>
    zoda_t = 96,

    /// <summary>
    ///     METADATA. Invalid index start.
    /// </summary>
    metadata_invalid_id_start = 97,

    /// <summary>
    ///     METADATA. Invalid index end.
    /// </summary>
    metadata_invalid_id_end = 254,

    /// <summary>
    ///     Random selection ID.
    /// </summary>
    metadata_random = 255,

    /// <summary>
    ///     Random selection ID.
    /// </summary>
    metadata_no_final_lap_bgm = 0xFF,
}
