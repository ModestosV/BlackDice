using UnityEngine;
using UnityEngine.UI;

public class EndMatchMenu : MonoBehaviour, IEndMatchMenu, IEventSubscriber
{

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

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        if (type == typeof(EndMatchEvent))
        {
            var endGameEvent = (EndMatchEvent) @event;
            Show();
            SetWinnerText(endGameEvent.EndingText);
        }
    }
}
