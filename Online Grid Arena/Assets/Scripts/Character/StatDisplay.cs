/*
 * Credit: Kryzarel's free Unity asset titled "Character Stats".
 * Obtained from Unity Asset Store on 2018/09/14. https://assetstore.unity.com/packages/tools/integration/character-stats-106351
 */

using System;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    public Text nameText;
    public Text valueText;

    [NonSerialized]
    public CharacterStat stat;

    public StatDisplay(Text nameText, Text valueText, CharacterStat stat)
    {
        this.nameText = nameText;
        this.valueText = valueText;
        this.stat = stat;
    }

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        valueText = texts[1];
    }
}
