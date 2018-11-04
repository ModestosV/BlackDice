public interface ITurnTile : IMonoBehaviour
{
    ITurnTileController Controller { get; }

    void updateTile();
}
