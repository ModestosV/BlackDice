using System.Collections.Generic;

public class TargetLineAbilitySelectionController : AbstractAbilitySelectionController
{
    private bool canCast = false;

    public TargetLineAbilitySelectionController(IGridSelectionController gridSelectionController) : base(gridSelectionController)
    {

    }
    
    protected override void DoClickOccupiedOtherTile()
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();
        IHexTileController selectedTile = gridSelectionController.SelectedTile;
        int distance = selectedTile.GetAbsoluteDistance(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, distance);
        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile, true);

        if (inRange && canCast)
        {
            List<IHexTileController> target = new List<IHexTileController>();
            target.Add(path[path.Count - 2]);
            target.Add(inputParameters.TargetTile);
            selectedCharacter.ExecuteAbility(activeAbilityIndex, target);
            EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
            return;
        }
    }
    protected override void DoClickUnoccupiedOtherTile()
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();
        IHexTileController selectedTile = gridSelectionController.SelectedTile;
        int distance = selectedTile.GetAbsoluteDistance(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, distance);

        if (inRange && canCast)
        {
            List<IHexTileController> target = new List<IHexTileController>();
            target.Add(inputParameters.TargetTile);

            selectedCharacter.ExecuteAbility(activeAbilityIndex, target);
            EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
            return;
        }
    }

    protected override void DoHoverUnoccupiedTile()
    {
        inputParameters.TargetTile.Highlight();
        IHexTileController selectedTile = gridSelectionController.SelectedTile;
        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile, true);
        bool isStraightLine = false;
        if (selectedTile.X == inputParameters.TargetTile.X || selectedTile.Y == inputParameters.TargetTile.Y || selectedTile.Z == inputParameters.TargetTile.Z)
        {
            isStraightLine = true; //this just means that target and selected are on the same line. must check whole path.
            for (int i = 1; i < path.Count; i++)
            {
                if (!(selectedTile.X == path[i].X || selectedTile.Y == path[i].Y || selectedTile.Z == path[i].Z))
                {
                    isStraightLine = false;
                }
            }
        }

        if (!isStraightLine)
        {
            canCast = false;
            for (int i = 1; i < path.Count; i++)
            {
                path[i].HoverError();
            }
            path[path.Count - 1].HoverInvalid();
            return;
        }
        else
        {
            canCast = true;
            for (int i = 1; i < path.Count; i++)
            {
                path[i].Highlight();
                if (path[i].OccupantCharacter != null)
                {
                    if (path[i].OccupantCharacter.IsAlly(selectedTile.OccupantCharacter))
                    {
                        path[i].HoverError();
                    }
                }
            }
        }
        return;
    }

    protected override void DoHoverSelectedTile()
    {
        inputParameters.TargetTile.HoverInvalid();
    }

    protected override void DoHoverOccupiedTile()
    {
        DoHoverUnoccupiedTile();
    }
}
