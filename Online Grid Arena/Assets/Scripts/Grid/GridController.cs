using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class GridController : IGridSelectionController
{
    private Dictionary<Tuple<int, int, int>, IHexTile> hexTiles;
    public int majorAxisLength;

    private List<IHexTile> selectedTiles;
    private List<IHexTile> hoveredTiles;
    private List<IHexTile> pathTiles;

    public void Init()
    {
        selectedTiles = new List<IHexTile>();
        hoveredTiles = new List<IHexTile>();
        pathTiles = new List<IHexTile>();
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

    public void AddPathTile(IHexTile pathTile)
    {
        pathTiles.Add(pathTile);
    }

    public bool RemovePathTile(IHexTile pathTile)
    {
        return pathTiles.Remove(pathTile);
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

    public void ScrubPathAll()
    {
        for (int i = pathTiles.Count - 1; i >= 0; i--)
        {
            pathTiles[i].Controller().ScrubPath();
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
                    tile.Controller().MarkPath();
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
        HashSet<Tuple<int, int, int>> closed = new HashSet<Tuple<int, int, int>>();
        Dictionary<Tuple<int, int, int>, int> fValues = new Dictionary<Tuple<int, int, int>, int>();
        Dictionary<Tuple<int, int, int>, int> gValues = new Dictionary<Tuple<int, int, int>, int>();
        Dictionary<Tuple<int, int, int>, IHexTile> bestParents = new Dictionary<Tuple<int, int, int>, IHexTile>();

        open.Add(startTile);
        gValues[startTile.Coordinates()] = 0;
        fValues[startTile.Coordinates()] = gValues[startTile.Coordinates()] + ManhattanDistance(startTile, endTile);
        bestParents[startTile.Coordinates()] = null;

        while (open.Count > 0)
        {
            open.Sort((x, y) => fValues[x.Coordinates()].CompareTo(fValues[y.Coordinates()]));
            IHexTile currentTile = open.First();
            open.Remove(currentTile);

            closed.Add(currentTile.Coordinates());

            if (currentTile == endTile)
            {
                return Backtrace(currentTile, bestParents);
            }

            List<IHexTile> neighbors = GetNeighbors(currentTile);
            neighbors.RemoveAll(item => !item.Controller().IsEnabled);

            foreach (IHexTile neighbor in neighbors)
            {
                if (closed.Contains(neighbor.Coordinates())) continue; // Skip nodes that have already been evaluated. Assumes heuristic monotonicity.

                if (!neighbor.Controller().IsEnabled) // Ignore disabled nodes.
                {
                    closed.Add(neighbor.Coordinates());
                    continue;
                }

                int g = gValues[currentTile.Coordinates()] + 1;

                if (!open.Contains(neighbor))
                {
                    open.Add(neighbor);
                }
                else if (gValues[neighbor.Coordinates()] < g) continue; // Not a better path

                // Best path so far
                bestParents[neighbor.Coordinates()] = currentTile;

                gValues[neighbor.Coordinates()] = g;
                fValues[neighbor.Coordinates()] = g + ManhattanDistance(neighbor, endTile);
            }
        }

        return new List<IHexTile>();
    }

    private List<IHexTile> Backtrace(IHexTile goalTile, Dictionary<Tuple<int, int, int>, IHexTile> bestParents)
    {
        IHexTile node = goalTile;
        List<IHexTile> path = new List<IHexTile> { goalTile };

        while (bestParents[node.Coordinates()] != null)
        {
            IHexTile parent = bestParents[node.Coordinates()];
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