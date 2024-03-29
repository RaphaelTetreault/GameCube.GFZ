﻿using Manifold;
using Manifold.IO;
using System.IO;
using System;

namespace GameCube.GFZ.Stage
{
    /// <summary>
    /// Represents a Maya (4.X) KeyableAttribute (animation keyframe).
    /// </summary>
    [Serializable]
    public sealed class KeyableAttribute :
        IBinaryAddressable,
        IBinarySerializable,
        ITextPrintable
    {
        // FEILDS
        private InterpolationMode easeMode;
        private float time;
        private float value;
        private float tangentIn;
        private float tangentOut;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public InterpolationMode EaseMode { get => easeMode; set => easeMode = value; }
        public float Time { get => time; set => time = value; }
        public float Value { get => value; set => this.value = value; }
        public float TangentIn { get => tangentIn; set => tangentIn = value; }
        public float TangentOut { get => tangentOut; set => tangentOut = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref easeMode);
                reader.Read(ref time);
                reader.Read(ref value);
                reader.Read(ref tangentIn);
                reader.Read(ref tangentOut);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(easeMode);
                writer.Write(time);
                writer.Write(value);
                writer.Write(tangentIn);
                writer.Write(tangentOut);
            }
            this.RecordEndAddress(writer);
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, nameof(KeyableAttribute));
            indentLevel++;
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(EaseMode)}: {EaseMode}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Time)}: {Time}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Value)}: {Value}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(TangentIn)}: {TangentIn}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(TangentOut)}: {TangentOut}");
        }

        public string PrintSingleLine()
        {
            return $"{nameof(KeyableAttribute)}({PrintKeyableCondensed()})";
        }

        public override string ToString() => PrintSingleLine();

        public string PrintKeyableCondensed()
        {
            // Prints all values on single line, limits float precision
            return
                $"{nameof(EaseMode)}: {EaseMode}, " +
                $"{nameof(Time)}: {Time,6:##0.000}, " +
                $"{nameof(Value)}: {Value,8:####0.000}, " +
                $"{nameof(TangentIn)}: {TangentIn,8:####0.000}, " +
                $"{nameof(TangentOut)}: {TangentOut,8:####0.000}";
        }
    }
}