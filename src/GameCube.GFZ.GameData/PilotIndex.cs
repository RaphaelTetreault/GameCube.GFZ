namespace GameCube.GFZ.GameData;

/// <summary>
///     Pilot index (internal pilot index, not display number).
/// </summary>
public enum PilotIndex : byte
{
    MightyGazelle =  0, // Pilot Number =  1
    JodySummer    =  1, // Pilot Number =  2
    DrStewart     =  2, // Pilot Number =  3
    Baba          =  3, // Pilot Number =  4
    SamuraiGoroh  =  4, // Pilot Number =  5
    Pico          =  5, // Pilot Number =  6
    CaptainFalcon =  6, // Pilot Number =  7
    Octoman       =  7, // Pilot Number =  8
    MrEAD         =  8, // Pilot Number =  9
    JamesMcCloud  =  9, // Pilot Number = 10
    Billy         = 10, // Pilot Number = 11
    KateAlen      = 11, // Pilot Number = 12
    Zoda          = 12, // Pilot Number = 13
    JackLevin     = 13, // Pilot Number = 14
    BioRex        = 14, // Pilot Number = 15
    TheSkull      = 15, // Pilot Number = 16
    AntonioGuster = 16, // Pilot Number = 17
    Beastman      = 17, // Pilot Number = 18
    Leon          = 18, // Pilot Number = 19
    SuperArrow    = 19, // Pilot Number = 20
    MrsArrow      = 20, // Pilot Number = 21
    Shioh         = 21, // Pilot Number = 22 Pilot #1
    SilverNeelsen = 22, // Pilot Number = 23
    MichaelChain  = 23, // Pilot Number = 24
    BloodFalcon   = 24, // Pilot Number = 25
    JohnTanaka    = 25, // Pilot Number = 26
    Draq          = 26, // Pilot Number = 27
    RogerBuster   = 27, // Pilot Number = 28
    DrClash       = 28, // Pilot Number = 29
    BlackShadow   = 29, // Pilot Number = 30
    Deathborn     = 30, // Pilot Number =  0
    DonGenie      = 31, // Pilot Number = 31
    DigiBoy       = 32, // Pilot Number = 32
    Dai           = 33, // Pilot Number = 33 Pilot #1
    Spade         = 34, // Pilot Number = 34
    Daigoroh      = 35, // Pilot Number = 35
    PrinciaRamode = 36, // Pilot Number = 36
    LilyFlyer     = 37, // Pilot Number = 37
    PJ            = 38, // Pilot Number = 38
    QQQ           = 39, // Pilot Number = 39
    Pheonix       = 40, // Pilot Number = 40

    // TODO: confirm this order
    Gomar        = 41, // Pilot Number = 22 Pilot #2
    San          = 42, // Pilot Number = 33 Pilot #2
    Gen          = 43, // Pilot Number = 33 Pilot #3
    MrZero       = 44, // THANK YOU FOR THE INTERVIEW!
}

public static class PilotIndexExtensions
{
    extension(PilotIndex pilotIndex)
    {
        public byte Byte => (byte)pilotIndex;
    }
}