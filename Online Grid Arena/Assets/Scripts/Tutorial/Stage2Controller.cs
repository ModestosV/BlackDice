using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class Stage2Controller: IStageController, IEventSubscriber
{
    private ICharacterController character;
    private IHexTileController finishTile;
    private int turns = 0;

    public Stage2Controller(ICharacterController character, IHexTileController finishTile)
    {
        this.character = character;
        this.finishTile = finishTile;

        EventBus.Subscribe<StartNewTurnEvent>(this);
    }

    public bool CharacterOnFinishTile()
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

        if (type == typeof(StartNewTurnEvent))
        {
            turns += 1;

            if (turns % 2 == 1)
            {
                // Skip other team's turn
                EventBus.Publish(new StartNewTurnEvent());
            }
        }

        if (CharacterOnFinishTile())
        {
            EndStage();
        }
    }
}