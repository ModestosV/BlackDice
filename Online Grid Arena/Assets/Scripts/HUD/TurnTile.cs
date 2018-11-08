using UnityEngine;
using UnityEngine.UI;

public class TurnTile : MonoBehaviour, ITurnTile
{
    private TurnTileController controller;
    public GameObject character;
    
    public void updateTile(ICharacterController character)
    {
        // TO-DO: Refactor to get texture through characterController
        try
        {
            //this.GameObject.transform.GetChild(0).GetComponent<RawImage>().texture = character;
            //if (character.OwnedByPlayer == "0")
            //    this.GameObject.GetComponent<Image>().color = new Color32(0, 150, 255, 255);
            //else
            //    this.GameObject.GetComponent<Image>().color = new Color32(255, 150, 0, 255);
        }
        catch
        {
            // logging
        }
    }

    public ITurnTileController Controller { get { return controller; } }

    public GameObject GameObject
    {
        get { return gameObject; }
    }
}
