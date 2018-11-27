using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IHealthBar
{
    Text healthText { set; }
    Image healthBarImage { set; }
    Image healthBarBackground { set; }

    void SetHealthBarRatio(float healthRatio);
}
