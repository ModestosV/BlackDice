using System.Collections.Generic;

public class TargetCharacterLineAbilitySelectionController : TargetLineAbilitySelectionController
{
    public TargetCharacterLineAbilitySelectionController(IGridSelectionController gridSelectionController) : base(gridSelectionController)
    {

    }

    protected override void DoClickUnoccupiedOtherTile()
    {
        DoHoverUnoccupiedTile();
    }

    protected override void DoHoverUnoccupiedTile()
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();
        inputParameters.TargetTile.Highlight();
        IHexTileController selectedTile = gridSelectionController.SelectedTile;
        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile, true);
        for (int i = 1; i < path.Count; i++)
        {
            path[i].HoverError();
        }
        path[path.Count - 1].Hover(HoverType.INVALID);
        return;
    }

    protected override void DoHoverOccupiedTile()
    {
        base.DoHoverUnoccupiedTile();
    }
}