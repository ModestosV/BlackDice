using System.Collections.Generic;
using System.Linq;

public sealed class TurnController : ITurnController, IEventSubscriber
{
    public List<ICharacterController> RefreshedCharacters { private get; set; }
    public List<ICharacterController> ExhaustedCharacters { private get; set; }
    public ICharacterController ActiveCharacter { private get; set; }
    public ITurnPanelController TurnTracker { private get; set; }

    public TurnController()
    {
        RefreshedCharacters = new List<ICharacterController>();
        ExhaustedCharacters = new List<ICharacterController>();
    }
    
    public void AddCharacters(List<ICharacterController> characters)
    {
        foreach (ICharacterController character in characters)
        {
            RefreshedCharacters.Add(character);
        }
    }

    public void AddCharacter(ICharacterController character)
    {
        RefreshedCharacters.Add(character);
    }

    public void SelectActiveCharacter()
    {
        if (ActiveCharacter != null)
            ActiveCharacter.Select();
    }

    public List<ICharacterController> GetLivingCharacters()
    {
        List<ICharacterController> livingCharacters = new List<ICharacterController>();

        foreach (ICharacterController character in RefreshedCharacters)
        {
            livingCharacters.Add(character);
        }
        foreach (ICharacterController character in ExhaustedCharacters)
        {
            livingCharacters.Add(character);
        }
        if (ActiveCharacter != null)
            livingCharacters.Add(ActiveCharacter);

        return livingCharacters;
    }

    public bool IsActiveCharacter(ICharacterController character)
    {
        return ActiveCharacter == character;
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        if(type == typeof(DeathEvent))
        {
            var deathEvent = (DeathEvent) @event;
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
    }

    private void Surrender()
    {
        string activePlayerName = ActiveCharacter.OwnedByPlayer;
        List<ICharacterController> livingCharacters = GetLivingCharacters();
        foreach (ICharacterController character in livingCharacters)
        {
            if (character.OwnedByPlayer == activePlayerName)
            {
                character.Die();
            }
        }

        EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
    }

    private void RemoveCharacter(ICharacterController character)
    {
        RefreshedCharacters.Remove(character);
        ExhaustedCharacters.Remove(character);
        if (ActiveCharacter == character)
            ActiveCharacter = null;
        TurnTracker.UpdateQueue(ActiveCharacter, RefreshedCharacters, ExhaustedCharacters);
    }

    private void CheckWinCondition()
    {
        List<ICharacterController> livingCharacters = GetLivingCharacters();
        List<string> livingPlayers = new List<string>();

        foreach (ICharacterController character in livingCharacters)
        {
            string playerName = character.OwnedByPlayer;
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
        if (ActiveCharacter != null)
        {
            ExhaustedCharacters.Add(ActiveCharacter);
            ActiveCharacter.Deselect();
        }

        if (!(RefreshedCharacters.Count > 0))
        {
            RefreshedCharacters = ExhaustedCharacters;
            ExhaustedCharacters = new List<ICharacterController>();
        }

        // Sort characters by ascending initiative
        RefreshedCharacters.Sort((x, y) => x.GetInitiative().CompareTo(y.GetInitiative()));

        ActiveCharacter = RefreshedCharacters.ElementAt(0);
        RefreshedCharacters.RemoveAt(0);

        ActiveCharacter.Refresh();

        TurnTracker.UpdateQueue(ActiveCharacter, RefreshedCharacters, ExhaustedCharacters);

        ActiveCharacter.DeHighlight();

        EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));

        ActiveCharacter.Highlight();
    }
}
