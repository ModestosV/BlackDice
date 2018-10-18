using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class GridController : IGridSelectionController {

    public Dictionary<Tuple<int, int, int>, HexTile2> hexTiles;
    public int majorAxisLength;

    public List<HexTile2> selectedTiles;
    public List<HexTile2> hoveredTiles;

    public void Init()
    {
        selectedTiles = new List<HexTile2>();
    }

    public void SetHexTiles(HexTile2[] hexTiles)
    {
        this.hexTiles = new Dictionary<Tuple<int, int, int>, HexTile2>();

        for (int i = 0; i < hexTiles.Length; i++)
        {
            HexTile2 hexTile = hexTiles[i];
            int col = i % majorAxisLength;
            int row = i / majorAxisLength;

            int cubeX = col - row / 2;
            int cubeY = -(col + (row + 1) / 2);
            int cubeZ = row;

            hexTile.controller.x = cubeX;
            hexTile.controller.y = cubeY;
            hexTile.controller.z = cubeZ;

            this.hexTiles.Add(new Tuple<int, int, int>(cubeX, cubeY, cubeZ), hexTile);
        }

        // Arrange hex tiles
        for (int i = 0; i < hexTiles.Length; i++)
        {
            HexTile2 hexTile = hexTiles[i];
            int col = i % majorAxisLength;
            int row = i / majorAxisLength;

            Vector3 meshSize = hexTile.gameObject.GetComponent<Renderer>().bounds.size;

            float rowOffset = row % 2 == 0 ? 0.0f : meshSize.x / 2.0f;

            hexTile.gameObject.transform.position = new Vector3(col * meshSize.x + rowOffset, 0, -(row * 0.75f * meshSize.z));
        }
    }

    #region IGridSelectionController implementation

    public void AddSelectedTile(HexTile2 selectedTile)
    {
        selectedTiles.Add(selectedTile);
    }

    public bool RemovedSelectedTile(HexTile2 removedTile)
    {
        return selectedTiles.Remove(removedTile);
    }

    public void AddHoveredTile(HexTile2 hoveredTile)
    {
        hoveredTiles.Add(hoveredTile);
    }

    public bool RemoveHoveredTile(HexTile2 removedTile)
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

    public void DrawPath(HexTile2 endTile)
    {
        if (selectedTiles.Count > 0)
        {
            foreach (HexTile2 startTile in selectedTiles)
            {
                List<HexTile2> path = GetPath(startTile, endTile);

                foreach (HexTile2 tile in path)
                {
                    tile.controller.Hover();
                }
            }
        }
    }

    #endregion

    public HexTile2 GetNorthEast(HexTile2 tile)
    {
        HexTile2 neighborNorthEast;
        int x = tile.controller.x + 1;
        int y = tile.controller.y;
        int z = tile.controller.z - 1;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborNorthEast);

        //Debug.Log($"NE: {neighborNorthEast}");
        return neighborNorthEast;
    }

    public HexTile2 GetEast(HexTile2 tile)
    {
        HexTile2 neighborEast;
        int x = tile.controller.x + 1;
        int y = tile.controller.y - 1;
        int z = tile.controller.z;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborEast);

        //Debug.Log($"E: {neighborEast}");
        return neighborEast;
    }

    public HexTile2 GetSouthEast(HexTile2 tile)
    {
        HexTile2 neighborSouthEast;
        int x = tile.controller.x;
        int y = tile.controller.y - 1;
        int z = tile.controller.z + 1;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborSouthEast);

        //Debug.Log($"SE: {neighborSouthEast}");
        return neighborSouthEast;
    }

    public HexTile2 GetSouthWest(HexTile2 tile)
    {
        HexTile2 neighborSouthWest;
        int x = tile.controller.x - 1;
        int y = tile.controller.y;
        int z = tile.controller.z + 1;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborSouthWest);

        //Debug.Log($"SW: {neighborSouthWest}");
        return neighborSouthWest;
    }

    public HexTile2 GetWest(HexTile2 tile)
    {
        HexTile2 neighborWest;
        int x = tile.controller.x - 1;
        int y = tile.controller.y + 1;
        int z = tile.controller.z;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborWest);

        //Debug.Log($"W: {neighborWest}");
        return neighborWest;
    }

    public HexTile2 GetNorthWest(HexTile2 tile)
    {
        HexTile2 neighborNorthWest;
        int x = tile.controller.x;
        int y = tile.controller.y + 1;
        int z = tile.controller.z - 1;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborNorthWest);

        //Debug.Log($"NW: {neighborNorthWest}");
        return neighborNorthWest;
    }

    public List<HexTile2> GetNeighbors(HexTile2 tile)
    {
        List<HexTile2> neighbors = new List<HexTile2>
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

    public List<HexTile2> GetPath(HexTile2 startTile, HexTile2 endTile)
    {
        List<HexTile2> open = new List<HexTile2>();
        HashSet<string> closed = new HashSet<string>();
        Dictionary<string, int> fValues = new Dictionary<string, int>();
        Dictionary<string, int> gValues = new Dictionary<string, int>();
        Dictionary<string, HexTile2> bestParents = new Dictionary<string, HexTile2>();

        open.Add(startTile);
        gValues[startTile.Key()] = 0;
        fValues[startTile.Key()] = gValues[startTile.Key()] + ManhattanDistance(startTile, endTile);
        bestParents[startTile.Key()] = null;

        while (open.Count > 0)
        {
            open.Sort((x, y) => fValues[x.Key()].CompareTo(fValues[y.Key()]));
            HexTile2 currentTile = open.First();
            open.Remove(currentTile);

            closed.Add(currentTile.Key());

            if (currentTile == endTile)
            {
                return Backtrace(currentTile, bestParents);
            }

            List<HexTile2> neighbors = GetNeighbors(currentTile);
            neighbors.RemoveAll(item => !item.controller.isEnabled);

            foreach (HexTile2 neighbor in neighbors)
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

        return new List<HexTile2>();
    }

    private List<HexTile2> Backtrace(HexTile2 goalTile, Dictionary<string, HexTile2> bestParents)
    {
        HexTile2 node = goalTile;
        List<HexTile2> path = new List<HexTile2> { goalTile };

        while (bestParents[node.Key()])
        {
            HexTile2 parent = bestParents[node.Key()];
            path.Add(parent);

            node = parent;
        }

        path.Reverse();
        return path;
    }

    private int ManhattanDistance(HexTile2 startTile, HexTile2 endTile)
    {
        int startX = startTile.controller.x;
        int startY = startTile.controller.y;
        int startZ = startTile.controller.z;
        int endX = endTile.controller.x;
        int endY = endTile.controller.y;
        int endZ = endTile.controller.z;

        int xDistance = Math.Abs(startX - endX);
        int yDistance = Math.Abs(startY - endY);
        int zDistance = Math.Abs(startZ - endZ);

        return (xDistance + yDistance + zDistance) / 2;
    }
}