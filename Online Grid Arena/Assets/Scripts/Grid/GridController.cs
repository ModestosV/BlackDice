using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class GridController : IGridSelectionController
{
    public Dictionary<Tuple<int, int, int>, HexTile> hexTiles;
    public int majorAxisLength;

    public List<HexTile> selectedTiles;
    public List<HexTile> hoveredTiles;

    public void Init()
    {
        selectedTiles = new List<HexTile>();
    }

    public void SetHexTiles(HexTile[] hexTiles)
    {
        this.hexTiles = new Dictionary<Tuple<int, int, int>, HexTile>();

        for (int i = 0; i < hexTiles.Length; i++)
        {
            HexTile hexTile = hexTiles[i];
            int col = i % majorAxisLength;
            int row = i / majorAxisLength;

            int cubeX = col - row / 2;
            int cubeY = -(col + (row + 1) / 2);
            int cubeZ = row;

            hexTile.controller.X = cubeX;
            hexTile.controller.Y = cubeY;
            hexTile.controller.Z = cubeZ;

            this.hexTiles.Add(new Tuple<int, int, int>(cubeX, cubeY, cubeZ), hexTile);
        }

        // Arrange hex tiles
        for (int i = 0; i < hexTiles.Length; i++)
        {
            HexTile hexTile = hexTiles[i];
            int col = i % majorAxisLength;
            int row = i / majorAxisLength;

            Vector3 meshSize = hexTile.gameObject.GetComponent<Renderer>().bounds.size;

            float rowOffset = row % 2 == 0 ? 0.0f : meshSize.x / 2.0f;

            hexTile.gameObject.transform.position = new Vector3(col * meshSize.x + rowOffset, 0, -(row * 0.75f * meshSize.z));
        }
    }

    #region IGridSelectionController implementation

    public void AddSelectedTile(HexTile selectedTile)
    {
        selectedTiles.Add(selectedTile);
    }

    public bool RemovedSelectedTile(HexTile removedTile)
    {
        return selectedTiles.Remove(removedTile);
    }

    public void AddHoveredTile(HexTile hoveredTile)
    {
        hoveredTiles.Add(hoveredTile);
    }

    public bool RemoveHoveredTile(HexTile removedTile)
    {
        return hoveredTiles.Remove(removedTile);
    }

    public void DeselectAll()
    {
        for (int i = selectedTiles.Count - 1; i >= 0; i--)
        {
            selectedTiles[i].controller.Deselect();
        }
    }

    public void BlurAll()
    {
        for (int i = hoveredTiles.Count - 1; i >= 0; i--)
        {
            hoveredTiles[i].controller.Blur();
        }
    }

    public void DrawPath(HexTile endTile)
    {
        if (selectedTiles.Count > 0)
        {
            foreach (HexTile startTile in selectedTiles)
            {
                List<HexTile> path = GetPath(startTile, endTile);

                foreach (HexTile tile in path)
                {
                    tile.controller.Hover();
                }
            }
        }
    }

    #endregion

    public HexTile GetNorthEast(HexTile tile)
    {
        HexTile neighborNorthEast;
        int x = tile.controller.X + 1;
        int y = tile.controller.Y;
        int z = tile.controller.Z - 1;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborNorthEast);

        //Debug.Log($"NE: {neighborNorthEast}");
        return neighborNorthEast;
    }

    public HexTile GetEast(HexTile tile)
    {
        HexTile neighborEast;
        int x = tile.controller.X + 1;
        int y = tile.controller.Y - 1;
        int z = tile.controller.Z;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborEast);

        //Debug.Log($"E: {neighborEast}");
        return neighborEast;
    }

    public HexTile GetSouthEast(HexTile tile)
    {
        HexTile neighborSouthEast;
        int x = tile.controller.X;
        int y = tile.controller.Y - 1;
        int z = tile.controller.Z + 1;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborSouthEast);

        //Debug.Log($"SE: {neighborSouthEast}");
        return neighborSouthEast;
    }

    public HexTile GetSouthWest(HexTile tile)
    {
        HexTile neighborSouthWest;
        int x = tile.controller.X - 1;
        int y = tile.controller.Y;
        int z = tile.controller.Z + 1;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborSouthWest);

        //Debug.Log($"SW: {neighborSouthWest}");
        return neighborSouthWest;
    }

    public HexTile GetWest(HexTile tile)
    {
        HexTile neighborWest;
        int x = tile.controller.X - 1;
        int y = tile.controller.Y + 1;
        int z = tile.controller.Z;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborWest);

        //Debug.Log($"W: {neighborWest}");
        return neighborWest;
    }

    public HexTile GetNorthWest(HexTile tile)
    {
        HexTile neighborNorthWest;
        int x = tile.controller.X;
        int y = tile.controller.Y + 1;
        int z = tile.controller.Z - 1;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborNorthWest);

        //Debug.Log($"NW: {neighborNorthWest}");
        return neighborNorthWest;
    }

    public List<HexTile> GetNeighbors(HexTile tile)
    {
        List<HexTile> neighbors = new List<HexTile>
        {
            GetNorthEast(tile),
            GetEast(tile),
            GetSouthEast(tile),
            GetSouthWest(tile),
            GetWest(tile),
            GetNorthWest(tile)
        };

        neighbors.RemoveAll(item => item == null);

        return neighbors;
    }

    public List<HexTile> GetPath(HexTile startTile, HexTile endTile)
    {
        List<HexTile> open = new List<HexTile>();
        HashSet<string> closed = new HashSet<string>();
        Dictionary<string, int> fValues = new Dictionary<string, int>();
        Dictionary<string, int> gValues = new Dictionary<string, int>();
        Dictionary<string, HexTile> bestParents = new Dictionary<string, HexTile>();

        open.Add(startTile);
        gValues[startTile.Key()] = 0;
        fValues[startTile.Key()] = gValues[startTile.Key()] + ManhattanDistance(startTile, endTile);
        bestParents[startTile.Key()] = null;

        while (open.Count > 0)
        {
            open.Sort((x, y) => fValues[x.Key()].CompareTo(fValues[y.Key()]));
            HexTile currentTile = open.First();
            open.Remove(currentTile);

            closed.Add(currentTile.Key());

            if (currentTile == endTile)
            {
                return Backtrace(currentTile, bestParents);
            }

            List<HexTile> neighbors = GetNeighbors(currentTile);
            neighbors.RemoveAll(item => !item.controller.IsEnabled);

            foreach (HexTile neighbor in neighbors)
            {
                if (closed.Contains(neighbor.Key())) continue; // Skip nodes that have already been evaluated. Assumes heuristic monotonicity.

                if (!neighbor.enabled) // Ignore disabled nodes.
                {
                    closed.Add(neighbor.Key());
                    continue;
                }

                int g = gValues[currentTile.Key()] + 1;

                if (!open.Contains(neighbor))
                {
                    open.Add(neighbor);
                }
                else if (gValues[neighbor.Key()] < g) continue; // Not a better path

                // Best path so far
                bestParents[neighbor.Key()] = currentTile;

                gValues[neighbor.Key()] = g;
                fValues[neighbor.Key()] = g + ManhattanDistance(neighbor, endTile);
            }
        }

        return new List<HexTile>();
    }

    private List<HexTile> Backtrace(HexTile goalTile, Dictionary<string, HexTile> bestParents)
    {
        HexTile node = goalTile;
        List<HexTile> path = new List<HexTile> { goalTile };

        while (bestParents[node.Key()])
        {
            HexTile parent = bestParents[node.Key()];
            path.Add(parent);

            node = parent;
        }

        path.Reverse();
        return path;
    }

    private int ManhattanDistance(HexTile startTile, HexTile endTile)
    {
        int startX = startTile.controller.X;
        int startY = startTile.controller.Y;
        int startZ = startTile.controller.Z;
        int endX = endTile.controller.X;
        int endY = endTile.controller.Y;
        int endZ = endTile.controller.Z;

        int xDistance = Math.Abs(startX - endX);
        int yDistance = Math.Abs(startY - endY);
        int zDistance = Math.Abs(startZ - endZ);

        return (xDistance + yDistance + zDistance) / 2;
    }
}