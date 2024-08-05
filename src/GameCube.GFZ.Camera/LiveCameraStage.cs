using Manifold;
using Manifold.IO;
using System;
using System.IO;
using System.Numerics;

namespace GameCube.GFZ.Camera
{
    [Serializable]
    public sealed class LiveCameraStage :
        IBinarySerializable,
        IBinaryFileType,
        ITsvSerializable
    {
        public const Endianness endianness = Endianness.BigEndian;

        // FIELDS
        private CameraPan[] pans = new CameraPan[0];


        // PROPERTIES
        public Endianness Endianness => endianness;
        public string FileName { get; set; } = string.Empty;
        public string FileExtension => ".bin";

        public CameraPan[] Pans
        {
            get => pans;
            set => pans = value;
        }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            // Figure out how many camera pans are in this file
            var nPans = (int)(reader.BaseStream.Length / CameraPan.kStructureSize);
            
            // Read that many structures out of the file
            reader.Read(ref pans, nPans);

            // Sanity check. We should be at the end of the stream
            Assert.IsTrue(reader.BaseStream.IsAtEndOfStream());
        }

        public void Deserialize(StreamReader reader)
        {
            // What would be useful:
            // READ FLOAT3 where each element is read rfom base index

            string[] lines = reader.ReadToEnd().Split('\n');
            int panCount = lines.Length - 2;
            pans = new CameraPan[panCount];
            for (int i = 0; i < panCount; i++)
            {
                pans[i] = new CameraPan();
                var pan = pans[i];

                int lineIndex = i + 1;
                int dataIndex = 0;
                var data = lines[lineIndex].Split('\t');

                pan.FrameCount = int.Parse(data[dataIndex++]);
                pan.LerpSpeed = float.Parse(data[dataIndex++]);

                pan.From.Interpolation = Enum.Parse<CameraPanInterpolation>(data[dataIndex++]);
                pan.To.Interpolation = Enum.Parse<CameraPanInterpolation>(data[dataIndex++]);
                pan.From.FieldOfView = float.Parse(data[dataIndex++]);
                pan.To.FieldOfView = float.Parse(data[dataIndex++]);
                pan.From.RotationRoll = float.Parse(data[dataIndex++]);
                pan.To.RotationRoll= float.Parse(data[dataIndex++]);

                Vector3 fromPos = new();
                fromPos.X = float.Parse(data[dataIndex++]);
                fromPos.Y = float.Parse(data[dataIndex++]);
                fromPos.Z = float.Parse(data[dataIndex++]);
                pan.From.CameraPosition = fromPos;
                Vector3 toPos = new();
                toPos.X = float.Parse(data[dataIndex++]);
                toPos.Y = float.Parse(data[dataIndex++]);
                toPos.Z = float.Parse(data[dataIndex++]);
                pan.To.CameraPosition = toPos;

                Vector3 fromLookat = new();
                fromLookat.X = float.Parse(data[dataIndex++]);
                fromLookat.Y = float.Parse(data[dataIndex++]);
                fromLookat.Z = float.Parse(data[dataIndex++]);
                pan.From.LookAtPosition = fromLookat;
                Vector3 toLookat = new();
                toLookat.X = float.Parse(data[dataIndex++]);
                toLookat.Y = float.Parse(data[dataIndex++]);
                toLookat.Z = float.Parse(data[dataIndex++]);
                pan.To.LookAtPosition = toLookat;
            }
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(pans);
        }

        public void Serialize(StreamWriter writer)
        {
            writer.WriteNextCol(nameof(CameraPan.FrameCount));
            writer.WriteNextCol(nameof(CameraPan.LerpSpeed));

            writer.WriteNextCol(nameof(CameraPan.From) + "." + nameof(CameraPanTarget.Interpolation));
            writer.WriteNextCol(nameof(CameraPan.To) + "." + nameof(CameraPanTarget.Interpolation));
            writer.WriteNextCol(nameof(CameraPan.From) + "." + nameof(CameraPanTarget.FieldOfView));
            writer.WriteNextCol(nameof(CameraPan.To) + "." + nameof(CameraPanTarget.FieldOfView));
            writer.WriteNextCol(nameof(CameraPan.From) + "." + nameof(CameraPanTarget.RotationRoll));
            writer.WriteNextCol(nameof(CameraPan.To) + "." + nameof(CameraPanTarget.RotationRoll));

            writer.WriteNextCol(nameof(CameraPan.From) + "." + nameof(CameraPanTarget.CameraPosition) + ".X");
            writer.WriteNextCol(nameof(CameraPan.From) + "." + nameof(CameraPanTarget.CameraPosition) + ".Y");
            writer.WriteNextCol(nameof(CameraPan.From) + "." + nameof(CameraPanTarget.CameraPosition) + ".Z");
            writer.WriteNextCol(nameof(CameraPan.To) + "." + nameof(CameraPanTarget.CameraPosition) + ".X");
            writer.WriteNextCol(nameof(CameraPan.To) + "." + nameof(CameraPanTarget.CameraPosition) + ".Y");
            writer.WriteNextCol(nameof(CameraPan.To) + "." + nameof(CameraPanTarget.CameraPosition) + ".Z");

            writer.WriteNextCol(nameof(CameraPan.From) + "." + nameof(CameraPanTarget.LookAtPosition) + ".X");
            writer.WriteNextCol(nameof(CameraPan.From) + "." + nameof(CameraPanTarget.LookAtPosition) + ".Y");
            writer.WriteNextCol(nameof(CameraPan.From) + "." + nameof(CameraPanTarget.LookAtPosition) + ".Z");
            writer.WriteNextCol(nameof(CameraPan.To) + "." + nameof(CameraPanTarget.LookAtPosition) + ".X");
            writer.WriteNextCol(nameof(CameraPan.To) + "." + nameof(CameraPanTarget.LookAtPosition) + ".Y");
            writer.WriteNextCol(nameof(CameraPan.To) + "." + nameof(CameraPanTarget.LookAtPosition) + ".Z");
            writer.WriteNextRow();

            foreach (var pan in pans)
            {
                writer.WriteNextCol(pan.FrameCount);
                writer.WriteNextCol(pan.LerpSpeed);

                writer.WriteNextCol(pan.From.Interpolation);
                writer.WriteNextCol(pan.To.Interpolation);
                writer.WriteNextCol(pan.From.FieldOfView);
                writer.WriteNextCol(pan.To.FieldOfView);
                writer.WriteNextCol(pan.From.RotationRoll);
                writer.WriteNextCol(pan.To.RotationRoll);

                writer.WriteNextCol(pan.From.CameraPosition.X);
                writer.WriteNextCol(pan.From.CameraPosition.Y);
                writer.WriteNextCol(pan.From.CameraPosition.Z);
                writer.WriteNextCol(pan.To.CameraPosition.X);
                writer.WriteNextCol(pan.To.CameraPosition.Y);
                writer.WriteNextCol(pan.To.CameraPosition.Z);

                writer.WriteNextCol(pan.From.LookAtPosition.X);
                writer.WriteNextCol(pan.From.LookAtPosition.Y);
                writer.WriteNextCol(pan.From.LookAtPosition.Z);
                writer.WriteNextCol(pan.To.LookAtPosition.X);
                writer.WriteNextCol(pan.To.LookAtPosition.Y);
                writer.WriteNextCol(pan.To.LookAtPosition.Z);
                writer.WriteNextRow();
            }
        }
    }
}
