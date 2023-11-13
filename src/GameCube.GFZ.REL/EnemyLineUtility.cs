using Manifold;
using Manifold.IO;
using GameCube.GFZ;
using GameCube.GFZ.CarData;
using System;
using System.IO;
using System.Linq;

namespace GameCube.GFZ.REL
{
    public class EnemyLineUtility
    {
        private static int lwz(EndianBinaryReader reader, int offset, int src)
        {
            reader.JumpToAddress(src + offset);
            int value =  reader.ReadInt32();
            return value;
        }
        private static void stw(EndianBinaryWriter writer, int val, int offset, int des)
        {
            writer.JumpToAddress(des + offset);
            writer.Write(val);
        }
        private static int mulhwu(int val1, int val2)
        {
            Int64 result = (Int64)val1 * (Int64)val2;
            result >>= 32;
            return (int)result;
        }
        private static int rlwinm(int src, int shift, int maskBeg, int maskEnd)
        {
            //inspired by rygorous' example
            UInt32 maskBegBin = 0xffffffff >> maskBeg;
            UInt32 maskEndBin = 0xffffffff << (31 - maskEnd);
            UInt32 bitMask = (maskBeg <= maskEnd) ? maskBegBin & maskEndBin : maskBegBin | maskEndBin;
            return (int)(((src << shift) | (src >> ((32 - shift) & 31))) & bitMask);
        }


        // TODO: combine? not much use if the stream versionrequires +0x18
        public static MemoryStream Crypt(string filePath, EnemyLineDataBlocks lookup)
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            byte[] streamData = new byte[fileData.Length + 0x18];
            fileData.CopyTo(streamData, 0); 
            using MemoryStream data = new MemoryStream(streamData);
            var stream = Crypt(data, lookup);
            return stream;
        }
        public static MemoryStream Crypt(MemoryStream data, EnemyLineDataBlocks lookup)
        {
            // Wrap data in reader and writer for BE operations
            EndianBinaryReader rdata = new EndianBinaryReader(data, EnemyLine.endianness);
            EndianBinaryWriter wdata = new EndianBinaryWriter(data, EnemyLine.endianness);
            
            int salt = lookup.Salt;
            int key0 = lookup.Key0;
            int key1 = lookup.Key1;
            int key2 = lookup.Key2;
            int r8 = rlwinm((int)data.Length, 30, 2, 31);
            int cryptedWords = 0;
            int r11 = 0;
            int r12 = 0;
            int r3 = r8 - 1;
            int rwLocation = 0;
            r3 = rlwinm(r3, 29, 3, 31);

            for (int i = r3; i > 0; --i)
            {
                //decrypts/encrypts each block of 0x20 bytes in 4-bytes strides
                int keyProd = key2 * key1;
                r3 = lwz(rdata, 0, rwLocation);
                int r29 = keyProd + key0;
                r3 ^= r29;
                stw(wdata, r3, 0, rwLocation);

                r12 = mulhwu(key2, key1);
                int val = lwz(rdata, 4, rwLocation);
                r3 = 0;
                r11 = mulhwu(r29, key1);
                int r26 = r12;
                int r27 = key2 * salt;
                r12 = r11;
                r26 += r27;
                r11 = r29 * salt;
                r27 = r29 * key1;
                r12 += r11;
                r29 = r27 + key0;
                r12 = r29 * key1;
                val ^= r29;
                stw(wdata, val, 4, rwLocation);

                val = lwz(rdata, 8, rwLocation);
                key2 = r12 + key0;
                r11 = mulhwu(r29, key1);
                val ^= key2;
                stw(wdata, val, 8, rwLocation);

                val = lwz(rdata, 0xC, rwLocation);
                r12 = r29 * salt;
                r27 = r11;
                r12 = key2 * key1;
                r29 = r12 + key0;
                r11 = mulhwu(key2, key1);
                val ^= r29;
                stw(wdata, val, 0xC, rwLocation);

                val = lwz(rdata, 0x10, rwLocation);
                r12 = key2 * salt;
                r27 = r11;
                r12 = r29 * key1;
                key2 = r12 + key0;
                r11 = mulhwu(r29, key1);
                val ^= key2;
                stw(wdata, val, 0x10, rwLocation);

                val = lwz(rdata, 0x14, rwLocation);
                r12 = r29 * salt;
                r27 = r11;
                r12 = key2 * key1;
                r27 = r12 + key0;
                r11 = mulhwu(key2, key1);
                val ^= r27;
                stw(wdata, val, 0x14, rwLocation);

                val = lwz(rdata, 0x18, rwLocation);
                r12 = key2 * salt;
                r29 = r11;
                r12 = r27 * key1;
                r29 = r12 + key0;
                r11 = mulhwu(r27, key1);
                val ^= r29;
                stw(wdata, val, 0x18, rwLocation);

                val = lwz(rdata, 0x1C, rwLocation);
                r12 = r27 * salt;
                key2 = r11;
                r12 += key2;
                r11 = mulhwu(r29, key1);
                r12 = r29 * key1;
                r11 += r3;
                key2 = r12 + key0;
                r3 = val ^ key2;
                stw(wdata, r3, 0x1C, rwLocation);

                salt *= r29;
                rwLocation += 0x20;
                r3 = r11 + salt;
                cryptedWords += 8;
            }

            if (cryptedWords < r8)
            {
                //set parameters for last block
                r3 = lookup.BlockKey0;
                rwLocation = rlwinm(cryptedWords, 2, 0, 29);
                r11 = r3 + lookup.BlockKey1;
                r3 = 0x000d0000;
                int r0 = r8 - cryptedWords;
                r3 += lookup.BlockKey2;

                //crypt last block
                for (int i = r0; i > 0; --i)
                {
                    r0 = lwz(rdata, 0, rwLocation);
                    int r7 = key2 * r11;
                    key2 = r7 + r3;
                    r0 ^= key2;
                    stw(wdata, r0, 0, rwLocation);
                    rwLocation += 4;
                }
            }

            byte[] bytes = data.ToArray()[0..^0x18];
            MemoryStream trimmed = new MemoryStream(bytes);
            return trimmed;
        }

        private static void ValidateStageIndex(int index, int maxIndex, int minIndex = 0)
        {
            bool tooSmall = index < minIndex;
            bool tooBig = index > maxIndex;
            if (tooSmall || tooBig)
                throw new IndexOutOfRangeException($"Index must be between {minIndex} and {maxIndex}. ({index})");
        }

        public static void PatchCustomCourseName(EndianBinaryWriter writer, EnemyLineDataBlocks lookup, int index, byte[] courseName)
        {
            ValidateStageIndex(index, 110);

            int newLength = courseName.Length + 4 - (courseName.Length % 4);
            byte[] courseNameExtended = new byte[newLength];
            Buffer.BlockCopy(courseName, 0, courseNameExtended, 0, courseName.Length);

            //Debug.Log(lookup.CourseNameAreas.Count);
            for (int i = 0; i < lookup.CourseNameAreas.Count; ++i)
            {
                //Debug.Log(lookup.CourseNameAreas[i]);
                if (lookup.CourseNameAreas[i].Occupied + newLength <= lookup.CourseNameAreas[i].Size)
                {
                    int nameAddress = lookup.CourseNameAreas[i].Occupied + lookup.CourseNameAreas[i].Address;
                    writer.JumpToAddress(nameAddress);
                    writer.Write(courseNameExtended);

                    int indexAddress = lookup.CourseNameOffsetStructs.Address + (int)index * 0x30;
                    writer.JumpToAddress(indexAddress);
                    writer.Write(nameAddress - lookup.CourseNamePointerOffsetBase);

                    lookup.CourseNameAreas[i].Occupied += newLength;
                    return;
                }
            }

            throw new System.IndexOutOfRangeException("No more free space for course names");
        }
        public static void PatchCustomCourseName(EndianBinaryWriter writer, EnemyLineDataBlocks lookup, int index, ShiftJisCString courseName)
        {
            var bytes = ShiftJisCString.shiftJis.GetBytes(courseName);
            PatchCustomCourseName(writer, lookup, index, bytes);
        }

        public static void PatchCustomMinimapParameters(EndianBinaryWriter writer, EnemyLineDataBlocks lookup, int index, float[] minimapParams)
        {
            ValidateStageIndex(index, 45);

            writer.JumpToAddress(lookup.CourseMinimapParameterStructs.Address + MinimapProjection.Size * index);
            writer.Write(minimapParams);
        }

        public static void PatchBgmToSlot(EndianBinaryWriter writer, EnemyLineDataBlocks lookup, int index, byte trackId)
        {
            if (index > 55)
            {
                throw new IndexOutOfRangeException("Index must be between 0 and 55");
            }

            if (trackId > 96 && trackId < 255)
            {
                throw new ArgumentException("Track ID must be between 0 and 96, or 255");
            }

            writer.JumpToAddress(lookup.CourseSlotBgm.Address + (int)index);
            writer.Write(trackId);
        }

        public static void PatchFinalLapBgmToSlot(EndianBinaryWriter writer, EnemyLineDataBlocks lookup, int index, byte trackId)
        {
            ValidateStageIndex(index, 45);

            if (trackId > 96 && trackId < 255)
            {
                throw new ArgumentException("Track ID must be between 0 and 96, or 255");
            }

            writer.JumpToAddress(lookup.CourseSlotBgmFinalLap.Address + index * 4);
            writer.Write(trackId);
        }

        public static void PatchVenueIndex(EndianBinaryWriter writer, EnemyLineDataBlocks lookup, int index, Venue venue)
        {
            ValidateStageIndex(index, 110);

            if ((byte)venue > 0x14)
            {
                throw new ArgumentException("Invalid Venue");
            }

            writer.JumpToAddress(lookup.SlotVenueDefinitions.Address + (int)index);
            writer.Write((byte)venue);
        }

        public static void PatchDifficultyRatingToSlot(EndianBinaryWriter writer, EnemyLineDataBlocks lookup, int index, byte difficulty)
        {
            ValidateStageIndex(index, 110);

            if (difficulty > 24)
            {
                //Debug.LogWarning("More than 24 stars cannot be displayed");
                throw new ArgumentException("More than 24 stars cannot be displayed");
            }

            writer.JumpToAddress(lookup.CourseSlotDifficulty.Address + (int)index);
            writer.Write(difficulty);
        }

        public static void PatchCupSlot(EndianBinaryWriter writer, EnemyLineDataBlocks lookup, Cup cup, int courseIndex)
        {
            ValidateStageIndex(courseIndex, 110, -1);

            // Offset between cups
            Offset offset = (int)cup * 0xC; // 12

            // Patch stage index in cup
            writer.JumpToAddress(lookup.CupCourseLut.Address + offset);
            writer.Write(courseIndex);
            // Patch index for loading in assets (GMA, TPL)
            writer.JumpToAddress(lookup.CupCourseLutAssets.Address + offset);
            writer.Write(courseIndex);
            // 
            writer.JumpToAddress(lookup.CupCourseLutUnk.Address + offset);
            writer.Write(courseIndex);
        }

        public static void PatchCupSlots(EndianBinaryWriter writer, EnemyLineDataBlocks lookup, Cup cup, Int16[] courses)
        {
            if (courses.Length < 1 || courses.Length > 6)
            {
                throw new IndexOutOfRangeException("At least 1 or up to 6 courses must be defined");
            }

            if ((byte)cup > 10)
            {
                throw new ArgumentException("Invalid Cup");
            }

            for (int i = 0; i < courses.Length; ++i)
            {
                if (courses[i] > 110 || courses[i] < -1)
                {
                    throw new ArgumentException("Invalid course ID");
                }
            }

            if (courses.Length < 6)
            {
                do
                {
                    courses = courses.Concat(new Int16[] { -1 }).ToArray();
                }
                while (courses.Length < 6);
            }

            writer.JumpToAddress(lookup.CupCourseLut.Address + (int)cup * 0xC);
            writer.Write(courses);
            writer.JumpToAddress(lookup.CupCourseLutAssets.Address + (int)cup * 0xC);
            writer.Write(courses);
            writer.JumpToAddress(lookup.CupCourseLutUnk.Address + (int)cup * 0xC);
            writer.Write(courses);
        }

        public static void PatchAxTimer(EndianBinaryWriter writer, EnemyLineDataBlocks lookup, AcCupCourse courseId, byte seconds)
        {
            if ((byte)courseId > 6)
            {
                throw new ArgumentException("Invalid Course ID");
            }

            writer.JumpToAddress(lookup.AxModeCourseTimers.Address + (int)courseId);
            writer.Write(seconds);
        }

        public static void PatchPilotPosition(EndianBinaryWriter writer, EnemyLineDataBlocks lookup, PilotID id, float[] position)
        {
            if (id > PilotID.Gen)
            {
                throw new ArgumentException("Invalid Pilot ID");
            }

            if (position.Length != 3)
            {
                throw new ArgumentException("Position array must contain 3 elements");
            }

            writer.JumpToAddress(lookup.PilotPositions.Address + (int)id * 0xc);
            writer.Write(position);
        }

        public static void PatchPilotToMachine(EndianBinaryWriter writer, EnemyLineDataBlocks lookup, Machine machine, PilotID pilot)
        {
            if (pilot > PilotID.Gen)
            {
                throw new ArgumentException("Invalid Pilot ID");
            }

            if (machine > Machine.RainbowPheonix)
            {
                throw new ArgumentException("Invalid Machine ID");
            }

            if (pilot > PilotID.Pheonix)
            {
                throw new ArgumentException($"ID: {pilot} will only work with Free Run Races! Different Race Settings will freeze the game!");
            }

            PatchPilotPosition(writer, lookup, pilot, PilotPosition.Default[(int)machine].Position);

            writer.JumpToAddress(lookup.PilotToMachineLut.Address + (int)machine * 4);
            writer.Write((int)pilot);
        }

    }
}
