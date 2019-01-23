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
        HighlightAffectedTiles(inputParameters.TargetTile);
    }

    private void HighlightAffectedTiles(IHexTileController target)
    {
        target.Dehighlight();
        foreach (IHexTileController affected in target.GetNeighbors())
        {
            affected.HoverError();
        }
        target.Hover();
    }
}
