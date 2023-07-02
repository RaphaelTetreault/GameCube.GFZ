using GameCube.GFZ.CarData;
using GameCube.GX;
using Manifold.IO;

namespace GameCube.GFZ.Replay
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     Byte-aligned despite being in serialized in the bitstream.
    /// </remarks>
    public class CustomMachine :
        IBitSerializable
    {
        // numbers in BYTES
        private byte[] unknown1; // 5
        private MachineID machineID; // 1
        private byte colorPaletteID; // 1
        private byte emblemCount; // 1
        private byte[] unknown2; // 7
        private byte[] speedSettings; // 7
        private byte[] unknown3; // 16
        private byte[][] emblemData = new byte[4][]; // 4*8288, is actually a byte-aligned structure
        private PilotID pilotID; // 4
        private CustomBodyPartName bodyID; // 4
        private GXColor bodyColor; // 4
        private CustomCockpitPartName cockpitID; // 4
        private GXColor cockpitColor; // 4
        private CustomBoosterPartName boosterID; // 4
        private GXColor boosterColor; // 4
        private uint unknown4; // 4

        public void Deserialize(BitStreamReader reader)
        {
            // bytes * 8 bits
            unknown1 = reader.ReadBytes(5 * 8);
            machineID = (MachineID)reader.ReadByte(1 * 8);
            colorPaletteID = reader.ReadByte(1 * 8);
            emblemCount = reader.ReadByte(1 * 8);
            unknown2 = reader.ReadBytes(7 * 8);
            speedSettings = reader.ReadBytes(7 * 8);
            unknown3 = reader.ReadBytes(16 * 8);
            Assert.IsTrue(emblemCount <= 4);
            for (int i = 0; i < emblemCount; i++)
                emblemData[i] = reader.ReadBytes(8288*8); // bytes
            pilotID = (PilotID)reader.ReadUInt(4 * 8);
            bodyID = (CustomBodyPartName)reader.ReadUInt(4 * 8);
            bodyColor = new GXColor(reader.ReadUInt(4 * 8));
            cockpitID = (CustomCockpitPartName)reader.ReadUInt(4 * 8);
            cockpitColor = new GXColor(reader.ReadUInt(4 * 8));
            boosterID = (CustomBoosterPartName)reader.ReadUInt(4 * 8);
            boosterColor = new GXColor(reader.ReadUInt(4 * 8));
        }

        public void Serialize(BitStreamWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
