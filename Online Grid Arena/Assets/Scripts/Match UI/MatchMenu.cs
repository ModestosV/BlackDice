using UnityEngine;

public class MatchMenu : MonoBehaviour, IMatchMenu {
    
    private bool Visible { get; set; }
    private CanvasGroup CanvasGroup { get; set; }

    void OnValidate()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
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

    private void Hide()
    {
        CanvasGroup.alpha = 0.0f;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }

    private void Show()
    {
        CanvasGroup.alpha = 1.0f;
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }
}
