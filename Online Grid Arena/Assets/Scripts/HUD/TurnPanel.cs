using System.Collections.Generic;
using UnityEngine;

public class TurnPanel : MonoBehaviour, ITurnPanel
{
    private TurnPanelController turnPanelController;

    public ITurnPanelController Controller { get { return turnPanelController; } }

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    private void Awake()
    {
        
        turnPanelController = new TurnPanelController()
        {
            TurnPanel = this
        };
        
        foreach (ITurnTile tile in GetComponentsInChildren<TurnTile>())
        {
            turnPanelController.AddTurnTile(tile);
        }
    }
}
