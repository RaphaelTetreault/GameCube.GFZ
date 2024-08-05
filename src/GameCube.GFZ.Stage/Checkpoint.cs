using Manifold;
using Manifold.IO;
using System;
using System.IO;
using System.Numerics;

namespace GameCube.GFZ.Stage
{
    /// <summary>
    /// Defines a checkpoint range along the track spline. This is a vital piece of
    /// metadata as it tells the game which part of the track any vehicle is on and 
    /// uses it compute a lot of the vehicle position and validity of that position.
    /// When incorrect, many strange, buggy things happen.
    /// </summary>
    [Serializable]
    public sealed class Checkpoint :
        IBinaryAddressable,
        IBinarySerializable,
        ITextPrintable
    {
        // FIELDS
        private float curveTimeStart;
        private float curveTimeEnd;
        private Plane planeStart;
        private Plane planeEnd;
        private float startDistance;
        private float endDistance;
        private float trackWidth;
        private bool connectToTrackIn;
        private bool connectToTrackOut;
        private ushort zero_0x4E; // asserted


        // PROPERTIES
        public AddressRange AddressRange { get; set; }

        /// <summary>
        /// Where along the track's Position.XYZ animation curve 'start' elements were sampled.
        /// </summary>
        public float CurveTimeStart { get => curveTimeStart; set => curveTimeStart = value; }

        /// <summary>
        /// Where along the track's Position.XYZ animation curve 'end' elements were sampled.
        /// </summary>
        public float CurveTimeEnd { get => curveTimeEnd; set => curveTimeEnd = value; }

        /// <summary>
        /// A plane which partitions global coordinate space into two. Plane points "forward"
        /// along track to indicate what is in the checkpoint. With 'end' plane, creates
        /// a volume which is considered inside this checkpoint.
        /// </summary>
        public Plane PlaneStart { get => planeStart; set => planeStart = value; }

        /// <summary>
        /// A plane which partitions global coordinate space into two. Plane points "backwards"
        /// along track to indicate what is in the checkpoint. With 'start' plane, creates
        /// a volume which is considered inside this checkpoint.
        /// </summary>
        public Plane PlaneEnd { get => planeEnd; set => planeEnd = value; }

        /// <summary>
        /// Absolute distance marker indicating where the 'start' plane is for this checkpoint
        /// along the track. Ex: 3000m/8000m
        /// </summary>
        public float StartDistance { get => startDistance; set => startDistance = value; }

        /// <summary>
        /// Absolute distance marker indicating where the 'end' plane is for this checkpoint
        /// along the track. Ex: 3500m/8000m
        /// </summary>
        public float EndDistance { get => endDistance; set => endDistance = value; }

        /// <summary>
        /// How wide the track is. TODO: confirm if width is for start point (assumed) or end point.
        /// </summary>
        public float TrackWidth { get => trackWidth; set => trackWidth = value; }

        /// <summary>
        /// Whether or not checkpoint is physically connected to the previous checkpoint.
        /// False when there is a gap between this checkpoint and the previous checkpoint.
        /// </summary>
        public bool ConnectToTrackIn { get => connectToTrackIn; set => connectToTrackIn = value; }

        /// <summary>
        /// Whether or not checkpoint is physically connected to the next checkpoint.
        /// False when there is a gap between this checkpoint and the next checkpoint.
        /// </summary>
        public bool ConnectToTrackOut { get => connectToTrackOut; set => connectToTrackOut = value; }



        /// <summary>
        /// Returns the minimum Y component (height) of any of the checkpoints'
        /// two planes. Needed for other track metatdata elsewhere.
        /// </summary>
        /// <param name="checkpoints"></param>
        /// <returns></returns>
        public static float GetMinHeight(Checkpoint[] checkpoints)
        {
            var min = float.PositiveInfinity;

            // iterate over every position, mo
            foreach (var checkpoint in checkpoints)
            {
                min = Math.Min(min, checkpoint.PlaneStart.origin.Y);
                min = Math.Min(min, checkpoint.PlaneEnd.origin.Y);
            }

            return min;
        }

        // track checkpoint matrix bounds
        public float GetMinPositionX()
        {
            return Math.Min(PlaneStart.origin.X, PlaneEnd.origin.X);
        }
        public float GetMinPositionZ()
        {
            return Math.Min(PlaneStart.origin.Z, PlaneEnd.origin.Z);
        }
        public float GetMaxPositionX()
        {
            return Math.Max(PlaneStart.origin.X, PlaneEnd.origin.X);
        }
        public float GetMaxPositionZ()
        {
            return Math.Max(PlaneStart.origin.Z, PlaneEnd.origin.Z);
        }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref curveTimeStart);
                reader.Read(ref curveTimeEnd);
                reader.Read(ref planeStart);
                reader.Read(ref planeEnd);
                reader.Read(ref startDistance);
                reader.Read(ref endDistance);
                reader.Read(ref trackWidth);
                reader.Read(ref connectToTrackIn);
                reader.Read(ref connectToTrackOut);
                reader.Read(ref zero_0x4E);
            }
            this.RecordEndAddress(reader);
            {
                Assert.IsTrue(zero_0x4E == 0);
            }
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(curveTimeStart);
                writer.Write(curveTimeEnd);
                writer.Write(planeStart);
                writer.Write(planeEnd);
                writer.Write(startDistance);
                writer.Write(endDistance);
                writer.Write(trackWidth);
                writer.Write(connectToTrackIn);
                writer.Write(connectToTrackOut);
                writer.Write(zero_0x4E);
            }
            this.RecordEndAddress(writer);
        }

        public override string ToString() => PrintSingleLine();

        public string PrintSingleLine()
        {
            return $"{nameof(Checkpoint)}({nameof(CurveTimeStart)}: {CurveTimeStart:0.00}, {nameof(CurveTimeEnd)}: {CurveTimeEnd:0.00})";
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, nameof(Checkpoint));
            indentLevel++;
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(curveTimeStart)}: {curveTimeStart}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(curveTimeEnd)}: {curveTimeEnd}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(planeStart)}: {planeStart}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(planeEnd)}: {planeEnd}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(startDistance)}: {startDistance}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(endDistance)}: {endDistance}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(trackWidth)}: {trackWidth}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(connectToTrackIn)}: {connectToTrackIn}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(connectToTrackOut)}: {connectToTrackOut}");
        }

    }
}
