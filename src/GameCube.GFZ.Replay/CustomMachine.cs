using GameCube.GFZ.CarData;
using GameCube.GFZ.REL;
using GameCube.GX;
using GameCube.GX.Texture;
using Manifold.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameCube.GFZ.Replay
{
    public class CustomMachine :
        IBitSerializable
    {
        // numbers in BYTES
        private byte[] unknown1; // 7
        private byte colorID; // 1
        private byte emblemCount; // 1
        private byte[] unknown2; // 7
        private byte[] speedSettings; // 7
        private byte[] unknown3; // 16
        private byte[][] emblemData = new byte[4][]; // 4*8288
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
            unknown1 = reader.ReadBytes(7); // bytes
            colorID = reader.ReadByte(8);
            emblemCount = reader.ReadByte(8);
            unknown2 = reader.ReadBytes(7); // bytes
            speedSettings = reader.ReadBytes(7); // bytes
            unknown3 = reader.ReadBytes(16); // bytes
            for (int i = 0; i < emblemData.Length; i++)
                emblemData[i] = reader.ReadBytes(8288); // bytes
            pilotID = (PilotID)reader.ReadUInt(32);
            bodyID = (CustomBodyPartName)reader.ReadUInt(32);
            bodyColor = new GXColor(reader.ReadUInt(32));
            cockpitID = (CustomCockpitPartName)reader.ReadUInt(32);
            cockpitColor = new GXColor(reader.ReadUInt(32));
            boosterID = (CustomBoosterPartName)reader.ReadUInt(32);
            boosterColor = new GXColor(reader.ReadUInt(32));
        }

        public void Serialize(BitStreamWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
