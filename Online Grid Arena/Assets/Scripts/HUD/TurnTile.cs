using UnityEngine;
using UnityEngine.UI;

//TODO: Remove this class
public class TurnTile : HideableUI, ITurnTile
{
    public Color32 BorderColor { protected get; set; }
    public Texture CharacterIcon { protected get; set; }

    protected RawImage characterIcon;
    protected Image border;
    
    private void Awake()
    {
        characterIcon = GetComponentInChildren<RawImage>();
        border = GetComponent<Image>();
    }

    public void UpdateTile()
    {
        characterIcon.texture = CharacterIcon;
        border.color = new Color32(250, 250, 250, 250);
    }
}
