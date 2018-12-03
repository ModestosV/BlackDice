using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IHealthBar
{
    Text HealthText { set; }
    Image HealthBarImage { set; }
    Image HealthBarBackground { set; }
    CanvasGroup Alpha { set; }

    void SetHealthBarRatio(float healthRatio);
    void SetHealthText(string currentHealth, string totalHealth);
    void Show();
    void Hide();
}
