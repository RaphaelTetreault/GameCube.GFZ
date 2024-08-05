using System;
using System.Collections.Generic;
using System.Numerics;

namespace GameCube.GFZ.Stage
{
    // 2022/03/09: formerly TrackCheckpointGrid

    /// <summary>
    /// A table for index lists specifically for track checkpoints.
    /// </summary>
    [System.Serializable]
    public sealed class CheckpointGrid : IndexGrid
    {
        // CONSTANTS
        public const int Subdivisions = 8;
        public const int kListCount = Subdivisions * Subdivisions;

        // PROPERTIES
        public override int SubdivisionsX => Subdivisions;
        public override int SubdivisionsZ => Subdivisions;


        // STATIC METHODS
        public static GridXZ GetMatrixBoundsXZ(Checkpoint[] checkpoints)
        {
            // Get min and max XZ values of any checkpoint
            Vector3 min = new Vector3(float.MaxValue, 0, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, 0, float.MinValue);

            foreach (var checkpoint in checkpoints)
            {
                // MIN
                min.X = Math.Min(min.X, checkpoint.GetMinPositionX());
                min.Z = Math.Min(min.Z, checkpoint.GetMinPositionZ());

                // MAX
                max.X = Math.Max(max.X, checkpoint.GetMaxPositionX());
                max.Z = Math.Max(max.Z, checkpoint.GetMaxPositionZ());
            }

            // Compute bounds
            var bounds = new GridXZ();
            bounds.NumSubdivisionsX = Subdivisions;
            bounds.NumSubdivisionsZ = Subdivisions;
            bounds.Left = min.X;
            bounds.Top = min.Z;
            bounds.SubdivisionWidth = (max.X - min.X) / Subdivisions; // delta / subdivisions
            bounds.SubdivisionLength = (max.Z - min.Z) / Subdivisions; // delta / subdivisions

            return bounds;
        }

        // METHODS
        public void GenerateIndexesOld(GridXZ matrixBoundsXZ, Checkpoint[] checkpoints)
        {
            // Init. Value is from inherited structure.
            IndexLists = new IndexList[kListCount];

            // so if track has no width, we still pick up some points
            //var widthX = Math.Max(matrixBoundsXZ.subdivisionWidth, 1f);
            //var lengthZ = Math.Max(matrixBoundsXZ.subdivisionLength, 1f);
            var widthX = matrixBoundsXZ.SubdivisionWidth;
            var lengthZ = matrixBoundsXZ.SubdivisionLength;

            // Condition where theere is no w/l and so no checkpoints are added
            var hasNoWidthOrHeight = widthX == 0 || lengthZ == 0;
            if (hasNoWidthOrHeight)
            {
                var list = new List<int>();
                for (int i = 0; i < checkpoints.Length; i++)
                    list.Add(i);

                for (int i = 0; i < IndexLists.Length; i++)
                    IndexLists[i] = IndexList.CreateIndexList(list);

                return;
            }

            // Iterate over each subdivision in the course
            for (int z = 0; z < SubdivisionsZ; z++)
            {
                // Get the minimum and maximum Z coordinates allowed to exist in this cell
                var minZIndex = Math.Clamp(z - 2, 0, SubdivisionsZ - 1);
                var maxZIndex = Math.Clamp(z + 2, 0, SubdivisionsZ - 1);
                var minZ = matrixBoundsXZ.Top + (lengthZ * minZIndex);
                var maxZ = matrixBoundsXZ.Top + (lengthZ * maxZIndex);

                for (int x = 0; x < SubdivisionsX; x++)
                {
                    // Get the minimum and maximum X coordinates allowed to exist this cell
                    var minXIndex = Math.Clamp(x - 2, 0, SubdivisionsX - 1);
                    var maxXIndex = Math.Clamp(x + 2, 0, SubdivisionsX - 1);
                    var minX = matrixBoundsXZ.Left + (widthX * minXIndex);
                    var maxX = matrixBoundsXZ.Left + (widthX * maxXIndex);

                    // Iterate over every checkpoint the course has
                    var indexes = new List<int>();
                    for (int i = 0; i < checkpoints.Length; i++)
                    {
                        var checkpoint = checkpoints[i];

                        var posX = checkpoint.PlaneStart.origin.X;
                        var posZ = checkpoint.PlaneStart.origin.Z;

                        bool isBetweenX = IsBetween(posX, minX, maxX);
                        bool isBetweenZ = IsBetween(posZ, minZ, maxZ);

                        // if the x and z coordinates are within the region we want, store index to checkpoint
                        bool isInRegion = isBetweenX && isBetweenZ;
                        if (isInRegion)
                        {
                            indexes.Add(i);
                        }
                    }

                    // Turn those indexes into the structure
                    var cell = z * SubdivisionsZ + x;
                    IndexLists[cell] = IndexList.CreateIndexList(indexes);
                }
            }
        }

        private bool IsBetween(float value, float min, float max)
        {
            bool isMoreThanMin = value >= min;
            bool isLessThanMax = value <= max;
            bool isBetween = isMoreThanMin && isLessThanMax;
            return isBetween;
        }

        public void GenerateIndexesBlanks(GridXZ matrixBoundsXZ, Checkpoint[] checkpoints)
        {
            var list = new List<int>();
            for (int i = 0; i < checkpoints.Length; i++)
                list.Add(i);

            for (int i = 0; i < IndexLists.Length; i++)
                IndexLists[i] = IndexList.CreateIndexList(list);
        }


        public void GenerateIndexesBetter(GridXZ matrixBoundsXZ, Checkpoint[] checkpoints)
        {
            //
            var widthX = matrixBoundsXZ.SubdivisionWidth;
            var lengthZ = matrixBoundsXZ.SubdivisionLength;
            // Iterate over each subdivision in the course
            for (int z = 0; z < SubdivisionsZ; z++)
            {
                // Get the minimum and maximum Z coordinates allowed to exist in this cell
                var minZIndex = Math.Clamp(z - 1, 0, SubdivisionsZ - 1);
                var maxZIndex = Math.Clamp(z + 1, 0, SubdivisionsZ - 1);
                var minZ = matrixBoundsXZ.Top + (lengthZ * minZIndex);
                var maxZ = matrixBoundsXZ.Top + (lengthZ * maxZIndex);

                for (int x = 0; x < SubdivisionsX; x++)
                {
                    // Get the minimum and maximum X coordinates allowed to exist in this cell
                    var minXIndex = Math.Clamp(x - 1, 0, SubdivisionsX - 1);
                    var maxXIndex = Math.Clamp(x + 1, 0, SubdivisionsX - 1);
                    var minX = matrixBoundsXZ.Left + (widthX * minXIndex);
                    var maxX = matrixBoundsXZ.Left + (widthX * maxXIndex);

                    // Iterate over every checkpoint the course has
                    var indexes = new List<int>();
                    for (int i = 0; i < checkpoints.Length; i++)
                    {
                        var checkpoint = checkpoints[i];
                        var start = checkpoint.PlaneStart.origin;
                        var end = checkpoint.PlaneEnd.origin;

                        bool intersects = line_rect_isect(start.X, start.Z, end.X, end.Z, minX, maxX, minZ, maxZ);
                        if (intersects)
                            indexes.Add(i);

                        //bool minAboveMaxZ = start.Z > maxZ && end.Z > maxZ;
                        //bool maxBelowMinZ = start.Z < minZ && end.Z < minZ;
                        //bool maxLeftOfMinX = start.X < minX && end.X < minX;
                        //bool minRightOfMaxX = start.X > maxX && end.X > maxX;
                        //bool doesNotIntersect = minAboveMaxZ || maxBelowMinZ || maxLeftOfMinX || minRightOfMaxX;
                        //if (doesNotIntersect)
                        //    continue;
                    }

                    // Turn those indexes into the structure
                    var cell = z * SubdivisionsZ + x;
                    IndexLists[cell] = IndexList.CreateIndexList(indexes);
                }
            }



        }

        //https://www.lexaloffle.com/bbs/?pid=80455
        private bool line_rect_isect(float startX, float startZ, float endX, float endZ, float minX, float maxX, float minZ, float maxZ)
        {
            float tl = (minX - startX) / (endX - startX);
            float tr = (maxX - startX) / (endX - startX);
            float tt = (maxZ - startZ) / (endZ - startZ);
            float tb = (minZ - startZ) / (endZ - startZ);

            bool intersects = 
                Math.Max(0, Math.Max(Math.Min(tl, tr), Math.Min(tt, tb))) <
                Math.Min(1, Math.Min(Math.Max(tl, tr), Math.Max(tt, tb)));
            return intersects;
        }
    }
}
