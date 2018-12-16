using UnityEngine.UI;

public sealed class EndMatchMenu : HideableUI, IEndMatchPanel, IEventSubscriber {

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
