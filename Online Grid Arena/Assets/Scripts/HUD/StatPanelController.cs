using UnityEngine;
using System.Collections.Generic;

public sealed class StatPanelController : IStatPanelController
{
    public List<IStatDisplay> StatDisplays { private get; set; }
    public Dictionary<string, ICharacterStat> CharacterStats { private get; set; }
    public List<string> StatNames { private get; set; }
    
    public void UpdateStatValues()
    {
        StatDisplays[0].SetCurrentValueText(Mathf.CeilToInt(CharacterStats["health"].CurrentValue).ToString());
        StatDisplays[0].SetMaxValueText(Mathf.CeilToInt(CharacterStats["health"].Value).ToString());

        StatDisplays[1].SetCurrentValueText(Mathf.CeilToInt(CharacterStats["moves"].CurrentValue).ToString());
        StatDisplays[1].SetMaxValueText(Mathf.CeilToInt(CharacterStats["moves"].BaseValue).ToString());

        StatDisplays[2].SetCurrentValueText(Mathf.CeilToInt(CharacterStats["attack"].CurrentValue).ToString());
        StatDisplays[2].SetMaxValueText(CheckAttackBuff());

        StatDisplays[3].SetCurrentValueText(Mathf.CeilToInt(CharacterStats["defense"].CurrentValue).ToString());
        StatDisplays[3].SetMaxValueText(CheckDefenseBuff());
    }

    public string CheckAttackBuff()
    {
        if (CharacterStats["attack"].CurrentValue > CharacterStats["attack"].BaseValue)
        {
            return " / " + Mathf.CeilToInt(CharacterStats["attack"].BaseValue).ToString();
        }
        else
        {
            return "TEST";
        }
    }

    public string CheckDefenseBuff()
    {
        if (CharacterStats["defense"].CurrentValue > CharacterStats["defense"].BaseValue)
        {
            return " / " + Mathf.CeilToInt(CharacterStats["defense"].BaseValue).ToString();
        }
        else
        {
            return "TEST";
        }
    }

    public void DisableStatDisplays()
    {
        foreach (IStatDisplay display in StatDisplays)
        {
            display.Deactivate();
        }
    }

    public void EnableStatDisplays()
    {
        foreach (IStatDisplay display in StatDisplays)
        {
            display.Activate();
        }
    }
}
