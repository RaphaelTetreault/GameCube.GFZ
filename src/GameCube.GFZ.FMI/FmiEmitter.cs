using Manifold.IO;
using System;
using System.IO;
using Unity.Mathematics;

namespace GameCube.GFZ.FMI
{
    /// <summary>
    ///     Represents a machine thruster/exhaust particle emitter.
    /// </summary>
    /// <remarks>
    ///     Rotate and Scale parameters only apply to booster custom parts 6, 7, and 23.
    /// </remarks>
    [Serializable]
    public class FmiEmitter :
        IBinarySerializable,
        IBinaryAddressable,
        ITextSerializable
    {
        // FIELDS
        private float3 position;
        private float rotateY; // radians?
        private float rotateX; // radians?
        private float scaleMin;
        private float scaleMax;
        private FmiColorRGB colorMin;
        private FmiColorRGB colorMax;
        private uint zero_0x34;

        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public float3 Position { get => position; set => position = value; }
        public float RotateY { get => rotateY; set => rotateY = value; }
        public float RotateX { get => rotateX; set => rotateX = value; }
        public float ScaleMin { get => scaleMin; set => scaleMin = value; }
        public float ScaleMax { get => scaleMax; set => scaleMax = value; }
        public FmiColorRGB ColorMin { get => colorMin; set => colorMin = value; }
        public FmiColorRGB ColorMax { get => colorMax; set => colorMax = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref position);
                reader.Read(ref rotateY);
                reader.Read(ref rotateX);
                reader.Read(ref scaleMin);
                reader.Read(ref scaleMax);
                reader.Read(ref colorMin);
                reader.Read(ref colorMax);
                reader.Read(ref zero_0x34);
            }
            this.RecordEndAddress(reader);
            {
                Assert.IsTrue(zero_0x34 == 0, "not zero");
            }
        }

        public void Deserialize(StreamReader reader)
        {
            throw new NotImplementedException();
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(position);
                writer.Write(rotateY);
                writer.Write(rotateX);
                writer.Write(scaleMin);
                writer.Write(scaleMax);
                writer.Write(colorMin);
                writer.Write(colorMax);
                writer.Write(zero_0x34);
            }
            this.RecordEndAddress(writer);
        }

        public void Serialize(StreamWriter writer)
        {
            writer.WriteLine($"{nameof(position)}: {position}");
            writer.WriteLine($"{nameof(rotateY)}: {rotateY}");
            writer.WriteLine($"{nameof(rotateX)}: {rotateX}");
            writer.WriteLine($"{nameof(scaleMin)}: {scaleMin}");
            writer.WriteLine($"{nameof(scaleMax)}: {scaleMax}");
            writer.WriteLine($"{nameof(colorMin)}: {colorMin}");
            writer.WriteLine($"{nameof(colorMax)}: {colorMax}");
        }
    }
}