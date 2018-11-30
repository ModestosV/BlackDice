public class TargetAllyAbilitySelectionController : AbilitySelectionController
{
    protected override void DoFirst()
    {
        SetActiveAbility();
        GridSelectionController.BlurAll();
        GridSelectionController.DehighlightAll();
    }

    protected override void DoEscapePressed()
    {
        SelectionManager.SelectionMode = SelectionMode.FREE;
    }

    protected override void DoTabPressed()
    {

    }

    protected override void DoClickOffGrid()
    {

    }

    protected override void DoHoverOffGrid()
    {

    }

    protected override void DoClickDisabledTile()
    {

    }

    protected override void DoHoverDisabledTile()
    {

    }

    protected override void DoClickUnoccupiedOtherTile()
    {

    }

    protected override void DoClickSelectedTile()
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();

        selectedCharacter.ExecuteAbility(activeAbilityIndex, inputParameters.TargetTile);
        SelectionManager.SelectionMode = SelectionMode.FREE;
    }

    protected override void DoClickOccupiedOtherTile()
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();
        ICharacterController targetCharacter = inputParameters.TargetTile.OccupantCharacter;
        bool targetCharacterIsAlly = selectedCharacter.IsAlly(targetCharacter);

        if (targetCharacterIsAlly)
        {
            selectedCharacter.ExecuteAbility(activeAbilityIndex, inputParameters.TargetTile);
            SelectionManager.SelectionMode = SelectionMode.FREE;
            return;
        }
    }

    protected override void DoHoverUnoccupiedTile()
    {
        inputParameters.TargetTile.HoverError();
    }

    protected override void DoHoverSelectedTile()
    {
        inputParameters.TargetTile.Highlight();
    }

    protected override void DoHoverOccupiedTile()
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();
        ICharacterController targetCharacter = inputParameters.TargetTile.OccupantCharacter;
        bool targetCharacterIsAlly = selectedCharacter.IsAlly(targetCharacter);

        if (targetCharacterIsAlly)
        {
            inputParameters.TargetTile.Highlight();
        }
        else
        {
            inputParameters.TargetTile.HoverError();
        }
    }
}
