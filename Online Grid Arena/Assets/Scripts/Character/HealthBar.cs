using UnityEngine;
using UnityEngine.UI;

public class HealthBar : HideableUI, IHealthBar
{
    private Text healthText;
    private Image healthBarImage;
    private Image healthBarBackground;
    private CanvasGroup alpha;

    void Awake()
    {
        healthText = GetComponentInChildren<Text>();
        healthBarImage = GetComponentsInChildren<Image>()[1];
        healthBarBackground = GetComponentsInChildren<Image>()[0];
        alpha = GetComponentInChildren<CanvasGroup>();
    }

    public void SetHealthText(string currentHealth, string maxHealth)
    {
        healthText.text = $"{currentHealth} / {maxHealth}";
    }

    public void SetHealthBarRatio(float healthRatio)
    {
        healthBarImage.fillAmount = healthRatio;
    }
}
