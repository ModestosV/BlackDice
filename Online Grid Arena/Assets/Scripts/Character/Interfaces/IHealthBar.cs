using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IHealthBar
{
    void SetHealthBarRatio(float healthRatio);
    void SetHealthText(string currentHealth, string totalHealth);
    void Show();
    void Hide();
}
