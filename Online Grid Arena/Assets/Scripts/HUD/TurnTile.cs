using UnityEngine;
using UnityEngine.UI;

public class TurnTile : MonoBehaviour, ITurnTile
{
    public Color32 BorderColor { protected get; set; }
    public Texture CharacterIcon { protected get; set; }

    private CanvasGroup canvasGroup;
    private RawImage characterIcon;
    private Image border;
    
    private void Awake()
    {
        characterIcon = GetComponentInChildren<RawImage>();
        border = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();     
    }

    public void UpdateTile()
    {
        characterIcon.texture = CharacterIcon;
        border.color = BorderColor;
    }

    public void Show()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.interactable = true;    
    }

    public void Hide()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.interactable = false;
    }

    public GameObject GameObject
    {
        get { return gameObject; }
    }
}
