using UnityEngine;
using UnityEngine.UI;

public class HealthBar : HideableUI, IHealthBar
{
    private Text HealthText;
    private Image HealthBarImage;
    private Image HealthBarBackground;
    private CanvasGroup Alpha;

    void Awake()
    {
        HealthText = GetComponentInChildren<Text>();
        HealthBarImage = GetComponentsInChildren<Image>()[1];
        HealthBarBackground = GetComponentsInChildren<Image>()[0];
        Alpha = GetComponentInChildren<CanvasGroup>();
    }

    public void SetHealthText(string currentHealth, string maxHealth)
    {
        HealthText.text = $"{currentHealth} / {maxHealth}";
    }

    public void SetHealthBarRatio(float healthRatio)
    {
        HealthBarImage.fillAmount = healthRatio;
    }
}
