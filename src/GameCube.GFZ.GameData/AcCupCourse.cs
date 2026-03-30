using System.ComponentModel;

namespace GameCube.GFZ.GameData;

/// <summary>
///     Arcade Cup (AX Cup) stages.
/// </summary>
/// <remarks>
///     Description attribute contains display name of each stage.
/// </remarks>
public enum AcCupCourse : byte
{
    [Description("Mute City [Sonic Oval]")]
    MCSO,

    [Description("Aeropolis [Screw Drive]")]
    ASD,

    [Description("Outer Space [Meteor Stream]")]
    OSMS,

    [Description("Port Town [Cylinder Wave]")]
    PTCW,

    [Description("Lightning [Thunder Road]")]
    LTR,

    [Description("Green Plant [Spiral")]
    GPS,
}
