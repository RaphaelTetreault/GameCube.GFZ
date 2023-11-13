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

        public static readonly PilotPosition[] Default = new PilotPosition[]
        {
            new PilotPosition(PilotID.MightyGazelle, new float[] { 0f, 0.62f, 1.085f }),
            new PilotPosition(PilotID.JudySummer, new float[] { 0f, 0.36f, -0.4715f }),
            new PilotPosition(PilotID.DrStewart, new float[] { 0f, 0.55f, 0.115f }),
            new PilotPosition(PilotID.Baba, new float[] { 0f, 0.505f, 0.4f }),
            new PilotPosition(PilotID.SamuraiGoroh, new float[] { 0f, 0.525f, -1.01f }),
            new PilotPosition(PilotID.Pico, new float[] { 0f, 0.63f, -1.29f }),
            new PilotPosition(PilotID.CaptainFalcon, new float[] { 0f, 0.43f, -0.665f }),
            new PilotPosition(PilotID.Octoman, new float[] { 0f, 0.435f, -0.61f }),
            new PilotPosition(PilotID.MrEAD, new float[] { 0f, 0.825f, -0.31f }),
            new PilotPosition(PilotID.JamesMcCloud, new float[] { 0f, 0.64f, 0.2f }),
            new PilotPosition(PilotID.Billy, new float[] { 0f, 0.62f, -1.02f }),
            new PilotPosition(PilotID.KateAlen, new float[] { 0f, 0.39f, -0.96f }),
            new PilotPosition(PilotID.Zoda, new float[] { 0f, 0.73f, 0.4625f }),
            new PilotPosition(PilotID.JackLevin, new float[] { 0f, 0.485f, -0.3f }),
            new PilotPosition(PilotID.BioRex, new float[] { 0f, 0.63f, 0.09f }),
            new PilotPosition(PilotID.TheSkull, new float[] { 0f, 0.45f, 0.4975f }),
            new PilotPosition(PilotID.AntonioGuster, new float[] { 0f, 0.585f, -0.2075f }),
            new PilotPosition(PilotID.Beastman, new float[] { 0f, 0.615f, -0.175f }),
            new PilotPosition(PilotID.Leon, new float[] { 0f, 1f, -0.66f }),
            new PilotPosition(PilotID.SuperArrow, new float[] { 0f, 0.93f, 1.04f }),
            new PilotPosition(PilotID.MrsArrow, new float[] { 0f, 0.92f, 1.06f }),
            new PilotPosition(PilotID.GomarShioh, new float[] { 0.715f, 0.415f, 0.925f }),
            new PilotPosition(PilotID.SilverNeelsen, new float[] { 0f, 0.52f, 0.29f }),
            new PilotPosition(PilotID.MichaelChain, new float[] { 0f, 0.44f, -1.115f }),
            new PilotPosition(PilotID.BloodFalcon, new float[] { 0f, 0.375f, -0.27f }),
            new PilotPosition(PilotID.JohnTanaka, new float[] { 0f, 0.76f, -0.03f }),
            new PilotPosition(PilotID.Draq, new float[] { 0f, 0.39f, -0.02f }),
            new PilotPosition(PilotID.RogerBuster, new float[] { 0f, 0.73f, 0.035f }),
            new PilotPosition(PilotID.DrClash, new float[] { 0f, 0.85f, -0.07f }),
            new PilotPosition(PilotID.BlackShadow, new float[] { 0f, 1.455f, 0.81f }),
            new PilotPosition(PilotID.Deathborn, new float[] { 0f, 0.8f, -0.065f }),
            new PilotPosition(PilotID.DonGenie, new float[] { 0f, 0.65f, -0.19f }),
            new PilotPosition(PilotID.DigiBoy, new float[] { 0f, 1.475f, 0.595f }),
            new PilotPosition(PilotID.DaiSanGen, new float[] { 0f, 0.85f, -1.72f }),
            new PilotPosition(PilotID.Spade, new float[] { 0f, 2f, 0f }),
            new PilotPosition(PilotID.Daigoroh, new float[] { 0f, 0.565f, -0.1f }),
            new PilotPosition(PilotID.Princia, new float[] { 0f, 0.65f, -0.725f }),
            new PilotPosition(PilotID.Lily, new float[] { 0f, 0.7f, -0.92f }),
            new PilotPosition(PilotID.PJ, new float[] { -0.29f, 0.7f, -0.29f }),
            new PilotPosition(PilotID.QQQ, new float[] { 0f, 2f, 0f }),
            new PilotPosition(PilotID.Pheonix, new float[] { 0f, 0.685f, -0.43f }),
            new PilotPosition(PilotID.Gomar, new float[] { -0.715f, 0.345f, 0.805f }),
            new PilotPosition(PilotID.San, new float[] { 0.42f, 0.85f, -1.17f }),
            new PilotPosition(PilotID.Gen, new float[] { -0.42f, 0.85f, -1.17f }),
        };
    };
}
