using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IHealthBar
{
    private Text HealthText { get; set; }
    private Image HealthBarImage { get; set; }
    private Image HealthBarBackground { get; set; }
    private CanvasGroup Alpha { get; set; }

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
