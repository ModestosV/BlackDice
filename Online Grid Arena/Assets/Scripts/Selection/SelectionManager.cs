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

    private readonly Dictionary<string, ISelectionController> selectionControllers;
    private SelectionMode selectionMode = SelectionMode.FREE;
    private ISelectionController activeSelectionController;
    private readonly ITurnController turnController;
    private readonly IGridSelectionController gridSelectionController;
    private int lastAbilityIndex = -1;

    public SelectionManager(ITurnController turnController, IGridSelectionController gridSelectionController, Dictionary<string, ISelectionController> selectionControllers)
    {
        this.turnController = turnController;
        this.gridSelectionController = gridSelectionController;
        this.selectionControllers = selectionControllers;
    }

    public void UpdateSelectionMode(IInputParameters inputParameters)
    {
        int abilityIndex = inputParameters.GetAbilityNumber();

        if (inputParameters.IsAbilityKeyPressed() && SelectedCharacterCanUseAbility(abilityIndex))
        {
            if (abilityIndex != lastAbilityIndex)
            {
                lastAbilityIndex = abilityIndex;
                EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.ABILITY));
                EventBus.Publish(new AbilitySelectedEvent(inputParameters.GetAbilityNumber()));
            }
            else
            {
                lastAbilityIndex = -1;
                EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
            }
        }
        else if (inputParameters.IsKeyFDown && SelectedCharacterCanMove())
        {
            if (selectionMode == SelectionMode.MOVEMENT)
            {
                EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
            } else
            {
                EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.MOVEMENT));
            }
        }
        else if (inputParameters.IsKeyTDown)
        {
            EventBus.Publish(new StartNewTurnEvent());
        }

        switch (selectionMode)
        {
            case SelectionMode.FREE:
                activeSelectionController = selectionControllers["free"];
                break;
            case SelectionMode.MOVEMENT:
                activeSelectionController = selectionControllers["movement"];
                break;
            case SelectionMode.ABILITY:
                if (abilityIndex < 0) break;
                var updatedAbilitySelectionController = GetAbilitySelectionController(abilityIndex);

                if (updatedAbilitySelectionController != null)
                {
                    activeSelectionController = updatedAbilitySelectionController;
                }
                break;
        }

        if (activeSelectionController == null)
        {
            activeSelectionController = selectionControllers["free"];
        }
        activeSelectionController.UpdateSelection(inputParameters);
    }

    private ISelectionController GetAbilitySelectionController(int abilityIndex)
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();

        AbilityType activeAbilityType = selectedCharacter.GetAbilityType(abilityIndex);

        switch (activeAbilityType)
        {
            case AbilityType.TARGET_ENEMY:
                return selectionControllers["target_enemy"];
            case AbilityType.TARGET_ALLY:
                return selectionControllers["target_ally"];
            case AbilityType.TARGET_TILE:
                return selectionControllers["target_tile"];
            case AbilityType.TARGET_TILE_AOE:
                return selectionControllers["target_tile_aoe"];
            case AbilityType.TARGET_LINE:
                return selectionControllers["target_line"];
            case AbilityType.TARGET_LINE_AOE:
                return selectionControllers["target_line_aoe"];
            case AbilityType.TARGET_CHARACTER_LINE:
                return selectionControllers["target_character_line"];
            case AbilityType.TARGET_AOE:
                return selectionControllers["target_aoe"];
            default:
                return null;
        }
    }

    private bool SelectedCharacterCanMove()
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();

        if (selectedCharacter == null)
            return false;

        return turnController.IsActiveCharacter(selectedCharacter) && selectedCharacter.CanMove();
    }

    public bool SelectedCharacterCanUseAbility(int abilityIndex)
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();

        if (selectedCharacter == null)
            return false;

        return turnController.IsActiveCharacter(selectedCharacter) && selectedCharacter.CanUseAbility(abilityIndex);
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        if (type == typeof(UpdateSelectionModeEvent))
        {
            var newSelectionMode = (UpdateSelectionModeEvent)@event;
            selectionMode = newSelectionMode.SelectionMode;

            if (selectionMode != SelectionMode.ABILITY)
            {
                lastAbilityIndex = -1;
            }

            Debug.Log(ToString() + " has selection mode " + selectionMode.ToString());
        }
    }
}
