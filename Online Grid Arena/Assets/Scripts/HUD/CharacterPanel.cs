public class CharacterPanel : BlackDiceMonoBehaviour
{
    public CharacterTile[] CharacterTiles { get; set; }

    public void Awake()
    {
        CharacterTiles = GetComponentsInChildren<CharacterTile>();
    }
}