using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPanel : MonoBehaviour, ITurnPanel
{
    public TurnPanelController controller;

    public ITurnPanelController Controller { get { return controller; } }

    public GameObject GameObject
    {
        get { return gameObject; }
    }
  

    public List<GameObject> turnTiles;
    private GameObject gameManager;

    private void Awake()
    {
        turnTiles = new List<GameObject>();

        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("TurnTile").OrderBy(go=>go.name))
        {
           turnTiles.Add(tile);
        }
    }

    public void updateQueue(ICharacter ActiveCharacter, List<ICharacter> RefreshedCharacters, List<ICharacter> ExhaustedCharacters)
    {
        try
        {
            activate(turnTiles[0], ActiveCharacter.GameObject);

            int n = 1;
            foreach (Character character in RefreshedCharacters)
            {
                activate(turnTiles[n], character.gameObject);
                turnTiles[n].SetActive(true);
                n++;
                if (n > turnTiles.Count) break;
            }

            foreach (Character character in ExhaustedCharacters)
            {
                activate(turnTiles[n], character.gameObject);
                turnTiles[n].SetActive(true);
                n++;
                if (n > turnTiles.Count) break;
            }

            foreach (GameObject tile in turnTiles)
            {
                if (tile.GetComponent<TurnTile>().character == null)
                {
                    tile.SetActive(false);
                }
            }
        }
        catch
        {
            // logging
        }
    }

    private void activate(GameObject activeTile, GameObject character)
    {
        activeTile.GetComponent<TurnTile>().character = character;
        activeTile.GetComponent<TurnTile>().updateTile();
    }    
}
