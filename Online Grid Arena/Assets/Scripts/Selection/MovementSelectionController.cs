using System.Collections.Generic;

public sealed class MovementSelectionController : AbstractSelectionController
{
    protected override void DoFirst()
    {
        GridSelectionController.BlurAll();
        GridSelectionController.DehighlightAll();
    }

    protected override void DoEscapePressed()
    {
        EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
    }

    protected override void DoClickUnoccupiedOtherTile()
    {
        IHexTileController selectedTile = GridSelectionController.SelectedTile;
        ICharacterController selectedCharacter = selectedTile.OccupantCharacter;

        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile);
        bool isReachable = path.Count > 0;
        bool inRange = selectedCharacter.CanMove(path.Count - 1);
        
        if (!isReachable || !inRange) return;

        // Clicked reachable in range unoccupied tile
        selectedCharacter.ExecuteMove(path);
        if (!selectedCharacter.CanMove())
        {
            EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
        }
    }

    protected override void DoClickOccupiedOtherTile()
    {
        inputParameters.TargetTile.HoverError();
    }

    protected override void DoHoverUnoccupiedTile()
    {
        IHexTileController selectedTile = GridSelectionController.SelectedTile;
        ICharacterController selectedCharacter = selectedTile.OccupantCharacter;

        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile);
        bool isReachable = path.Count > 0;
        bool inRange = selectedCharacter.CanMove(path.Count - 1);

        // Hovered over unreachable tile
        if (!isReachable)
        {
            inputParameters.TargetTile.HoverError();
            return;
        }

        // Hovered over reachable out of range tile
        if (!inRange)
        {
            for (int i = 1; i < path.Count; i++)
            {
                path[i].HoverError();
            }
            return;
        }

        // Hovered over reachable in range tile
        for (int i = 1; i < path.Count; i++)
        {
            path[i].Highlight();
        }
        return;
    }

    protected override void DoHoverOccupiedTile()
    {
        inputParameters.TargetTile.HoverError();
    }
}
