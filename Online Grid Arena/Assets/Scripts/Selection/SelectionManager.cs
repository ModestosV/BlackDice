using System.Collections.Generic;
using UnityEngine;

public enum SelectionMode
{
    FREE,
    ABILITY,
    MOVEMENT
}

public sealed class SelectionManager : ISelectionManager, IEventSubscriber
{
    public IGridSelectionController GridSelectionController { private get; set; }
    public Dictionary<string, ISelectionController> SelectionControllers { private get; set; }
    public ITurnController TurnController { get; set; }


    private SelectionMode selectionMode = SelectionMode.FREE;
    private ISelectionController activeSelectionController;

    public void Update(IInputParameters inputParameters)
    {
        int abilityIndex = inputParameters.GetAbilityNumber();

        if (inputParameters.IsAbilityKeyPressed() && SelectedCharacterCanUseAbility(abilityIndex))
        {
            selectionMode = SelectionMode.ABILITY;
        }
        else if (inputParameters.IsKeyFDown && SelectedCharacterCanMove())
        {
            selectionMode = SelectionMode.MOVEMENT;
        }

        switch (selectionMode)
        {
            case SelectionMode.FREE:
                activeSelectionController = SelectionControllers["free"];
                activeSelectionController.Update(inputParameters);
                break;
            case SelectionMode.MOVEMENT:
                activeSelectionController = SelectionControllers["movement"];
                activeSelectionController.Update(inputParameters);
                break;
            case SelectionMode.ABILITY:
                if (abilityIndex < 0) break;
                EventBus.Publish(new AbilityClickEvent(abilityIndex));
                break;
        }

        if (activeSelectionController == null)
        {
            activeSelectionController = SelectionControllers["free"];
            activeSelectionController.Update(inputParameters);
        }
    }

    public void UpdateOnAbilityClickEvent(int abilityIndex)
    {
        if (SelectedCharacterCanUseAbility(abilityIndex))
        {
            activeSelectionController = GetAbilitySelectionController(abilityIndex);

            IInputParameters inputParameters = new InputParameters()
            {
                IsKeyQDown = false,
                IsKeyWDown = false,
                IsKeyEDown = false,
                IsKeyRDown = false,
                IsKeyFDown = false,
                IsKeyEscapeDown = false,
                IsKeyTabDown = false,

                IsLeftClickDown = false,
                IsRightClickDown = false,

                IsMouseOverGrid = false,
                TargetTile = null
            };

            if (abilityIndex == 0) inputParameters.IsKeyQDown = true;
            if (abilityIndex == 1) inputParameters.IsKeyWDown = true;
            if (abilityIndex == 2) inputParameters.IsKeyEDown = true;
            if (abilityIndex == 3) inputParameters.IsKeyRDown = true;

            activeSelectionController.Update(inputParameters);
        }
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
            case AbilityType.TARGET_TILE:
                return SelectionControllers["target_tile"];
            case AbilityType.TARGET_LINE:
                return SelectionControllers["target_line"];
            case AbilityType.TARGET_LINE_AOE:
                return SelectionControllers["target_line_aoe"];
            default:
                return null;
        }
    }

    private bool SelectedCharacterCanMove()
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();

        if (selectedCharacter == null)
            return false;

        return TurnController.IsActiveCharacter(selectedCharacter) && selectedCharacter.CanMove();
    }

    private bool SelectedCharacterCanUseAbility(int abilityIndex)
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();

        if (selectedCharacter == null)
            return false;

        return TurnController.IsActiveCharacter(selectedCharacter) && selectedCharacter.CanUseAbility(abilityIndex);
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        if (type == typeof(UpdateSelectionModeEvent))
        {
            var newSelectionMode = (UpdateSelectionModeEvent) @event;
            selectionMode = newSelectionMode.SelectionMode;
        }

        if (type == typeof(AbilityClickEvent))
        {
            var newAbilityClicked = (AbilityClickEvent) @event;
            UpdateOnAbilityClickEvent(newAbilityClicked.AbilityIndex);
        }
    }
}

