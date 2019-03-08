using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class TurnController : ITurnController, IEventSubscriber
{
    private ICharacterController activeCharacter;
    private List<IPlayer> players;
    private bool isPlayerOneTurn;
    private bool inCharacterSelectionState;
    private List<CharacterPanel> characterPanels;

    public TurnController(List<IPlayer> players)
    {
        this.players = players;
    }

    public bool IsActiveCharacter(ICharacterController character)
    {
        return activeCharacter == character;
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        if(type == typeof(DeathEvent))
        {
            var deathEvent = (DeathEvent) @event;
            if (IsActiveCharacter(deathEvent.CharacterController))
            {
                EventBus.Publish(new StartNewTurnEvent());
            }
            CheckWinCondition();
        }
        if (type == typeof(StartNewTurnEvent))
        {
            StartNextTurn();
        }

        if(type == typeof(SurrenderEvent))
        {
            Surrender();
        }
        if(type == typeof(SelectActivePlayerEvent))
        {
            SelectActiveCharacter();
        }
        if(type == typeof(SelectTileEvent))
        {
            var selectTileEvent = (SelectTileEvent)@event;
            if(selectTileEvent.SelectedTile.OccupantCharacter != null && inCharacterSelectionState)
            {
                MakeCharacterActive(selectTileEvent.SelectedTile.OccupantCharacter);
            }
        }
    }

    private void Surrender()
    {
        foreach (ICharacterController character in GetActivePlayer().CharacterControllers)
        {
            if(character.CharacterState != CharacterState.DEAD)
            {
                character.Die();
            }
        }

        EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
    }

    private void CheckWinCondition()
    {
        if (players[0].areAllCharactersDead() && players[1].areAllCharactersDead())
        {
            EventBus.Publish(new EndMatchEvent($"Draw"));
        }
        if (players[0].areAllCharactersDead())
        {
            EventBus.Publish(new EndMatchEvent($"{players[0]} wins!"));
        }

        if (players[1].areAllCharactersDead())
        {
            EventBus.Publish(new EndMatchEvent($"{players[1]} wins!"));
        }
    }

    private void StartNextTurn()
    {
        if (activeCharacter != null)
        {
            activeCharacter.EndOfTurn();
        }

        activeCharacter = null;

        isPlayerOneTurn = !isPlayerOneTurn;
        inCharacterSelectionState = true;

        CheckForUnusedCharacters();

        EventBus.Publish(new ActiveCharacterEvent(activeCharacter));
        EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
    }

    private void SelectActiveCharacter()
    {
        if (activeCharacter != null)
        {
            EventBus.Publish(new SelectTileEvent(activeCharacter.OccupiedTile));
        }
    }
}
