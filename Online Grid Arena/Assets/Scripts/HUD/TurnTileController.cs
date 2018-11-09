using UnityEngine;

public class TurnTileController : ITurnTileController
{
    public ITurnTile TurnTile;
    public ICharacterController Character { protected get; set; }
    public Texture CharacterIcon { protected get; set; }
    public string PlayerName { protected get; set; }

    public void updateTile(ICharacterController character)
    {
        Character = character;
        CharacterIcon = Character.GetIcon();
        PlayerName = Character.GetPlayerName();

        TurnTile.updateTile(CharacterIcon, PlayerName);
    }
	
}
