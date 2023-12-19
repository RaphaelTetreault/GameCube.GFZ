using Manifold.IO;
using System;

namespace GameCube.GFZ.LineREL
{
    public struct VehicleRating :
        IBinarySerializable
    {
        public const int Size = sizeof(LetterRating) * 3;

        public LetterRating body;
        public LetterRating cockpit;
        public LetterRating booster;

        public VehicleRating()
        {
        }
        public VehicleRating(string value)
        {
            VehicleRating temp = FromString(value);
            body = temp.body;
            cockpit = temp.cockpit;
            booster = temp.booster;
        }

        public static VehicleRating FromString(string rating)
        {
            if (rating == null)
            {
                string msg = $"Argument {nameof(rating)} is null.";
                throw new ArgumentException(msg);
            }

            if (rating.Length != 3)
            {
                string msg = $"Argument {nameof(rating)} is not exactly 3 characters long.";
                throw new ArgumentException(msg);
            }

            VehicleRating value = new VehicleRating
            {
                body = FromChar(rating[0]),
                cockpit = FromChar(rating[1]),
                booster = FromChar(rating[2]),
            };
            return value;
        }
        public static LetterRating FromChar(char rating)
        {
            const string msg = "Character is not S, A, B, C, D, or E.";

            return rating switch
            {
                // Uppercase
                'S' => LetterRating.S,
                'A' => LetterRating.A,
                'B' => LetterRating.B,
                'C' => LetterRating.C,
                'D' => LetterRating.D,
                'E' => LetterRating.E,
                // Lowercase
                's' => LetterRating.S,
                'a' => LetterRating.A,
                'b' => LetterRating.B,
                'c' => LetterRating.C,
                'd' => LetterRating.D,
                'e' => LetterRating.E,
                // Error on anything else
                _ => throw new System.ArgumentException(msg),
            };
        }

        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref body);
            reader.Read(ref cockpit);
            reader.Read(ref booster);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(body);
            writer.Write(cockpit);
            writer.Write(booster);
        }
    }
}
