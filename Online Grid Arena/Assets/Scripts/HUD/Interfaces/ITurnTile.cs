using UnityEngine;
using UnityEngine.UI;

public interface ITurnTile : IMonoBehaviour
{
    Texture CharacterIcon { set; }
    Color32 BorderColor { set; }

    void UpdateTile();
    void Show();
    void Hide();
}
