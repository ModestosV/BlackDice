using UnityEngine;
using UnityEngine.UI;

public class TurnIndicator : BlackDiceMonoBehaviour, IEventSubscriber
{
    private Text turnIndicator;

    public void Awake()
    {
        turnIndicator = GetComponent<Text>();
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();

        if (type == typeof(SelectActivePlayerEvent))
        {
            var selectActivePlayerEvent = (SelectActivePlayerEvent) @event;
            if (selectActivePlayerEvent.ActivePlayer.Name.Equals("1"))
            {
                turnIndicator.text = "Player 1";
                turnIndicator.color = new Color32(255, 162, 0, 255);
            }
            else
            {
                turnIndicator.text = "Player 2";
                turnIndicator.color = new Color32(0, 162, 255, 255);
            }
        }
    }
}
