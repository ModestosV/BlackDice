using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour, IStatDisplay
{
    protected Text nameText;
    protected Text currentValueText;
    protected Text maxValueText;
    protected CanvasGroup canvasGroup;

    void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        currentValueText = texts[1];
        maxValueText = texts[3];
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Awake()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        currentValueText = texts[1];
        maxValueText = texts[3];
        canvasGroup = GetComponent<CanvasGroup>();
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
        Show();
    }

    public void Deactivate()
    {
        Hide();
    }

    private void Hide()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void Show()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    #endregion

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
