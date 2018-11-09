using UnityEngine;
using UnityEngine.UI;

public class TurnTile : MonoBehaviour, ITurnTile
{
    private TurnTileController controller;

    public ITurnTileController Controller { get { return controller; } }

    private Image border;
    private RawImage characterIcon;
    
    private void Awake()
    {
        controller = new TurnTileController
        {
            TurnTile = this,
            Character = null,
            CharacterIcon = null,
            Player = 0
        };

        characterIcon = GetComponentInChildren<RawImage>();
        border = GetComponent<Image>();
    }

    public void UpdateTile(Texture icon, int player)
    {
        characterIcon.texture = icon;

        switch (player)
        {
            case 0:
                border.color = new Color32(0, 150, 255, 255);
                break;
            case 1:
                border.color = new Color32(255, 150, 0, 255);
                break;
            case 2:
                border.color = new Color32(150, 0, 255, 255);
                break;
            default:
                border.color = new Color32(255, 255, 255, 255);
                break;
        }
    }

    public GameObject GameObject
    {
        get { return gameObject; }
    }
}
