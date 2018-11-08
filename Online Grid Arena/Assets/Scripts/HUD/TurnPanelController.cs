using System.Collections.Generic;

public class TurnPanelController : ITurnPanelController
{ 
    public ITurnPanel turnPanel { protected get; set; }
    public List<ITurnTileController> turnTiles { protected get; set; }

    public void addTurnTile(ITurnTileController tile)
    {
        turnTiles.Add(tile);
    }

    public void activate(ITurnTileController tile, ICharacterController character)
    {
        tile.Character = character;
        tile.updateTile();
    }

    public void updateQueue(ICharacterController ActiveCharacter, List<ICharacterController> RefreshedCharacters, List<ICharacterController> ExhaustedCharacters)
    {
        foreach (ITurnTile tile in turnTiles)
        {
            activate(turnTiles[0], ActiveCharacter);

            int n = 1;
            foreach (ICharacterController character in RefreshedCharacters)
            {
                activate(turnTiles[n], character);
                n++;
                if (n > turnTiles.Count) break;
            }

            foreach (ICharacterController character in ExhaustedCharacters)
            {
                activate(turnTiles[n], character);
                n++;
                if (n > turnTiles.Count) break;
            }

        }

        turnPanel.updateQueue();
    }
}
