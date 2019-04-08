using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stage7Controller : AbstractStageController, IEventSubscriber
{
    private const string STAGE_FAILED = "Stage Failed!\nYou lost!\nRedirecting Tutorial";
    private const int STAGE_INDEX = 7;

    private IPlayer player2;
    private GridSelectionController gridSelectionController;
    private TurnController turnController;

    public Stage7Controller(IPlayer player2, GridSelectionController gridSelectionController, TurnController turnController)
    {
        this.player2 = player2;
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

        turnController.MakeCharacterActive(player2.CharacterControllers[2]);
        player2.CharacterControllers[2].CanUseAbility(3);

        EventBus.Publish(new StartNewTurnEvent());
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();

        if (type == typeof(StartNewTurnEvent) && turnController.GetActivePlayer() == player2)
        {
            HandleAiTurn();
        }
    }
}