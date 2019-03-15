public class ControlsMenu : HideableUI, IControlsMenu, IEventSubscriber
{ 
    private bool visible;

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
        if (type == typeof(EscapePressedEvent))
        {
            if(visible)
            {
                Toggle();
            }
        }
    }
    
}
