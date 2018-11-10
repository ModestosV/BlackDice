using System.Collections.Generic;
using UnityEngine;

public class TurnPanelController : ITurnPanelController
{ 
    public ITurnPanel TurnPanel { protected get; set; }
    public List<ITurnTile> TurnTiles { protected get; set; }
    public List<ICharacterController> CharacterOrder { protected get; set; }

    public TurnPanelController()
    {
        TurnTiles = new List<ITurnTile>();
        CharacterOrder = new List<ICharacterController>();
    }

    public void AddTurnTile(ITurnTile tile)
    {
        TurnTiles.Add(tile);
    }
    
    public void SetTiles()
    {
        for (int n = 0; n < TurnTiles.Count; n++)
        {
            if (n >= CharacterOrder.Count)
            {
                TurnTiles[n].Hide();
            }
            else
            {
                TurnTiles[n].Show();
                TurnTiles[n].UpdateTile(CharacterOrder[n].CharacterIcon, CharacterOrder[n].BorderColor);
            }
        }
    }

    public void UpdateQueue(ICharacterController ActiveCharacter, List<ICharacterController> RefreshedCharacters, List<ICharacterController> ExhaustedCharacters)
    {
        CharacterOrder.Clear();

        CharacterOrder.Add(ActiveCharacter);
        CharacterOrder.AddRange(RefreshedCharacters);
        CharacterOrder.AddRange(ExhaustedCharacters);

        SetTiles();
    }
}
