namespace GameCube.GFZ.GameData
{
    public static class BgmReference
    {
        /// <summary>
        ///     Get the associated final lap BGM loop point offset
        ///     for <paramref name="bgmFinalLapIndex"/>.
        /// </summary>
        /// <param name="bgmFinalLapIndex">The BGM song index.</param>
        /// <returns>
        ///     The relevant 16-bit offset to the correct loop point data.
        /// </returns>
        public static ushort GetBgmLoopPointOffset(byte bgmFinalLapIndex)
        {
            return bgmFinalLapIndex switch
            {
                0x33 => 0x0400,// Aeropolis
                0x17 => 0x0500,// Meteor Stream
                0x1A => 0x0600,// Mute City
                0x15 => 0x0700,// Lightning
                0x22 => 0x0800,// Port Town
                0x0E => 0x0900,// Green Plant
                0x2A => 0x0A00,// Sand Ocean
                0x24 => 0x0B00,// Phantom Road
                0x0C => 0x0C00,// Fire Field
                0x1C => 0x0D00,// Big Blue
                0x0A => 0x0E00,// Cosmo Terminal
                0x05 => 0x0F00,// Casino Palace
                0xFF => 0xFFFF,// No BGM
                _ => 0xFFFF,
            };
        }

        /// <summary>
        ///     Get the associated final lap BGM loop point offset
        ///     for <paramref name="bgmFinalLapIndex"/>.
        /// </summary>
        /// <param name="bgmFinalLapIndex"></param>
        /// <returns>
        ///     The relevant 16-bit offset to the correct loop point data.
        /// </returns>
        public static ushort GetBgmLoopPointOffset(Bgm bgmFinalLapIndex)
        {
            ushort value = GetBgmLoopPointOffset((byte)bgmFinalLapIndex);
            return value;
        }

    }
}
