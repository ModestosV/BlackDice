using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour, IStatDisplay
{
    public Text nameText;
    public Text valueText;

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
