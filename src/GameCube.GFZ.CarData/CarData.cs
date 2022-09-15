using Manifold;
using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.CarData
{
    [Serializable]
    public class CarData :
        IBinarySerializable,
        IBinaryFileType
    {
        // CONSTANTS
        // Numbers of things
        public const int MachineCount = 41;
        public const int BodyCount = 25;
        public const int CockpitCount = 25;
        public const int BoosterCount = 25;
        // Sizes of things
        public const int kPaddingSize = 12;
        public const int kMachineNameTable = 43;
        public const int kPartsInternalTable = 32;


        // FIELDS
        // String table
        private byte[] padding; // 12 bytes
        public ShiftJisCString[] machineNames;
        public ShiftJisCString[] partsInternalNames;
        // Vehicles
        public VehicleParameters DarkSchneider;
        public VehicleParameters RedGazelle;
        public VehicleParameters WhiteCat;
        public VehicleParameters GoldenFox;
        public VehicleParameters IronTiger;
        public VehicleParameters FireStingray;
        public VehicleParameters WildGoose;
        public VehicleParameters BlueFalcon;
        public VehicleParameters DeepClaw;
        public VehicleParameters GreatStar;
        public VehicleParameters LittleWyvern;
        public VehicleParameters MadWolf;
        public VehicleParameters SuperPiranha;
        public VehicleParameters DeathAnchor;
        public VehicleParameters AstroRobin;
        public VehicleParameters BigFang;
        public VehicleParameters SonicPhantom;
        public VehicleParameters GreenPanther;
        public VehicleParameters HyperSpeeder;
        public VehicleParameters SpaceAngler;
        public VehicleParameters KingMeteor;
        public VehicleParameters QueenMeteor;
        public VehicleParameters TwinNoritta;
        public VehicleParameters NightThunder;
        public VehicleParameters WildBoar;
        public VehicleParameters BloodHawk;
        public VehicleParameters WonderWasp;
        public VehicleParameters MightyTyphoon;
        public VehicleParameters MightyHurricane;
        public VehicleParameters CrazyBear;
        public VehicleParameters BlackBull;
        public VehicleParameters FatShark;
        public VehicleParameters CosmicDolphin;
        public VehicleParameters PinkSpider;
        public VehicleParameters MagicSeagull;
        public VehicleParameters SilverRat;
        public VehicleParameters SparkMoon;
        public VehicleParameters BunnyFlash;
        public VehicleParameters GroovyTaxi;
        public VehicleParameters RollingTurtle;
        public VehicleParameters RainbowPhoenix;
        // Body Parts
        public VehicleParameters BraveEagle;
        public VehicleParameters GalaxyFalcon;
        public VehicleParameters GiantPlanet;
        public VehicleParameters MegaloCruiser;
        public VehicleParameters SplashWhale;
        public VehicleParameters WildChariot;
        public VehicleParameters ValiantJaguar;
        public VehicleParameters HolySpider;
        public VehicleParameters BloodRaven;
        public VehicleParameters FunnySwallow;
        public VehicleParameters OpticalWing;
        public VehicleParameters MadBull;
        public VehicleParameters BigTyrant;
        public VehicleParameters GrandBase;
        public VehicleParameters FireWolf;
        public VehicleParameters DreadHammer;
        public VehicleParameters SilverSword;
        public VehicleParameters RageKnight;
        public VehicleParameters RapidBarrel;
        public VehicleParameters SkyHorse;
        public VehicleParameters AquaGoose;
        public VehicleParameters SpaceCancer;
        public VehicleParameters MetalShell;
        public VehicleParameters SpeedyDragon;
        public VehicleParameters LibertyManta;
        // Cockpit parts
        public VehicleParameters WonderWorm;
        public VehicleParameters RushCyclone;
        public VehicleParameters CombatCannon;
        public VehicleParameters MuscleGorilla;
        public VehicleParameters CyberFox;
        public VehicleParameters HeatSnake;
        public VehicleParameters RaveDrifter;
        public VehicleParameters AerialBullet;
        public VehicleParameters SparkBird;
        public VehicleParameters BlastCamel;
        public VehicleParameters DarkChaser;
        public VehicleParameters GarnetPhantom;
        public VehicleParameters BrightSpear;
        public VehicleParameters HyperStream;
        public VehicleParameters SuperLynx;
        public VehicleParameters CrystalEgg;
        public VehicleParameters WindyShark;
        public VehicleParameters RedRex;
        public VehicleParameters SonicSoldier;
        public VehicleParameters MaximumStar;
        public VehicleParameters MoonSnail;
        public VehicleParameters CrazyBuffalo;
        public VehicleParameters ScudViper;
        public VehicleParameters RoundDisk;
        public VehicleParameters EnergyCrest;
        // Booster parts
        public VehicleParameters Euros_01;
        public VehicleParameters Triangle_GT;
        public VehicleParameters Velocity_J;
        public VehicleParameters Sunrise140;
        public VehicleParameters Saturn_SG;
        public VehicleParameters Bluster_X;
        public VehicleParameters Devilfish_RX;
        public VehicleParameters Mars_EX;
        public VehicleParameters Titan_G4;
        public VehicleParameters Extreme_ZZ;
        public VehicleParameters Thunderbolt_V2;
        public VehicleParameters Boxer_2C;
        public VehicleParameters Shuttle_M2;
        public VehicleParameters Punisher_4X;
        public VehicleParameters Scorpion_R;
        public VehicleParameters Raiden_88;
        public VehicleParameters Impulse220;
        public VehicleParameters Bazooka_YS;
        public VehicleParameters Meteor_RR;
        public VehicleParameters Tiger_RZ;
        public VehicleParameters Hornet_FX;
        public VehicleParameters Jupiter_Q;
        public VehicleParameters Comet_V;
        public VehicleParameters Crown_77;
        public VehicleParameters Triple_Z;


        // Properties
        public string FileName { get; set; }

        public Endianness Endianness => Endianness.LittleEndian;

        public string FileExtension => "";

        /// <summary>
        /// Returns all machines in internal strucuture order (X, GX, AX)
        /// </summary>
        public VehicleParameters[] MachinesInternalOrder => new VehicleParameters[]
        {
            RedGazelle,
            WhiteCat,
            GoldenFox,
            IronTiger,
            FireStingray,
            WildGoose,
            BlueFalcon,
            DeepClaw,
            GreatStar,
            LittleWyvern,
            MadWolf,
            SuperPiranha,
            DeathAnchor,
            AstroRobin,
            BigFang,
            SonicPhantom,
            GreenPanther,
            HyperSpeeder,
            SpaceAngler,
            KingMeteor,
            QueenMeteor,
            TwinNoritta,
            NightThunder,
            WildBoar,
            BloodHawk,
            WonderWasp,
            MightyTyphoon,
            MightyHurricane,
            CrazyBear,
            BlackBull,
            DarkSchneider,
            FatShark,
            CosmicDolphin,
            PinkSpider,
            MagicSeagull,
            SilverRat,
            SparkMoon,
            BunnyFlash,
            GroovyTaxi,
            RollingTurtle,
            RainbowPhoenix,
        };

        /// <summary>
        /// Returns all body parts in internal order.
        /// </summary>
        public VehicleParameters[] BodyParts => new VehicleParameters[]
        {
            BraveEagle,
            GalaxyFalcon,
            GiantPlanet,
            MegaloCruiser,
            SplashWhale,
            WildChariot,
            ValiantJaguar,
            HolySpider,
            BloodRaven,
            FunnySwallow,
            OpticalWing,
            MadBull,
            BigTyrant,
            GrandBase,
            FireWolf,
            DreadHammer,
            SilverSword,
            RageKnight,
            RapidBarrel,
            SkyHorse,
            AquaGoose,
            SpaceCancer,
            MetalShell,
            SpeedyDragon,
            LibertyManta,
        };

        /// <summary>
        /// Returns all cockpoit parts in internal order.
        /// </summary>
        public VehicleParameters[] CockpitParts => new VehicleParameters[]
        {
            WonderWorm,
            RushCyclone,
            CombatCannon,
            MuscleGorilla,
            CyberFox,
            HeatSnake,
            RaveDrifter,
            AerialBullet,
            SparkBird,
            BlastCamel,
            DarkChaser,
            GarnetPhantom,
            BrightSpear,
            HyperStream,
            SuperLynx,
            CrystalEgg,
            WindyShark,
            RedRex,
            SonicSoldier,
            MaximumStar,
            MoonSnail,
            CrazyBuffalo,
            ScudViper,
            RoundDisk,
            EnergyCrest,
        };

        /// <summary>
        /// 
        /// </summary>
        public VehicleParameters[] BoosterParts => new VehicleParameters[]
        {
            Euros_01,
            Triangle_GT,
            Velocity_J,
            Sunrise140,
            Saturn_SG,
            Bluster_X,
            Devilfish_RX,
            Mars_EX,
            Titan_G4,
            Extreme_ZZ,
            Thunderbolt_V2,
            Boxer_2C,
            Shuttle_M2,
            Punisher_4X,
            Scorpion_R,
            Raiden_88,
            Impulse220,
            Bazooka_YS,
            Meteor_RR,
            Tiger_RZ,
            Hornet_FX,
            Jupiter_Q,
            Comet_V,
            Crown_77,
            Triple_Z,
        };

        /// <summary>
        /// 
        /// </summary>
        public static readonly ShiftJisCString[] InternalPartNamesTable = new ShiftJisCString[]
        {
            "GC-D3",
            "GC-E2",
            "AC-B2",
            "AC-C1",
            "GC-C1",
            "GC-C2",
            "GC-C4",
            "GC-D2",
            "AC-C2",
            "AC-A2",
            "GC-B2",
            "GC-A2",
            "GC-A1",
            "AC-B3",
            "AC-D2",
            "GC-D1",
            "GC-C3",
            "AC-C3",
            "GC-B1",
            "AC-B4",
            "AC-E1",
            "AC-A1",
            "AC-B1",
            "GC-E1",
            "AC-D1",
            "AC-C4",
            "GC-B3",
            "AC-E2",
            "GC-A3",
            "AC-D3",
        };

        /// <summary>
        /// Ordered as they are in the file
        /// </summary>
        public static ShiftJisCString[] MachineNamesTable = new ShiftJisCString[]
        {
            "Rainbow Phoenix",
            "Rolling Turtle",
            "Groovy Taxi",
            "Bunny Flash",
            "Spark Moon",
            "Silver Rat",
            "Magic Seagull",
            "Pink Spider",
            "Cosmic Dolphin",
            "Fat Shark",
            "Dark Schneider",
            "Black Bull",
            "Crazy Bear",
            "Mighty Hurricane",
            "Mighty Typhoon",
            "Wonder Wasp",
            "Blood Hawk",
            "Wild Boar",
            "Night Thunder",
            "Twin Noritta",
            "Queen Meteor",
            "King Meteor",
            "Space Angler",
            "Hyper Speeder",
            "Green Panther",
            "Sonic Phantom",
            "Big Fang",
            "Astro Robin",
            "Death Anchor",
            "Super Piranha",
            "Mad Wolf",
            "Little Wyvern",
            "Great Star",
            "Deep Claw",
            "Blue Falcon",
            "Wild Goose",
            "Fire Stingray",
            "Iron Tiger",
            "Golden Fox",
            "White Cat",
            "Red Gazelle",
        };


        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref RedGazelle);
            reader.Read(ref WhiteCat);
            reader.Read(ref GoldenFox);
            reader.Read(ref IronTiger);
            reader.Read(ref FireStingray);
            reader.Read(ref WildGoose);
            reader.Read(ref BlueFalcon);
            reader.Read(ref DeepClaw);
            reader.Read(ref GreatStar);
            reader.Read(ref LittleWyvern);
            reader.Read(ref MadWolf);
            reader.Read(ref SuperPiranha);
            reader.Read(ref DeathAnchor);
            reader.Read(ref AstroRobin);
            reader.Read(ref BigFang);
            reader.Read(ref SonicPhantom);
            reader.Read(ref GreenPanther);
            reader.Read(ref HyperSpeeder);
            reader.Read(ref SpaceAngler);
            reader.Read(ref KingMeteor);
            reader.Read(ref QueenMeteor);
            reader.Read(ref TwinNoritta);
            reader.Read(ref NightThunder);
            reader.Read(ref WildBoar);
            reader.Read(ref BloodHawk);
            reader.Read(ref WonderWasp);
            reader.Read(ref MightyTyphoon);
            reader.Read(ref MightyHurricane);
            reader.Read(ref CrazyBear);
            reader.Read(ref BlackBull);
            reader.Read(ref DarkSchneider);
            reader.Read(ref FatShark);
            reader.Read(ref CosmicDolphin);
            reader.Read(ref PinkSpider);
            reader.Read(ref MagicSeagull);
            reader.Read(ref SilverRat);
            reader.Read(ref SparkMoon);
            reader.Read(ref BunnyFlash);
            reader.Read(ref GroovyTaxi);
            reader.Read(ref RollingTurtle);
            reader.Read(ref RainbowPhoenix);

            // Read some padding
            reader.Read(ref padding, kPaddingSize);
            foreach (var @byte in padding)
                Assert.IsTrue(@byte == 0);

            // 2022/01/28: I may have broken this. Used to be for-loop
            // manually assigning each value from length-init array.
            reader.Read(ref machineNames, kMachineNameTable);

            // Body parts
            reader.Read(ref BraveEagle);
            reader.Read(ref GalaxyFalcon);
            reader.Read(ref GiantPlanet);
            reader.Read(ref MegaloCruiser);
            reader.Read(ref SplashWhale);
            reader.Read(ref WildChariot);
            reader.Read(ref ValiantJaguar);
            reader.Read(ref HolySpider);
            reader.Read(ref BloodRaven);
            reader.Read(ref FunnySwallow);
            reader.Read(ref OpticalWing);
            reader.Read(ref MadBull);
            reader.Read(ref BigTyrant);
            reader.Read(ref GrandBase);
            reader.Read(ref FireWolf);
            reader.Read(ref DreadHammer);
            reader.Read(ref SilverSword);
            reader.Read(ref RageKnight);
            reader.Read(ref RapidBarrel);
            reader.Read(ref SkyHorse);
            reader.Read(ref AquaGoose);
            reader.Read(ref SpaceCancer);
            reader.Read(ref MetalShell);
            reader.Read(ref SpeedyDragon);
            reader.Read(ref LibertyManta);

            // Cockpit parts
            reader.Read(ref WonderWorm);
            reader.Read(ref RushCyclone);
            reader.Read(ref CombatCannon);
            reader.Read(ref MuscleGorilla);
            reader.Read(ref CyberFox);
            reader.Read(ref HeatSnake);
            reader.Read(ref RaveDrifter);
            reader.Read(ref AerialBullet);
            reader.Read(ref SparkBird);
            reader.Read(ref BlastCamel);
            reader.Read(ref DarkChaser);
            reader.Read(ref GarnetPhantom);
            reader.Read(ref BrightSpear);
            reader.Read(ref HyperStream);
            reader.Read(ref SuperLynx);
            reader.Read(ref CrystalEgg);
            reader.Read(ref WindyShark);
            reader.Read(ref RedRex);
            reader.Read(ref SonicSoldier);
            reader.Read(ref MaximumStar);
            reader.Read(ref MoonSnail);
            reader.Read(ref CrazyBuffalo);
            reader.Read(ref ScudViper);
            reader.Read(ref RoundDisk);
            reader.Read(ref EnergyCrest);

            // Booster parts
            reader.Read(ref Euros_01);
            reader.Read(ref Triangle_GT);
            reader.Read(ref Velocity_J);
            reader.Read(ref Sunrise140);
            reader.Read(ref Saturn_SG);
            reader.Read(ref Bluster_X);
            reader.Read(ref Devilfish_RX);
            reader.Read(ref Mars_EX);
            reader.Read(ref Titan_G4);
            reader.Read(ref Extreme_ZZ);
            reader.Read(ref Thunderbolt_V2);
            reader.Read(ref Boxer_2C);
            reader.Read(ref Shuttle_M2);
            reader.Read(ref Punisher_4X);
            reader.Read(ref Scorpion_R);
            reader.Read(ref Raiden_88);
            reader.Read(ref Impulse220);
            reader.Read(ref Bazooka_YS);
            reader.Read(ref Meteor_RR);
            reader.Read(ref Tiger_RZ);
            reader.Read(ref Hornet_FX);
            reader.Read(ref Jupiter_Q);
            reader.Read(ref Comet_V);
            reader.Read(ref Crown_77);
            reader.Read(ref Triple_Z);

            // 2022/01/28: I may have broken this. Used to be for-loop
            // manually assigning each value from length-init array.
            reader.Read(ref partsInternalNames, kPartsInternalTable);

        }

        public void Serialize(EndianBinaryWriter writer)
        {
            // Machines
            writer.Write(RedGazelle);
            writer.Write(WhiteCat);
            writer.Write(GoldenFox);
            writer.Write(IronTiger);
            writer.Write(FireStingray);
            writer.Write(WildGoose);
            writer.Write(BlueFalcon);
            writer.Write(DeepClaw);
            writer.Write(GreatStar);
            writer.Write(LittleWyvern);
            writer.Write(MadWolf);
            writer.Write(SuperPiranha);
            writer.Write(DeathAnchor);
            writer.Write(AstroRobin);
            writer.Write(BigFang);
            writer.Write(SonicPhantom);
            writer.Write(GreenPanther);
            writer.Write(HyperSpeeder);
            writer.Write(SpaceAngler);
            writer.Write(KingMeteor);
            writer.Write(QueenMeteor);
            writer.Write(TwinNoritta);
            writer.Write(NightThunder);
            writer.Write(WildBoar);
            writer.Write(BloodHawk);
            writer.Write(WonderWasp);
            writer.Write(MightyTyphoon);
            writer.Write(MightyHurricane);
            writer.Write(CrazyBear);
            writer.Write(BlackBull);
            writer.Write(DarkSchneider);
            writer.Write(FatShark);
            writer.Write(CosmicDolphin);
            writer.Write(PinkSpider);
            writer.Write(MagicSeagull);
            writer.Write(SilverRat);
            writer.Write(SparkMoon);
            writer.Write(BunnyFlash);
            writer.Write(GroovyTaxi);
            writer.Write(RollingTurtle);
            writer.Write(RainbowPhoenix);

            for (int i = 0; i < kPaddingSize; i++)
                writer.Write((byte)0);

            // Machine names
            writer.Write(machineNames);

            // Body parts
            writer.Write(BraveEagle);
            writer.Write(GalaxyFalcon);
            writer.Write(GiantPlanet);
            writer.Write(MegaloCruiser);
            writer.Write(SplashWhale);
            writer.Write(WildChariot);
            writer.Write(ValiantJaguar);
            writer.Write(HolySpider);
            writer.Write(BloodRaven);
            writer.Write(FunnySwallow);
            writer.Write(OpticalWing);
            writer.Write(MadBull);
            writer.Write(BigTyrant);
            writer.Write(GrandBase);
            writer.Write(FireWolf);
            writer.Write(DreadHammer);
            writer.Write(SilverSword);
            writer.Write(RageKnight);
            writer.Write(RapidBarrel);
            writer.Write(SkyHorse);
            writer.Write(AquaGoose);
            writer.Write(SpaceCancer);
            writer.Write(MetalShell);
            writer.Write(SpeedyDragon);
            writer.Write(LibertyManta);

            // Cockpoit parts
            writer.Write(WonderWorm);
            writer.Write(RushCyclone);
            writer.Write(CombatCannon);
            writer.Write(MuscleGorilla);
            writer.Write(CyberFox);
            writer.Write(HeatSnake);
            writer.Write(RaveDrifter);
            writer.Write(AerialBullet);
            writer.Write(SparkBird);
            writer.Write(BlastCamel);
            writer.Write(DarkChaser);
            writer.Write(GarnetPhantom);
            writer.Write(BrightSpear);
            writer.Write(HyperStream);
            writer.Write(SuperLynx);
            writer.Write(CrystalEgg);
            writer.Write(WindyShark);
            writer.Write(RedRex);
            writer.Write(SonicSoldier);
            writer.Write(MaximumStar);
            writer.Write(MoonSnail);
            writer.Write(CrazyBuffalo);
            writer.Write(ScudViper);
            writer.Write(RoundDisk);
            writer.Write(EnergyCrest);

            // Booster parts
            writer.Write(Euros_01);
            writer.Write(Triangle_GT);
            writer.Write(Velocity_J);
            writer.Write(Sunrise140);
            writer.Write(Saturn_SG);
            writer.Write(Bluster_X);
            writer.Write(Devilfish_RX);
            writer.Write(Mars_EX);
            writer.Write(Titan_G4);
            writer.Write(Extreme_ZZ);
            writer.Write(Thunderbolt_V2);
            writer.Write(Boxer_2C);
            writer.Write(Shuttle_M2);
            writer.Write(Punisher_4X);
            writer.Write(Scorpion_R);
            writer.Write(Raiden_88);
            writer.Write(Impulse220);
            writer.Write(Bazooka_YS);
            writer.Write(Meteor_RR);
            writer.Write(Tiger_RZ);
            writer.Write(Hornet_FX);
            writer.Write(Jupiter_Q);
            writer.Write(Comet_V);
            writer.Write(Crown_77);
            writer.Write(Triple_Z);

            // Custom parts names
            writer.Write(partsInternalNames);
        }

    }
}