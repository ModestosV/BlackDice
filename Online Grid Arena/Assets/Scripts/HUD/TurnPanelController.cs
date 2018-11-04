using System.Collections.Generic;

public class TurnPanelController : ITurnPanelController
{ 
    public ITurnController TurnController { get; set; }

    public List<ITurnTile> turnTiles { get; set; }

    public void updateQueue()
    {
        foreach (ITurnTile tile in turnTiles)
        {
            tile.updateTile();
        }
    }
}
