using UnityEngine;
using UnityEngine.UI;

public class TurnTile : MonoBehaviour
{
    private Image border;
    private RawImage characterIcon;
    
    private void Awake()
    {
        characterIcon = GetComponentInChildren<RawImage>();
        border = GetComponent<Image>();
    }

    public void UpdateTile(Texture icon, Color32 borderColor)
    {
        characterIcon.texture = icon;

        border.color = borderColor;
    }

    public GameObject GameObject
    {
        get { return gameObject; }
    }
}
