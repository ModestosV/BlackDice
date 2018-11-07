using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour, IStatDisplay
{
    public Text nameText;
    public Text currentValueText;
    public Text maxValueText;

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        currentValueText = texts[1];
        maxValueText = texts[3];
    }

    #region IStatDisplay implementation

    public void SetNameText(string nameText)
    {
        this.nameText.text = nameText;
    }

    public void SetCurrentValueText(string currentValueText)
    {
        this.currentValueText.text = currentValueText;
    }

    public void SetMaxValueText(string maxValueText)
    {
        this.maxValueText.text = maxValueText;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    #endregion

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
