public class ControlsMenu : HideableUI, IControlsMenu
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
}
