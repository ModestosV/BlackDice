using System.Collections.Generic;
using System.Linq;
using System;

public class TurnController : ITurnController
{
    public List<ICharacterController> RefreshedCharacters { protected get; set; }
    public List<ICharacterController> ExhaustedCharacters { protected get; set; }
    public ICharacterController ActiveCharacter { protected get; set; }
    public ITurnPanel TurnTracker { protected get; set; }

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

    public void RemoveCharacter(ICharacterController character)
    {
        RefreshedCharacters.Remove(character);
        ExhaustedCharacters.Remove(character);
        if (ActiveCharacter == character)
            ActiveCharacter = null;
    }

    public void StartNextTurn()
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
        TurnTracker.updateQueue(ActiveCharacter, RefreshedCharacters, ExhaustedCharacters);
    }

    public void SelectActiveCharacter()
    {
        if (ActiveCharacter != null)
            ActiveCharacter.Select();
    }
}
