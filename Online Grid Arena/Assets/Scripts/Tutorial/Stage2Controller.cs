using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using TMPro;

public class Stage2Controller: IStageController, IEventSubscriber
{
    private String TUTORIAL_STEP_1 = "Select Character\nClick On Rocket Cat";
    private String TUTORIAL_STEP_2 = "Press F";
    private String TUTORIAL_STEP_3 = "Click on Tile with\nRed Arrow";
    private String TUTORIAL_STEP_4 = "After Using all Moves\nClick End Turn";
    private ICharacterController character;
    private IHexTileController finishTile;
    private int turns = 0;

    public Stage2Controller(ICharacterController character, IHexTileController finishTile)
    {
        this.character = character;
        this.finishTile = finishTile;

        EventBus.Subscribe<StartNewTurnEvent>(this);
        EventBus.Subscribe<SelectActivePlayerEvent>(this);
        EventBus.Subscribe<UpdateSelectionModeEvent>(this);
    }

    private bool CharacterOnFinishTile()
    {
        if (this.character.OccupiedTile == this.finishTile)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EndStage()
    {
        EventBus.Publish(new StageCompletedEvent(2));
        SceneManager.LoadScene(2);
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();

        if (type == typeof(UpdateSelectionModeEvent))
        {
            var selectionModeEvent = (UpdateSelectionModeEvent)@event;

            if (selectionModeEvent.SelectionMode == SelectionMode.MOVEMENT)
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_3;
            }
            else
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_4;
            }
        }

        if (type == typeof(SelectActivePlayerEvent))
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_2;
        }

        if (type == typeof(StartNewTurnEvent))
        {
            turns += 1;

            if (turns % 2 == 1)
            {
                // Skip other team's turn
                EventBus.Publish(new StartNewTurnEvent());
            }

            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_1;
        }

        if (CharacterOnFinishTile())
        {
            EndStage();
        }
    }
}