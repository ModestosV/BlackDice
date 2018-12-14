using UnityEngine;

public class MatchMenu : HideableUI, IMatchMenu {
    
    private bool Visible { get; set; }
    private CanvasGroup CanvasGroup { get; set; }

    void OnValidate()
    {
        base.Init();
    }

    void Awake()
    {
        base.Init();
        Hide();
    }

    public void Toggle()
    {
        Visible = !Visible;

        if (!Visible)
        {
            Hide();
        } else
        {
            Show();
        }
    }
}
