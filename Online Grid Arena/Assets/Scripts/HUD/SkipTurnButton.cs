using UnityEngine;


public class SkipTurnButton : MonoBehaviour
{
    public SkipTurnButtonController Controller { get; set; }

    public void SkipTurn()
    {
        Controller.SkipTurn();
    }
}
