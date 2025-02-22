using GameCube.GX.Texture;
using Manifold.IO;

namespace GameCube.GFZ.TPL;

/// <summary>
///     A main textures and its mipmaps, if any.
/// </summary>
public class TextureBundle
{
    public TextureBundleElement[] Elements { get; internal set; } = [];
    public TextureBundleDescription Description { get; internal set; }
    public AddressRange AddressRange { get; internal set; }
    public int Length => Elements is null ? 0 : Elements.Length;

    /// <summary>
    ///     Create <see cref="TextureBundle"/> from desciptive data.
    /// </summary>
    /// <param name="textureBundleDescription"></param>
    public TextureBundle(TextureBundleDescription textureBundleDescription)
    {
        Description = textureBundleDescription;

        // Initialiize texture bundle
        int numberOfTextures = textureBundleDescription.NumberOfTextures;
        Elements = new TextureBundleElement[numberOfTextures];
        for (int i = 0; i < Elements.Length; i++)
            Elements[i] = new TextureBundleElement();
    }

    /// <summary>
    ///     
    /// </summary>
    /// <param name="textureBundleDescription"></param>
    public TextureBundle(TextureBundleElement[] elements, TextureFormat textureFormat)
    {
        // Assign elements
        Elements = elements;
        // Construct description from remaining data
        Description = new TextureBundleDescription()
        {
            Width = (ushort)elements[0].Texture.Width,
            Height = (ushort)elements[0].Texture.Height,
            MipmapLevels = (ushort)(elements.Length == 1 ? 0 : elements.Length),
            TextureFormat = textureFormat,
        };
    }
}
