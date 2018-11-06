using UnityEngine;
using System.Collections.Generic;

public class StatPanelController : IStatPanelController
{
    public List<IStatDisplay> StatDisplays { protected get; set; }
    public List<ICharacterStat> CharacterStats { protected get; set; }
    public List<string> StatNames { protected get; set; }
    
    public void UpdateStatValues()
    {
        if (!StatsAndNamesAreConsistent()) return;

        for (int i = 0; i < StatDisplays.Count; i++)
        {
            StatDisplays[i].SetValueText(CharacterStats[i].Value.ToString());
        }
    }

    public void UpdateStatNames()
    {
        if (!StatsAndNamesAreConsistent()) return;

        for (int i = 0; i < StatDisplays.Count; i++)
        {
            StatDisplays[i].SetNameText(StatNames[i]);
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

    private bool StatsAndNamesAreConsistent()
    {

        if (StatNames.Count != StatDisplays.Count)
        {
            Debug.Log($"The size of the selected charactar stats set ({StatNames.Count}) does not match the number of stat displays ({StatDisplays.Count}).");
            return false;
        }

        if (CharacterStats.Count != StatDisplays.Count)
        {
            Debug.LogError($"The number of select charactar stats ({CharacterStats.Count}) does not match the number of stat displays ({StatDisplays.Count}).");
            return false;
        }

        return true;
    }
}
