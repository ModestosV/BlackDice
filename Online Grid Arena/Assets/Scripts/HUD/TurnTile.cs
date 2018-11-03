using UnityEngine;
using UnityEngine.UI;

public class TurnTile : MonoBehaviour, ITurnTile
{
    public TurnTileController controller;

    [SerializeField]
    GameObject character;

    private void Awake()
    {
        this.GameObject.transform.GetChild(0).GetComponent<RawImage>().texture = character.GetComponent<Character>().characterIcon;
        if (character.GetComponent<Character>().controller.ownedByPlayer == 0)
            this.GameObject.GetComponent<Image>().color = new Color32(0, 150, 255, 255);
        else
            this.GameObject.GetComponent<Image>().color = new Color32(255, 50, 0, 255);
    }

    public ITurnTileController Controller { get { return controller; } }

    public GameObject GameObject
    {
        get { return gameObject; }
    }
}
