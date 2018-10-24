using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour, IStatDisplay
{
    public StatDisplayController controller;

    public Text nameText;
    public Text valueText;

    public IStatDisplayController Controller
    {
        get { return controller; }
    }

    public Text NameText
    {
        get { return nameText; }
        set { nameText = value; }
    }

    public Text ValueText
    {
        get { return valueText; }
        set { valueText = value; }
    }

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        valueText = texts[1];
    }

    #region IStatDisplay implementation

    public void SetNameText(string nameText)
    {
        this.nameText.text = nameText;
    }

    public void SetValueText(string valueText)
    {
        this.valueText.text = valueText;
    }

    #endregion

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
