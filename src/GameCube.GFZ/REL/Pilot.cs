using System.ComponentModel;
using System.Collections.Generic;

namespace GameCube.GFZ.REL
{
    
    
    public class PilotInformation
    {
        public enum Pilot : byte
    {
        [Description("Mighty Gazelle")]
        MightyGazelle,

        [Description("Judy Summer")]
        JudySummer,

        [Description("Dr. Stewart")]
        DrStewart,

        [Description("Baba")]
        Baba,

        [Description("Samurai Goroh")]
        SamuraiGoroh,

        [Description("Pico")]
        Pico,

        [Description("Captain Falcon")]
        CaptainFalcon,

        [Description("Octoman")]
        Octoman,

        [Description("Mr. EAD")]
        MrEAD,

        [Description("James McCloud")]
        JamesMcCloud,

        [Description("Billy")]
        Billy,

        [Description("Kate Alen")]
        KateAlen,

        [Description("Zoda")]
        Zoda,

        [Description("Jack Levin")]
        JackLevin,

        [Description("Bio Rex")]
        BioRex,

        [Description("The Skull")]
        TheSkull,

        [Description("Antonio Guster")]
        AntonioGuster,

        [Description("Beastman")]
        Beastman,

        [Description("Leon")]
        Leon,

        [Description("Super Arrow")]
        SuperArrow,

        [Description("Mrs. Arrow")]
        MrsArrow,

        [Description("Gomar & Shioh")]
        GomarShioh,

        [Description("Silver Neelsen")]
        SilverNeelsen,

        [Description("Michael Chain")]
        MichaelChain,

        [Description("Blood Falcon")]
        BloodFalcon,

        [Description("John Tanaka")]
        JohnTanaka,

        [Description("Draq")]
        Draq,

        [Description("Roger Buster")]
        RogerBuster,

        [Description("Dr. Clash")]
        DrClash,

        [Description("Black Shadow")]
        BlackShadow,

        [Description("Deathborn")]
        Deathborn,

        [Description("Don Genie")]
        DonGenie,

        [Description("Digi-Boy")]
        DigiBoy,

        [Description("Dai San Gen")]
        DaiSanGen,

        [Description("Spade")]
        Spade,

        [Description("Daigoroh")]
        Daigoroh,

        [Description("Princia")]
        Princia,

        [Description("Lily")]
        Lily,

        [Description("PJ")]
        PJ,

        [Description("QQQ")]
        QQQ,

        [Description("Pheonix")]
        Pheonix,

        [Description("Gomar")]
        Gomar,

        [Description("San")]
        San,

        [Description("Gen")]
        Gen,

        [Description("Interviewer")]
        Interviewer,
    }
            public class PilotPosition
            {
                public Pilot ID { get; }
                public float[] Position { get; }

                public PilotPosition(Pilot id, float[] position)
                {
                    ID = id;
                    Position = position;
                }
        };
        public List<PilotPosition> PilotPositions = new List<PilotPosition>();
        public PilotInformation()
        {
            PilotPositions.Add(new PilotPosition(Pilot.MightyGazelle, new float[] { 0f, 0.62f, 1.085f } ));
            PilotPositions.Add(new PilotPosition(Pilot.JudySummer, new float[] { 0f, 0.36f, -0.4715f } ));
            PilotPositions.Add(new PilotPosition(Pilot.DrStewart, new float[] { 0f, 0.55f, 0.115f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Baba, new float[] { 0f, 0.505f, 0.4f } ));
            PilotPositions.Add(new PilotPosition(Pilot.SamuraiGoroh, new float[] { 0f, 0.525f, -1.01f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Pico, new float[] { 0f, 0.63f, -1.29f } ));
            PilotPositions.Add(new PilotPosition(Pilot.CaptainFalcon, new float[] { 0f, 0.43f, -0.665f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Octoman, new float[] { 0f, 0.435f, -0.61f } ));
            PilotPositions.Add(new PilotPosition(Pilot.MrEAD, new float[] { 0f, 0.825f, -0.31f } ));
            PilotPositions.Add(new PilotPosition(Pilot.JamesMcCloud, new float[] { 0f, 0.64f, 0.2f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Billy, new float[] { 0f, 0.62f, -1.02f } ));
            PilotPositions.Add(new PilotPosition(Pilot.KateAlen, new float[] { 0f, 0.39f, -0.96f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Zoda, new float[] { 0f, 0.73f, 0.4625f } ));
            PilotPositions.Add(new PilotPosition(Pilot.JackLevin, new float[] { 0f, 0.485f, -0.3f } ));
            PilotPositions.Add(new PilotPosition(Pilot.BioRex, new float[] { 0f, 0.63f, 0.09f } ));
            PilotPositions.Add(new PilotPosition(Pilot.TheSkull, new float[] { 0f, 0.45f, 0.4975f } ));
            PilotPositions.Add(new PilotPosition(Pilot.AntonioGuster, new float[] { 0f, 0.585f, -0.2075f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Beastman, new float[] { 0f, 0.615f, -0.175f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Leon, new float[] { 0f, 1f, -0.66f } ));
            PilotPositions.Add(new PilotPosition(Pilot.SuperArrow, new float[] { 0f, 0.93f, 1.04f } ));
            PilotPositions.Add(new PilotPosition(Pilot.MrsArrow, new float[] { 0f, 0.92f, 1.06f } ));
            PilotPositions.Add(new PilotPosition(Pilot.GomarShioh, new float[] { 0.715f, 0.415f, 0.925f } ));
            PilotPositions.Add(new PilotPosition(Pilot.SilverNeelsen, new float[] { 0f, 0.52f, 0.29f } ));
            PilotPositions.Add(new PilotPosition(Pilot.MichaelChain, new float[] { 0f, 0.44f, -1.115f } ));
            PilotPositions.Add(new PilotPosition(Pilot.BloodFalcon, new float[] { 0f, 0.375f, -0.27f } ));
            PilotPositions.Add(new PilotPosition(Pilot.JohnTanaka, new float[] { 0f, 0.76f, -0.03f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Draq, new float[] { 0f, 0.39f, -0.02f } ));
            PilotPositions.Add(new PilotPosition(Pilot.RogerBuster, new float[] { 0f, 0.73f, 0.035f } ));
            PilotPositions.Add(new PilotPosition(Pilot.DrClash, new float[] { 0f, 0.85f, -0.07f } ));
            PilotPositions.Add(new PilotPosition(Pilot.BlackShadow, new float[] { 0f, 1.455f, 0.81f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Deathborn, new float[] { 0f, 0.8f, -0.065f } ));
            PilotPositions.Add(new PilotPosition(Pilot.DonGenie, new float[] { 0f, 0.65f, -0.19f } ));
            PilotPositions.Add(new PilotPosition(Pilot.DigiBoy, new float[] { 0f, 1.475f, 0.595f } ));
            PilotPositions.Add(new PilotPosition(Pilot.DaiSanGen, new float[] { 0f, 0.85f, -1.72f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Spade, new float[] { 0f, 2f, 0f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Daigoroh, new float[] { 0f, 0.565f, -0.1f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Princia, new float[] { 0f, 0.65f, -0.725f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Lily, new float[] { 0f, 0.7f, -0.92f } ));
            PilotPositions.Add(new PilotPosition(Pilot.PJ, new float[] { -0.29f, 0.7f, -0.29f } ));
            PilotPositions.Add(new PilotPosition(Pilot.QQQ, new float[] { 0f, 2f, 0f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Pheonix, new float[] { 0f, 0.685f, -0.43f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Gomar, new float[] { -0.715f, 0.345f, 0.805f } ));
            PilotPositions.Add(new PilotPosition(Pilot.San, new float[] { 0.42f, 0.85f, -1.17f } ));
            PilotPositions.Add(new PilotPosition(Pilot.Gen, new float[] { -0.42f, 0.85f, -1.17f } ));
        }
    };
    

 
}
