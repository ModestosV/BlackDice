using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class HexTileController : IHexTileController
{
    public Tuple<int, int, int> Coordinates { get; set; }

    public bool IsEnabled { get; set; }
    public bool IsSelected { private get; set; }

    public IGridSelectionController GridSelectionController { private get; set; }
    public IGridController GridController { private get; set; }
    public IHexTile HexTile { get; set; }
    public ICharacterController OccupantCharacter { get; set; }

    public int X { get { return Coordinates.Item1; } }
    public int Y { get { return Coordinates.Item2; } }
    public int Z { get { return Coordinates.Item3; } }

    public void Select()
    {
        if (!IsEnabled) return;

        if (OccupantCharacter != null)
            OccupantCharacter.UpdateSelectedHUD();

        if (IsSelected) return;

        IsSelected = true;
        HexTile.SetClickedMaterial();
        GridSelectionController.AddSelectedTile(this);
    }

    public void Deselect()
    {
        if (!IsEnabled) return;

        if (OccupantCharacter != null)
            OccupantCharacter.ClearSelectedHUD();

        if (!IsSelected) return;

        IsSelected = false;
        if (HexTile.IsMouseOver())
        {
            HexTile.SetHoverMaterial();
        } else
        {
            HexTile.SetDefaultMaterial();
        }
        GridSelectionController.RemoveSelectedTile(this);
    }

    public void Hover()
    {
        if (!IsEnabled) return;

        if (OccupantCharacter != null)
            OccupantCharacter.UpdateTargetHUD();

        GridSelectionController.AddHoveredTile(this);

        HexTile.SetHoverMaterial();
    }

    public void HoverError()
    {
        if (!IsEnabled) return;

        if (OccupantCharacter != null)
            OccupantCharacter.UpdateTargetHUD();

        GridSelectionController.AddHoveredTile(this);

        HexTile.SetErrorMaterial();
    }

    public void Blur()
    {
        if (!IsEnabled) return;

        if (OccupantCharacter != null)
            OccupantCharacter.ClearTargetHUD();

        GridSelectionController.RemoveHoveredTile(this);

        if (IsSelected)
        {
            HexTile.SetClickedMaterial();
        }
        else
        {
            HexTile.SetDefaultMaterial();
        }
    }

    public void Highlight()
    {
        if (!IsEnabled) return;

        if (OccupantCharacter != null)
            OccupantCharacter.UpdateTargetHUD();

        GridSelectionController.AddHighlightedTile(this);
        
        HexTile.SetHighlightMaterial();
    }

    public void Dehighlight()
    {
        if (!IsEnabled) return;

        if (OccupantCharacter != null)
            OccupantCharacter.ClearTargetHUD();

        GridSelectionController.RemoveHighlightedTile(this);

        if (IsSelected)
        {
            HexTile.SetClickedMaterial();
        } else
        {
            HexTile.SetDefaultMaterial();
        }
    }

    public void ClearOccupant()
    {
        OccupantCharacter = null;
    }

    public bool IsOccupied()
    {
        return OccupantCharacter != null;
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

    public List<IHexTileController> GetPath(IHexTileController goalTile)
    {
        List<IHexTileController> open = new List<IHexTileController>();
        HashSet<Tuple<int, int, int>> closed = new HashSet<Tuple<int, int, int>>();
        Dictionary<Tuple<int, int, int>, int> fValues = new Dictionary<Tuple<int, int, int>, int>();
        Dictionary<Tuple<int, int, int>, int> gValues = new Dictionary<Tuple<int, int, int>, int>();
        Dictionary<Tuple<int, int, int>, IHexTileController> bestParents = new Dictionary<Tuple<int, int, int>, IHexTileController>();

        open.Add(this);
        gValues[this.Coordinates] = 0;
        fValues[this.Coordinates] = gValues[this.Coordinates] + ManhattanDistance(this, goalTile);
        bestParents[this.Coordinates] = null;

        while (open.Count > 0)
        {
            open.Sort((x, y) => fValues[x.Coordinates].CompareTo(fValues[y.Coordinates]));
            IHexTileController currentTile = open.First();
            open.Remove(currentTile);

            closed.Add(currentTile.Coordinates);

            if (currentTile == goalTile)
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

    public int GetDistance(IHexTileController targetTile)
    {
        return (int)(Math.Sqrt(Math.Pow((this.Coordinates.Item1 - targetTile.Coordinates.Item1), 2) + 
                               Math.Pow((this.Coordinates.Item2 - targetTile.Coordinates.Item2), 2)));
    }

    public void PlayAbilityAnimation(GameObject abilityAnimationPrefab)
    {
        HexTile.PlayAbilityAnimation(abilityAnimationPrefab);
    }

    public void Damage(float damage)
    {
        if (OccupantCharacter != null)
        {
            OccupantCharacter.Damage(damage);
        }
    }

    public void Heal(float healing)
    {
        if (OccupantCharacter != null)
        {
            OccupantCharacter.Heal(healing);
        }
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
