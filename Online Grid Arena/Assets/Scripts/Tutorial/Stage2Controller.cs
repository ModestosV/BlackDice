using UnityEngine;
using System;
using TMPro;

public class Stage2Controller: AbstractStageController, IEventSubscriber
{
    private const string TUTORIAL_STEP_1 = "Click On Rocket Cat";
    private const string TUTORIAL_STEP_2 = "Left Click or Press F";
    private const string TUTORIAL_STEP_3 = "Click on Tile with\nRed Arrow";
    private const string TUTORIAL_STEP_4 = "After Using all Moves\nClick End Turn";
    private const int STAGE_INDEX = 2;

    private ICharacterController character;
    private IHexTileController finishTile;
    private SelectionMode selectionMode = SelectionMode.FREE;

    public Stage2Controller(ICharacterController character, IHexTileController finishTile)
    {
        this.character = character;
        this.finishTile = finishTile;
    }

    private bool CharacterOnFinishTile()
    {
        return this.character.OccupiedTile == this.finishTile ? true : false;
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();

        if (type == typeof(UpdateSelectionModeEvent))
        {
            var selectionModeEvent = (UpdateSelectionModeEvent)@event;

            selectionMode = selectionModeEvent.SelectionMode;
        }

        if (selectionMode == SelectionMode.MOVEMENT)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_3;
        }
        else
        {
            if (!character.CanMove() && character.IsActive)
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_4;
            }
            else if (character.IsActive)
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_2;
            }
            else
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_1;
            }
        }

        if (type == typeof(SelectActivePlayerEvent))
        {
            var selectActivePlayerEvent = (SelectActivePlayerEvent) @event;

            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_2;

            if (!selectActivePlayerEvent.ActivePlayer.Name.Equals(character.Owner))
            {
                // Skip other team's turn
                EventBus.Publish(new StartNewTurnEvent());
            }
        }

        if (CharacterOnFinishTile())
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = STAGE_COMPLETE;

            CompleteStage(STAGE_INDEX);
        }
    }
}