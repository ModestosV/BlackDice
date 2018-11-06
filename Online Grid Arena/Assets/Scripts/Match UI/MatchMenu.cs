using UnityEngine;

public class MatchMenu : MonoBehaviour, IMatchMenu {
    
    public bool Visible { protected get; set; }
    public CanvasGroup canvasGroup;

    void OnValidate()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void Show()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
