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
            TurnPanel = this,
            TurnTiles = new List<TurnTile>(),
            CharacterOrder = new List<ICharacterController>()
        };
        
        foreach (TurnTile tile in GetComponentsInChildren<TurnTile>())
        {
            turnPanelController.AddTurnTile(tile);
        }
    }
}
