using UnityEngine;
using UnityEngine.UI;

public class EndMatchMenu : MonoBehaviour, IEndMatchPanel {

    private Text Text { get; set; }
    private CanvasGroup CanvasGroup { get; set; }

    void OnValidate()
    {
        Text = GetComponentInChildren<Text>();
        CanvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    void Awake()
    {
        Text = GetComponentInChildren<Text>();
        CanvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    public void Show()
    {
        CanvasGroup.alpha = 1.0f;
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        CanvasGroup.alpha = 0.0f;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }

    public void SetWinnerText(string winnerText)
    {
        Text.text = winnerText;
    }
}
