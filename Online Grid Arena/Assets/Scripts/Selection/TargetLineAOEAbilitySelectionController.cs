using System.Collections.Generic;
using UnityEngine;

public class TargetLineAOEAbilitySelectionController : TargetLineAbilitySelectionController
{
    protected override void DoHoverOccupiedTile()
    {
        base.DoHoverOccupiedTile();
        
        IHexTileController selectedTile = GridSelectionController.SelectedTile;
        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile, true);
        DehighlightNeighboringTiles(inputParameters.TargetTile, path[path.Count - 2]);
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

    private void DehighlightNeighboringTiles(IHexTileController target, IHexTileController exception)
    {
        foreach (IHexTileController neighbor in target.GetNeighbors())
        {
            if (neighbor != exception)
            {
                neighbor.Dehighlight();
            }
        }
    }
}
