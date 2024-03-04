using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoissonDiscSampling
{
    public static List<Vector2> GeneratePoints(ObjectProba[] prefabs, Vector2 sampleregionSize, int numSampleBeforeRejection, int maxPoints, float maxRadius)
    {
        float cellSize = maxRadius / Mathf.Sqrt(2);

        int[,] grid = new int[Mathf.CeilToInt(sampleregionSize.x / cellSize), Mathf.CeilToInt(sampleregionSize.y / cellSize)];
        List<Vector2> points = new List<Vector2>();
        List<Vector2> spawnPoints = new List<Vector2>();

        spawnPoints.Add( sampleregionSize /2);
        while (spawnPoints.Count > 0 && points.Count < maxPoints)
        {
            int spawnIndex = Random.Range( 0, spawnPoints.Count );
            Vector2 spawnCentre = spawnPoints[spawnIndex];
            bool candideteAccepted = false;

            for(int i = 0; i < numSampleBeforeRejection; i++)
            {
                float angle = Random.value * Mathf.PI * 2;
                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                Vector2 candidate = spawnCentre + dir * Random.Range(maxRadius, 2 * maxRadius);
                if (IsValid(candidate, sampleregionSize,cellSize, maxRadius,points,grid)) {
                    points.Add(candidate);
                    spawnPoints.Add(candidate);
                    grid[(int)(candidate.x/cellSize), (int)(candidate.y/cellSize)] = points.Count;
                    candideteAccepted = true;
                    break;
                }
            }
            if (!candideteAccepted)
            {
                spawnPoints.RemoveAt(spawnIndex);
            }
        }
        return points;
    }

    static bool IsValid(Vector2 candidate, Vector2 sampleRegionSize, float cellSize, float radius, List<Vector2> points, int[,] grid)
    {
        if (candidate.x >=0 && candidate.x < sampleRegionSize.x && candidate.y >=0 && candidate.y < sampleRegionSize.y)
        {
            int cellX = (int)(candidate.x/cellSize);
            int cellY = (int)(candidate.y/cellSize);
            int searchStartX = Mathf.Max(0, cellX - 2);
            int searchEndX = Mathf.Min(cellX+2, grid.GetLength(0)-1);
            int searchStartY = Mathf.Max(0, cellY - 2);
            int searchEndY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

            for (int x = searchStartX; x <= searchEndX; x++)
            {
                for (int y = searchStartY; y <= searchEndY; y++)
                {
                    int pointIndex = grid[x, y] - 1;
                    if (pointIndex != -1)
                    {
                        float sqrdst = (candidate - points[pointIndex]).sqrMagnitude;
                        if (sqrdst < radius)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }

    static GameObject PickRandomObject(ObjectProba[] prefabs)
    {
        float totalWeight = 0;
        for (int i = 0; i < prefabs.Length; i++)
        {
            totalWeight += prefabs[i].proba;
        }

        float rand = Random.value;

        for (int i = 0; i < prefabs.Length; i++)
        {
            if (rand <= prefabs[i].proba / totalWeight)
            {
                return prefabs[i].go;
            }
            else
            {
                rand -= prefabs[i].proba / totalWeight;
            }
        }
        return prefabs[prefabs.Length].go;
    }
}
