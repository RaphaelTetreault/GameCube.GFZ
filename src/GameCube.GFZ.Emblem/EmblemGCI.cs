using GameCube.DiskImage;
using GameCube.GFZ.GCI;

namespace GameCube.GFZ.Emblem
{
    /// <summary>
    ///     Player-created emblem.
    /// </summary>
    /// <example>
    ///     Primary source of information:
    ///     https://docs.google.com/document/d/1c4a7d6xZ-rnK-E5p7d__6V1qhLxcdOHwAmv-FR8K50s/edit
    /// </example>
    public class EmblemGCI : GfzGci<EmblemBIN>
    {
        public const ushort UID = 0x0401; // NOT A UNIQUE ID

        public readonly ushort[] UIDs = [UID];
        public override ushort[] UniqueIDs => UIDs;

        public Emblem Emblem
        {
            get => FileData.Value.Emblems[0];
            set => FileData.Value.Emblems[0] = value;
        }


        public EmblemGCI() : base()
        {
            InitEmlbemGroupTo1();
        }

        public EmblemGCI(Region region) : base(region)
        {
            InitEmlbemGroupTo1();
        }

        /// <summary>
        ///     Make sure 1 emblem always exists.
        /// </summary>
        private void InitEmlbemGroupTo1()
        {
            FileData = new();
            FileData.Value.Emblems = new Emblem[1];
        }
    }
}
