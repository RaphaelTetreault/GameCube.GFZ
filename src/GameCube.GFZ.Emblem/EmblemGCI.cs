using GameCube.DiskImage;
using GameCube.GFZ.GCI;

namespace GameCube.GFZ.Emblem
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///     Primary source of information:
    ///     https://docs.google.com/document/d/1c4a7d6xZ-rnK-E5p7d__6V1qhLxcdOHwAmv-FR8K50s/edit
    /// </example>
    public class EmblemGCI : GfzGci<Emblem>
    {
        public const ushort UID = 0x0401; // NOT A UNIQUE ID

        public readonly ushort[] UIDs = { UID };
        public override ushort[] UniqueIDs => UIDs;

        public Emblem Emblem { get => FileData; set => FileData = value; }


        public EmblemGCI() : base() { }
        public EmblemGCI(Region region) : base(region) { }
    }
}
