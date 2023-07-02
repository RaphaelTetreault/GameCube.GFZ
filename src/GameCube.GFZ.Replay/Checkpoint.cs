using Manifold.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCube.GFZ.Replay
{
    public class Checkpoint :
        IBitSerializable
    {
        public void Deserialize(BitStreamReader reader)
        {
            
        }

        public void Serialize(BitStreamWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
