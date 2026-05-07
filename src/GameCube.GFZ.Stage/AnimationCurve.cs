using Manifold;
using Manifold.IO;

namespace GameCube.GFZ.Stage;

/// <summary>
///     A series of animation keys to define how a singular float value changes over "time"
/// </summary>
/// <remarks>
///     Structurally, this class is a simple wrapper around a KeyableAttribute[].
/// </remarks>
public sealed class AnimationCurve :
    IBinaryAddressable,
    IBinarySerializable,
    ITextPrintable
{
    // FIELDS
    private KeyableAttribute[] keyableAttributes = [];


    // CONSTRUCTORS
    public AnimationCurve()
    {
    }
    public AnimationCurve(int numKeyables)
    {
        keyableAttributes = new KeyableAttribute[numKeyables];
    }
    public AnimationCurve(params KeyableAttribute[] keyables)
    {
        keyableAttributes = keyables;
    }


    // INDEXERS
    /// <summary>
    ///     Indexes into the underlying <cref>KeyableAttribute</cref> array.
    /// </summary>
    /// <param name="index">The index of the desired value.</param>
    /// <returns>
    ///     Returns the <cref>KeyableAttribute</cref> at the specified <paramref name="index"/>.
    /// </returns>
    public KeyableAttribute this[int index] { get => keyableAttributes[index]; set => keyableAttributes[index] = value; }

    // PROPERTIES
    public AddressRange AddressRange { get; set; }
    public int Length => keyableAttributes.Length;
    public KeyableAttribute[] KeyableAttributes { get => keyableAttributes; set => keyableAttributes = value; }


    // METHODS
    public void Deserialize(EndianBinaryReader reader)
    {
        this.RecordStartAddress(reader);
        {
            reader.Read(ref keyableAttributes, KeyableAttributes.Length);
        }
        this.RecordEndAddress(reader);
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        if (keyableAttributes.Length > 0)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(keyableAttributes);
            }
            this.RecordEndAddress(writer);
        }
        else
        {
            // If nothing to serialize, zero out address as
            // it will be used to get ptr
            AddressRange = new AddressRange();
        }
    }

    /// <summary>
    ///     Forwards the underlying KeyableAttributes[].GetArrayPointer();
    /// </summary>
    /// <returns>
    ///     
    /// </returns>
    public ArrayPointer GetArrayPointer()
    {
        return keyableAttributes.GetArrayPointer();
    }

    public override string ToString()
    {
        return PrintSingleLine();
    }

    public string PrintSingleLine()
    {
        // Print a summary of how many animation keys this curve has
        return $"{nameof(AnimationCurve)}({nameof(KeyableAttribute)}s:[{keyableAttributes.Length}])";
    }

    public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
    {
        int lengthKeyables = keyableAttributes.Length;

        // Print overview of the animation curve
        builder.AppendLineIndented(indent, indentLevel, PrintSingleLine());
        indentLevel++;
        // Then print each keyable (single line) on their own line
        for (int i = 0; i < lengthKeyables; i++)
        {
            var keyable = keyableAttributes[i];
            var keyableText = keyable.PrintKeyableCondensed();
            builder.AppendLineIndented(indent, indentLevel, $"[{i}]\t {keyableText}");
        }
    }

    public float Evaluate(float time)
    {
        // If null.
        if (keyableAttributes == null)
            throw new System.NullReferenceException($"{nameof(KeyableAttributes)} is null.");
        // If no keys. TODO: better to have custom exception here.
        else if (keyableAttributes.Length == 0)
            throw new System.Exception($"No {nameof(KeyableAttributes)}.");
        // If only 1 key -or- time is before first key.
        else if (keyableAttributes.Length == 1 || time <= keyableAttributes[0].Time)
            return keyableAttributes[0].Value;
        // If time is after last key.
        else if (keyableAttributes[^1].Time <= time)
            return keyableAttributes[^1].Value;

        // Find which keys to sample from.
        // Max iterations is -2 due to sampling 2 keys. If only 2 keys, no iterations.
        int keyIndex0 = 0;
        for (; keyIndex0 < keyableAttributes.Length - 2; keyIndex0++)
            if (time < keyableAttributes[keyIndex0].Time)
                break;
        int keyIndex1 = keyIndex0 + 1; // just the next index
        // Select appropriate keys
        KeyableAttribute key0 = keyableAttributes[keyIndex0];
        KeyableAttribute key1 = keyableAttributes[keyIndex1];
        // Interpolate based on mode of key0
        float value = key0.EaseMode switch
        {
            InterpolationMode.Constant => key0.Value,
            InterpolationMode.Linear => EvaluateLinear(key0, key1, time),
            // Yes, the game really does treat anything not above as cubic.
            InterpolationMode.Cubic or
            InterpolationMode.CubicAlt or
            _ => EvaluateCubic(key0, key1, time),
        };
        return value;
    }

    public static float EvaluateLinear(KeyableAttribute key0, KeyableAttribute key1, float time)
    {
        float timeInterval = key1.Time - key0.Time;
        float timeNormalized = (time - key0.Time) / timeInterval;
        float interpolatedValue = (key0.Value * (1.0f - timeNormalized)) + (key1.Value * timeNormalized);
        return interpolatedValue;
    }

    public static float EvaluateCubic(KeyableAttribute key0, KeyableAttribute key1, float time)
    {
        // 2026/04/15 Port of Twilight's decompilation efforts. Thanks!
        float timeInterval = key1.Time - key0.Time;
        float timeNormalized = (time - key0.Time) / timeInterval;
        float timeSquared = timeNormalized * timeNormalized;
        float timeCubicMinusSquared = (timeNormalized * timeSquared) - timeSquared;
        float cubicWeight = timeCubicMinusSquared + timeCubicMinusSquared - timeSquared;
        float interpolatedValue =
            ((key0.TangentOut * (timeCubicMinusSquared - timeSquared + timeNormalized) + timeCubicMinusSquared * key1.TangentIn) * timeInterval) +
            ((key0.Value * (cubicWeight + 1.0f)) - cubicWeight * key1.Value);
        return interpolatedValue;
    }
}
