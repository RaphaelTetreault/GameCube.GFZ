using GameCube.GFZ.CarData;
using Manifold.IO;
using System;

namespace GameCube.GFZ.Replay
{
    // https://github.com/JoselleAstrid/fzerogx-docs/blob/master/file_formats/gci_replay.md#racer-array

    public class RaceParticipant :
        IBitSerializable
    {
        private bool isHuman; // 1, 1
        private MachineID machineID; // 6, 7
        private byte gridIndex; // 5, 12
        private byte graphConsoleSetting; // 7, 19
        private byte colorPalette; // 2, 21
        private byte unknown1; // 7, 28
        private bool isCustomMachine; // 1, 29
        private CustomMachine customMachineData = new(); // 8*33216, nullable

        public void Deserialize(BitStreamReader reader)
        {
            isHuman = reader.ReadBool();
            machineID = (MachineID)reader.ReadByte(6);
            gridIndex = reader.ReadByte(5);
            graphConsoleSetting = reader.ReadByte(7);
            colorPalette = reader.ReadByte(2);
            unknown1 = reader.ReadByte(7);
            isCustomMachine = reader.ReadBool();
            if (isCustomMachine)
                reader.Read(ref customMachineData);
        }

        public void Serialize(BitStreamWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
