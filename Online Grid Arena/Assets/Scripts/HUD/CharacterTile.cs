using UnityEngine;
using UnityEngine.UI;

public sealed class CharacterTile : BlackDiceMonoBehaviour
{
    private RawImage characterIcon;
    private Image border;

    private void Awake()
    {
        characterIcon = GetComponentInChildren<RawImage>();
        border = GetComponent<Image>();
    }

    public void Setup(Texture texture, Color32 color32)
    {
        characterIcon.texture = texture;
        border.color = color32;
    }
}
