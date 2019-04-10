using System.Collections.Generic;

public abstract class AbstractLineAbilitySelectionController : AbstractAbilitySelectionController
{
    private bool canCast = false;

    protected AbstractLineAbilitySelectionController(IGridSelectionController gridSelectionController) : base(gridSelectionController)
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
            target.Add(path[1]);
            selectedCharacter.ExecuteAbility(activeAbilityIndex, target);
            EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
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
        }
    }

    protected override void DoHoverUnoccupiedTile()
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();
        inputParameters.TargetTile.Highlight();
        IHexTileController selectedTile = gridSelectionController.SelectedTile;
        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile, true);
        bool isStraightLine = false;
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, path.Count - 1);
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
            path[path.Count - 1].Hover(HoverType.INVALID);
        }
        else
        {
            canCast = true;
            if (inRange)
            {
                for (int i = 1; i < path.Count; i++)
                {
                    path[i].Highlight();
                    if (path[i].OccupantCharacter != null)
                    {
                        if (path[i].OccupantCharacter.IsAlly(selectedTile.OccupantCharacter))
                        {
                            path[i].HoverError();
                        }
                        else
                        {
                            path[i].Hover(HoverType.DAMAGE);
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i < path.Count; i++)
                {
                    path[i].HoverError();
                }

            }
        }
    }

    protected override void DoHoverSelectedTile()
    {
        inputParameters.TargetTile.Hover(HoverType.INVALID);
    }

    protected override void DoHoverOccupiedTile()
    {
        DoHoverUnoccupiedTile();
    }
}
