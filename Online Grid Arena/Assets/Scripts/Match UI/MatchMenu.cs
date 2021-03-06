﻿public class MatchMenu : HideableUI, IMatchMenu, IEventSubscriber
{
    private bool visible;
    private IControlsMenu controlsMenu;

    void OnValidate()
    {
        Init();
    }

    void Awake()
    {
        Init();
        Hide();
    }

    public void Toggle()
    {

        visible = !visible;
        EventBus.Publish(new PauseGameEvent());

        if (!visible)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        if (type == typeof(SurrenderEvent))
        {
            Toggle();
        }

        if(type == typeof(EscapePressedEvent))
        {
            Toggle();
        }
    }
}
