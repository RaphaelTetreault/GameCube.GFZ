﻿using Manifold;
using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.Stage
{
    /// <summary>
    /// Binds an object name to a loadable display model.
    /// </summary>
    [Serializable]
    public sealed class SceneObjectLOD :
        IBinaryAddressable,
        IBinarySerializable,
        IHasReference,
        ITextPrintable
    {
        // FIELDS
        private uint zero_0x00;
        private Pointer lodNamePtr;
        private uint zero_0x08;
        private float lodDistance;
        // REFERENCE FIELDS
        private ShiftJisCString name;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public Pointer LodNamePtr { get => lodNamePtr; set => lodNamePtr = value; }
        public float LodDistance { get => lodDistance; set => lodDistance = value; }
        public ShiftJisCString Name { get => name; set => name = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref zero_0x00);
                reader.Read(ref lodNamePtr);
                reader.Read(ref zero_0x08);
                reader.Read(ref lodDistance);
            }
            this.RecordEndAddress(reader);
            {
                Assert.IsTrue(zero_0x00 == 0);
                Assert.IsTrue(zero_0x08 == 0);

                reader.JumpToAddress(lodNamePtr);
                reader.Read(ref name);
            }
            this.SetReaderToEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            {
                Assert.IsTrue(zero_0x00 == 0);
                Assert.IsTrue(zero_0x08 == 0);

                lodNamePtr = name.GetPointer();
            }
            this.RecordStartAddress(writer);
            {
                writer.Write(zero_0x00);
                writer.Write(lodNamePtr);
                writer.Write(zero_0x08);
                writer.Write(lodDistance);
            }
            this.RecordEndAddress(writer);
        }

        public void ValidateReferences()
        {
            // This pointer CANNOT be null and must refer to an object name.

            Assert.IsTrue(name != null); // true null only, name can be string.Empty
            Assert.ReferencePointer(name, lodNamePtr); // 2022/01/25: must always have instance pointer!

            // Constants
            Assert.IsTrue(zero_0x00 == 0);
            Assert.IsTrue(zero_0x08 == 0);
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, PrintSingleLine());
        }

        public string PrintSingleLine()
        {
            return $"{nameof(SceneObjectLOD)}({name}, {nameof(LodDistance)}: {LodDistance})";
        }

        public override string ToString() => PrintSingleLine();

    }
}