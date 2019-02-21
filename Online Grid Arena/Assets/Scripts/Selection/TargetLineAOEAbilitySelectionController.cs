using System.Collections.Generic;

public class TargetLineAOEAbilitySelectionController : TargetLineAbilitySelectionController
{
    public TargetLineAOEAbilitySelectionController(IGridSelectionController gridSelectionController) : base(gridSelectionController)
    {

    }

    protected override void DoHoverOccupiedTile()
    {
        base.DoHoverOccupiedTile();
        
        IHexTileController selectedTile = gridSelectionController.SelectedTile;
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
        IHexTileController selectedTile = gridSelectionController.SelectedTile;
        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile, true);
        bool isStraightLine = true;

        for (int i = 1; i < path.Count; i++)
        {
            if (!(selectedTile.X == path[i].X || selectedTile.Y == path[i].Y || selectedTile.Z == path[i].Z))
            {
                isStraightLine = false;
            }
        }

        if (isStraightLine)
        {
            foreach (IHexTileController affected in target.GetNeighbors())
            {
                affected.Hover(HoverType.DAMAGE);
            }
            target.Hover(HoverType.DAMAGE);
        }

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
