using System;
using System.Linq;
using System.Collections.Generic;

[Serializable]
public class GridTraversalController : IGridTraversalController
{
    public Dictionary<Tuple<int, int, int>, IHexTile> HexTiles { get; set; }

    #region IGridTraversalController implementation

    public void Init()
    {
        HexTiles = new Dictionary<Tuple<int, int, int>, IHexTile>();
    }

    public void SetHexTiles(Dictionary<Tuple<int, int, int>, IHexTile> hexTiles)
    {
        this.HexTiles = hexTiles;
    }

    public IHexTile GetHexTile(Tuple<int, int, int> coordinates)
    {
        IHexTile tile;
        HexTiles.TryGetValue(coordinates, out tile);
        return tile;
    }

    public IHexTile GetNorthEastNeighbor(IHexTile tile)
    {
        IHexTile neighborNorthEast;
        int x = tile.Controller.X + 1;
        int y = tile.Controller.Y;
        int z = tile.Controller.Z - 1;
        HexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborNorthEast);

        //Debug.Log($"NE: {neighborNorthEast}");
        return neighborNorthEast;
    }

    public IHexTile GetEastNeighbor(IHexTile tile)
    {
        IHexTile neighborEast;
        int x = tile.Controller.X + 1;
        int y = tile.Controller.Y - 1;
        int z = tile.Controller.Z;
        HexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborEast);

        //Debug.Log($"E: {neighborEast}");
        return neighborEast;
    }

    public IHexTile GetSouthEastNeighbor(IHexTile tile)
    {
        IHexTile neighborSouthEast;
        int x = tile.Controller.X;
        int y = tile.Controller.Y - 1;
        int z = tile.Controller.Z + 1;
        HexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborSouthEast);

        //Debug.Log($"SE: {neighborSouthEast}");
        return neighborSouthEast;
    }

    public IHexTile GetSouthWestNeighbor(IHexTile tile)
    {
        IHexTile neighborSouthWest;
        int x = tile.Controller.X - 1;
        int y = tile.Controller.Y;
        int z = tile.Controller.Z + 1;
        HexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborSouthWest);

        //Debug.Log($"SW: {neighborSouthWest}");
        return neighborSouthWest;
    }

    public IHexTile GetWestNeighbor(IHexTile tile)
    {
        IHexTile neighborWest;
        int x = tile.Controller.X - 1;
        int y = tile.Controller.Y + 1;
        int z = tile.Controller.Z;
        HexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborWest);

        //Debug.Log($"W: {neighborWest}");
        return neighborWest;
    }

    public IHexTile GetNorthWestNeighbor(IHexTile tile)
    {
        IHexTile neighborNorthWest;
        int x = tile.Controller.X;
        int y = tile.Controller.Y + 1;
        int z = tile.Controller.Z - 1;
        HexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborNorthWest);

        //Debug.Log($"NW: {neighborNorthWest}");
        return neighborNorthWest;
    }

    public List<IHexTile> GetNeighbors(IHexTile tile)
    {
        List<IHexTile> neighbors = new List<IHexTile>
        {
            GetNorthEastNeighbor(tile),
            GetEastNeighbor(tile),
            GetSouthEastNeighbor(tile),
            GetSouthWestNeighbor(tile),
            GetWestNeighbor(tile),
            GetNorthWestNeighbor(tile)
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
            neighbors.RemoveAll(item => !item.Controller.IsEnabled);

            foreach (IHexTile neighbor in neighbors)
            {
                if (closed.Contains(neighbor.Coordinates())) continue; // Skip nodes that have already been evaluated. Assumes heuristic monotonicity.

                if (!neighbor.Controller.IsEnabled) // Ignore disabled nodes.
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

    #endregion

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
        int startX = startTile.Controller.X;
        int startY = startTile.Controller.Y;
        int startZ = startTile.Controller.Z;
        int endX = endTile.Controller.X;
        int endY = endTile.Controller.Y;
        int endZ = endTile.Controller.Z;

        int xDistance = Math.Abs(startX - endX);
        int yDistance = Math.Abs(startY - endY);
        int zDistance = Math.Abs(startZ - endZ);

        return (xDistance + yDistance + zDistance) / 2;
    }

}
