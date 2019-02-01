using System.Collections.Generic;
using UnityEngine;

public sealed class TargetTileAbilitySelectionController : AbstractAbilitySelectionController
{
    public TargetTileAbilitySelectionController(IGridSelectionController gridSelectionController) : base(gridSelectionController)
    {

    }

    protected override void DoFirst()
    {
        SetActiveAbility();
        gridSelectionController.BlurAll();
        gridSelectionController.DehighlightAll();
    }

    protected override void DoEscapePressed()
    {
        EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
    }

    protected override void DoClickUnoccupiedOtherTile()
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();

        IHexTileController selectedTile = gridSelectionController.SelectedTile;
        int distance = selectedTile.GetAbsoluteDistance(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, distance);

        if (inRange)
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
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();

        IHexTileController selectedTile = gridSelectionController.SelectedTile;
        int distance = selectedTile.GetAbsoluteDistance(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, distance);

        if (inRange)
        {
            inputParameters.TargetTile.Highlight();
            foreach (IHexTileController affected in inputParameters.TargetTile.GetNeighbors())
            {
                affected.HoverError();
            }
        }
        else
        {
            inputParameters.TargetTile.HoverError();
        }
    }

    protected override void DoHoverSelectedTile()
    {
        inputParameters.TargetTile.HoverError();
    }

    protected override void DoHoverOccupiedTile()
    {
        inputParameters.TargetTile.HoverError();
    }
}
