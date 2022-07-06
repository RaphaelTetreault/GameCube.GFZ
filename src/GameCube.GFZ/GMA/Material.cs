using GameCube.GX;
using Manifold;
using Manifold.IO;
using System.IO;

namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// The material properties applied to a GCMF's display lists.
    /// </summary>
    public class Material :
        IBinaryAddressable,
        IBinarySerializable
    {
        // FIELDS    
        private GXColor materialColor = new GXColor(ComponentType.GX_RGBA8);
        private GXColor ambientColor = new GXColor(ComponentType.GX_RGBA8);
        private GXColor specularColor = new GXColor(ComponentType.GX_RGBA8);
        private MatFlags0x10 unk0x10;
        private byte alpha = 255;
        private byte textureCount; // tev layer count
        private DisplayListFlags displayListFlags;
        private sbyte unkIncrementalIndex = -1; // 0xFF. Mainly -1 (3/4 of data). Index => previous material's index for same model.
        private MatFlags0x15 unk0x15;
        private short textureIndex0 = -1; // 0xFFFF // tevLayerIdxs[3] - Up to 3 indices into model's tev layer list. -1 means end of list
        private short textureIndex1 = -1; // 0xFFFF
        private short textureIndex2 = -1; // 0xFFFF

        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public GXColor MaterialColor { get => materialColor; set => materialColor = value; }
        public GXColor AmbientColor { get => ambientColor; set => ambientColor = value; }
        public GXColor SpecularColor { get => specularColor; set => specularColor = value; }
        public MatFlags0x10 Unk0x10 { get => unk0x10; set => unk0x10 = value; }
        public byte Alpha { get => alpha; set => alpha = value; }
        public byte TextureCount { get => textureCount; set => textureCount = value; }
        public DisplayListFlags DisplayListFlags { get => displayListFlags; set => displayListFlags = value; }
        public sbyte Unk0x14 { get => unkIncrementalIndex; set => unkIncrementalIndex = value; }
        public MatFlags0x15 Unk0x15 { get => unk0x15; set => unk0x15 = value; }
        public short TextureIndex0 { get => textureIndex0; set => textureIndex0 = value; }
        public short TextureIndex1 { get => textureIndex1; set => textureIndex1 = value; }
        public short TextureIndex2 { get => textureIndex2; set => textureIndex2 = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                materialColor.Deserialize(reader);
                ambientColor.Deserialize(reader);
                specularColor.Deserialize(reader);
                reader.Read(ref unk0x10);
                reader.Read(ref alpha);
                reader.Read(ref textureCount);
                reader.Read(ref displayListFlags);
                reader.Read(ref unkIncrementalIndex);
                reader.Read(ref unk0x15);
                reader.Read(ref textureIndex0);
                reader.Read(ref textureIndex1);
                reader.Read(ref textureIndex2);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            {
                Assert.IsTrue(materialColor.ComponentType == ComponentType.GX_RGBA8);
                Assert.IsTrue(ambientColor.ComponentType == ComponentType.GX_RGBA8);
                Assert.IsTrue(specularColor.ComponentType == ComponentType.GX_RGBA8);
            }
            this.RecordStartAddress(writer);
            {
                writer.Write(materialColor);
                writer.Write(ambientColor);
                writer.Write(specularColor);
                writer.Write(unk0x10);
                writer.Write(alpha);
                writer.Write(textureCount);
                writer.Write(displayListFlags);
                writer.Write(unkIncrementalIndex);
                writer.Write(unk0x15);
                writer.Write(textureIndex0);
                writer.Write(textureIndex1);
                writer.Write(textureIndex2);
            }
            this.RecordEndAddress(writer);
        }

    }
}