using Manifold.IO;
using System;

namespace GameCube.GFZ.CarData;

/// <summary>
///     
/// </summary>
public struct CarDataPadding :
    IBinarySerializable
{
    public const int PaddingCount = 0x26;

    public static readonly byte[] Padding =
    [
        0x00, 0x30, 0x31, 0x32,
        0x33, 0x34, 0x35, 0x36,
        0x37, 0x38, 0x39, 0x30,
        0x31, 0x32, 0x33, 0x34,
        0x35, 0x36, 0x37, 0x38,
        0x39, 0x30, 0x31, 0x32,
        0x33, 0x34, 0x35, 0x36,
        0x37, 0x38, 0x39, 0x30,
        0x31, 0x32, 0x33, 0x34,
        0x35, 0x00,
    ];

    public readonly void Deserialize(EndianBinaryReader reader)
    {
        // Read what should be padding
        var buffer = Array.Empty<byte>();
        reader.Read(ref buffer, PaddingCount);

        // Assert padding
        var expectedValue = Padding;
        for (int i = 0; i < PaddingCount; i++)
        {
            Assert.IsTrue(buffer[i] == expectedValue[i]);
        }
    }

    public readonly void Serialize(EndianBinaryWriter writer)
    {
        writer.Write(Padding);
    }
}