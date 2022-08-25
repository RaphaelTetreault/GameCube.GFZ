using GameCube.GX;
using Manifold;
using Manifold.IO;
using Unity.Mathematics;

namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// Appears to be a GX vertex meant for skinning. It is not part of a formal
    /// display list. The GXAttributes properly describe the intial position,
    /// normal, textureUV0, textureUV1, and textureUV2. The data afterwards is somewhat
    /// strange. The color values may not be true colors, but some kind of weight painting.
    /// </summary>
    public class SkinnedVertexB :
        IBinaryAddressable,
        IBinarySerializable
    {
        // FIELDS
        private float3 position;
        private float3 normal;
        private float2 textureUV0;
        private float2 textureUV1;
        private float2 textureUV2;
        private GXColor color0 = new GXColor(ComponentType.GX_RGBA8); // RGBA. Appears to truly be a color.
        private GXColor color1 = new GXColor(ComponentType.GX_RGBA8); // Magic bits. Variations: 00000000, 00010100, 02000002, 01000001, 03000003
        private GXColor color2 = new GXColor(ComponentType.GX_RGBA8); // RGBA. Color-looking, but does use alpha channel.
        private GXColor color3 = new GXColor(ComponentType.GX_RGBA8); // RGBA. Color-looking, but does use alpha channel. 00000004 is the only magic-bit looking value.

        // PROPERTIES
        public AddressRange AddressRange { get; set; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref position);
                reader.Read(ref normal);
                reader.Read(ref textureUV0);
                reader.Read(ref textureUV1);
                reader.Read(ref textureUV2);
                color0.Deserialize(reader);
                color1.Deserialize(reader);
                color2.Deserialize(reader);
                color3.Deserialize(reader);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(position);
                writer.Write(normal);
                writer.Write(textureUV0);
                writer.Write(textureUV1);
                writer.Write(textureUV2);
                writer.Write(color0);
                writer.Write(color1);
                writer.Write(color2);
                writer.Write(color3);
            }
            this.RecordEndAddress(writer);
        }

    }
}