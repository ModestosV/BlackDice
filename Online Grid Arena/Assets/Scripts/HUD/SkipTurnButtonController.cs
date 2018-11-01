using UnityEngine;
using System.Collections;

public class SkipTurnButtonController
{
    public ITurnController TurnController { get; set; }

    public void SkipTurn() 
    {
        TurnController.EndTurn();
    }
}
