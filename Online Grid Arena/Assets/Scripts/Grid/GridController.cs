using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class GridController : IGridSelectionController
{
    public Dictionary<Tuple<int, int, int>, IHexTile> hexTiles;
    public int majorAxisLength;

    public List<IHexTile> selectedTiles;
    public List<IHexTile> hoveredTiles;

    public void Init()
    {
        selectedTiles = new List<IHexTile>();
        hoveredTiles = new List<IHexTile>();
    }

    public void SetHexTiles(HexTile[] hexTiles)
    {
        this.hexTiles = new Dictionary<Tuple<int, int, int>, IHexTile>();

        for (int i = 0; i < hexTiles.Length; i++)
        {
            IHexTile hexTile = hexTiles[i];
            int col = i % majorAxisLength;
            int row = i / majorAxisLength;

            int cubeX = col - row / 2;
            int cubeY = -(col + (row + 1) / 2);
            int cubeZ = row;

            hexTile.Controller().X = cubeX;
            hexTile.Controller().Y = cubeY;
            hexTile.Controller().Z = cubeZ;

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

    public void AddSelectedTile(IHexTile selectedTile)
    {
        selectedTiles.Add(selectedTile);
    }

    public bool RemovedSelectedTile(IHexTile removedTile)
    {
        return selectedTiles.Remove(removedTile);
    }

    public void AddHoveredTile(IHexTile hoveredTile)
    {
        hoveredTiles.Add(hoveredTile);
    }

    public bool RemoveHoveredTile(IHexTile removedTile)
    {
        return hoveredTiles.Remove(removedTile);
    }

    public void DeselectAll()
    {
        for (int i = selectedTiles.Count - 1; i >= 0; i--)
        {
            selectedTiles[i].Controller().Deselect();
        }
    }

    public void BlurAll()
    {
        for (int i = hoveredTiles.Count - 1; i >= 0; i--)
        {
            hoveredTiles[i].Controller().Blur();
        }
    }

    public void DrawPath(IHexTile endTile)
    {
        if (selectedTiles.Count > 0)
        {
            foreach (IHexTile startTile in selectedTiles)
            {
                List<IHexTile> path = GetPath(startTile, endTile);

                foreach (IHexTile tile in path)
                {
                    tile.Controller().Hover();
                }
            }
        }
    }

    #endregion

    public IHexTile GetNorthEast(IHexTile tile)
    {
        IHexTile neighborNorthEast;
        int x = tile.Controller().X + 1;
        int y = tile.Controller().Y;
        int z = tile.Controller().Z - 1;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborNorthEast);

        //Debug.Log($"NE: {neighborNorthEast}");
        return neighborNorthEast;
    }

    public IHexTile GetEast(IHexTile tile)
    {
        IHexTile neighborEast;
        int x = tile.Controller().X + 1;
        int y = tile.Controller().Y - 1;
        int z = tile.Controller().Z;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborEast);

        //Debug.Log($"E: {neighborEast}");
        return neighborEast;
    }

    public IHexTile GetSouthEast(IHexTile tile)
    {
        IHexTile neighborSouthEast;
        int x = tile.Controller().X;
        int y = tile.Controller().Y - 1;
        int z = tile.Controller().Z + 1;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborSouthEast);

        //Debug.Log($"SE: {neighborSouthEast}");
        return neighborSouthEast;
    }

    public IHexTile GetSouthWest(IHexTile tile)
    {
        IHexTile neighborSouthWest;
        int x = tile.Controller().X - 1;
        int y = tile.Controller().Y;
        int z = tile.Controller().Z + 1;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborSouthWest);

        //Debug.Log($"SW: {neighborSouthWest}");
        return neighborSouthWest;
    }

    public IHexTile GetWest(IHexTile tile)
    {
        IHexTile neighborWest;
        int x = tile.Controller().X - 1;
        int y = tile.Controller().Y + 1;
        int z = tile.Controller().Z;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborWest);

        //Debug.Log($"W: {neighborWest}");
        return neighborWest;
    }

    public IHexTile GetNorthWest(IHexTile tile)
    {
        IHexTile neighborNorthWest;
        int x = tile.Controller().X;
        int y = tile.Controller().Y + 1;
        int z = tile.Controller().Z - 1;
        hexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborNorthWest);

        //Debug.Log($"NW: {neighborNorthWest}");
        return neighborNorthWest;
    }

    public List<IHexTile> GetNeighbors(IHexTile tile)
    {
        List<IHexTile> neighbors = new List<IHexTile>
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

    public List<IHexTile> GetPath(IHexTile startTile, IHexTile endTile)
    {
        List<IHexTile> open = new List<IHexTile>();
        HashSet<string> closed = new HashSet<string>();
        Dictionary<string, int> fValues = new Dictionary<string, int>();
        Dictionary<string, int> gValues = new Dictionary<string, int>();
        Dictionary<string, IHexTile> bestParents = new Dictionary<string, IHexTile>();

        open.Add(startTile);
        gValues[startTile.Key()] = 0;
        fValues[startTile.Key()] = gValues[startTile.Key()] + ManhattanDistance(startTile, endTile);
        bestParents[startTile.Key()] = null;

        while (open.Count > 0)
        {
            open.Sort((x, y) => fValues[x.Key()].CompareTo(fValues[y.Key()]));
            IHexTile currentTile = open.First();
            open.Remove(currentTile);

            closed.Add(currentTile.Key());

            if (currentTile == endTile)
            {
                return Backtrace(currentTile, bestParents);
            }

            List<IHexTile> neighbors = GetNeighbors(currentTile);
            neighbors.RemoveAll(item => !item.Controller().IsEnabled);

            foreach (IHexTile neighbor in neighbors)
            {
                if (closed.Contains(neighbor.Key())) continue; // Skip nodes that have already been evaluated. Assumes heuristic monotonicity.

                if (!neighbor.Controller().IsEnabled) // Ignore disabled nodes.
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

        return new List<IHexTile>();
    }

    private List<IHexTile> Backtrace(IHexTile goalTile, Dictionary<string, IHexTile> bestParents)
    {
        IHexTile node = goalTile;
        List<IHexTile> path = new List<IHexTile> { goalTile };

        while (bestParents[node.Key()] != null)
        {
            IHexTile parent = bestParents[node.Key()];
            path.Add(parent);

            node = parent;
        }

        path.Reverse();
        return path;
    }

    private int ManhattanDistance(IHexTile startTile, IHexTile endTile)
    {
        int startX = startTile.Controller().X;
        int startY = startTile.Controller().Y;
        int startZ = startTile.Controller().Z;
        int endX = endTile.Controller().X;
        int endY = endTile.Controller().Y;
        int endZ = endTile.Controller().Z;

        int xDistance = Math.Abs(startX - endX);
        int yDistance = Math.Abs(startY - endY);
        int zDistance = Math.Abs(startZ - endZ);

        return (xDistance + yDistance + zDistance) / 2;
    }
}