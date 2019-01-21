using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class HUDController : IHUDController
{
    public IStatPanelController SelectedStatPanel { private get; set; }
    public IPlayerPanel SelectedPlayerPanel { private get; set; }

    public IStatPanelController TargetStatPanel { private get; set; }
    public IPlayerPanel TargetPlayerPanel { private get; set; }

    public IAbilityPanel AbilityPanel { private get; set; }

    public void ClearSelectedHUD()
    {
        SelectedStatPanel.DisableStatDisplays();
        SelectedPlayerPanel.ClearPlayerName();
        AbilityPanel.Hide();
    }

    public void UpdateSelectedHUD(Dictionary<string, ICharacterStat> characterStats, string playerName, List<IAbility> abilities)
    {
        SelectedStatPanel.EnableStatDisplays();
        SelectedStatPanel.CharacterStats = characterStats;
        SelectedStatPanel.UpdateStatValues();
        SelectedPlayerPanel.SetPlayerName($"Player {playerName}");

        AbilityPanel.UpdateAbilityIcons(abilities);
        AbilityPanel.Show();
    }

    public void ClearTargetHUD()
    {
        TargetStatPanel.DisableStatDisplays();
        TargetPlayerPanel.ClearPlayerName();
    }

    public void UpdateTargetHUD(Dictionary<string, ICharacterStat> characterStats, string playerName)
    {
        TargetStatPanel.EnableStatDisplays();
        TargetStatPanel.CharacterStats = characterStats;
        TargetStatPanel.UpdateStatValues();
        TargetPlayerPanel.SetPlayerName($"Player {playerName}");
    }
}
