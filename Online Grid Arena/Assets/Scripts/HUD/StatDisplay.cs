using UnityEngine.UI;

public class StatDisplay : HideableUI, IStatDisplay
{
    private Text nameText;
    private Text currentValueText;
    private Text maxValueText;

    void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        currentValueText = texts[1];
        maxValueText = texts[3];
        Init();
    }

    void Awake()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        currentValueText = texts[1];
        maxValueText = texts[3];
        Init();
    }
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
}
