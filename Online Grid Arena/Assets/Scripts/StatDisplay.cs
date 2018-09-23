/*
 * Credit: Kryzarel's free Unity asset titled "Character Stats".
 * Obtained from Unity Asset Store on 2018/09/14. https://assetstore.unity.com/packages/tools/integration/character-stats-106351
 */

using System;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    public Text NameText;
    public Text ValueText;

    [NonSerialized]
    public CharacterStat Stat;

    public StatDisplay(Text _NameText, Text _ValueText, CharacterStat _Stat)
    {
        this.NameText = _NameText;
        this.ValueText = _ValueText;
        this.Stat = _Stat;
    }

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        NameText = texts[0];
        ValueText = texts[1];
    }
}
