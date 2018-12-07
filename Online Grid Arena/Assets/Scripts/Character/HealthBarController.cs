using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : IHealthBar
{
    private Text HealthText { get; set; }
    private Image HealthBarImage { get; set; }
    private Image HealthBarBackground { get; set; }
    private CanvasGroup Alpha { get; set; }

    public void SetHealthText(string currentHealth, string maxHealth)
    {
        HealthText.text = $"{currentHealth} / {maxHealth}";
    }

    public void SetHealthBarRatio(float healthRatio)
    {
        HealthBarImage.fillAmount = healthRatio;
    }

    public void Show()
    {
        Alpha.alpha = 1f;
        Alpha.blocksRaycasts = true;
    }

    public void Hide()
    {
        Alpha.alpha = 0f;
        Alpha.blocksRaycasts = false;
    }
}
