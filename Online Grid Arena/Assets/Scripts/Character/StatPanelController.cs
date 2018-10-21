using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class StatPanelController
{
    public List<IStatDisplay> StatDisplays { get; set; }
    public ICharacter SelectedCharacter { get; set; }

    public void SetCharacter(ICharacter selectedCharacter)
    {
        SelectedCharacter = selectedCharacter;

        if (!AssertCharacterStatCountIsConsistent()) return;

        for (int i = 0; i < StatDisplays.Count; i++)
        {
            StatDisplays[i].Controller.CharacterStat = SelectedCharacter.Controller().CharacterStats[i];
        }
    }

    public void UpdateStatValues()
    {
        if (!AssertCharacterStatCountIsConsistent()) return;

        for (int i = 0; i < StatDisplays.Count; i++)
        {
            StatDisplays[i].Controller.valueText.text = SelectedCharacter.Controller().CharacterStats[i].Value.ToString();
        }
    }

    public void UpdateStatNames()
    {
        for (int i = 0; i < StatDisplays.Count; i++)
        {
            StatDisplays[i].Controller.nameText.text = SelectedCharacter.Controller().characterStatNameSet.StatNames[i];
        }
    }

    private bool AssertCharacterStatCountIsConsistent()
    {
        int characterStatSetCount = SelectedCharacter.Controller().characterStatNameSet.StatNames.Count;
        int characterStatCount = SelectedCharacter.Controller().CharacterStats.Count;
        int statDisplaysCount = StatDisplays.Count;

        if (characterStatSetCount != statDisplaysCount)
        {
            Debug.LogError($"The size of the selected charactar stats set ({characterStatSetCount}) does not match the number of stat displays ({statDisplaysCount}).");
            return false;
        }

        if (characterStatCount != statDisplaysCount)
        {
            Debug.LogError($"The number of select charactar stats ({characterStatCount}) does not match the number of stat displays ({statDisplaysCount}).");
            return false;
        }

        return true;
    }
}
