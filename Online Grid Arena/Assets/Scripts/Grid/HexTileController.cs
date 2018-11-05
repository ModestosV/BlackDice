using System;
using System.Collections.Generic;
using System.Linq;

public class HexTileController : IHexTileController
{
    public Tuple<int, int, int> Coordinates { get; set; }

    public bool IsEnabled { get; set; }
    public bool IsSelected { protected get; set; }

    public IGridSelectionController GridSelectionController { protected get; set; }
    public IGridController GridController { protected get; set; }
    public IHexTile HexTile { protected get; set; }
    public ICharacterController OccupantCharacter { protected get; set; }

    public int X { get { return Coordinates.Item1; } }
    public int Y { get { return Coordinates.Item2; } }
    public int Z { get { return Coordinates.Item3; } }

    public void Select()
    {
        if (!IsEnabled || IsSelected) return;

        GridSelectionController.DeselectAll();
        IsSelected = true;
        HexTile.SetClickedMaterial();
        GridSelectionController.AddSelectedTile(this);
    }

    public void Deselect()
    {
        if (!IsEnabled || !IsSelected) return;

        IsSelected = false;
        if (HexTile.IsMouseOver())
        {
            HexTile.SetHoverMaterial();
        } else
        {
            HexTile.SetErrorMaterial();
        }
        GridSelectionController.RemoveSelectedTile(this);
    }

    public void Hover()
    {
        if (!IsEnabled || IsSelected) return;

        HexTile.SetHoverMaterial();
        GridSelectionController.AddHoveredTile(this);
    }

    public void HoverError()
    {
        if (!IsEnabled || IsSelected) return;
        
        HexTile.SetErrorMaterial();
        GridSelectionController.AddHoveredTile(this);
    }

    public void Blur()
    {
        if (!IsEnabled || IsSelected) return;

        HexTile.SetDefaultMaterial();
        GridSelectionController.RemoveHoveredTile(this);
    }

    public void Highlight()
    {
        if (!IsEnabled || IsSelected) return;

        HexTile.SetHighlightMaterial();
        GridSelectionController.AddHighlightedTile(this);
    }

    public void Dehighlight()
    {
        if (!IsEnabled || IsSelected) return;

        HexTile.SetDefaultMaterial();
        GridSelectionController.RemoveHighlightedTile(this);
    }



    public IHexTileController GetNorthEastNeighbor()
    {
        return GridController.GetTile(new Tuple<int, int, int>(X + 1, Y, Z - 1));
    }

    public IHexTileController GetEastNeighbor()
    {
        return GridController.GetTile(new Tuple<int, int, int>(X + 1, Y - 1, Z));
    }

    public IHexTileController GetSouthEastNeighbor()
    {
        return GridController.GetTile(new Tuple<int, int, int>(X, Y - 1, Z + 1));
    }

    public IHexTileController GetSouthWestNeighbor()
    {
        return GridController.GetTile(new Tuple<int, int, int>(X - 1, Y, Z + 1));
    }

    public IHexTileController GetWestNeighbor()
    {
        return GridController.GetTile(new Tuple<int, int, int>(X - 1, Y + 1, Z));
    }

    public IHexTileController GetNorthWestNeighbor()
    {
        return GridController.GetTile(new Tuple<int, int, int>(X, Y + 1, Z - 1));
    }

    public List<IHexTileController> GetNeighbors()
    {
        List<IHexTileController> neighbors = new List<IHexTileController>
        {
            GetNorthEastNeighbor(),
            GetEastNeighbor(),
            GetSouthEastNeighbor(),
            GetSouthWestNeighbor(),
            GetWestNeighbor(),
            GetNorthWestNeighbor()
        };

        neighbors.RemoveAll(item => item == null);

        return neighbors;
    }

    public List<IHexTileController> GetPath(IHexTileController startTile)
    {
        List<IHexTileController> open = new List<IHexTileController>();
        HashSet<Tuple<int, int, int>> closed = new HashSet<Tuple<int, int, int>>();
        Dictionary<Tuple<int, int, int>, int> fValues = new Dictionary<Tuple<int, int, int>, int>();
        Dictionary<Tuple<int, int, int>, int> gValues = new Dictionary<Tuple<int, int, int>, int>();
        Dictionary<Tuple<int, int, int>, IHexTileController> bestParents = new Dictionary<Tuple<int, int, int>, IHexTileController>();

        open.Add(startTile);
        gValues[startTile.Coordinates] = 0;
        fValues[startTile.Coordinates] = gValues[startTile.Coordinates] + ManhattanDistance(startTile, this);
        bestParents[startTile.Coordinates] = null;

        while (open.Count > 0)
        {
            open.Sort((x, y) => fValues[x.Coordinates].CompareTo(fValues[y.Coordinates]));
            IHexTileController currentTile = open.First();
            open.Remove(currentTile);

            closed.Add(currentTile.Coordinates);

            if (currentTile == this)
            {
                return Backtrace(currentTile, bestParents);
            }

            List<IHexTileController> neighbors = currentTile.GetNeighbors();
            neighbors.RemoveAll(tile => !tile.IsEnabled);

            foreach (IHexTileController neighbor in neighbors)
            {
                if (closed.Contains(neighbor.Coordinates)) continue; // Skip nodes that have already been evaluated. Assumes heuristic monotonicity.

                if (!neighbor.IsEnabled) // Ignore disabled nodes.
                {
                    closed.Add(neighbor.Coordinates);
                    continue;
                }

                int g = gValues[currentTile.Coordinates] + 1;

                if (!open.Contains(neighbor))
                {
                    open.Add(neighbor);
                }
                else if (gValues[neighbor.Coordinates] < g) continue; // Not a better path

                // Best path so far
                bestParents[neighbor.Coordinates] = currentTile;

                gValues[neighbor.Coordinates] = g;
                fValues[neighbor.Coordinates] = g + ManhattanDistance(neighbor, this);
            }
        }

        return new List<IHexTileController>();
    }

    private List<IHexTileController> Backtrace(IHexTileController goalTile, Dictionary<Tuple<int, int, int>, IHexTileController> bestParents)
    {
        IHexTileController node = goalTile;
        List<IHexTileController> path = new List<IHexTileController> { goalTile };

        while (bestParents[node.Coordinates] != null)
        {
            IHexTileController parent = bestParents[node.Coordinates];
            path.Add(parent);

            node = parent;
        }

        path.Reverse();
        return path;
    }

    private int ManhattanDistance(IHexTileController startTile, IHexTileController endTile)
    {
        int xDistance = Math.Abs(startTile.X - endTile.X);
        int yDistance = Math.Abs(startTile.Y - endTile.Y);
        int zDistance = Math.Abs(startTile.Z - endTile.Z);

        return (xDistance + yDistance + zDistance) / 2;
    }


















}
