public class CharacterPanel : BlackDiceMonoBehaviour
{
    public CharacterTile[] CharacterTiles { get; private set; }

    public void Awake()
    {
        CharacterTiles = GetComponentsInChildren<CharacterTile>();
    }
}