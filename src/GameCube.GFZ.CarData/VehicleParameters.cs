using Manifold.IO;
using Unity.Mathematics;

namespace GameCube.GFZ.CarData
{
    // Structure
    // https://github.com/yoshifan/fzerogx-docs/blob/master/addresses/base_machine_stat_blocks.md

    [Serializable]
    public struct VehicleParameters :
        IBinarySerializable,
        IBinaryAddressable
    {
        // FIELDS
        private Pointer runtimeNamePtr;
        private float weight;
        private float acceleration;
        private float maxSpeed;
        private float grip1;
        private float grip3;
        private float turnTension;
        private float driftAcceleration;
        private float turnMovement;
        private float strafeTurn;
        private float strafe;
        private float turnReaction;
        private float grip2;
        private float boostStrength;
        private float boostDuration;
        private float turnDeceleration;
        private float drag;
        private float body;
        private CarDataFlags0x48 unk_0x48;
        private CarDataFlags0x49 unk_0x49;
        private ushort zero_0x4A;
        private float cameraReorientation;
        private float cameraRepositioning;
        private float3 tiltFrontRight;
        private float3 tiltFrontLeft;
        private float3 tiltBackRight;
        private float3 tiltBackLeft;
        private float3 wallCollisionFrontRight;
        private float3 wallCollisionFrontLeft;
        private float3 wallCollisionBackRight;
        private float3 wallCollisionBackLeft;
        // REFERENCES
        private ShiftJisCString runtimeName;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public Pointer RuntimeNamePtr { get => runtimeNamePtr; set => runtimeNamePtr = value; }
        public float Weight { get => weight; set => weight = value; }
        public float Acceleration { get => acceleration; set => acceleration = value; }
        public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
        public float Grip1 { get => grip1; set => grip1 = value; }
        public float Grip3 { get => grip3; set => grip3 = value; }
        public float TurnTension { get => turnTension; set => turnTension = value; }
        public float DriftAcceleration { get => driftAcceleration; set => driftAcceleration = value; }
        public float TurnMovement { get => turnMovement; set => turnMovement = value; }
        public float StrafeTurn { get => strafeTurn; set => strafeTurn = value; }
        public float Strafe { get => strafe; set => strafe = value; }
        public float TurnReaction { get => turnReaction; set => turnReaction = value; }
        public float Grip2 { get => grip2; set => grip2 = value; }
        public float BoostStrength { get => boostStrength; set => boostStrength = value; }
        public float BoostDuration { get => boostDuration; set => boostDuration = value; }
        public float TurnDeceleration { get => turnDeceleration; set => turnDeceleration = value; }
        public float Drag { get => drag; set => drag = value; }
        public float Body { get => body; set => body = value; }
        public CarDataFlags0x48 Unk_0x48 { get => unk_0x48; set => unk_0x48 = value; }
        public CarDataFlags0x49 Unk_0x49 { get => unk_0x49; set => unk_0x49 = value; }
        public ushort Zero_0x4A { get => zero_0x4A; set => zero_0x4A = value; }
        public float CameraReorientation { get => cameraReorientation; set => cameraReorientation = value; }
        public float CameraRepositioning { get => cameraRepositioning; set => cameraRepositioning = value; }
        public float3 TiltFrontRight { get => tiltFrontRight; set => tiltFrontRight = value; }
        public float3 TiltFrontLeft { get => tiltFrontLeft; set => tiltFrontLeft = value; }
        public float3 TiltBackRight { get => tiltBackRight; set => tiltBackRight = value; }
        public float3 TiltBackLeft { get => tiltBackLeft; set => tiltBackLeft = value; }
        public float3 WallCollisionFrontRight { get => wallCollisionFrontRight; set => wallCollisionFrontRight = value; }
        public float3 WallCollisionFrontLeft { get => wallCollisionFrontLeft; set => wallCollisionFrontLeft = value; }
        public float3 WallCollisionBackRight { get => wallCollisionBackRight; set => wallCollisionBackRight = value; }
        public float3 WallCollisionBackLeft { get => wallCollisionBackLeft; set => wallCollisionBackLeft = value; }
        public ShiftJisCString RuntimeName { get => runtimeName; set => runtimeName = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref runtimeNamePtr);
                reader.Read(ref weight);
                reader.Read(ref acceleration);
                reader.Read(ref maxSpeed);
                reader.Read(ref grip1);
                reader.Read(ref grip3);
                reader.Read(ref turnTension);
                reader.Read(ref driftAcceleration);
                reader.Read(ref turnMovement);
                reader.Read(ref strafeTurn);
                reader.Read(ref strafe);
                reader.Read(ref turnReaction);
                reader.Read(ref grip2);
                reader.Read(ref boostStrength);
                reader.Read(ref boostDuration);
                reader.Read(ref turnDeceleration);
                reader.Read(ref drag);
                reader.Read(ref body);
                reader.Read(ref unk_0x48);
                reader.Read(ref unk_0x49);
                reader.Read(ref zero_0x4A);
                reader.Read(ref cameraReorientation);
                reader.Read(ref cameraRepositioning);
                reader.Read(ref tiltFrontRight);
                reader.Read(ref tiltFrontLeft);
                reader.Read(ref tiltBackRight);
                reader.Read(ref tiltBackLeft);
                reader.Read(ref wallCollisionFrontRight);
                reader.Read(ref wallCollisionFrontLeft);
                reader.Read(ref wallCollisionBackRight);
                reader.Read(ref wallCollisionBackLeft);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(runtimeNamePtr);
                writer.Write(weight);
                writer.Write(acceleration);
                writer.Write(maxSpeed);
                writer.Write(grip1);
                writer.Write(grip3);
                writer.Write(turnTension);
                writer.Write(driftAcceleration);
                writer.Write(turnMovement);
                writer.Write(strafeTurn);
                writer.Write(strafe);
                writer.Write(turnReaction);
                writer.Write(grip2);
                writer.Write(boostStrength);
                writer.Write(boostDuration);
                writer.Write(turnDeceleration);
                writer.Write(drag);
                writer.Write(body);
                writer.Write(unk_0x48);
                writer.Write(unk_0x49);
                writer.Write(zero_0x4A);
                writer.Write(cameraReorientation);
                writer.Write(cameraRepositioning);
                writer.Write(tiltFrontRight);
                writer.Write(tiltFrontLeft);
                writer.Write(tiltBackRight);
                writer.Write(tiltBackLeft);
                writer.Write(wallCollisionFrontRight);
                writer.Write(wallCollisionFrontLeft);
                writer.Write(wallCollisionBackRight);
                writer.Write(wallCollisionBackLeft);
            }
            this.RecordEndAddress(writer);
        }

    }
}