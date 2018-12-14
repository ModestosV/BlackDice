using UnityEngine.UI;

public sealed class EndMatchMenu : HideableUI, IEndMatchPanel {

    private Text text;

    void OnValidate()
    {
        text = GetComponentInChildren<Text>();
        base.Init();
        Hide();
    }

    void Awake()
    {
        text = GetComponentInChildren<Text>();
        base.Init();
        Hide();
    }

    public void SetWinnerText(string winnerText)
    {
        text.text = winnerText;
    }
}
