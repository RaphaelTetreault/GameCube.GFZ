using System.ComponentModel;

namespace GameCube.GFZ.REL
{
    public enum Cup : byte
    {
        [Description("All")]
        All,

        [Description("Ruby Cup")]
        Ruby,

        [Description("Sapphire Cup")]
        Sapphire,

        [Description("Emerald Cup")]
        Emerald,

        [Description("Diamond Cup")]
        Diamond,

        [Description("AX Cup")]
        AX,

        [Description("AC Cup")]
        AC,

        [Description("WHF Cup")]
        WHF,

        [Description("E3-0")]
        E30,

        [Description("E3-1")]
        E31,

        [Description("E3 VS")]
        E3VS,
    }
}
