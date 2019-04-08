using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stage7Controller : AbstractStageController, IEventSubscriber
{
    private const string STAGE_FAILED = "Stage Failed!\nYou lost!\nRedirecting Tutorial";
    private const int STAGE_INDEX = 7;

    private GridSelectionController gridSelectionController;
    private TurnController turnController;

    public Stage7Controller(GridSelectionController gridSelectionController, TurnController turnController)
    {
        this.gridSelectionController = gridSelectionController;
        this.turnController = turnController;
    }

    private void StageFailed()
    {
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = STAGE_FAILED;
        EventBus.Publish(new SurrenderEvent());
    }

    private void HandleAiTurn()
    {
        // TODO
        Debug.Log("AI's turn");
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();

        if (type == typeof(StartNewTurnEvent) && turnController.IsPlayerTwoTurn())
        {
            HandleAiTurn();
        }
    }
}