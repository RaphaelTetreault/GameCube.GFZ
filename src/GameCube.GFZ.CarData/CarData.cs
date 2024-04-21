using Manifold;
using Manifold.IO;
using Manifold.Text.Tables;
using System;
using System.IO;
using System.Linq;
using Unity.Mathematics;

namespace GameCube.GFZ.CarData
{
    public class CarData :
        IBinarySerializable,
        IBinaryFileType,
        ITableCollectionSerializable
    {
        // CONSTANTS
        // Numbers of things
        public const int MachineCount = 41;
        public const int BodyCount = 25;
        public const int CockpitCount = 25;
        public const int BoosterCount = 25;
        // Sizes of things
        public const int kPaddingSize = 12;
        public const int kMachineNameTable = MachineCount;
        public const int kPartsInternalTable = 30;
        //
        public const Endianness endianness = Endianness.LittleEndian;

        // FIELDS
        // String table
        public ShiftJisCString[] machineNames = new ShiftJisCString[0];
        public ShiftJisCString[] partsInternalNames = new ShiftJisCString[0];
        // Vehicles
        public VehicleParameters[] Machines = new VehicleParameters[MachineCount];
        public VehicleParameters[] BodyParts = new VehicleParameters[BodyCount];
        public VehicleParameters[] CockpitParts = new VehicleParameters[CockpitCount];
        public VehicleParameters[] BoosterParts = new VehicleParameters[BoosterCount];

        public VehicleParameters DarkSchneider => Machines[30];
        public VehicleParameters RedGazelle => Machines[0];
        public VehicleParameters WhiteCat => Machines[1];
        public VehicleParameters GoldenFox => Machines[2];
        public VehicleParameters IronTiger => Machines[3];
        public VehicleParameters FireStingray => Machines[4];
        public VehicleParameters WildGoose => Machines[5];
        public VehicleParameters BlueFalcon => Machines[6];
        public VehicleParameters DeepClaw => Machines[7];
        public VehicleParameters GreatStar => Machines[8];
        public VehicleParameters LittleWyvern => Machines[9];
        public VehicleParameters MadWolf => Machines[10];
        public VehicleParameters SuperPiranha => Machines[11];
        public VehicleParameters DeathAnchor => Machines[12];
        public VehicleParameters AstroRobin => Machines[13];
        public VehicleParameters BigFang => Machines[14];
        public VehicleParameters SonicPhantom => Machines[15];
        public VehicleParameters GreenPanther => Machines[16];
        public VehicleParameters HyperSpeeder => Machines[17];
        public VehicleParameters SpaceAngler => Machines[18];
        public VehicleParameters KingMeteor => Machines[19];
        public VehicleParameters QueenMeteor => Machines[20];
        public VehicleParameters TwinNoritta => Machines[21];
        public VehicleParameters NightThunder => Machines[22];
        public VehicleParameters WildBoar => Machines[23];
        public VehicleParameters BloodHawk => Machines[24];
        public VehicleParameters WonderWasp => Machines[25];
        public VehicleParameters MightyTyphoon => Machines[26];
        public VehicleParameters MightyHurricane => Machines[27];
        public VehicleParameters CrazyBear => Machines[28];
        public VehicleParameters BlackBull => Machines[29];
        public VehicleParameters FatShark => Machines[31];
        public VehicleParameters CosmicDolphin => Machines[32];
        public VehicleParameters PinkSpider => Machines[33];
        public VehicleParameters MagicSeagull => Machines[34];
        public VehicleParameters SilverRat => Machines[35];
        public VehicleParameters SparkMoon => Machines[36];
        public VehicleParameters BunnyFlash => Machines[37];
        public VehicleParameters GroovyTaxi => Machines[38];
        public VehicleParameters RollingTurtle => Machines[39];
        public VehicleParameters RainbowPhoenix => Machines[40];
        // Body Parts
        public VehicleParameters BraveEagle => BodyParts[0];
        public VehicleParameters GalaxyFalcon => BodyParts[1];
        public VehicleParameters GiantPlanet => BodyParts[2];
        public VehicleParameters MegaloCruiser => BodyParts[3];
        public VehicleParameters SplashWhale => BodyParts[4];
        public VehicleParameters WildChariot => BodyParts[5];
        public VehicleParameters ValiantJaguar => BodyParts[6];
        public VehicleParameters HolySpider => BodyParts[7];
        public VehicleParameters BloodRaven => BodyParts[8];
        public VehicleParameters FunnySwallow => BodyParts[9];
        public VehicleParameters OpticalWing => BodyParts[10];
        public VehicleParameters MadBull => BodyParts[11];
        public VehicleParameters BigTyrant => BodyParts[12];
        public VehicleParameters GrandBase => BodyParts[13];
        public VehicleParameters FireWolf => BodyParts[14];
        public VehicleParameters DreadHammer => BodyParts[15];
        public VehicleParameters SilverSword => BodyParts[16];
        public VehicleParameters RageKnight => BodyParts[17];
        public VehicleParameters RapidBarrel => BodyParts[18];
        public VehicleParameters SkyHorse => BodyParts[19];
        public VehicleParameters AquaGoose => BodyParts[20];
        public VehicleParameters SpaceCancer => BodyParts[21];
        public VehicleParameters MetalShell => BodyParts[22];
        public VehicleParameters SpeedyDragon => BodyParts[23];
        public VehicleParameters LibertyManta => BodyParts[24];
        // Cockpit parts
        public VehicleParameters WonderWorm => CockpitParts[0];
        public VehicleParameters RushCyclone => CockpitParts[1];
        public VehicleParameters CombatCannon => CockpitParts[2];
        public VehicleParameters MuscleGorilla => CockpitParts[3];
        public VehicleParameters CyberFox => CockpitParts[4];
        public VehicleParameters HeatSnake => CockpitParts[5];
        public VehicleParameters RaveDrifter => CockpitParts[6];
        public VehicleParameters AerialBullet => CockpitParts[7];
        public VehicleParameters SparkBird => CockpitParts[8];
        public VehicleParameters BlastCamel => CockpitParts[9];
        public VehicleParameters DarkChaser => CockpitParts[10];
        public VehicleParameters GarnetPhantom => CockpitParts[11];
        public VehicleParameters BrightSpear => CockpitParts[12];
        public VehicleParameters HyperStream => CockpitParts[13];
        public VehicleParameters SuperLynx => CockpitParts[14];
        public VehicleParameters CrystalEgg => CockpitParts[15];
        public VehicleParameters WindyShark => CockpitParts[16];
        public VehicleParameters RedRex => CockpitParts[17];
        public VehicleParameters SonicSoldier => CockpitParts[18];
        public VehicleParameters MaximumStar => CockpitParts[19];
        public VehicleParameters MoonSnail => CockpitParts[20];
        public VehicleParameters CrazyBuffalo => CockpitParts[21];
        public VehicleParameters ScudViper => CockpitParts[22];
        public VehicleParameters RoundDisk => CockpitParts[23];
        public VehicleParameters EnergyCrest => CockpitParts[24];
        // Booster parts
        public VehicleParameters Euros_01 => BoosterParts[0];
        public VehicleParameters Triangle_GT => BoosterParts[1];
        public VehicleParameters Velocity_J => BoosterParts[2];
        public VehicleParameters Sunrise140 => BoosterParts[3];
        public VehicleParameters Saturn_SG => BoosterParts[4];
        public VehicleParameters Bluster_X => BoosterParts[5];
        public VehicleParameters Devilfish_RX => BoosterParts[6];
        public VehicleParameters Mars_EX => BoosterParts[7];
        public VehicleParameters Titan_G4 => BoosterParts[8];
        public VehicleParameters Extreme_ZZ => BoosterParts[9];
        public VehicleParameters Thunderbolt_V2 => BoosterParts[10];
        public VehicleParameters Boxer_2C => BoosterParts[11];
        public VehicleParameters Shuttle_M2 => BoosterParts[12];
        public VehicleParameters Punisher_4X => BoosterParts[13];
        public VehicleParameters Scorpion_R => BoosterParts[14];
        public VehicleParameters Raiden_88 => BoosterParts[15];
        public VehicleParameters Impulse220 => BoosterParts[16];
        public VehicleParameters Bazooka_YS => BoosterParts[17];
        public VehicleParameters Meteor_RR => BoosterParts[18];
        public VehicleParameters Tiger_RZ => BoosterParts[19];
        public VehicleParameters Hornet_FX => BoosterParts[20];
        public VehicleParameters Jupiter_Q => BoosterParts[21];
        public VehicleParameters Comet_V => BoosterParts[22];
        public VehicleParameters Crown_77 => BoosterParts[23];
        public VehicleParameters Triple_Z => BoosterParts[24];


        // Properties
        public string FileName { get; set; } = string.Empty;

        public Endianness Endianness => endianness;

        public string FileExtension => "";


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

        public static readonly byte[] DigitsPadding = new byte[]
        {
            0,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35,
            0,
        };

        public static readonly byte[] ZerosPadding = new byte[12];

        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref Machines, MachineCount);
            SkipPadding(reader, ZerosPadding);
            reader.Read<ShiftJisCString>(ref machineNames, kMachineNameTable);
            SkipPadding(reader, DigitsPadding);
            reader.Read(ref BodyParts, BodyCount);
            reader.Read(ref CockpitParts, CockpitCount);
            reader.Read(ref BoosterParts, BoosterCount);
            reader.Read<ShiftJisCString>(ref partsInternalNames, kPartsInternalTable);
            SkipPadding(reader, DigitsPadding);

            // Name table is reversed, so swap it around.
            machineNames = machineNames.Reverse().ToArray();
            for (int i = 0; i < Machines.Length; i++)
                Machines[i].RuntimeName = machineNames[i];

            for (int i = 0; i < BodyParts.Length; i++)
                BodyParts[i].RuntimeName = ((CustomBodyPartName)i).ToString();
            for (int i = 0; i < CockpitParts.Length; i++)
                CockpitParts[i].RuntimeName = ((CustomCockpitPartName)i).ToString();
            for (int i = 0; i < BoosterParts.Length; i++)
                BoosterParts[i].RuntimeName = ((CustomBoosterPartName)i).ToString();
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            // Name table is reversed. Don't assign reference otherwise it flips each write.
            var machineNamesReverse = machineNames.Reverse().ToArray();

            writer.Write(Machines);
            writer.Write(ZerosPadding);
            writer.Write<ShiftJisCString>(machineNamesReverse);
            writer.Write(DigitsPadding);
            writer.Write(BodyParts);
            writer.Write(CockpitParts);
            writer.Write(BoosterParts);
            writer.Write<ShiftJisCString>(partsInternalNames);
            writer.Write(DigitsPadding);
        }

        public void SkipPadding(EndianBinaryReader reader, byte[] padding)
        {
            foreach (var @byte in padding)
                Assert.IsTrue(reader.ReadByte() == @byte);
        }


        // TODO: replace instances of this function with new table serialization
        public void Deserialize(StreamReader reader)
        {
            throw new NotFiniteNumberException();
        }

        public void FromTables(TableCollection tableCollection)
        {
            throw new NotImplementedException();
        }

        public void ToTables(TableCollection tableCollection)
        {
            Table[] tables = CreateTables();
            tableCollection.Add(tables);
        }

        public Table[] CreateTables()
        {
            string[] headers = Machines[0].GetHeaders();
            string[] machineNames = MachineNamesTable.Reverse().ToArray().AsStringArray();
            string[] bodyNames = Enum.GetNames<CustomBodyPartName>();
            string[] cockpitNames = Enum.GetNames<CustomCockpitPartName>();
            string[] boosterNames = Enum.GetNames<CustomBoosterPartName>();

            Table[] tables = new Table[4]
            {
                CreateTable("Machine Name", headers, machineNames, Machines),
                CreateTable("Body Name", headers, bodyNames, BodyParts),
                CreateTable("Cockpit Name", headers, cockpitNames, CockpitParts),
                CreateTable("Booster Name", headers, boosterNames, BoosterParts),
            };
            return tables;
        }

        private static Table CreateTable(string rowColHeader, string[] headers, string[] names, VehicleParameters[] vehicleParameters)
        {
            bool isValidLengths = names.Length == vehicleParameters.Length;
            if (!isValidLengths)
            {
                throw new Exception();
            }

            Table table = new Table();

            // Set headers, row headers
            table.SetCell(0, 0, rowColHeader);
            table.SetRow(0, headers, 1);
            table.SetColumn(0, names, 1);

            // Set each value
            table.ResetActiveCell();
            foreach (var vehicleParameter in vehicleParameters)
            {
                vehicleParameter.WriteCells(table);
                table.LineFeed();
            }

            return table;
        }


        // Need a distinct interface for table "root"
        public string[] GetHeaders() => Array.Empty<string>();
    }
}