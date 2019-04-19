using UnityEngine;

public abstract class HideableUI : BlackDiceMonoBehaviour
{
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();    
    }

    protected void Init()
    {
        Awake();
    }

    public void Hide()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.blocksRaycasts = false;
    }

    public void Show()  
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }
}
