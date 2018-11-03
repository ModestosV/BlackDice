using UnityEngine;


public class SkipTurnButton : MonoBehaviour, ISkipTurnButton
{
    public SkipTurnButtonController Controller { get; set; }

    public void SkipTurn()
    {
        Controller.SkipTurn();
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
