using System.ComponentModel;

namespace GameCube.GFZ.LineREL
{
    public enum AcCupCourse : byte
    {
        [Description("Mute City - Sonic Oval")]
        MCSO,

        [Description("Aeropolis - Screw Drive")]
        ASD,

        [Description("Outer Space - Meteor Stream")]
        OSMS,

        [Description("Port Town - Cylinder Wave")]
        PTCW,

        [Description("Lightning - Thunder Road")]
        LTR,

        [Description("Green Plant - Spiral")]
        GPS,
    }
}
