using System.Collections.Generic;

public class CharacterPanel : BlackDiceMonoBehaviour
{
    private ICharacterPanelController characterPanelController;

    public CharacterTile[] CharacterTiles { get; set; }

    public void Awake()
    {
        CharacterTiles = GetComponentsInChildren<CharacterTile>();
    }
}