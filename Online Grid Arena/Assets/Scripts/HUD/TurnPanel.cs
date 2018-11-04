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

    private void Start()
    {
        turnTiles = new List<GameObject>();
        gameManager = GameObject.Find("GameManager");

        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("TurnTile").OrderBy(go=>go.name))
        {
           turnTiles.Add(tile);
        }
    }

    private void Update()
    {
        if (turnTiles[0].GetComponent<TurnTile>().character != gameManager.GetComponent<GameManager>().turnController.ActiveCharacter.GameObject)
        {
            refreshTiles();
        }
    }

    private void refreshTiles()
    {
        try
        {
            activate(turnTiles[0], gameManager.GetComponent<GameManager>().turnController.ActiveCharacter.GameObject);

            int n = 1;
            foreach (Character character in gameManager.GetComponent<GameManager>().turnController.RefreshedCharacters)
            {

                activate(turnTiles[n], character.gameObject);
                turnTiles[n].SetActive(true);
                n++;
                if (n > turnTiles.Count) break;
            }

            if (gameManager.GetComponent<GameManager>().turnController.RefreshedCharacters.Count < 2)
            {
                turnTiles[gameManager.GetComponent<GameManager>().turnController.RefreshedCharacters.Count + 1].SetActive(false);
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
        Debug.Log(character);
        activeTile.GetComponent<TurnTile>().character = character;
    }    
}
