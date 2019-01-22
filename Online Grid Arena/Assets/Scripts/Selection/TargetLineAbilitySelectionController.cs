using System.Collections.Generic;

public class TargetLineAbilitySelectionController : AbstractAbilitySelectionController
{
    bool canCast = false;
    protected override void DoFirst()
    {
        SetActiveAbility();
        GridSelectionController.BlurAll();
        GridSelectionController.DehighlightAll();
    }

    protected override void DoEscapePressed()
    {
        EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
    }
    protected override void DoClickOccupiedOtherTile()
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();
        IHexTileController selectedTile = GridSelectionController.SelectedTile;
        int distance = selectedTile.GetAbsoluteDistance(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, distance);
        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile, true);

        if (inRange)
        {
            if (canCast)
            {
                List<IHexTileController> target = new List<IHexTileController>();
                target.Add(path[path.Count - 2]);
                target.Add(inputParameters.TargetTile);
                selectedCharacter.ExecuteAbility(activeAbilityIndex, target);
                EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
                return;
            }
        }
    }
    protected override void DoClickUnoccupiedOtherTile()
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();
        IHexTileController selectedTile = GridSelectionController.SelectedTile;
        int distance = selectedTile.GetAbsoluteDistance(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, distance);

        if (inRange)
        {
            if (canCast)
            {
                List<IHexTileController> target = new List<IHexTileController>();
                target.Add(inputParameters.TargetTile);

                selectedCharacter.ExecuteAbility(activeAbilityIndex, target);
                EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
                return;
            }
        }
    }

    protected override void DoHoverUnoccupiedTile()
    {
        DoHoverOccupiedTile();
    }

    protected override void DoHoverSelectedTile()
    {
        inputParameters.TargetTile.HoverError();
    }

    protected override void DoHoverOccupiedTile()
    {
        inputParameters.TargetTile.Highlight();
        IHexTileController selectedTile = GridSelectionController.SelectedTile;
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
            return;
        }
        if (isStraightLine)
        {
            canCast = true;
            // Hovered over reachable in range tile
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
}
