using UnityEngine;
using System.Collections.Generic;

public class StatPanel : MonoBehaviour, IStatPanel
{
    private StatPanelController controller;

    void Awake()
    {
        controller = new StatPanelController();
        controller.StatDisplays = new List<IStatDisplay>(GetComponentsInChildren<StatDisplay>());
        controller.DisableStatDisplays();
    }

    public IStatPanelController Controller
    {
        get { return controller; }
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
