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

        public void Deserialize(StreamReader reader)
        {
            Machines = new VehicleParameters[MachineCount];
            BodyParts = new VehicleParameters[BodyCount];
            CockpitParts = new VehicleParameters[CockpitCount];
            BoosterParts = new VehicleParameters[BoosterCount];

            var lines = reader.ReadToEnd().Split('\n');
            int currIndex = 2; // skip 2 lines

            // MACHINES
            for (int i = 0; i < Machines.Length; i++)
                Machines[i] = ReadVehicleParameters(lines[currIndex++]);

            // BODY
            currIndex += 3;
            for (int i = 0; i < BodyParts.Length; i++)
                BodyParts[i] = ReadVehicleParameters(lines[currIndex++]);
            // COCKPIT
            currIndex += 3;
            for (int i = 0; i < CockpitParts.Length; i++)
                CockpitParts[i] = ReadVehicleParameters(lines[currIndex++]);
            // BOOSTER
            currIndex += 3;
            for (int i = 0; i < BoosterParts.Length; i++)
                BoosterParts[i] = ReadVehicleParameters(lines[currIndex++]);

            // NAMES
            // My TSV format stores the name as the first entry
            machineNames = new ShiftJisCString[Machines.Length];
            for (int i = 0; i < Machines.Length; i++)
                machineNames[i] = Machines[i].RuntimeName;

            // Default
            partsInternalNames = InternalPartNamesTable;
        }

        public void Serialize(StreamWriter writer)
        {
            writer.WriteLine("Machines");
            WriteHeadings(writer);
            for (int i = 0; i < Machines.Length; i++)
                WriteMachineParameters(writer, Machines[i], i);
            writer.WriteLine();

            writer.WriteLine("Body Parts");
            WriteHeadings(writer);
            for (int i = 0; i < BodyParts.Length; i++)
                WriteMachineParameters(writer, BodyParts[i], i);
            writer.WriteLine();

            writer.WriteLine("Cockpit Parts");
            WriteHeadings(writer);
            for (int i = 0; i < CockpitParts.Length; i++)
                WriteMachineParameters(writer, CockpitParts[i], i);
            writer.WriteLine();

            writer.WriteLine("Booster Parts");
            WriteHeadings(writer);
            for (int i = 0; i < BoosterParts.Length; i++)
                WriteMachineParameters(writer, BoosterParts[i], i);
            writer.WriteLine();
        }

        public void WriteHeadings(StreamWriter writer)
        {
            writer.WriteNextCol(nameof(VehicleParameters.RuntimeName));
            writer.WriteNextCol("Index");
            writer.WriteNextCol(nameof(VehicleParameters.Weight));
            writer.WriteNextCol(nameof(VehicleParameters.Acceleration));
            writer.WriteNextCol(nameof(VehicleParameters.MaxSpeed));
            writer.WriteNextCol(nameof(VehicleParameters.Grip1));
            writer.WriteNextCol(nameof(VehicleParameters.Grip3));
            writer.WriteNextCol(nameof(VehicleParameters.TurnTension));
            writer.WriteNextCol(nameof(VehicleParameters.DriftAcceleration));
            writer.WriteNextCol(nameof(VehicleParameters.TurnMovement));
            writer.WriteNextCol(nameof(VehicleParameters.StrafeTurn));
            writer.WriteNextCol(nameof(VehicleParameters.Strafe));
            writer.WriteNextCol(nameof(VehicleParameters.TurnReaction));
            writer.WriteNextCol(nameof(VehicleParameters.Grip2));
            writer.WriteNextCol(nameof(VehicleParameters.BoostStrength));
            writer.WriteNextCol(nameof(VehicleParameters.BoostDuration));
            writer.WriteNextCol(nameof(VehicleParameters.TurnDeceleration));
            writer.WriteNextCol(nameof(VehicleParameters.Drag));
            writer.WriteNextCol(nameof(VehicleParameters.Body));
            writer.WriteNextCol(nameof(VehicleParameters.Unk_0x48));
            writer.WriteNextCol(nameof(VehicleParameters.Unk_0x49));
            writer.WriteNextCol(nameof(VehicleParameters.Zero_0x4A));
            writer.WriteNextCol(nameof(VehicleParameters.CameraReorientation));
            writer.WriteNextCol(nameof(VehicleParameters.CameraRepositioning));
            writer.WriteNextCol(nameof(VehicleParameters.TiltFrontRight) + ".x");
            writer.WriteNextCol(nameof(VehicleParameters.TiltFrontRight) + ".y");
            writer.WriteNextCol(nameof(VehicleParameters.TiltFrontRight) + ".z");
            writer.WriteNextCol(nameof(VehicleParameters.TiltFrontLeft) + ".x");
            writer.WriteNextCol(nameof(VehicleParameters.TiltFrontLeft) + ".y");
            writer.WriteNextCol(nameof(VehicleParameters.TiltFrontLeft) + ".z");
            writer.WriteNextCol(nameof(VehicleParameters.TiltBackRight) + ".x");
            writer.WriteNextCol(nameof(VehicleParameters.TiltBackRight) + ".y");
            writer.WriteNextCol(nameof(VehicleParameters.TiltBackRight) + ".z");
            writer.WriteNextCol(nameof(VehicleParameters.TiltBackLeft) + ".x");
            writer.WriteNextCol(nameof(VehicleParameters.TiltBackLeft) + ".y");
            writer.WriteNextCol(nameof(VehicleParameters.TiltBackLeft) + ".z");
            writer.WriteNextCol(nameof(VehicleParameters.WallCollisionFrontRight) + ".x");
            writer.WriteNextCol(nameof(VehicleParameters.WallCollisionFrontRight) + ".y");
            writer.WriteNextCol(nameof(VehicleParameters.WallCollisionFrontRight) + ".z");
            writer.WriteNextCol(nameof(VehicleParameters.WallCollisionFrontLeft) + ".x");
            writer.WriteNextCol(nameof(VehicleParameters.WallCollisionFrontLeft) + ".y");
            writer.WriteNextCol(nameof(VehicleParameters.WallCollisionFrontLeft) + ".z");
            writer.WriteNextCol(nameof(VehicleParameters.WallCollisionBackRight) + ".x");
            writer.WriteNextCol(nameof(VehicleParameters.WallCollisionBackRight) + ".y");
            writer.WriteNextCol(nameof(VehicleParameters.WallCollisionBackRight) + ".z");
            writer.WriteNextCol(nameof(VehicleParameters.WallCollisionBackLeft) + ".x");
            writer.WriteNextCol(nameof(VehicleParameters.WallCollisionBackLeft) + ".y");
            writer.WriteNextCol(nameof(VehicleParameters.WallCollisionBackLeft) + ".z");
            writer.WriteNextRow();
        }
        public void WriteMachineParameters(StreamWriter writer, VehicleParameters parameters, int index)
        {
            string name = parameters.RuntimeName == null ? "null" : parameters.RuntimeName;

            writer.WriteNextCol(name);
            writer.WriteNextCol(index);
            writer.WriteNextCol(parameters.Weight);
            writer.WriteNextCol(parameters.Acceleration);
            writer.WriteNextCol(parameters.MaxSpeed);
            writer.WriteNextCol(parameters.Grip1);
            writer.WriteNextCol(parameters.Grip3);
            writer.WriteNextCol(parameters.TurnTension);
            writer.WriteNextCol(parameters.DriftAcceleration);
            writer.WriteNextCol(parameters.TurnMovement);
            writer.WriteNextCol(parameters.StrafeTurn);
            writer.WriteNextCol(parameters.Strafe);
            writer.WriteNextCol(parameters.TurnReaction);
            writer.WriteNextCol(parameters.Grip2);
            writer.WriteNextCol(parameters.BoostStrength);
            writer.WriteNextCol(parameters.BoostDuration);
            writer.WriteNextCol(parameters.TurnDeceleration);
            writer.WriteNextCol(parameters.Drag);
            writer.WriteNextCol(parameters.Body);
            writer.WriteNextCol((int)parameters.Unk_0x48);
            writer.WriteNextCol((int)parameters.Unk_0x49);
            writer.WriteNextCol(parameters.Zero_0x4A);
            writer.WriteNextCol(parameters.CameraReorientation);
            writer.WriteNextCol(parameters.CameraRepositioning);
            writer.WriteNextCol(parameters.TiltFrontRight.x);
            writer.WriteNextCol(parameters.TiltFrontRight.y);
            writer.WriteNextCol(parameters.TiltFrontRight.z);
            writer.WriteNextCol(parameters.TiltFrontLeft.x);
            writer.WriteNextCol(parameters.TiltFrontLeft.y);
            writer.WriteNextCol(parameters.TiltFrontLeft.z);
            writer.WriteNextCol(parameters.TiltBackRight.x);
            writer.WriteNextCol(parameters.TiltBackRight.y);
            writer.WriteNextCol(parameters.TiltBackRight.z);
            writer.WriteNextCol(parameters.TiltBackLeft.x);
            writer.WriteNextCol(parameters.TiltBackLeft.y);
            writer.WriteNextCol(parameters.TiltBackLeft.z);
            writer.WriteNextCol(parameters.WallCollisionFrontRight.x);
            writer.WriteNextCol(parameters.WallCollisionFrontRight.y);
            writer.WriteNextCol(parameters.WallCollisionFrontRight.z);
            writer.WriteNextCol(parameters.WallCollisionFrontLeft.x);
            writer.WriteNextCol(parameters.WallCollisionFrontLeft.y);
            writer.WriteNextCol(parameters.WallCollisionFrontLeft.z);
            writer.WriteNextCol(parameters.WallCollisionBackRight.x);
            writer.WriteNextCol(parameters.WallCollisionBackRight.y);
            writer.WriteNextCol(parameters.WallCollisionBackRight.z);
            writer.WriteNextCol(parameters.WallCollisionBackLeft.x);
            writer.WriteNextCol(parameters.WallCollisionBackLeft.y);
            writer.WriteNextCol(parameters.WallCollisionBackLeft.z);
            writer.WriteNextRow();
        }
        public VehicleParameters ReadVehicleParameters(string line)
        {
            VehicleParameters parameters = new VehicleParameters();
            var cols = line.Split('\t');
            int baseIndex = 0;

            parameters.RuntimeName = ReadNextCol(line, ref baseIndex);
            string index = ReadNextCol(line, ref baseIndex);
            parameters.Weight = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.Acceleration = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.MaxSpeed = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.Grip1 = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.Grip3 = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.TurnTension = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.DriftAcceleration = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.TurnMovement = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.StrafeTurn = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.Strafe = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.TurnReaction = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.Grip2 = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.BoostStrength = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.BoostDuration = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.TurnDeceleration = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.Drag = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.Body = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.Unk_0x48 = (CarDataFlags0x48)int.Parse(ReadNextCol(line, ref baseIndex));
            parameters.Unk_0x49 = (CarDataFlags0x49)int.Parse(ReadNextCol(line, ref baseIndex));
            parameters.Zero_0x4A = (ushort)int.Parse(ReadNextCol(line, ref baseIndex));
            parameters.CameraReorientation = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.CameraRepositioning = float.Parse(ReadNextCol(line, ref baseIndex));
            parameters.TiltFrontRight = ParseFloat3(line, ref baseIndex);
            parameters.TiltFrontLeft = ParseFloat3(line, ref baseIndex);
            parameters.TiltBackRight = ParseFloat3(line, ref baseIndex);
            parameters.TiltBackLeft = ParseFloat3(line, ref baseIndex);
            parameters.WallCollisionFrontRight = ParseFloat3(line, ref baseIndex);
            parameters.WallCollisionFrontLeft = ParseFloat3(line, ref baseIndex);
            parameters.WallCollisionBackRight = ParseFloat3(line, ref baseIndex);
            parameters.WallCollisionBackLeft = ParseFloat3(line, ref baseIndex);

            return parameters;
        }

        public float3 ParseFloat3(string line, ref int baseIndex)
        {
            float x = float.Parse(ReadNextCol(line, ref baseIndex));
            float y = float.Parse(ReadNextCol(line, ref baseIndex));
            float z = float.Parse(ReadNextCol(line, ref baseIndex));
            float3 value = new float3(x, y, z);
            return value;
        }

        public string ReadNextCol(string line, ref int baseIndex)
        {
            // read column string length
            int stringLength = 0;
            for (int i = baseIndex; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '\t' || c == '\n')
                    break;
                stringLength++;
            }

            string str = line.Substring(baseIndex, stringLength);
            baseIndex += stringLength + 1;
            return str;
        }

        public void SkipPadding(EndianBinaryReader reader, byte[] padding)
        {
            foreach (var @byte in padding)
                Assert.IsTrue(reader.ReadByte() == @byte);
        }




        public void GetTables(TableCollection tableCollection)
        {
            throw new NotImplementedException();
        }

        public void AddToTables(TableCollection tableCollection)
        {
            Table machineTable = new Table()
            {
                Name = "Machines",
                ColumnHeadersCount = 1,
                RowHeadersCount = 1,
            };
            tableCollection.Add(machineTable);
            //tableCollection.SetCurrentTable("Machines");

            // Set machines names in column
            machineTable.SetCell(0, 0, "Machine Names");
            string[] machineNames = MachineNamesTable.Reverse().ToArray().AsStringArray();
            machineTable.SetColumn(0, machineNames, 1);
            // Set headers for all columns
            string[] machineAttributes = Machines[0].GetHeaders();
            machineTable.SetRow(0, machineAttributes, 1);
            //
            machineTable.ResetActiveCell();
            foreach (var machine in Machines)
            {
                machine.WriteCells(machineTable);
                machineTable.LineFeed();
            }
        }

        public string[] GetHeaders() => Array.Empty<string>();
    }
}