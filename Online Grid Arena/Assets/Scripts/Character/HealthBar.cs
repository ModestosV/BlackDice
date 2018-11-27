using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IHealthBar
{
    public Text healthText { get; set; }
    public Image healthBarImage { get; set; }
    public Image healthBarBackground { get; set; }

    void Awake()
    {
        healthText = GetComponentInChildren<Text>();
        healthBarImage = GetComponentsInChildren<Image>()[1];
        healthBarBackground = GetComponentsInChildren<Image>()[0];
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
