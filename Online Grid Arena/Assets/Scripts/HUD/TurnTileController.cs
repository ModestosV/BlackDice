using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTileController : ITurnTileController
{
    public ITurnTile turnTile;
    public ICharacterController Character { protected get; set; }

    public void updateTile()
    {
        turnTile.updateTile();
    }
	
}
