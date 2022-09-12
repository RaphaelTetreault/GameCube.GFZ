using Manifold;
using Manifold.IO;
using GameCube.GFZ;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace GameCube.GFZ.REL
{
    public class EnemyLineUtility
    {
        private static byte[] _lineBinary;
        private static BinaryReader _reader;
        private static int lwz(int offset, int src)
        {
            byte[] data = new byte[4];
            Buffer.BlockCopy(_lineBinary, src + offset, data, 0, 4);
            Array.Reverse(data);
            return BitConverter.ToInt32(data, 0);
        }

        private static void stw(int val, int offset, int des)
        {
            byte[] data = new byte[4];
            data = BitConverter.GetBytes(val);
            Array.Reverse(data);
            Buffer.BlockCopy(data, 0, _lineBinary, des + offset, 4);
        }

        private static int mulhwu(int val1, int val2)
        {
            Int64 result = (Int64)val1 * (Int64)val2;
            result >>= 32;
            return (int)result;
        }

        private static int rlwinm(int src, int shift, int maskBeg, int maskEnd)
        {   //inspired by rygorous' example
            UInt32 maskBegBin = 0xffffffff >> maskBeg;
            UInt32 maskEndBin = 0xffffffff << (31 - maskEnd);
            UInt32 bitMask = (maskBeg <= maskEnd) ? maskBegBin & maskEndBin : maskBegBin | maskEndBin;

            return (int)(((src << shift) | (src >> ((32 - shift) & 31))) & bitMask);
        }

        public static void TestPatchEncrypted(string filePath)
        {
            // Gets called with "(source)/enemy_line/line__.bin" if the file exists.
            var enemyLine = EnemyLine.Open(filePath);
        }
        public static void TestPatchDecrypted(string filePath)
        {
            // Gets called with "(working)/enemy_line/line__.rel" if the file exists.
            //
            // This file needs to be made. Once Crypt() is implemented,
            // you can run the menu item to decrypt it to your working dir,
            // then the will be loaded here.
            var enemyLine = EnemyLine.Open(filePath);
        }

        public static MemoryStream Crypt(Stream file, bool isJPN)
        {
            _reader = new BinaryReader(file);
            int fileSize = (int)file.Length;
            int fileSizeExtended = fileSize + 0x1f;
            _lineBinary = new byte[fileSizeExtended];
            _reader.BaseStream.Seek(0, SeekOrigin.Begin);
            _reader.Read(_lineBinary, 0, fileSize);
            int saltUSA = 0x180a;
            int saltJPN = 0x0ce0;
            int salt = 0;
            int key0USA = 0x000cd8f3;
            int key0JPN = 0x0004f107;
            int key0 = isJPN ? key0JPN : key0USA;
            int key1USA = unchecked ((int)0x9b36bb94);
            int key1JPN = unchecked ((int)0xb5fb6483);
            int key1 = isJPN ? key1JPN : key1USA;
            int key2USA = unchecked ((int)0xaf8910be);
            int key2JPN = unchecked ((int)0xdeaddead);
            int key2 = isJPN ? key2JPN : key2USA;
            int r8 = rlwinm(fileSizeExtended, 30, 2, 31);
            int cryptedWords = 0;
            int r11 = 0;
            int r12 = 0;
            int r3 = r8 - 1;
            int rwLocation = 0;
            r3 = rlwinm(r3, 29, 3, 31);

            for (int i = r3; i > 0; --i)
            {   //decrypts/encrypts each block of 0x20 bytes in 4-bytes strides
                int keyProd = key2 * key1;
                salt = isJPN ? saltJPN : saltUSA;
                r3 = lwz(0, rwLocation);
                int r29 = keyProd + key0;
                r3 ^= r29;
                stw(r3, 0, rwLocation);

                r12 = mulhwu(key2, key1);
                int val = lwz(4, rwLocation);
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
                stw(val, 4, rwLocation);

                val = lwz(8, rwLocation);
                key2 = r12 + key0;
                r11 = mulhwu(r29, key1);
                val ^= key2;
                stw(val, 8, rwLocation);

                val = lwz(0xC, rwLocation);
                r12 = r29 * salt;
                r27 = r11;
                r12 = key2 * key1;
                r29 = r12 + key0;
                r11 = mulhwu(key2, key1);
                val ^= r29;
                stw(val, 0xC, rwLocation);

                val = lwz(0x10, rwLocation);
                r12 = key2 * salt;
                r27 = r11;
                r12 = r29 * key1;
                key2 = r12 + key0;
                r11 = mulhwu(r29, key1);
                val ^= key2;
                stw(val, 0x10, rwLocation);

                val = lwz(0x14, rwLocation);
                r12 = r29 * salt;
                r27 = r11;
                r12 = key2 * key1;
                r27 = r12 + key0;
                r11 = mulhwu(key2, key1);
                val ^= r27;
                stw(val, 0x14, rwLocation);

                val = lwz(0x18, rwLocation);
                r12 = key2 * salt;
                r29 = r11;
                r12 = r27 * key1;
                r29 = r12 + key0;
                r11 = mulhwu(r27, key1);
                val ^= r29;
                stw(val, 0x18, rwLocation);

                val = lwz(0x1C, rwLocation);
                r12 = r27 * salt;
                key2 = r11;
                r12 += key2;
                r11 = mulhwu(r29, key1);
                r12 = r29 * key1;
                r11 += r3;
                key2 = r12 + key0;
                r3 = val ^ key2;
                stw(r3, 0x1C, rwLocation);

                salt *= r29;
                rwLocation += 0x20;
                r3 = r11 + salt;
                cryptedWords += 8;
            }

            if (cryptedWords < r8)
            {
                //set parameters for last block
                r3 = isJPN ? unchecked ((int)0xb5fb0000) : unchecked ((int)0x9b370000);
                rwLocation = rlwinm(cryptedWords, 2, 0, 29);
                r11 = r3 + (isJPN ? unchecked ((short)0x6483) : unchecked ((short)0xbb94));
                r3 = 0x000d0000;
                int r0 = r8 - cryptedWords;
                r3 += isJPN ? unchecked ((short)0xf107) : unchecked ((short)0xd8f3);
            
                //crypt last block
                for (int i = r0; i > 0; --i)
                {
                    r0 = lwz(0, rwLocation);
                    int r7 = key2 * r11;
                    key2 = r7 + r3;
                    r0 ^= key2;
                    stw(r0, 0, rwLocation);
                    rwLocation += 4;
                }
            }

            Array.Resize(ref _lineBinary, fileSize);
            file = new MemoryStream(_lineBinary);
            var memoryStream = new MemoryStream((int)fileSize);
            file.CopyTo(memoryStream);
            return memoryStream;
        }
        public static MemoryStream Decrypt(Stream file, bool isJPN) => Crypt(file, isJPN);
        public static MemoryStream Encrypt(Stream file, bool isJPN) => Crypt(file, isJPN);

        public static void PatchCustomCourseName(EndianBinaryWriter writer, EnemyLineInformationLookup lookup, uint index, byte[] courseName)
        {
            if(index > 110)
        {
                throw new System.IndexOutOfRangeException("Index must be between 0 and 110");
        }

            int newLength = courseName.Length + 4 - (courseName.Length % 4);
            byte[] courseNameExtended = new byte[newLength];
            Buffer.BlockCopy(courseName, 0, courseNameExtended, 0, courseName.Length);

            Debug.LogError(lookup.CourseNameAreas.Count);
            for(int i =  0; i < lookup.CourseNameAreas.Count; ++i)
        {
                Debug.LogError(lookup.CourseNameAreas[i]);
                if(lookup.CourseNameAreas[i].Occupied + newLength <= lookup.CourseNameAreas[i].Size)
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

        public static void PatchCustomMinimapParameters(EndianBinaryWriter writer, EnemyLineInformationLookup lookup, uint index, float[] minimapParams)
        {
            if(index > 45)
            {
                throw new System.IndexOutOfRangeException("Index must be between 0 and 45");
            }

            writer.JumpToAddress(lookup.CourseMinimapParameterStructs.Address + (7 * 4) * (int)index);
            writer.Write(minimapParams);
        }

        public static void PatchBgmToSlot(EndianBinaryWriter writer, EnemyLineInformationLookup lookup, uint index, byte trackId)
        {
            if(index > 55)
            {
                throw new System.IndexOutOfRangeException("Index must be between 0 and 55");
            }

            if(trackId > 96 && trackId < 255)
            {
                throw new System.ArgumentException("Track ID musst be between 0 and 96, or 255");
            }

            writer.JumpToAddress(lookup.CourseSlotBgm.Address + (int)index);
            writer.Write(trackId);
        }

        public static void PatchFinalLapBgmToSlot(EndianBinaryWriter writer, EnemyLineInformationLookup lookup, uint index, byte trackId)
        {
            if(index > 45)
            {
                throw new System.IndexOutOfRangeException("Index must be between 0 and 45");
            }

            if(trackId > 96 && trackId < 255)
            {
                throw new System.ArgumentException("Track ID musst be between 0 and 96, or 255");
            }

            writer.JumpToAddress(lookup.CourseSlotBgmFinalLap.Address + (int)index * 4);
            writer.Write(trackId);
        }

        public static void PatchVenueIndex(EndianBinaryWriter writer, EnemyLineInformationLookup lookup, uint index, GameCube.GFZ.REL.Venue venue)
        {
            if(index > 110)
            {
                throw new System.IndexOutOfRangeException("Index must be between 0 and 110");
            }

            if((byte)venue > 0x14)
            {
                throw new System.ArgumentException("Track ID musst be between 0 and 96, or 255");
            }

            writer.JumpToAddress(lookup.SlotVenueDefinitions.Address + (int)index);
            writer.Write((byte)venue);
        }
    }
}
