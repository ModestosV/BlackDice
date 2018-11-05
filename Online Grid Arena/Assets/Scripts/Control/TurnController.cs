using System.Collections.Generic;
using System.Linq;
using System;

public class TurnController : ITurnController
{
    private List<ICharacter> RefreshedCharacters { get; set; }
    private List<ICharacter> ExhaustedCharacters { get; set; }
    private ICharacter ActiveCharacter { get; set; }
    private IHUDController HUDController { get; set; }

    private TurnController()
    {
        RefreshedCharacters = new List<ICharacter>();
        ExhaustedCharacters = new List<ICharacter>();
    }

    public TurnController(IHUDController hudController) : this()
    {
        HUDController = hudController;
    }

    public void AddCharacters(List<ICharacter> characters)
    {
        foreach (ICharacter character in characters)
        {
            RefreshedCharacters.Add(character);
        }
    }

    public void AddCharacter(ICharacter character)
    {
        RefreshedCharacters.Add(character);
    }

    public void RemoveCharacter(ICharacter character)
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
            ActiveCharacter.Controller.OccupiedTile.Deselect();
            HUDController.ClearSelectedHUD();
        }

        if (!(RefreshedCharacters.Count > 0))
        {
            RefreshedCharacters = ExhaustedCharacters;
            ExhaustedCharacters = new List<ICharacter>();
        }

        // Sort characters by ascending initiative
        RefreshedCharacters.Sort((x, y) => x.Controller.GetInitiative().CompareTo(y.Controller.GetInitiative()));

        ActiveCharacter = RefreshedCharacters.ElementAt(0);
        RefreshedCharacters.RemoveAt(0);

        ActiveCharacter.Controller.Refresh();
    }
}
