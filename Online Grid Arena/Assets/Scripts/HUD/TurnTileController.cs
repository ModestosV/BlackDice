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
        CharacterIcon = Character.GetIcon();
        PlayerName = Character.GetPlayerName();

        TurnTile.updateTile(CharacterIcon, PlayerName);
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
