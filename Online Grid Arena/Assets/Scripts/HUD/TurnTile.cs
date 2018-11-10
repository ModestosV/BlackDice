using UnityEngine;
using UnityEngine.UI;

public class TurnTile : MonoBehaviour, ITurnTile
{
    private Image border;
    private RawImage characterIcon;
    private CanvasGroup canvasGroup;
    
    private void Awake()
    {
        characterIcon = GetComponentInChildren<RawImage>();
        border = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void UpdateTile(Texture icon, Color32 borderColor)
    {
        characterIcon.texture = icon;

        border.color = borderColor;
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
