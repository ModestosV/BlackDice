using System.Collections.Generic;

public enum SelectionMode
{
    FREE,
    ABILITY,
    MOVEMENT
}

public class SelectionManager : ISelectionManager
{
    public IGridSelectionController GridSelectionController { protected get; set; }
    public Dictionary<string, ISelectionController> SelectionControllers { protected get; set; }
    public SelectionMode SelectionMode { protected get; set; }

    private ISelectionController activeSelectionController;

    public void Update(IInputParameters inputParameters)
    {
        int abilityIndex = inputParameters.GetAbilityNumber();

        if (inputParameters.IsAbilityKeyPressed() && SelectedCharacterCanUseAbility(abilityIndex))
        {
            SelectionMode = SelectionMode.ABILITY;
        }
        else if (inputParameters.IsKeyFDown && SelectedCharacterCanMove())
        {
            SelectionMode = SelectionMode.MOVEMENT;
        }

        switch (SelectionMode)
        {
            case SelectionMode.FREE:
                activeSelectionController = SelectionControllers["free"];
                break;
            case SelectionMode.MOVEMENT:
                activeSelectionController = SelectionControllers["movement"];
                break;
            case SelectionMode.ABILITY:
                if (abilityIndex < 0) break;
                activeSelectionController = GetAbilitySelectionController(abilityIndex);
                break;
        }

        activeSelectionController.Update(inputParameters);
    }

    private ISelectionController GetAbilitySelectionController(int abilityIndex)
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();

        AbilityType activeAbilityType = selectedCharacter.GetAbilityType(abilityIndex);

        switch (activeAbilityType)
        {
            case AbilityType.TARGET_ENEMY:
                return SelectionControllers["target_enemy"];
            case AbilityType.TARGET_ALLY:
                return SelectionControllers["target_ally"];
            default:
                return null;
        }
    }

    private bool SelectedCharacterCanMove()
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();

        if (selectedCharacter == null)
            return false;

        return selectedCharacter.IsActiveCharacter() && selectedCharacter.CanMove();
    }

    private bool SelectedCharacterCanUseAbility(int abilityIndex)
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();

        if (selectedCharacter == null)
            return false;

        return selectedCharacter.IsActiveCharacter() && selectedCharacter.CanUseAbility(abilityIndex);
    }
}

