using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour, IStatDisplay
{
    public StatDisplayController controller;

    public StatDisplayController Controller
    {
        get { return controller; }
        set { controller = value; }
    }

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        controller.nameText = texts[0];
        controller.valueText = texts[1];
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
