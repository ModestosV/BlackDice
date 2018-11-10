using UnityEngine;

public interface ITurnTile : IMonoBehaviour
{
    void UpdateTile(Texture icon, Color32 borderColor);
}
