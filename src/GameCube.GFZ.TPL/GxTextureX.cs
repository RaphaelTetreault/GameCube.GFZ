//using GameCube.GX.Texture;
//using Manifold.IO;

//namespace GameCube.GFZ.TPL
//{
//    public class GxTexture :
//        IBinarySerializable,
//        IBinaryAddressable
//    {
//        // Fields
//        private TextureBundleDescription description;
//        private Texture[] textures;

//        // Properties
//        public AddressRange AddressRange { get; set; }
//        public TextureBundleDescription Description { get => description; set => description = value; }
//        public Texture[] Textures{ get => textures; set => textures = value; }


//        public GxTexture(TextureBundle textureSeries)
//        {
//            // Create description from texture series
//            description = new TextureBundleDescription()
//            {
//                IsNull = false,
//                TextureFormat = textureSeries.Description.TextureFormat,
//                Width = textureSeries.Description.Width,
//                Height = textureSeries.Description.Height,
//                MipmapLevels = (ushort)textureSeries.Length,
//            };

//            // Copy texture references
//            textures = new Texture[textureSeries.Length];
//            for (int i = 0; i < textures.Length; i++)
//                textures[i] = textureSeries.Elements[i].Texture;
//        }

//        public void Deserialize(EndianBinaryReader reader)
//        {
//            // Read description
//            reader.Read(ref description);
//            // Read textures
//            textures = new Texture[description.NumberOfTextures];
//            int width = description.Width;
//            int height = description.Height;
//            for (int i = 0; i < textures.Length; i++)
//            {
//                textures[i] = Texture.ReadDirectColorTexture(reader, description.TextureFormat, width, height);
//                width >>= 1;
//                height >>= 1;
//            }
//        }

//        public void Serialize(EndianBinaryWriter writer)
//        {
//            writer.Write(description);
//            foreach (var texture in textures)
//            {
//                Texture.WriteDirectColorTexture(writer, texture, description.TextureFormat);
//            }
//        }
//    }
//}
