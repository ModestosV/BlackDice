using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class TurnController : ITurnController, IEventSubscriber
{
    private List<ICharacterController> refreshedCharacters;
    private List<ICharacterController> exhaustedCharacters;
    private ICharacterController activeCharacter;
    private List<IPlayer> players;
    private List<CharacterPanel> characterPanels;

    public TurnController(List<ICharacterController> refreshedCharacters, List<ICharacterController> exhaustedCharacters, List<IPlayer> players, List<CharacterPanel> characterPanels)
    {
        this.refreshedCharacters = refreshedCharacters;
        this.exhaustedCharacters = exhaustedCharacters;
        this.players = players;
        this.characterPanels = characterPanels;
    }

    public List<ICharacterController> GetLivingCharacters()
    {
        List<ICharacterController> livingCharacters = new List<ICharacterController>();

        foreach (ICharacterController character in refreshedCharacters)
        {
            livingCharacters.Add(character);
        }
        foreach (ICharacterController character in exhaustedCharacters)
        {
            livingCharacters.Add(character);
        }
        if (activeCharacter != null)
            livingCharacters.Add(activeCharacter);

        return livingCharacters;
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
            RemoveCharacter(deathEvent.CharacterController);
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
    }

    private void Surrender()
    {
        string activePlayerName = activeCharacter.Owner;
        List<ICharacterController> livingCharacters = GetLivingCharacters();
        foreach (ICharacterController character in livingCharacters)
        {
            if (character.Owner == activePlayerName)
            {
                character.Die();
            }
        }

        EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
    }

    private void RemoveCharacter(ICharacterController character)
    {
        refreshedCharacters.Remove(character);
        exhaustedCharacters.Remove(character);
        if (activeCharacter == character)
            activeCharacter = null;
    }

    private void CheckWinCondition()
    {
        List<ICharacterController> livingCharacters = GetLivingCharacters();
        List<string> livingPlayers = new List<string>();

        foreach (ICharacterController character in livingCharacters)
        {
            string playerName = character.Owner;
            if (!livingPlayers.Contains(playerName))
            {
                livingPlayers.Add(playerName);
            }
        }

        if (livingPlayers.Count == 1)
        {
            EventBus.Publish(new EndMatchEvent($"Player {livingPlayers[0]} wins!"));
        }

        if (livingPlayers.Count == 0)
        {
            EventBus.Publish(new EndMatchEvent($"Draw"));
        }
    }

    private void StartNextTurn()
    {
        if (activeCharacter != null)
        {
            activeCharacter.EndOfTurn();
            exhaustedCharacters.Add(activeCharacter);
        }

        if (!(refreshedCharacters.Count > 0))
        {
            refreshedCharacters = exhaustedCharacters;
            exhaustedCharacters = new List<ICharacterController>();
        }

        activeCharacter = refreshedCharacters.ElementAt(0);
        refreshedCharacters.RemoveAt(0);
        activeCharacter.StartOfTurn();

        UpdateCharacterPanels();
        
        EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
    }

    private void SelectActiveCharacter()
    {
        if (activeCharacter != null)
        {
            EventBus.Publish(new SelectTileEvent(activeCharacter.OccupiedTile));
        }
    }

    private void UpdateCharacterPanels()
    {
        foreach (CharacterPanel panel in characterPanels)
        {
            foreach (CharacterTile tile in panel.CharacterTiles)
            {
                tile.HideActive();
            }
        }

        int i = 0;
        int playerNumber = int.Parse(activeCharacter.Owner) - 1;
        foreach (ICharacterController character in players[playerNumber].CharacterControllers)
        {
            if (character == activeCharacter)
            {
                characterPanels[playerNumber].CharacterTiles[i].ShowActive();
            }
            i++;
        }
    }
}
