using UnityEngine;
using System.Collections.Generic;

public class StatPanel : MonoBehaviour, IStatPanel
{
    public StatPanelController controller;

    private void OnValidate()
    {
        controller.StatDisplays = new List<IStatDisplay>(GetComponentsInChildren<StatDisplay>());
    }

    private void Awake()
    {
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
