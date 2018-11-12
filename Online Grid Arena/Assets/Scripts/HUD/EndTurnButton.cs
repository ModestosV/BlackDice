﻿using UnityEngine;
using UnityEngine.UI;


public class EndTurnButton : MonoBehaviour
{
    private Button Button { get; set; }
    public ITurnController TurnController {protected get; set; }

    void OnValidate()
    {
        Button = GetComponent<Button>();
    }

    void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(EndTurn);
    }

    public void EndTurn()
    {
        TurnController.StartNextTurn();
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
