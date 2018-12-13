using System.Collections.Generic;

public class TargetEnemyAbilitySelectionController : AbilitySelectionController
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

    protected override void DoClickOccupiedOtherTile()
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();
        ICharacterController targetCharacter = inputParameters.TargetTile.OccupantCharacter;
        bool targetCharacterIsAlly = selectedCharacter.IsAlly(targetCharacter);

        IHexTileController selectedTile = GridSelectionController.GetSelectedTile();
        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, path.Count - 1);

        if (!targetCharacterIsAlly && inRange)
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
        inputParameters.TargetTile.HoverError();
    }

    protected override void DoHoverOccupiedTile()
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();
        ICharacterController targetCharacter = inputParameters.TargetTile.OccupantCharacter;
        bool targetCharacterIsAlly = selectedCharacter.IsAlly(targetCharacter);

        IHexTileController selectedTile = GridSelectionController.GetSelectedTile();
        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, path.Count - 1);

        if (!targetCharacterIsAlly && inRange)
        {
            inputParameters.TargetTile.Highlight();
        } else
        {
            inputParameters.TargetTile.HoverError();
        }
    }
}
