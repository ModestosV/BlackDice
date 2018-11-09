using UnityEngine;
using UnityEngine.UI;

public class TurnTile : MonoBehaviour, ITurnTile
{
    private TurnTileController controller;

    public ITurnTileController Controller { get { return controller; } }

    private Texture characterIcon;
    private string playerName;
    
    private void Awake()
    {
        controller = new TurnTileController
        {
            TurnTile = this,
            Character = null,
            CharacterIcon = null,
            PlayerName = null
        };
    }

    public void updateTile(Texture icon, string player)
    {
        characterIcon = icon;
        playerName = player;

        try
        {
            this.GameObject.transform.GetChild(0).GetComponent<RawImage>().texture = characterIcon;
            if (playerName == "1")
                this.GameObject.GetComponent<Image>().color = new Color32(0, 150, 255, 255);
            else
                this.GameObject.GetComponent<Image>().color = new Color32(255, 150, 0, 255);
        }
        catch
        {
            // logging
        }
    }

    public GameObject GameObject
    {
        get { return gameObject; }
    }
}
