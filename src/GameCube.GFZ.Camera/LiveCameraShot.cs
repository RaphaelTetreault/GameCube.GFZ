using Manifold.IO;
using System;
using System.Numerics;

namespace GameCube.GFZ.Camera;

public sealed class LiveCameraShot :
    IBinaryAddressable,
    IBinarySerializable
{
    public const int StructSize = 40; // 0x28

    private int ballIndex;
    private Vector3 positionFrom;
    private Vector3 positionTo;
    private Int16Rotation roll;
    private ushort zero_0x1E;
    private float fov;
    private LiveCameraType type;

    public AddressRange AddressRange { get; set; }
    public int BallIndex { get => ballIndex; set => ballIndex = value; }
    public Vector3 PositionFrom { get => positionFrom; set => positionFrom = value; }
    public Vector3 PositionTo { get => positionTo; set => positionTo = value; }
    public Int16Rotation Roll { get => roll; set => roll = value; }
    public ushort Zero_0x1E { get => zero_0x1E; set => zero_0x1E = value; }
    public float Fov { get => fov; set => fov = value; }
    public LiveCameraType Type { get => type; set => type = value; }

    public void Deserialize(EndianBinaryReader reader)
    {
        AddressRange.RecordStartAddress(reader);
        reader.Read(ref ballIndex);
        reader.Read(ref positionFrom);
        reader.Read(ref positionTo);
        reader.Read(ref roll);
        reader.Read(ref zero_0x1E);
        reader.Read(ref fov);
        reader.Read(ref type);
        AddressRange.RecordEndAddress(reader);

        Assert.IsTrue(zero_0x1E == 0);
        Assert.IsTrue(Enum.IsDefined(type));
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        Assert.IsTrue(zero_0x1E == 0);
        Assert.IsTrue(Enum.IsDefined(type));

        AddressRange.RecordStartAddress(writer);
        writer.Write(ballIndex);
        writer.Write(positionFrom);
        writer.Write(positionTo);
        writer.Write(roll);
        writer.Write(zero_0x1E);
        writer.Write(fov);
        writer.Write(type);
        AddressRange.RecordEndAddress(writer);
    }
}
