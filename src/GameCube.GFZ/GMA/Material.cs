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
        private byte tevLayerCount;
        private MaterialDestination materialDestination;
        private sbyte unkAlpha0x14 = -1; // 0xFF. Mainly -1 (3/4 of data). Index => previous material's index for same model.
        private MatFlags0x15 unk0x15;
        private short tevLayerIndex0 = -1; // 0xFFFF
        private short tevLayerIndex1 = -1; // 0xFFFF
        private short tevLayerIndex2 = -1; // 0xFFFF

        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public GXColor MaterialColor { get => materialColor; set => materialColor = value; }
        public GXColor AmbientColor { get => ambientColor; set => ambientColor = value; }
        public GXColor SpecularColor { get => specularColor; set => specularColor = value; }
        public MatFlags0x10 Unk0x10 { get => unk0x10; set => unk0x10 = value; }
        public byte Alpha { get => alpha; set => alpha = value; }
        public byte TevLayerCount { get => tevLayerCount; set => tevLayerCount = value; }
        public MaterialDestination MaterialDestination { get => materialDestination; set => materialDestination = value; }
        public sbyte UnkAlpha0x14 { get => unkAlpha0x14; set => unkAlpha0x14 = value; }
        public MatFlags0x15 Unk0x15 { get => unk0x15; set => unk0x15 = value; }
        public short TevLayerIndex0 { get => tevLayerIndex0; set => tevLayerIndex0 = value; }
        public short TevLayerIndex1 { get => tevLayerIndex1; set => tevLayerIndex1 = value; }
        public short TevLayerIndex2 { get => tevLayerIndex2; set => tevLayerIndex2 = value; }


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
                reader.Read(ref tevLayerCount);
                reader.Read(ref materialDestination);
                reader.Read(ref unkAlpha0x14);
                reader.Read(ref unk0x15);
                reader.Read(ref tevLayerIndex0);
                reader.Read(ref tevLayerIndex1);
                reader.Read(ref tevLayerIndex2);
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
                writer.Write(tevLayerCount);
                writer.Write(materialDestination);
                writer.Write(unkAlpha0x14);
                writer.Write(unk0x15);
                writer.Write(tevLayerIndex0);
                writer.Write(tevLayerIndex1);
                writer.Write(tevLayerIndex2);
            }
            this.RecordEndAddress(writer);
        }

    }
}