namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// Flags indicating which display list(s) are serialized.
    /// </summary>
    [System.Flags]
    public enum MaterialDestination : byte
    {
        /// <summary>
        /// If set, indicates that the submesh has a primary front-face-culling display list.
        /// </summary>
        PrimaryFrontCull = 1 << 0,

        /// <summary>
        /// If set, indicates that the submesh has a primary back-face-culling display list.
        /// </summary>
        PrimaryBackCull = 1 << 1,

        /// <summary>
        /// If set, indicates that the submesh has a secondary front-face-culling display list.
        /// </summary>
        SecondaryFrontCull = 1 << 2,

        /// <summary>
        /// If set, indicates that the submesh has a secondary back-face-culling display list.
        /// </summary>
        SecondaryBackCull = 1 << 3,
    }
}