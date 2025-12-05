using GameCube.GX.Texture;
using Manifold.IO;

namespace GameCube.GFZ.TPL;

/// <summary>
///     A main textures and its mipmaps, if any.
/// </summary>
public class TextureSequence
{
    public TextureSquenceElement[] Elements { get; internal set; } = [];
    public TextureSequenceDescription Description { get; internal set; }
    public AddressRange AddressRange { get; internal set; }
    public int Length => Elements is null ? 0 : Elements.Length;

    /// <summary>
    ///     Create <see cref="TextureSequence"/> from desciptive data.
    /// </summary>
    /// <param name="textureSequenceDescription"></param>
    public TextureSequence(TextureSequenceDescription textureSequenceDescription)
    {
        Description = textureSequenceDescription;

        // Initialize texture sequence
        int numberOfTextures = textureSequenceDescription.NumberOfTextures;
        Elements = new TextureSquenceElement[numberOfTextures];
        for (int i = 0; i < Elements.Length; i++)
            Elements[i] = new TextureSquenceElement();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="elements"></param>
    /// <param name="textureFormat"></param>
    public TextureSequence(TextureSquenceElement[] elements, TextureFormat textureFormat)
    {
        // Assign elements
        Elements = elements;
        // Construct description from remaining data
        Description = new TextureSequenceDescription()
        {
            Width = (ushort)elements[0].Texture.Width,
            Height = (ushort)elements[0].Texture.Height,
            MipmapLevels = (ushort)(elements.Length == 1 ? 0 : elements.Length),
            TextureFormat = textureFormat,
        };
    }
}
