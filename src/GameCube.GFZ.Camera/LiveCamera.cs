using Manifold.IO;

namespace GameCube.GFZ.Camera;

/// <summary>
///     Represents a series of camera pans for a stage (e.g. stage intro demo, attract mode exit).
/// </summary>
/// <remarks>
///     livecam_stage_#,##,###
///     livecam_stage_demo_#,##,###,50_end
/// </remarks>
public sealed class LiveCamera :
    IBinarySerializable
{
    private LiveCameraShot[] shots = [];
    public LiveCameraShot[] Shots { get => shots; set => shots = value; }

    public void Deserialize(EndianBinaryReader reader)
    {
        int count = (int)reader.BaseStream.Length / LiveCameraShot.StructSize;
        reader.Read(ref shots, count);
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        writer.Write(shots);
    }
}
