using UnityEngine;

public class TurnTileController : ITurnTileController
{
    public ITurnTile TurnTile;
    public ICharacterController Character { protected get; set; }
    public Texture CharacterIcon { protected get; set; }
    public int Player { protected get; set; }

    public void UpdateTile(ICharacterController character, int player)
    {
        Character = character;
        CharacterIcon = Character.CharacterIcon;
        Player = player;

        TurnTile.UpdateTile(CharacterIcon, Player);
    }
	
    public void Hide()
    {
        TurnTile.GameObject.SetActive(false);
    }

    public void Show()
    {
        TurnTile.GameObject.SetActive(true);
    }
}
