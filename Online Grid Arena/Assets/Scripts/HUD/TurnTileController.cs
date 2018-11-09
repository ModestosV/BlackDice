using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTileController : ITurnTileController
{
    public ITurnTile TurnTile;
    public ICharacterController Character { protected get; set; }
    public Texture CharacterIcon { protected get; set; }
    public string PlayerName { protected get; set; }

    public void updateTile()
    {
        TurnTile.updateTile(Character.GetIcon(), Character.GetPlayerName());
    }
	
}
