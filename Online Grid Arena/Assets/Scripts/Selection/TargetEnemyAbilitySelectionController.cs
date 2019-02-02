using System.Collections.Generic;

public sealed class TargetEnemyAbilitySelectionController : AbstractAbilitySelectionController
{
    public TargetEnemyAbilitySelectionController(IGridSelectionController gridSelectionController) : base(gridSelectionController)
    {

    }

    protected override void DoClickOccupiedOtherTile()
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();
        ICharacterController targetCharacter = inputParameters.TargetTile.OccupantCharacter;
        bool targetCharacterIsAlly = selectedCharacter.IsAlly(targetCharacter);

        IHexTileController selectedTile = gridSelectionController.SelectedTile;
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
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();
        ICharacterController targetCharacter = inputParameters.TargetTile.OccupantCharacter;
        bool targetCharacterIsAlly = selectedCharacter.IsAlly(targetCharacter);

        IHexTileController selectedTile = gridSelectionController.SelectedTile;
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
