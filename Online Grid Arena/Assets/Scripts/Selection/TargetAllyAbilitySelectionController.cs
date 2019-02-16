using System.Collections.Generic;

public sealed class TargetAllyAbilitySelectionController : AbstractAbilitySelectionController
{
    public TargetAllyAbilitySelectionController(IGridSelectionController gridSelectionController) : base(gridSelectionController)
    {

    }

    protected override void DoClickSelectedTile()
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();
        List<IHexTileController> target = new List<IHexTileController>();
        target.Add(inputParameters.TargetTile);

        selectedCharacter.ExecuteAbility(activeAbilityIndex, target);
        EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
    }

    protected override void DoClickOccupiedOtherTile()
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();
        ICharacterController targetCharacter = inputParameters.TargetTile.OccupantCharacter;
        bool targetCharacterIsAlly = selectedCharacter.IsAlly(targetCharacter);

        IHexTileController selectedTile = gridSelectionController.SelectedTile;
        int distance = selectedTile.GetAbsoluteDistance(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, distance);

        if (targetCharacterIsAlly && inRange)
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
        inputParameters.TargetTile.HoverInvalid();
    }

    protected override void DoHoverSelectedTile()
    {
        inputParameters.TargetTile.Highlight();
    }

    protected override void DoHoverOccupiedTile()
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();
        ICharacterController targetCharacter = inputParameters.TargetTile.OccupantCharacter;
        bool targetCharacterIsAlly = selectedCharacter.IsAlly(targetCharacter);

        IHexTileController selectedTile = gridSelectionController.SelectedTile;
        int distance = selectedTile.GetAbsoluteDistance(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, distance);

        if (targetCharacterIsAlly && inRange)
        {
            inputParameters.TargetTile.HoverHealing();
            targetCharacter.BorderColor = new UnityEngine.Color32(110, 11, 11, 100);
        }
        else
        {
            inputParameters.TargetTile.HoverInvalid();
        }
    }
}
