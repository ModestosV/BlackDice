using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPanel : MonoBehaviour, ITurnPanel
{
    private TurnPanelController turnPanelController;

    public ITurnPanelController Controller { get { return turnPanelController; } }

    public GameObject GameObject
    {
        get { return gameObject; }
    }
  
    public List<GameObject> turnTiles;

    private void Awake()
    {
        turnPanelController = new TurnPanelController();

        turnTiles = new List<GameObject>();

        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("TurnTile").OrderBy(go=>go.name))
        {
            turnPanelController.addTurnTile(tile.GetComponent<TurnTile>().Controller);
            turnTiles.Add(tile);
        }
    }
    
    public void updateQueue()
    {

    }

    //public void updateQueue()
    //{
    //    try
    //    {
    //        //activate(turnTiles[0], ActiveCharacter);

    //        int n = 1;
    //        foreach (Character character in RefreshedCharacters)
    //        {
    //            activate(turnTiles[n], character.gameObject);
    //            turnTiles[n].SetActive(true);
    //            n++;
    //            if (n > turnTiles.Count) break;
    //        }

    //        foreach (Character character in ExhaustedCharacters)
    //        {
    //            activate(turnTiles[n], character.gameObject);
    //            turnTiles[n].SetActive(true);
    //            n++;
    //            if (n > turnTiles.Count) break;
    //        }

    //        foreach (GameObject tile in turnTiles)
    //        {
    //            if (tile.GetComponent<TurnTile>().character == null)
    //            {
    //                tile.SetActive(false);
    //            }
    //        }
    //    }
    //    catch
    //    {
    //        // logging
    //    }
    //}

    private void activate(GameObject activeTile, GameObject character)
    {
        activeTile.GetComponent<TurnTile>().character = character;
        activeTile.GetComponent<TurnTile>().updateTile();
    }    
}
