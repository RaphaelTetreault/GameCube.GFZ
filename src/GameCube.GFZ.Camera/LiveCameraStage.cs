using Manifold.IO;
using System;
using System.IO;
using System.Numerics;

namespace GameCube.GFZ.Camera;


/// <summary>
///     Represents a series of camera pans for a stage (e.g. stage intro demo, attract mode exit).
/// </summary>
/// <remarks>
///     livecam_stage_#,##,###
///     livecam_stage_demo_#,##,###,50_end
/// </remarks>
public sealed class LiveCameraStage :
    IBinarySerializable,
    ITsvSerializable
{
    // FIELDS
    private PreviewCameraShot[] shots = [];


    // PROPERTIES
    public PreviewCameraShot[] Shots
    {
        get => shots;
        set => shots = value;
    }


    // METHODS
    public void Deserialize(EndianBinaryReader reader)
    {
        // Figure out how many camera pans are in this file
        var nShots = (int)(reader.BaseStream.Length / PreviewCameraShot.kStructureSize);
        
        // Read that many structures out of the file
        reader.Read(ref shots, nShots);

        // Sanity check. We should be at the end of the stream
        Assert.IsTrue(reader.BaseStream.IsAtEndOfStream());
    }

    public void Deserialize(StreamReader reader)
    {
        // What would be useful:
        // READ FLOAT3 where each element is read rfom base index

        string[] lines = reader.ReadToEnd().Split('\n');
        int shotCount = lines.Length - 2;
        shots = new PreviewCameraShot[shotCount];
        for (int i = 0; i < shotCount; i++)
        {
            shots[i] = new PreviewCameraShot();
            var shot = shots[i];

            int lineIndex = i + 1;
            int dataIndex = 0;
            var data = lines[lineIndex].Split('\t');

            shot.FrameCount = int.Parse(data[dataIndex++]);
            shot.LerpSpeed = float.Parse(data[dataIndex++]);

            shot.From.Mode = Enum.Parse<PreviewCameraMode>(data[dataIndex++]);
            shot.To.Mode = Enum.Parse<PreviewCameraMode>(data[dataIndex++]);
            shot.From.FieldOfView = float.Parse(data[dataIndex++]);
            shot.To.FieldOfView = float.Parse(data[dataIndex++]);
            shot.From.RotationRoll = float.Parse(data[dataIndex++]);
            shot.To.RotationRoll= float.Parse(data[dataIndex++]);

            Vector3 fromPos = new();
            fromPos.X = float.Parse(data[dataIndex++]);
            fromPos.Y = float.Parse(data[dataIndex++]);
            fromPos.Z = float.Parse(data[dataIndex++]);
            shot.From.CameraPosition = fromPos;
            Vector3 toPos = new();
            toPos.X = float.Parse(data[dataIndex++]);
            toPos.Y = float.Parse(data[dataIndex++]);
            toPos.Z = float.Parse(data[dataIndex++]);
            shot.To.CameraPosition = toPos;

            Vector3 fromLookat = new();
            fromLookat.X = float.Parse(data[dataIndex++]);
            fromLookat.Y = float.Parse(data[dataIndex++]);
            fromLookat.Z = float.Parse(data[dataIndex++]);
            shot.From.LookAtPosition = fromLookat;
            Vector3 toLookat = new();
            toLookat.X = float.Parse(data[dataIndex++]);
            toLookat.Y = float.Parse(data[dataIndex++]);
            toLookat.Z = float.Parse(data[dataIndex++]);
            shot.To.LookAtPosition = toLookat;
        }
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        writer.Write(shots);
    }

    public void Serialize(StreamWriter writer)
    {
        writer.WriteNextCol(nameof(PreviewCameraShot.FrameCount));
        writer.WriteNextCol(nameof(PreviewCameraShot.LerpSpeed));

        writer.WriteNextCol(nameof(PreviewCameraShot.From) + "." + nameof(PreviewCameraPoint.Mode));
        writer.WriteNextCol(nameof(PreviewCameraShot.To) + "." + nameof(PreviewCameraPoint.Mode));
        writer.WriteNextCol(nameof(PreviewCameraShot.From) + "." + nameof(PreviewCameraPoint.FieldOfView));
        writer.WriteNextCol(nameof(PreviewCameraShot.To) + "." + nameof(PreviewCameraPoint.FieldOfView));
        writer.WriteNextCol(nameof(PreviewCameraShot.From) + "." + nameof(PreviewCameraPoint.RotationRoll));
        writer.WriteNextCol(nameof(PreviewCameraShot.To) + "." + nameof(PreviewCameraPoint.RotationRoll));

        writer.WriteNextCol(nameof(PreviewCameraShot.From) + "." + nameof(PreviewCameraPoint.CameraPosition) + ".X");
        writer.WriteNextCol(nameof(PreviewCameraShot.From) + "." + nameof(PreviewCameraPoint.CameraPosition) + ".Y");
        writer.WriteNextCol(nameof(PreviewCameraShot.From) + "." + nameof(PreviewCameraPoint.CameraPosition) + ".Z");
        writer.WriteNextCol(nameof(PreviewCameraShot.To) + "." + nameof(PreviewCameraPoint.CameraPosition) + ".X");
        writer.WriteNextCol(nameof(PreviewCameraShot.To) + "." + nameof(PreviewCameraPoint.CameraPosition) + ".Y");
        writer.WriteNextCol(nameof(PreviewCameraShot.To) + "." + nameof(PreviewCameraPoint.CameraPosition) + ".Z");

        writer.WriteNextCol(nameof(PreviewCameraShot.From) + "." + nameof(PreviewCameraPoint.LookAtPosition) + ".X");
        writer.WriteNextCol(nameof(PreviewCameraShot.From) + "." + nameof(PreviewCameraPoint.LookAtPosition) + ".Y");
        writer.WriteNextCol(nameof(PreviewCameraShot.From) + "." + nameof(PreviewCameraPoint.LookAtPosition) + ".Z");
        writer.WriteNextCol(nameof(PreviewCameraShot.To) + "." + nameof(PreviewCameraPoint.LookAtPosition) + ".X");
        writer.WriteNextCol(nameof(PreviewCameraShot.To) + "." + nameof(PreviewCameraPoint.LookAtPosition) + ".Y");
        writer.WriteNextCol(nameof(PreviewCameraShot.To) + "." + nameof(PreviewCameraPoint.LookAtPosition) + ".Z");
        writer.WriteNextRow();

        foreach (var pan in shots)
        {
            writer.WriteNextCol(pan.FrameCount);
            writer.WriteNextCol(pan.LerpSpeed);

            writer.WriteNextCol(pan.From.Mode);
            writer.WriteNextCol(pan.To.Mode);
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
