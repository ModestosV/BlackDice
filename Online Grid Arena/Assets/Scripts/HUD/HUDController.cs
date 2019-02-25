﻿using System.Collections.Generic;

public sealed class HUDController : IHUDController
{
    public IStatPanelController SelectedStatPanel { private get; set; }
    public IPlayerPanel SelectedPlayerPanel { private get; set; }

    public IStatPanelController TargetStatPanel { private get; set; }
    public IPlayerPanel TargetPlayerPanel { private get; set; }

    public IAbilityPanelController  AbilityPanelController { private get; set; }
    public IAbilityPanel AbilityPanel { private get; set; }

    public EndTurnButton EndTurnButton { private get; set; }

    public void ClearSelectedHUD()
    {
        SelectedStatPanel.DisableStatDisplays();
        SelectedPlayerPanel.ClearPlayerName();
        AbilityPanelController.Hide();
    }

    public void UpdateSelectedHUD(Dictionary<string, ICharacterStat> characterStats, string playerName, List<IAbility> abilities, List<IEffect> effects)
    {
        SelectedStatPanel.EnableStatDisplays();
        SelectedStatPanel.CharacterStats = characterStats;
        SelectedStatPanel.UpdateStatValues();
        SelectedPlayerPanel.SetPlayerName($"Player {playerName}");

        AbilityPanelController.UpdateAbilityPanel(abilities, effects);
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

    public void PulseEndTurnButton()
    {
        EndTurnButton.Animator.SetBool("isPulsing", true);
    }
}
