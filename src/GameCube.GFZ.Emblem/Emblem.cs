using GameCube.GX.Texture;
using Manifold.IO;
using System;

namespace GameCube.GFZ.Emblem;

/// <summary>
///     An F-Zero GX custom machine emblem.
/// </summary>
/// <remarks>
///     An emblem is really just a specific spec for a GameCube GC texture.
/// </remarks>
public class Emblem :
    IBinaryAddressable,
    IBinarySerializable
{
    // Consts
    public const int Width = 64;
    public const int Height = 64;
    public const TextureFormat Format = TextureFormat.RGB5A3;
    public static readonly int Size = 64 * 64 * sizeof(ushort);
    public static readonly DirectEncoding DirectEncoding = DirectEncoding.GetEncoding(Format);

    // Properties
    public Texture Texture { get; set; } = new Texture();
    public AddressRange AddressRange { get; set; }


    // Constructors
    public Emblem()
    {
        Texture = new Texture(Width, Height, Format);
        ThrowErrorIfInvalid();
    }
    public Emblem(Texture texture)
    {
        Texture = texture;
        ThrowErrorIfInvalid();
    }

    // Methods
    public void Deserialize(EndianBinaryReader reader)
    {
        this.RecordStartAddress(reader);
        Texture = Texture.ReadDirectColorTexture(reader, Format, Width, Height);
        this.RecordEndAddress(reader);

        ThrowErrorIfInvalid();
        Assert.IsTrue(AddressRange.Size == Size);
    }
    public void Serialize(EndianBinaryWriter writer)
    {
        ThrowErrorIfInvalid();

        var blocks = Texture.CreateDirectColorBlocksFromTexture(Texture, DirectEncoding);
        this.RecordStartAddress(writer);
        DirectEncoding.WriteBlocks(writer, blocks);
        this.RecordEndAddress(writer);

        Assert.IsTrue(AddressRange.Size == Size);
    }

    private void ThrowErrorIfInvalid()
    {
        bool hasInvalidWidth = Texture.Width != Width;
        bool hasInvalidHeight = Texture.Height != Height;
        bool hasInvalidDimensions = hasInvalidWidth || hasInvalidHeight;
        if (hasInvalidDimensions)
        {
            string msg =
                $"{GetType().Name} has invalid dimensions ({Texture.Width},{Texture.Height}). " +
                $"{GetType().Name} must have a dimension of exactly ({Width}, {Height}).";
            throw new ArgumentException(msg);
        }
    }
}
