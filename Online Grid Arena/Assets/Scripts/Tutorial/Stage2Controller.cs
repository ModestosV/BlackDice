using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using TMPro;
using System.Threading;

public class Stage2Controller: IStageController, IEventSubscriber
{
    private String TUTORIAL_STEP_1 = "Click On Rocket Cat\nLeft Click or Press F";
    private String TUTORIAL_STEP_2 = "Click on Tile with\nRed Arrow";
    private String TUTORIAL_STEP_3 = "After Using all Moves\nClick End Turn";
    private String STAGE_COMPLETE = "Congratulations! Stage Completed";

    private ICharacterController character;
    private IHexTileController finishTile;

    public Stage2Controller(ICharacterController character, IHexTileController finishTile)
    {
        this.character = character;
        this.finishTile = finishTile;
    }

    public bool CharacterOnFinishTile()
    {
        return this.character.OccupiedTile == this.finishTile ? true : false;
    }

    public void CompleteStage()
    {
        EventBus.Publish(new StageCompletedEvent(2));
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();

        if (type == typeof(UpdateSelectionModeEvent))
        {
            var selectionModeEvent = (UpdateSelectionModeEvent)@event;

            if (selectionModeEvent.SelectionMode == SelectionMode.MOVEMENT)
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_2;
            }
            else
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_3;
            }
        }

        if (type == typeof(SelectActivePlayerEvent))
        {
            var selectActivePlayerEvent = (SelectActivePlayerEvent) @event;

            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_1;

            if (!selectActivePlayerEvent.ActivePlayer.Name.Equals(character.Owner))
            {
                // Skip other team's turn
                EventBus.Publish(new StartNewTurnEvent());
            }
        }

        if (CharacterOnFinishTile())
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = STAGE_COMPLETE;

            Thread.Sleep(5);

            CompleteStage();
        }
    }
}