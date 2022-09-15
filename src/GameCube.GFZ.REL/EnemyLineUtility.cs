using Manifold;
using Manifold.IO;
using GameCube.GFZ;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace GameCube.GFZ.REL
{
    public static class EnemyLineUtility
    {
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

        public static void PatchVenueIndex(EndianBinaryWriter writer, EnemyLineAddressLookup regionLUT, Venue venue, byte value)
        {
            int address = regionLUT.VenueIndexesU8Address + (byte)venue;
            writer.JumpToAddress(address);
            writer.Write(value);
        }

        public static void PatchStageName(EndianBinaryWriter writer, EnemyLineAddressLookup regionLUT, byte venueIndex, string value)
        {
            const int stride = 16;
            int address = regionLUT.VenueIndexesU8Address + (venueIndex * stride);
            writer.JumpToAddress(address);
            writer.Write<ShiftJisCString>(value);
        }

        public static MemoryStream Crypt(Stream file)
        {
            // The code below just copies the stream over to a MemoryStream
            //var memoryStream = new MemoryStream((int)file.Length);
            //file.CopyTo(memoryStream);
            //return memoryStream;

            throw new NotImplementedException();
        }
        public static MemoryStream Decrypt(Stream file) => Crypt(file);
        public static MemoryStream Encrypt(Stream file) => Crypt(file);

    }
}
