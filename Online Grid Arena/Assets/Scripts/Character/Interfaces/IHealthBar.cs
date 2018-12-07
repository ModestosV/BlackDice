using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IHealthBar
{
    Text HealthText { get; set; }
    Image HealthBarImage { get; set; }
    Image HealthBarBackground { get; set; }
    CanvasGroup Alpha { get; set; }

    void SetHealthBarRatio(float healthRatio);
    void SetHealthText(string currentHealth, string totalHealth);
    void Show();
    void Hide();
}
