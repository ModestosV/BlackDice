using System.Collections.Generic;

public sealed class TargetEnemyAbilitySelectionController : AbstractAbilitySelectionController
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

    protected override void DoClickOccupiedOtherTile()
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();
        ICharacterController targetCharacter = inputParameters.TargetTile.OccupantCharacter;
        bool targetCharacterIsAlly = selectedCharacter.IsAlly(targetCharacter);

        IHexTileController selectedTile = GridSelectionController.GetSelectedTile();
        int distance = selectedTile.GetAbsoluteDistance(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, distance);

        if (!targetCharacterIsAlly && inRange)
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
        inputParameters.TargetTile.HoverError();
    }

    protected override void DoHoverSelectedTile()
    {
        inputParameters.TargetTile.HoverError();
    }

    protected override void DoHoverOccupiedTile()
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();
        ICharacterController targetCharacter = inputParameters.TargetTile.OccupantCharacter;
        bool targetCharacterIsAlly = selectedCharacter.IsAlly(targetCharacter);

        IHexTileController selectedTile = GridSelectionController.GetSelectedTile();
        int distance = selectedTile.GetAbsoluteDistance(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, distance);

        if (!targetCharacterIsAlly && inRange)
        {
            inputParameters.TargetTile.Highlight();
        } else
        {
            inputParameters.TargetTile.HoverError();
        }
    }
}
