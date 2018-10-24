using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class StatPanelController : IStatPanelController
{
    public List<IStatDisplay> StatDisplays { get; set; }
    public ICharacter SelectedCharacter { get; set; }

    public void SetCharacter(ICharacter selectedCharacter)
    {
        SelectedCharacter = selectedCharacter;

        if (!AssertCharacterStatCountIsConsistentWithStatNameSet()) return;

        for (int i = 0; i < StatDisplays.Count; i++)
        {
            StatDisplays[i].Controller.CharacterStat = SelectedCharacter.Controller.CharacterStats[i];
        }
    }

    public void UpdateStatValues()
    {
        if (!AssertCharacterStatCountIsConsistentWithStatNameSet()) return;

        for (int i = 0; i < StatDisplays.Count; i++)
        {
            StatDisplays[i].SetValueText(SelectedCharacter.Controller.CharacterStats[i].Value.ToString());
        }
    }

    public void UpdateStatNames()
    {
        for (int i = 0; i < StatDisplays.Count; i++)
        {
            StatDisplays[i].SetNameText(SelectedCharacter.Controller.CharacterStatNameSet.StatNames[i]);
        }
    }

    public void DisableStatDisplays()
    {
        foreach (IStatDisplay display in StatDisplays)
        {
            display.GameObject.SetActive(false);
        }
    }

    public void EnableStatDisplays()
    {
        foreach (IStatDisplay display in StatDisplays)
        {
            display.GameObject.SetActive(true);
        }
    }

    private bool AssertCharacterStatCountIsConsistentWithStatNameSet()
    {
        int characterStatSetCount = SelectedCharacter.Controller.CharacterStatNameSet.StatNames.Count;
        int characterStatCount = SelectedCharacter.Controller.CharacterStats.Count;
        int statDisplaysCount = StatDisplays.Count;

        if (characterStatSetCount != statDisplaysCount)
        {
            Debug.Log($"The size of the selected charactar stats set ({characterStatSetCount}) does not match the number of stat displays ({statDisplaysCount}).");
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
