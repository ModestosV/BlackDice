using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour, IStatDisplay
{
    private Text NameText { get; set; }
    private Text CurrentValueText { get; set; }
    private Text MaxValueText { get; set; }
    private CanvasGroup CanvasGroup { get; set; }

    void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        NameText = texts[0];
        CurrentValueText = texts[1];
        MaxValueText = texts[3];
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    void Awake()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        NameText = texts[0];
        CurrentValueText = texts[1];
        MaxValueText = texts[3];
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    #region IStatDisplay implementation

    public void SetNameText(string nameText)
    {
        NameText.text = nameText;
    }

    public void SetCurrentValueText(string currentValueText)
    {
        CurrentValueText.text = currentValueText;
    }

    public void SetMaxValueText(string maxValueText)
    {
        MaxValueText.text = maxValueText;
    }

    public void Activate()
    {
        Show();
    }

    public void Deactivate()
    {
        Hide();
    }

    private void Hide()
    {
        CanvasGroup.alpha = 0.0f;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }

    private void Show()
    {
        CanvasGroup.alpha = 1.0f;
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }

    #endregion

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
