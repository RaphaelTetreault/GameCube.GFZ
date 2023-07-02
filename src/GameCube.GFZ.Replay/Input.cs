using Manifold.IO;

namespace GameCube.GFZ.Replay
{
    public class Input :
        IBitSerializable
    {
        private byte buttons; // split into 8 bools?
        private byte strafe;
        private byte acceleration;
        private byte brake;
        private byte frameCount; // count == count-1
        private byte steerX;
        private byte steerY;


        public void Deserialize(BitStreamReader reader)
        {
            reader.Read(ref buttons, 8);
            reader.Read(ref strafe, 8);
            reader.Read(ref acceleration, 7);
            reader.Read(ref brake, 7);
            reader.Read(ref frameCount, 8);
            reader.Read(ref steerX, 8);
            reader.Read(ref steerY, 8);
        }

        public void Serialize(BitStreamWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
