using UnityEngine;

public class TurnPanel : MonoBehaviour, ITurnPanel
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

    #region IMonobehavior implementation
    public GameObject GameObject
    {
        get { return gameObject; }
    }
    #endregion
}
