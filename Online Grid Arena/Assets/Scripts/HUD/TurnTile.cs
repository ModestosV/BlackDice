using UnityEngine;
using UnityEngine.UI;

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
        base.Init();
    }

    public void UpdateTile()
    {
        characterIcon.texture = CharacterIcon;
        border.color = BorderColor;
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
