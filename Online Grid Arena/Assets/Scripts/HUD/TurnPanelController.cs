using System.Collections.Generic;

//TODO: Remove class
public sealed class TurnPanelController : ITurnPanelController
{ 
    public ITurnPanel TurnPanel { private get; set; }
    public List<ITurnTile> TurnTiles { private get; set; }
    public List<ICharacterController> CharacterOrder { private get; set; }

    public TurnPanelController()
    {
        TurnTiles = new List<ITurnTile>();
        CharacterOrder = new List<ICharacterController>();
    }

    public void AddTurnTile(ITurnTile tile)
    {
        TurnTiles.Add(tile);
    }

    public void UpdateQueue(ICharacterController ActiveCharacter, List<ICharacterController> RefreshedCharacters, List<ICharacterController> ExhaustedCharacters)
    {
        // Update Character Order
        CharacterOrder.Clear();

        if (ActiveCharacter != null)
            CharacterOrder.Add(ActiveCharacter);
        CharacterOrder.AddRange(RefreshedCharacters);
        CharacterOrder.AddRange(ExhaustedCharacters);

        // Update Turn Tiles
        for (int n = 0; n < TurnTiles.Count; n++)
        {
            if (n >= CharacterOrder.Count)
            {
                TurnTiles[n].Hide();
            }
            else
            {
                TurnTiles[n].Show();
                CharacterOrder[n].UpdateTurnTile(TurnTiles[n]);
            }
        }
    }
}
