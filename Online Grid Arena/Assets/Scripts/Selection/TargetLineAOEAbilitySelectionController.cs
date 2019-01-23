using System.Collections.Generic;
using UnityEngine;

public class TargetLineAOEAbilitySelectionController : TargetLineAbilitySelectionController
{
    protected override void DoHoverOccupiedTile()
    {
        base.DoHoverOccupiedTile();
        IHexTileController selectedTile = GridSelectionController.SelectedTile;
        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile, true);
        if (path.Count >= 2)
        {
            HighlightAffectedTiles(path[path.Count - 2]);
        }
    }

    protected override void DoHoverUnoccupiedTile()
    {
        base.DoHoverUnoccupiedTile();
        bool isError = false;
        IHexTileController selectedTile = GridSelectionController.SelectedTile;
        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile, true);
        if (path.Count == 2)
        {
            DehighlightNeighbors(path[0]);
            HighlightAffectedTiles(path[1]);
            return;
        }
        for (int i = 0; i < path.Count; i++)
        {
            if (selectedTile.X != path[i].X || selectedTile.Y != path[i].Y || selectedTile.Z != path[i].Z)
            {
                isError = true;
            }
        }
        if (path.Count > 2)
        {
            DehighlightNeighbors(path[path.Count - 2]);
            HighlightAffectedTiles(path[path.Count - 1]);
            if (isError)
            {
                path[path.Count - 3].HoverError();
            }
            else
            {
                path[path.Count - 3].HoverError();
            }
        }
        path[path.Count - 1].Hover();

    }

    private void HighlightAffectedTiles(IHexTileController target)
    {
        foreach (IHexTileController affected in target.GetNeighbors())
        {
            affected.HoverError();
        }
        target.Hover();
    }

    private void DehighlightNeighbors(IHexTileController target)
    {
        target.HoverError();
        foreach (IHexTileController affected in target.GetNeighbors())
        {
            affected.Dehighlight();
        }
    }
}
