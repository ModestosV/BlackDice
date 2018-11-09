using UnityEngine;

public class TurnTileController : ITurnTileController
{
    public ITurnTile TurnTile;
    public ICharacterController Character { protected get; set; }
    public Texture CharacterIcon { protected get; set; }
    public string PlayerName { protected get; set; }

    public void UpdateTile(ICharacterController character)
    {
        Character = character;
        CharacterIcon = Character.CharacterIcon;
        PlayerName = Character.OwnedByPlayer;

        TurnTile.UpdateTile(CharacterIcon, PlayerName);
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
