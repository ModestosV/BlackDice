using System.Collections.Generic;
using UnityEngine;

public sealed class TargetTileAbilitySelectionController : AbstractAbilitySelectionController
{
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

    protected override void DoClickUnoccupiedOtherTile()
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();

        IHexTileController selectedTile = GridSelectionController.GetSelectedTile();
        int distance = selectedTile.GetDistance(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, distance);

        if (inRange)
        {
            selectedCharacter.ExecuteAbility(activeAbilityIndex, inputParameters.TargetTile);
            EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
            return;
        }
    }

    protected override void DoHoverUnoccupiedTile()
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();

        IHexTileController selectedTile = GridSelectionController.GetSelectedTile();
        int distance = selectedTile.GetDistance(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, distance);

        if (inRange)
        {
            inputParameters.TargetTile.Highlight();
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
