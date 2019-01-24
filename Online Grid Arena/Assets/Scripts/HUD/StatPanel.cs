using UnityEngine;
using System.Collections.Generic;

public class StatPanel : BlackDiceMonoBehaviour, IStatPanel
{
    protected StatPanelController controller;

    void Awake()
    {
        controller = new StatPanelController
        {
            StatDisplays = new List<IStatDisplay>(GetComponentsInChildren<StatDisplay>())
        };
    }

    void Start()
    {
        controller.DisableStatDisplays();
    }

    public IStatPanelController Controller
    {
        get { return controller; }
    }
}
