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
    private IHexTileController eagleFinalTile;
    private IHexTileController pengwinFinalTile;
    private IHexTileController frogFinalTile;

    public Stage7Controller(IPlayer player2, GridSelectionController gridSelectionController, TurnController turnController, IHexTileController eagleFinalTile, IHexTileController pengwinFinalTile, IHexTileController frogFinalTile)
    {
        this.player2 = player2;
        this.gridSelectionController = gridSelectionController;
        this.turnController = turnController;
        this.eagleFinalTile = eagleFinalTile;
        this.pengwinFinalTile = pengwinFinalTile;
        this.frogFinalTile = frogFinalTile;
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

        if (player2.CharacterControllers[0].CharacterState == CharacterState.UNUSED)
        {
            turnController.MakeCharacterActive(player2.CharacterControllers[0]);

            List<IHexTileController> path = player2.CharacterControllers[0].OccupiedTile.GetPath(frogFinalTile, false);
            player2.CharacterControllers[0].ExecuteMove(path);
            player2.CharacterControllers[0].ExecuteAbility(2, new List<IHexTileController>() { player2.CharacterControllers[1].OccupiedTile });
        }
        else if (player2.CharacterControllers[2].CharacterState == CharacterState.UNUSED)
        {
            turnController.MakeCharacterActive(player2.CharacterControllers[2]);
            
            List<IHexTileController>  path = player2.CharacterControllers[2].OccupiedTile.GetPath(eagleFinalTile, false);
            player2.CharacterControllers[2].ExecuteMove(path);
            player2.CharacterControllers[2].CanUseAbility(3);
        }
        else if (player2.CharacterControllers[1].CharacterState == CharacterState.UNUSED)
        {
            turnController.MakeCharacterActive(player2.CharacterControllers[1]);

            List<IHexTileController> path = player2.CharacterControllers[1].OccupiedTile.GetPath(pengwinFinalTile, false);
            player2.CharacterControllers[1].ExecuteMove(path);
            player2.CharacterControllers[1].CanUseAbility(2);

        }
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