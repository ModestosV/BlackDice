using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class SkipTurnButtonController: ISkipTurnButtonController
{
    public ITurnController TurnController { get; set; }

    public void SkipTurn() 
    {
        TurnController.EndTurn();
    }
}
