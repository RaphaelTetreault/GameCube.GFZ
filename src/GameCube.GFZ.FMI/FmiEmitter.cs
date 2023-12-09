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
    ///     `targetOffset` only applies to booster custom parts 6, 7, and 23 (angled thrusters).
    /// </remarks>
    [Serializable]
    public class FmiEmitter :
        IBinarySerializable,
        IBinaryAddressable,
        IPlainTextSerializable
    {
        // FIELDS
        private float3 position;
        private float3 targetOffset;
        private float scale;
        private FmiColorRGB accelColor;
        private FmiColorRGB boostColor;
        private uint zero_0x34;

        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        /// <summary>
        ///     The emitter's position.
        /// </summary>
        public float3 Position { get => position; set => position = value; }
        /// <summary>
        ///     Target offset position to point emitter towards relative to emitter position.
        /// </summary>
        public float3 TargetOffset { get => targetOffset; set => targetOffset = value; }
        public float ScaleMax { get => scale; set => scale = value; }
        public FmiColorRGB ColorMin { get => accelColor; set => accelColor = value; }
        public FmiColorRGB ColorMax { get => boostColor; set => boostColor = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref position);
                reader.Read(ref targetOffset);
                reader.Read(ref scale);
                reader.Read(ref accelColor);
                reader.Read(ref boostColor);
                reader.Read(ref zero_0x34);
            }
            this.RecordEndAddress(reader);
            {
                Assert.IsTrue(zero_0x34 == 0, "not zero");
            }
        }

        public void Deserialize(PlainTextReader reader)
        {
            reader.ReadLineValue(ref position.x);
            reader.ReadLineValue(ref position.y);
            reader.ReadLineValue(ref position.z);
            reader.ReadLineValue(ref targetOffset.x);
            reader.ReadLineValue(ref targetOffset.y);
            reader.ReadLineValue(ref targetOffset.z);
            reader.ReadLineValue(ref scale);
            reader.ReadLineValue(ref accelColor.r);
            reader.ReadLineValue(ref accelColor.g);
            reader.ReadLineValue(ref accelColor.b);
            reader.ReadLineValue(ref boostColor.r);
            reader.ReadLineValue(ref boostColor.g);
            reader.ReadLineValue(ref boostColor.b);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(position);
                writer.Write(targetOffset);
                writer.Write(scale);
                writer.Write(accelColor);
                writer.Write(boostColor);
                writer.Write(zero_0x34);
            }
            this.RecordEndAddress(writer);
        }

        public void Serialize(PlainTextWriter writer)
        {
            writer.WriteLineValue(nameof(position) + ".X", position.x);
            writer.WriteLineValue(nameof(position) + ".Y", position.y);
            writer.WriteLineValue(nameof(position) + ".Z", position.z);
            writer.WriteLineValue(nameof(targetOffset) + ".X", targetOffset.x);
            writer.WriteLineValue(nameof(targetOffset) + ".Y", targetOffset.y);
            writer.WriteLineValue(nameof(targetOffset) + ".Z", targetOffset.z);
            writer.WriteLineValue(nameof(scale), scale);
            writer.WriteLineValue(nameof(accelColor) + ".R", accelColor.r);
            writer.WriteLineValue(nameof(accelColor) + ".G", accelColor.g);
            writer.WriteLineValue(nameof(accelColor) + ".B", accelColor.b);
            writer.WriteLineValue(nameof(boostColor) + ".R", boostColor.r);
            writer.WriteLineValue(nameof(boostColor) + ".G", boostColor.g);
            writer.WriteLineValue(nameof(boostColor) + ".B", boostColor.b);
        }
    }
}