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
        IHexTileController selectedTile = GridSelectionController.SelectedTile;
        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile, true);
        if (path.Count > 2)
        {
            DehighlightNeighbors(path[path.Count - 2], path[path.Count - 3]);
            HighlightAffectedTiles(path[path.Count - 1]);
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

    private void DehighlightNeighbors(IHexTileController target, IHexTileController exception)
    {
        target.HoverError();
        foreach (IHexTileController affected in target.GetNeighbors())
        {
            if (affected != exception)
            {
                affected.Dehighlight();
            }
        }
    }
}
