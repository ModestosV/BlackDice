using UnityEngine;

public abstract class HideableUI : MonoBehaviour
{
    private CanvasGroup CanvasGroup { get; set; }

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    public void Hide()
    {
        CanvasGroup.alpha = 0.0f;
        CanvasGroup.blocksRaycasts = false;
    }

    public void Show()
    {
        CanvasGroup.alpha = 1.0f;
        CanvasGroup.blocksRaycasts = true;
    }
}
