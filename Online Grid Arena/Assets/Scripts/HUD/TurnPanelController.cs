using System.Collections.Generic;
using UnityEngine;

public class TurnPanelController : ITurnPanelController
{ 
    public ITurnPanel TurnPanel { protected get; set; }
    public List<ITurnTileController> TurnTiles { protected get; set; }
    public List<ICharacterController> CharacterOrder { protected get; set; }

    public void AddTurnTile(ITurnTileController tile)
    {
        TurnTiles.Add(tile);
    }

    public void SetTiles()
    {
        for (int n = 0; n < TurnTiles.Count; n++)
        {
            if (n >= CharacterOrder.Count) break;
            TurnTiles[n].Character = CharacterOrder[n];
            TurnTiles[n].updateTile();
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
