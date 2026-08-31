using Manifold.IO;
using System.Collections.Generic;
using Unity.Mathematics;

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
            float3 min = new float3(float.MaxValue, 0, float.MaxValue);
            float3 max = new float3(float.MinValue, 0, float.MinValue);

            foreach (var checkpoint in checkpoints)
            {
                // MIN
                min.x = math.min(min.x, checkpoint.GetMinPositionX());
                min.z = math.min(min.z, checkpoint.GetMinPositionZ());

                // MAX
                max.x = math.max(max.x, checkpoint.GetMaxPositionX());
                max.z = math.max(max.z, checkpoint.GetMaxPositionZ());
            }

            // Compute bounds
            var bounds = new GridXZ();
            bounds.NumSubdivisionsX = Subdivisions;
            bounds.NumSubdivisionsZ = Subdivisions;
            bounds.Left = min.x;
            bounds.Top = min.z;
            bounds.SubdivisionWidth = (max.x - min.x) / Subdivisions; // delta / subdivisions
            bounds.SubdivisionLength = (max.z - min.z) / Subdivisions; // delta / subdivisions

            return bounds;
        }

        // METHODS
        public void GenerateIndexesOld(GridXZ matrixBoundsXZ, Checkpoint[] checkpoints)
        {
            // Init. Value is from inherited structure.
            IndexLists = new IndexList[kListCount];

            // so if track has no width, we still pick up some points
            //var widthX = math.max(matrixBoundsXZ.subdivisionWidth, 1f);
            //var lengthZ = math.max(matrixBoundsXZ.subdivisionLength, 1f);
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
                var minZIndex = math.clamp(z - 2, 0, SubdivisionsZ - 1);
                var maxZIndex = math.clamp(z + 2, 0, SubdivisionsZ - 1);
                var minZ = matrixBoundsXZ.Top + (lengthZ * minZIndex);
                var maxZ = matrixBoundsXZ.Top + (lengthZ * maxZIndex);

                for (int x = 0; x < SubdivisionsX; x++)
                {
                    // Get the minimum and maximum X coordinates allowed to exist this cell
                    var minXIndex = math.clamp(x - 2, 0, SubdivisionsX - 1);
                    var maxXIndex = math.clamp(x + 2, 0, SubdivisionsX - 1);
                    var minX = matrixBoundsXZ.Left + (widthX * minXIndex);
                    var maxX = matrixBoundsXZ.Left + (widthX * maxXIndex);

                    // Iterate over every checkpoint the course has
                    var indexes = new List<int>();
                    for (int i = 0; i < checkpoints.Length; i++)
                    {
                        var checkpoint = checkpoints[i];

                        var posX = checkpoint.PlaneStart.origin.x;
                        var posZ = checkpoint.PlaneStart.origin.z;

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

        public void GenerateCheckpointIndexes(GridXZ matrixBoundsXZ, Checkpoint[] checkpoints)
        {
            int count = matrixBoundsXZ.NumSubdivisionsX * matrixBoundsXZ.NumSubdivisionsZ;
            Assert.IsTrue(count == 64);
            var checkpointIndexList = new List<ushort>[count];
            for (int i = 0; i < checkpointIndexList.Length; i++)
                checkpointIndexList[i] = new List<ushort>();

            // Make a circle that covers the rectangle
            float radiusOfGridCell = math.max(matrixBoundsXZ.SubdivisionWidth, matrixBoundsXZ.SubdivisionLength) / 2 * math.SQRT2;
            float3 cellCentreOffset = new float3(matrixBoundsXZ.SubdivisionWidth, 0, matrixBoundsXZ.SubdivisionLength) / 2;

            // for each checkpoint
            for (ushort checkpointIndex = 0; checkpointIndex < checkpoints.Length; checkpointIndex++)
            {
                var checkpoint = checkpoints[checkpointIndex];
                // Get centre of checkpoint as 2D XZ position, ignore Y
                float2 checkpointCentre = (checkpoint.PlaneStart.origin + checkpoint.PlaneEnd.origin).xz;
                // Get radius of circle around centre
                float checkpointRadius = math.distance(checkpoint.PlaneStart.origin, checkpoint.PlaneEnd.origin) + checkpoint.TrackWidth * 3; //* 3 for padding

                // Check checkpoint against each cell in grid, add to cell if needed
                for (int row = 0; row < matrixBoundsXZ.NumSubdivisionsZ; row++)
                {
                    for (int col = 0; col < matrixBoundsXZ.NumSubdivisionsX; col++)
                    {
                        float2 cellCentre = new float2(
                            matrixBoundsXZ.Left + cellCentreOffset.x + matrixBoundsXZ.SubdivisionWidth * col,
                            matrixBoundsXZ.Top + cellCentreOffset.z + matrixBoundsXZ.SubdivisionLength * row);

                        // Do circle-circle intersection
                        float sumRadii = checkpointRadius + radiusOfGridCell + math.max(matrixBoundsXZ.SubdivisionWidth, matrixBoundsXZ.SubdivisionLength) * 2;
                        float distanceTriFromCell = math.distance(cellCentre, checkpointCentre);
                        if (distanceTriFromCell <= sumRadii)
                        {
                            int cellIndex = row * matrixBoundsXZ.NumSubdivisionsX + col;
                            checkpointIndexList[cellIndex].Add(checkpointIndex);
                        }
                    }
                }
            }

            var indexLists = new IndexList[count];
            for (int i = 0; i < checkpointIndexList.Length; i++)
            {
                // Add terminator if required
                if (checkpointIndexList[i].Count > 0)
                    checkpointIndexList[i].Add(0xFFFF);
                // Then convert to appropriate type
                indexLists[i] = new IndexList();
                indexLists[i].Indexes = checkpointIndexList[i].ToArray();
            }
            // assign to self (instance)
            IndexLists = indexLists;
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
                var minZIndex = math.clamp(z - 1, 0, SubdivisionsZ - 1);
                var maxZIndex = math.clamp(z + 1, 0, SubdivisionsZ - 1);
                var minZ = matrixBoundsXZ.Top + (lengthZ * minZIndex);
                var maxZ = matrixBoundsXZ.Top + (lengthZ * maxZIndex);

                for (int x = 0; x < SubdivisionsX; x++)
                {
                    // Get the minimum and maximum X coordinates allowed to exist in this cell
                    var minXIndex = math.clamp(x - 1, 0, SubdivisionsX - 1);
                    var maxXIndex = math.clamp(x + 1, 0, SubdivisionsX - 1);
                    var minX = matrixBoundsXZ.Left + (widthX * minXIndex);
                    var maxX = matrixBoundsXZ.Left + (widthX * maxXIndex);

                    // Iterate over every checkpoint the course has
                    var indexes = new List<int>();
                    for (int i = 0; i < checkpoints.Length; i++)
                    {
                        var checkpoint = checkpoints[i];
                        var start = checkpoint.PlaneStart.origin;
                        var end = checkpoint.PlaneEnd.origin;

                        bool intersects = line_rect_isect(start.x, start.z, end.x, end.z, minX, maxX, minZ, maxZ);
                        if (intersects)
                            indexes.Add(i);

                        //bool minAboveMaxZ = start.z > maxZ && end.z > maxZ;
                        //bool maxBelowMinZ = start.z < minZ && end.z < minZ;
                        //bool maxLeftOfMinX = start.x < minX && end.x < minX;
                        //bool minRightOfMaxX = start.x > maxX && end.x > maxX;
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
                math.max(0, math.max(math.min(tl, tr), math.min(tt, tb))) <
                math.min(1, math.min(math.max(tl, tr), math.max(tt, tb)));
            return intersects;
        }
    }
}
