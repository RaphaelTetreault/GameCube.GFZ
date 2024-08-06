using Manifold.IO;
using System;

namespace GameCube.GFZ.Replay
{
    public class Checkpoint :
        IBitSerializable
    {
        ushort frameNumber0;
        ushort frameNumber1;
        byte unknown1;
        byte unknown2;
        byte unknown3;
        byte unknown4;
        byte unknown5;
        byte unknown6;
        float position1x;
        float position1y;
        float position1z;
        float position2x;
        float position2y;
        float position2z;
        float position3x;
        float position3y;
        float position3z;
        float position4x;
        float position4y;
        float position4z;

        public void Deserialize(BitStreamReader reader)
        {
            reader.Read(ref frameNumber0, 14);
            reader.Read(ref frameNumber1, 14);
            reader.Read(ref unknown1, 4);
            reader.Read(ref unknown2, 5);
            reader.Read(ref unknown3, 5);
            reader.Read(ref unknown4, 5);
            reader.Read(ref unknown5, 5);
            reader.Read(ref unknown6, 5);
            reader.Read(ref position1x);
            reader.Read(ref position1y);
            reader.Read(ref position1z);
            reader.Read(ref position2x);
            reader.Read(ref position2y);
            reader.Read(ref position2z);
            reader.Read(ref position3x);
            reader.Read(ref position3y);
            reader.Read(ref position3z);
            reader.Read(ref position4x);
            reader.Read(ref position4y);
            reader.Read(ref position4z);
        }

        public void Serialize(BitStreamWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
