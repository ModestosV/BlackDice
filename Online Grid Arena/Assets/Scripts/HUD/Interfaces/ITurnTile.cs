using UnityEngine;

public interface ITurnTile : IMonoBehaviour
{
    ITurnTileController Controller { get; }

    void UpdateTile(Texture icon, int name);
}
