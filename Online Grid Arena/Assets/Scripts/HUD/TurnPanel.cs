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
        turnPanelController = new TurnPanelController()
        {
            TurnPanel = this,
            TurnTiles = new List<ITurnTileController>(3),
            CharacterOrder = new List<ICharacterController>()
        };

        turnTiles = new List<GameObject>();

        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("TurnTile").OrderBy(go=>go.name))
        {
            turnPanelController.AddTurnTile(tile.GetComponent<TurnTile>().Controller);
            turnTiles.Add(tile);
        }
    }
    
}
