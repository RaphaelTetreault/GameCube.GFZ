using Manifold.IO;
using System;

namespace GameCube.GFZ.LineREL
{
    public struct VehicleRating :
        IBinarySerializable
    {
        public const int Size = sizeof(LetterRating) * 3;

        public LetterRating body;
        public LetterRating boost;
        public LetterRating grip;

        public VehicleRating()
        {
        }
        public VehicleRating(string value)
        {
            VehicleRating temp = FromString(value);
            body = temp.body;
            boost = temp.boost;
            grip = temp.grip;
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
                boost = FromChar(rating[1]),
                grip = FromChar(rating[2]),
            };
            return value;
        }
        public static LetterRating FromChar(char rating)
        {
            bool isAlphaLower = rating >= 'a' && rating <= 'z';
            bool isAlphaUpper = rating >= 'A' && rating <= 'Z';
            bool isNumeric = rating >= '0' && rating <= '9';
            bool isInvalid = !(isAlphaLower || isAlphaUpper || isNumeric);
            if (isInvalid)
            {
                string msg =
                    $"Character '{rating}' is not alphanumeric. " +
                    $"Use characters SABCDE, sabcde, or 012345.";
                throw new ArgumentException(msg);
            }

            LetterRating value = Enum.Parse<LetterRating>(rating.ToString(), true);
            return value;
        }

        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref body);
            reader.Read(ref boost);
            reader.Read(ref grip);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(body);
            writer.Write(boost);
            writer.Write(grip);
        }
    }
}
