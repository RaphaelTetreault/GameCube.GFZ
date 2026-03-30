using GameCube.AmusementVision;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;

namespace GameCube.GFZ.GameData;

/// <summary>
///     
/// </summary>
public static class CourseDatabase
{



    public static string GetVenueBackgroundName(VenueID venueID, GameCode gameCode) => venueID switch
    {
        VenueID.Aeropolis       => "tow",   // Tower = Aeropolis
        VenueID.BigBlue         => "big",   
        VenueID.BigBlueStory    => "big_s", 
        VenueID.CasinoPalace    => "cas",   // Vegas Palace = Casino Palace
        VenueID.CosmoTerminal   => "ele",   // Elevator = Cosmo Terminal
        VenueID.FireField       => "fir",   
        VenueID.FireFieldStory  => "fir_s", 
        VenueID.GreenPlant      => "for",   // Forest = Green Plant
        VenueID.Lightning       => "lig",   
        VenueID.LightningStory  => "lig_s", 
        VenueID.MuteCity        => "mut",   
        VenueID.MuteCityCom     => "com",   // Com for Combo? Mute City + Casino Palace.
        VenueID.MuteCityComStory => "com_s",
        VenueID.MuteCityGrandPrixPodium => IsGX(gameCode) ? "win_gx" : IsAX(gameCode) ? "win" : throw GetGameCodeNotAxOrGxException(gameCode),
        VenueID.OuterSpace      => "met",   // Meteor = Outer Space
        VenueID.PhantomRoad     => "rai",   // Rainbow Road = Phantom Road
        VenueID.PortTown        => "por",
        VenueID.PortTownStory   => "por_s",
        VenueID.SandOcean       => "san",
        VenueID.SandOceanStory  => "san_s",
        _ => throw GetVenueIDInvalidException(venueID),
    };

    /// <summary>
    ///         Get default venue for stage ID <paramref name="index"/>.
    /// </summary>
    /// <param name="index">The stage ID index (0-110).</param>
    /// <returns>
    ///     
    /// </returns>
    public static VenueID GetDefaultVenueID(int index)
    {
        return index switch
        {
            00 => VenueID.SandOcean,    // Unused Twist Road leftovers
            01 => VenueID.MuteCity,     // Mute City [Twist Road]
            02 => VenueID.MuteCity,     
            03 => VenueID.MuteCity,     // Mute City [Serial Gaps]
            04 => VenueID.MuteCity,     
            05 => VenueID.Aeropolis,    // Aeropolis [Multiplex]
            06 => VenueID.PortTown,     
            07 => VenueID.PortTown,     // Port Town [Aero Dive]
            08 => VenueID.Lightning,    // Lightning [Loop Cross]
            09 => VenueID.Lightning,    // Lightning [Half-Pipe]
            10 => VenueID.GreenPlant,   // Green Plant [Intersection]
            11 => VenueID.GreenPlant,   // Green Plant [Mobius Ring]
            12 => VenueID.Lightning,    
            13 => VenueID.PortTown,     // Port Town [Long Pipe]
            14 => VenueID.BigBlue,      // Big Blue [Drift Highway]
            15 => VenueID.FireField,    // Fire Field [Cylinder Knot]
            16 => VenueID.CasinoPalace, // Casino Palace [Split Oval]
            17 => VenueID.FireField,    // Fire Field [Undulation]
            18 => VenueID.FireField,    
            19 => VenueID.OuterSpace,   
            20 => VenueID.OuterSpace,   
            21 => VenueID.Aeropolis,    // Aeropolis [Dragon Slope]
            22 => VenueID.CosmoTerminal,
            23 => VenueID.Lightning,
            24 => VenueID.CosmoTerminal, // Cosmo Terminal [Trident]
            25 => VenueID.SandOcean,    // Sand Ocean [Lateral Shift]
            26 => VenueID.SandOcean,    // Sand Ocean [Surface Slide]
            27 => VenueID.BigBlue,      // Big Blue [Ordeal]
            28 => VenueID.PhantomRoad,  // Phantom Road [Slim-line Slits]
            29 => VenueID.CasinoPalace, // Casino Palace [Double Branches]
            30 => VenueID.SandOcean, 
            31 => VenueID.Aeropolis,    // Aeropolis [Screw Drive]
            32 => VenueID.OuterSpace,   // Outer Space [Meteor Stream]
            33 => VenueID.PortTown,     // Port Town [Cylinder Wave]
            34 => VenueID.Lightning,    // Lightning [Thunder Road]
            35 => VenueID.GreenPlant,   // Green Plant [Sprial]
            36 => VenueID.MuteCityCom,  // Mute City [Sonic Oval]
            37 => VenueID.MuteCityComStory, // Story 1: Captain Falcon Trains
            38 => VenueID.SandOceanStory,   // Story 2: Goroh: The Vengeful Samurai
            39 => VenueID.CasinoPalace,     // Story 3: High Stakes in Mute City
            40 => VenueID.BigBlueStory,     // Story 4: Challenge of the Bloody Chain
            41 => VenueID.Lightning,        // Story 5: Save Jody Summer!
            42 => VenueID.PortTownStory,    // Story 6: Black Shadow's Trap
            43 => VenueID.MuteCity,         // Story 7: The F-Zero Grand Prix
            44 => VenueID.FireFieldStory,   // Story 8: Secrets of the Champion Belt
            45 => VenueID.PhantomRoad,      // Story 9: Finale: Enter The Creators

            49 => VenueID.MuteCityGrandPrixPodium, // Grand Prix Podium
            50 => VenueID.MuteCityGrandPrixPodium, // Victory Lap

            _ => VenueID.None, // For all other indices, there is no venue
        };
    }

    public static string GetDefaultCourseName(GameCode gameCode, int courseIndex)
    {
        Course course = Course.DefaultCourses[courseIndex];
        string name = course.Name[gameCode];
        return name;
    }
    public static string GetDefaultVenueName(GameCode gameCode, int courseIndex)
    {
        Course course = Course.DefaultCourses[courseIndex];
        Venue venue = course.Venue;
        string name = venue.Name[gameCode];
        return name;
    }
    public static string GetDefaultVenueName(GameCode gameCode, VenueID venueID)
    {
        Venue venue = Venue.DefaultVenues[(int)venueID];
        string name = venue.Name[gameCode];
        return name;
    }

    // TODO: consider moving this into a utlity class for GameCode, GameCodeFields, AvGame, etc.
#pragma warning disable CA2248 // Provide correct 'enum' argument to 'Enum.HasFlag'
    private static bool IsAX(GameCode gameCode) => gameCode.HasFlag(GameCodeFields.AX);
    private static bool IsGX(GameCode gameCode) => gameCode.HasFlag(GameCodeFields.GX);
#pragma warning restore CA2248 // Provide correct 'enum' argument to 'Enum.HasFlag'



    private static ArgumentException GetGameCodeNotAxOrGxException(GameCode gameCode)
    {
        string msg = $"Invalid {nameof(GameCode)} {gameCode}. No flags for AX or GX defined.";
        return new ArgumentException(msg);
    }

    private static ArgumentException GetVenueIDInvalidException(VenueID venueID)
    {
        string msg = $"Invalid {nameof(VenueID)} value {venueID} ({(int)venueID}).";
        return new ArgumentException(msg);
    }

}
