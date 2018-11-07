using UnityEngine;
using UnityEngine.UI;

public class EndMatchPanel : MonoBehaviour, IEndMatchPanel {

    public Text text;
    public CanvasGroup canvasGroup;

    void OnValidate()
    {
        text = GetComponentInChildren<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    void Start()
    {
        text = GetComponentInChildren<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    public void Show()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void SetWinnerText(string winnerText)
    {
        text.text = winnerText;
    }
}
