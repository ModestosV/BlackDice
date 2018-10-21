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
        gameObject.SetActive(false);
    }

    public StatPanelController Controller
    {
        get { return controller; }
        set { controller = value; }
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
