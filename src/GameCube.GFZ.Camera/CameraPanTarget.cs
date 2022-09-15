using Manifold;
using Manifold.IO;
using System;
using System.IO;
using Unity.Mathematics;

namespace GameCube.GFZ.Camera
{
    [Serializable]
    public sealed class CameraPanTarget :
        IBinaryAddressable,
        IBinarySerializable
    {
        // FIELDS
        private float3 cameraPosition;
        private float3 lookAtPosition;
        private float fieldOfView;
        private Int16Rotation rotationRoll;
        private ushort zero_0x1E;
        private CameraPanInterpolation interpolation;
        private ushort zero_0x22;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }

        public float3 CameraPosition
        {
            get => cameraPosition;
            set => cameraPosition = value;
        }
        public float3 LookAtPosition
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
        public CameraPanInterpolation Interpolation
        {
            get => interpolation;
            set => interpolation = value;
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
                reader.Read(ref interpolation);
                reader.Read(ref zero_0x22);
            }
            this.RecordEndAddress(reader);

            // Assertions
            Assert.IsTrue(Zero_0x1E == 0);
            Assert.IsTrue(Zero_0x22 == 0);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(cameraPosition);
                writer.Write(lookAtPosition);
                writer.Write(fieldOfView);
                writer.Write(rotationRoll);
                writer.Write(zero_0x1E);
                writer.Write(interpolation);
                writer.Write(zero_0x22);
            }
            this.RecordEndAddress(writer);
        }

    }
}
