using UnityEngine;

// TODO: Remove this class
public class TurnPanel : BlackDiceMonoBehaviour, ITurnPanel
{
    protected TurnPanelController turnPanelController;

    public ITurnPanelController Controller { get { return turnPanelController; } }

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
