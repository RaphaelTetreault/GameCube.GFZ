using Manifold.IO;
using GameCube.GFZ.CarData;
using GameCube.GFZ.GameData;
using System;
using System.Linq;
using System.Numerics;

namespace GameCube.GFZ.REL;

/// <summary>
///     Utility for working with line__.rel.
/// </summary>
/// <remarks>
///     TODO: deprecate this file. Move all relevant code to <see cref="Manifold.GFZCLI.ActionsREL"/>.
/// </remarks>
public class FzMainRelUtility
{
    private static void ValidateStageIndex(int index, int maxIndex, int minIndex = 0)
    {
        bool tooSmall = index < minIndex;
        bool tooBig = index > maxIndex;
        if (tooSmall || tooBig)
            throw new IndexOutOfRangeException($"Index must be between {minIndex} and {maxIndex}. ({index})");
    }

    public static void PatchCustomMinimapParameters(EndianBinaryWriter writer, FzMainRel lookup, int index, MinimapProjection minimapProjection)
    {
        ValidateStageIndex(index, GameDataConsts.MaxMinimapIndex);

        int baseAddress = lookup.CourseMinimapParameterStructs.Address;
        int offset = MinimapProjection.StructSize * index;
        int address = baseAddress + offset;

        writer.JumpToAddress(address);
        writer.Write(minimapProjection);
    }

    public static void PatchCourseBgm(EndianBinaryWriter writer, FzMainRel lookup, int courseIndex, byte bgmIndex)
    {
        ValidateStageIndex(courseIndex, GameDataConsts.MaxBgmIndex);
        BgmMusicDB.ThrowIfBgmIndexInvalid(bgmIndex);
        // Patch BGM Final Lap music, stride of 1
        writer.JumpToAddress(lookup.CourseBgmIndex.Address + courseIndex);
        writer.Write(bgmIndex);
    }

    public static void PatchCourseBgmFinalLap(EndianBinaryWriter writer, FzMainRel lookup, int courseIndex, BgmFinalLap bgmfl)
    {
        ValidateStageIndex(courseIndex, GameDataConsts.MaxBgmflIndex);
        BgmMusicDB.ThrowIfBgmIndexInvalid(bgmfl.songIndex);
        // Patch BGM Final Lap music, stride of 4
        writer.JumpToAddress(lookup.CourseBgmFinalLapIndex.Address + courseIndex * 4);
        writer.Write(bgmfl);
    }

    public static void PatchVenueIndex(EndianBinaryWriter writer, FzMainRel lookup, int index, VenueIndex venue)
    {
        ValidateStageIndex(index, 110);

        if ((byte)venue > 0x14)
        {
            throw new ArgumentException("Invalid Venue");
        }

        writer.JumpToAddress(lookup.CourseVenueIndex.Address + (int)index);
        writer.Write((byte)venue);
    }

    public static void PatchDifficultyRatingToSlot(EndianBinaryWriter writer, FzMainRel lookup, int index, byte difficulty)
    {
        ValidateStageIndex(index, 110);

        if (difficulty > 24)
        {
            //Debug.LogWarning("More than 24 stars cannot be displayed");
            throw new ArgumentException("More than 24 stars cannot be displayed");
        }

        writer.JumpToAddress(lookup.CourseDifficulty.Address + (int)index);
        writer.Write(difficulty);
    }

    public static void PatchCupSlot(EndianBinaryWriter writer, FzMainRel lookup, CupIndex cup, int courseIndex)
    {
        ValidateStageIndex(courseIndex, 110, -1);

        // Offset between cups
        Offset offset = (int)cup * 0xC; // 12

        // Patch stage index in cup
        writer.JumpToAddress(lookup.CupCourseLut.Address + offset);
        writer.Write(courseIndex);
        // Patch index for loading in assets (GMA, TPL)
        writer.JumpToAddress(lookup.CupCourseLutAssets.Address + offset);
        writer.Write(courseIndex);
        // Patch the index for... something we don't currenlty know.
        writer.JumpToAddress(lookup.CupCourseLutUnk.Address + offset);
        writer.Write(courseIndex);
    }

    public static void PatchCupSlots(EndianBinaryWriter writer, FzMainRel lookup, CupIndex cup, short[] courses)
    {
        if (courses.Length < 1 || courses.Length > 6)
        {
            throw new IndexOutOfRangeException("At least 1 or up to 6 courses must be defined");
        }

        if ((byte)cup > 10)
        {
            throw new ArgumentException("Invalid Cup");
        }

        for (int i = 0; i < courses.Length; ++i)
        {
            if (courses[i] > 110 || courses[i] < -1)
            {
                throw new ArgumentException("Invalid course ID");
            }
        }

        if (courses.Length < 6)
        {
            do
            {
                courses = courses.Concat(new short[] { -1 }).ToArray();
            }
            while (courses.Length < 6);
        }

        writer.JumpToAddress(lookup.CupCourseLut.Address + (int)cup * 0xC);
        writer.Write(courses);
        writer.JumpToAddress(lookup.CupCourseLutAssets.Address + (int)cup * 0xC);
        writer.Write(courses);
        writer.JumpToAddress(lookup.CupCourseLutUnk.Address + (int)cup * 0xC);
        writer.Write(courses);
    }

    public static void PatchAxTimer(EndianBinaryWriter writer, FzMainRel lookup, AcCupCourseIndex courseId, byte seconds)
    {
        if ((byte)courseId > 6)
        {
            throw new ArgumentException("Invalid Course ID");
        }

        writer.JumpToAddress(lookup.AxModeCourseTimers.Address + (int)courseId);
        writer.Write(seconds);
    }

    public static void PatchPilotPosition(EndianBinaryWriter writer, FzMainRel lookup, PilotIndex id, Vector3 position)
    {
        if (id > PilotIndex.Gen)
        {
            throw new ArgumentException("Invalid Pilot ID");
        }

        writer.JumpToAddress(lookup.PilotPositions.Address + (int)id * 0xc);
        writer.Write(position);
    }

    public static void PatchPilotToMachine(EndianBinaryWriter writer, FzMainRel lookup, MachineIndex machine, PilotIndex pilot)
    {
        if (pilot > PilotIndex.Gen)
        {
            throw new ArgumentException("Invalid Pilot ID");
        }

        if (machine > MachineIndex.RainbowPhoenix)
        {
            throw new ArgumentException("Invalid Machine ID");
        }

        if (pilot > PilotIndex.Pheonix)
        {
            throw new ArgumentException($"ID: {pilot} will only work with Free Run Races! Different Race Settings will freeze the game!");
        }

        throw new NotImplementedException();
        //PatchPilotPosition(writer, lookup, pilot, PilotPosition.Default[(int)machine].Position);

        writer.JumpToAddress(lookup.PilotToMachineLut.Address + (int)machine * 4);
        writer.Write((int)pilot);
    }

}
