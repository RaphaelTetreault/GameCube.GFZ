using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.REL;

/// <summary>
///     Encryption/Decryption information for GX's <b>fz.main.rel</b> which
///     is hidden as <b>./enemy_line/line__.bin</b>.
/// </summary>
/// <remarks>
///     Huge shoutout to LawnMeower for finding this code and porting it to C#.
///     Thank you! ♥
/// </remarks>
public readonly record struct FzMainCrypter
{
    public const Endianness Endianness = Endianness.BigEndian;

    /// <summary>
    ///     Encryption/Decryption salt.
    /// </summary>
    public short Salt { get; init; }

    /// <summary>
    ///     Encryption/Decryption key 0.
    /// </summary>
    public int Key0 { get; init; }

    /// <summary>
    ///     Encryption/Decryption key 1.
    /// </summary>
    public int Key1 { get; init; }

    /// <summary>
    ///     Encryption/Decryption key 2.
    /// </summary>
    public int Key2 { get; init; }

    /// <summary>
    ///     Encryption/Decryption block key 0.
    /// </summary>
    public int BlockKey0 { get; init; }

    /// <summary>
    ///     Encryption/Decryption block key 1.
    /// </summary>
    public short BlockKey1 { get; init; }

    /// <summary>
    ///     Encryption/Decryption block key 2.
    /// </summary>
    public short BlockKey2 { get; init; }


    /// <summary>
    ///     
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns>
    ///     
    /// </returns>
    public MemoryStream Crypt(string filePath) => Crypt(filePath, this);

    /// <summary>
    ///     Bi-directional encrypt/decrypt of file <see cref="FzMainRel"/>.
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="lookup"></param>
    /// <returns>
    ///     
    /// </returns>
    public static MemoryStream Crypt(string filePath, FzMainCrypter lookup)
    {
        const int alignment = 0x18; // Padding for alignment to 0x20

        // Prepare data stream
        byte[] fileData = File.ReadAllBytes(filePath);
        int length = fileData.Length + alignment;
        byte[] streamData = new byte[length];
        fileData.CopyTo(streamData, 0);
        using MemoryStream data = new(streamData);

        // Wrap data in reader and writer for BE operations
        EndianBinaryReader rdata = new(data, FzMainCrypter.Endianness);
        EndianBinaryWriter wdata = new(data, FzMainCrypter.Endianness);

        int salt = lookup.Salt;
        int key0 = lookup.Key0;
        int key1 = lookup.Key1;
        int key2 = lookup.Key2;
        int r8 = PPC_rlwinm((int)data.Length, 30, 2, 31);
        int cryptedWords = 0;
        int r11 = 0;
        int r12 = 0;
        int r3 = r8 - 1;
        int rwLocation = 0;
        r3 = PPC_rlwinm(r3, 29, 3, 31);

        for (int i = r3; i > 0; --i)
        {
            //decrypts/encrypts each block of 0x20 bytes in 4-bytes strides
            int keyProd = key2 * key1;
            r3 = PPC_lwz(rdata, 0, rwLocation);
            int r29 = keyProd + key0;
            r3 ^= r29;
            PPC_stw(wdata, r3, 0, rwLocation);

            r12 = PPC_mulhwu(key2, key1);
            int val = PPC_lwz(rdata, 4, rwLocation);
            r3 = 0;
            r11 = PPC_mulhwu(r29, key1);
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
            PPC_stw(wdata, val, 4, rwLocation);

            val = PPC_lwz(rdata, 8, rwLocation);
            key2 = r12 + key0;
            r11 = PPC_mulhwu(r29, key1);
            val ^= key2;
            PPC_stw(wdata, val, 8, rwLocation);

            val = PPC_lwz(rdata, 0xC, rwLocation);
            r12 = r29 * salt;
            r27 = r11;
            r12 = key2 * key1;
            r29 = r12 + key0;
            r11 = PPC_mulhwu(key2, key1);
            val ^= r29;
            PPC_stw(wdata, val, 0xC, rwLocation);

            val = PPC_lwz(rdata, 0x10, rwLocation);
            r12 = key2 * salt;
            r27 = r11;
            r12 = r29 * key1;
            key2 = r12 + key0;
            r11 = PPC_mulhwu(r29, key1);
            val ^= key2;
            PPC_stw(wdata, val, 0x10, rwLocation);

            val = PPC_lwz(rdata, 0x14, rwLocation);
            r12 = r29 * salt;
            r27 = r11;
            r12 = key2 * key1;
            r27 = r12 + key0;
            r11 = PPC_mulhwu(key2, key1);
            val ^= r27;
            PPC_stw(wdata, val, 0x14, rwLocation);

            val = PPC_lwz(rdata, 0x18, rwLocation);
            r12 = key2 * salt;
            r29 = r11;
            r12 = r27 * key1;
            r29 = r12 + key0;
            r11 = PPC_mulhwu(r27, key1);
            val ^= r29;
            PPC_stw(wdata, val, 0x18, rwLocation);

            val = PPC_lwz(rdata, 0x1C, rwLocation);
            r12 = r27 * salt;
            key2 = r11;
            r12 += key2;
            r11 = PPC_mulhwu(r29, key1);
            r12 = r29 * key1;
            r11 += r3;
            key2 = r12 + key0;
            r3 = val ^ key2;
            PPC_stw(wdata, r3, 0x1C, rwLocation);

            salt *= r29;
            rwLocation += 0x20;
            r3 = r11 + salt;
            cryptedWords += 8;
        }

        if (cryptedWords < r8)
        {
            //set parameters for last block
            r3 = lookup.BlockKey0;
            rwLocation = PPC_rlwinm(cryptedWords, 2, 0, 29);
            r11 = r3 + lookup.BlockKey1;
            r3 = 0x000d0000;
            int r0 = r8 - cryptedWords;
            r3 += lookup.BlockKey2;

            //crypt last block
            for (int i = r0; i > 0; --i)
            {
                r0 = PPC_lwz(rdata, 0, rwLocation);
                int r7 = key2 * r11;
                key2 = r7 + r3;
                r0 ^= key2;
                PPC_stw(wdata, r0, 0, rwLocation);
                rwLocation += 4;
            }
        }

        byte[] bytes = data.ToArray()[0..^alignment];
        MemoryStream dataNoPadding = new(bytes);
        return dataNoPadding;
    }

    /// <summary>
    ///     PowerPC op "Load Word and Zero".
    ///     <see href="https://www.ibm.com/docs/en/aix/7.3.0?topic=set-lwz-l-load-word-zero-instruction"/>
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="offset"></param>
    /// <param name="src"></param>
    /// <returns>
    /// 
    /// </returns>
    private static int PPC_lwz(EndianBinaryReader reader, int offset, int src)
    {
        reader.JumpToAddress(src + offset);
        int value = reader.ReadInt32();
        return value;
    }

    /// <summary>
    ///     PowerPC op "Store".
    ///     <see href="https://www.ibm.com/docs/en/aix/7.3.0?topic=set-stw-st-store-instruction"/>
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="val"></param>
    /// <param name="offset"></param>
    /// <param name="des"></param>
    private static void PPC_stw(EndianBinaryWriter writer, int val, int offset, int des)
    {
        writer.JumpToAddress(des + offset, true);
        writer.Write(val);
    }

    /// <summary>
    ///     PowerPC op "Multiply High Word Unsigned".
    ///     <see href="https://www.ibm.com/docs/en/aix/7.1.0?topic=set-mulhwu-multiply-high-word-unsigned-instruction"/>
    /// </summary>
    /// <param name="val1"></param>
    /// <param name="val2"></param>
    /// <returns>
    /// 
    /// </returns>
    private static int PPC_mulhwu(int val1, int val2)
    {
        Int64 result = (Int64)val1 * (Int64)val2;
        result >>= 32;
        return (int)result;
    }

    /// <summary>
    ///     PowerPC op "Rotate Left Word Immediate Then AND with Mask".
    ///     <see href="https://www.ibm.com/docs/en/aix/7.1.0?topic=is-rlwinm-rlinm-rotate-left-word-immediate-then-mask-instruction"/>
    /// </summary>
    /// <param name="src"></param>
    /// <param name="shift"></param>
    /// <param name="maskBeg"></param>
    /// <param name="maskEnd"></param>
    /// <returns>
    /// 
    /// </returns>
    private static int PPC_rlwinm(int src, int shift, int maskBeg, int maskEnd)
    {
        //inspired by rygorous' example
        UInt32 maskBegBin = 0xffffffff >> maskBeg;
        UInt32 maskEndBin = 0xffffffff << (31 - maskEnd);
        UInt32 bitMask = (maskBeg <= maskEnd) ? maskBegBin & maskEndBin : maskBegBin | maskEndBin;
        return (int)(((src << shift) | (src >> ((32 - shift) & 31))) & bitMask);
    }
}
