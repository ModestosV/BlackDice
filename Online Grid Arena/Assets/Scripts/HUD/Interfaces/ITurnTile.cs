using UnityEngine;

public interface ITurnTile : IMonoBehaviour
{
    ITurnTileController Controller { get; }

    void UpdateTile(Texture icon, string name);
}
