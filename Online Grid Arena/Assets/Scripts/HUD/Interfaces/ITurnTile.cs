using UnityEngine;

public interface ITurnTile : IMonoBehaviour
{
    ITurnTileController Controller { get; }

    void updateTile(Texture icon, string name);
}
