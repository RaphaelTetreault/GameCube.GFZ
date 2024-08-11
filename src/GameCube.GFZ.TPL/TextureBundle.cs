using GameCube.GX.Texture;
using Manifold.IO;
using System;

namespace GameCube.GFZ.TPL
{
    public class TextureBundle
    {
        public TextureBundleElement[] Elements { get; internal set; } = Array.Empty<TextureBundleElement>();
        public TextureBundleDescription Description { get; internal set; }
        public AddressRange AddressRange { get; internal set; }
        public int Length => Elements is null ? 0 : Elements.Length;

        public TextureBundle(TextureBundleDescription textureBundleDescription)
        {
            Description = textureBundleDescription;

            // Initialiize texture bundle
            int numberOfTextures = textureBundleDescription.NumberOfTextures;
            Elements = new TextureBundleElement[numberOfTextures];
            for (int i = 0; i < Elements.Length; i++)
                Elements[i] = new TextureBundleElement();
        }
    }
}
