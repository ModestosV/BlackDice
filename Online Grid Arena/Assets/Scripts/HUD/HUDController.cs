using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class HUDController : IHUDController, IEventSubscriber
{
    public IStatPanelController SelectedStatPanel { private get; set; }
    public IPlayerPanel SelectedPlayerPanel { private get; set; }

    public IStatPanelController TargetStatPanel { private get; set; }
    public IPlayerPanel TargetPlayerPanel { private get; set; }

<<<<<<< HEAD
<<<<<<< HEAD
    public IAbilityPanelController  AbilityPanelController { private get; set; }
=======
    public IAbilityPanel AbilityPanel { get; set; }
>>>>>>> #107 add tests for HUDController
=======
    public IAbilityPanel AbilityPanel { private get; set; }
>>>>>>> #107 removed HUDController tests to rebase on master

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

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();

        if (type == typeof(AbilityUsedEvent))
        {
            var newAbilityClicked = (AbilityUsedEvent)@event;
            AbilityPanel.SetAbilityColorDefaultToAll();
            AbilityPanel.SetAbilityColorUsed(newAbilityClicked.AbilityIndex);
        }
        if (type == typeof(UpdateSelectionModeEvent))
        {
            AbilityPanel.SetAbilityColorDefaultToAll();
        }
    }
}
