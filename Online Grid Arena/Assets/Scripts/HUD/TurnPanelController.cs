using System.Collections.Generic;
using UnityEngine;

public class TurnPanelController : ITurnPanelController
{ 
    public ITurnPanel TurnPanel { protected get; set; }
    public List<TurnTile> TurnTiles { protected get; set; }
    public List<ICharacterController> CharacterOrder { protected get; set; }

    public void AddTurnTile(TurnTile tile)
    {
        TurnTiles.Add(tile);
    }
    
    public void SetTiles()
    {
        for (int n = 0; n < TurnTiles.Count; n++)
        {
            if (n >= CharacterOrder.Count)
            {
                TurnTiles[n].GameObject.SetActive(false);
            }
            else
            {
                TurnTiles[n].GameObject.SetActive(true);
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
