namespace GameCube.GFZ.CarData
{
    public class PilotPosition
    {
        public PilotID ID { get; }
        public float[] Position { get; }

        public PilotPosition(PilotID id, float[] position)
        {
            ID = id;
            Position = position;
        }
    };
}
