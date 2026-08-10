using Manifold.IO;
using System.Numerics;

namespace GameCube.GFZ.Camera;

/// <summary>
///     Represents initial position of camera and target to pan towards.
/// </summary>
public sealed class PreviewCameraPoint :
    IBinaryAddressable,
    IBinarySerializable
{
    // FIELDS
    private Vector3 cameraPosition;
    private Vector3 lookAtPosition;
    private float fieldOfView;
    private Int16Rotation rotationRoll;
    private ushort zero_0x1E;
    private PreviewCameraMode mode;
    private ushort zero_0x22;


    // PROPERTIES
    public AddressRange AddressRange { get; set; }

    public Vector3 CameraPosition
    {
        get => cameraPosition;
        set => cameraPosition = value;
    }
    public Vector3 LookAtPosition
    {
        get => lookAtPosition;
        set => lookAtPosition = value;
    }
    public float FieldOfView
    {
        get => fieldOfView;
        set => fieldOfView = value;
    }
    public Int16Rotation RotationRoll
    {
        get => rotationRoll;
        set => rotationRoll = value;
    }
    public ushort Zero_0x1E
    {
        get => zero_0x1E;
        set => zero_0x1E = value;
    }
    public PreviewCameraMode Mode
    {
        get => mode;
        set => mode = value;
    }
    public ushort Zero_0x22
    {
        get => zero_0x22;
        set => zero_0x22 = value;
    }


    // METHODS
    public void Deserialize(EndianBinaryReader reader)
    {
        this.RecordStartAddress(reader);
        {
            reader.Read(ref cameraPosition);
            reader.Read(ref lookAtPosition);
            reader.Read(ref fieldOfView);
            reader.Read(ref rotationRoll);
            reader.Read(ref zero_0x1E);
            reader.Read(ref mode);
            reader.Read(ref zero_0x22);
        }
        this.RecordEndAddress(reader);

        Assert.IsTrue(Zero_0x1E == 0);
        Assert.IsTrue(Zero_0x22 == 0);
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        Assert.IsTrue(Zero_0x1E == 0);
        Assert.IsTrue(Zero_0x22 == 0);

        this.RecordStartAddress(writer);
        {
            writer.Write(cameraPosition);
            writer.Write(lookAtPosition);
            writer.Write(fieldOfView);
            writer.Write(rotationRoll);
            writer.Write(zero_0x1E);
            writer.Write(mode);
            writer.Write(zero_0x22);
        }
        this.RecordEndAddress(writer);
    }

}
