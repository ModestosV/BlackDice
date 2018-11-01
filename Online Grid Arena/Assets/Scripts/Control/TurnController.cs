using System.Collections.Generic;
using System.Linq;
using System;

[Serializable]
public class TurnController : ITurnController
{
    public List<ICharacter> RefreshedCharacters { get; set; }
    public List<ICharacter> ExhaustedCharacters { get; set; }
    public ICharacter ActiveCharacter { get; set; }

    public void Init()
    {
        RefreshedCharacters = new List<ICharacter>();
        ExhaustedCharacters = new List<ICharacter>();
    }

    public void StartNextTurn()
    {
        if (!(RefreshedCharacters.Count > 0))
        {
            RefreshedCharacters = ExhaustedCharacters.ToList();
            ExhaustedCharacters.Clear();
        }

        // Sort characters by ascending initiative
        RefreshedCharacters.Sort((x, y) => x.Controller.GetInitiative().CompareTo(y.Controller.GetInitiative()));

        ActiveCharacter = RefreshedCharacters.ElementAt(0);
        RefreshedCharacters.RemoveAt(0);

        ActiveCharacter.Controller.Refresh();
        ActiveCharacter.Controller.OccupiedTile.Controller.Deselect();
        ActiveCharacter.Controller.OccupiedTile.Controller.Select();
    }

    public void EndTurn()
    {
        ExhaustedCharacters.Add(ActiveCharacter);

        StartNextTurn();
    }
}
