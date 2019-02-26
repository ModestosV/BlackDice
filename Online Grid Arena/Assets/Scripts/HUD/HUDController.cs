using System.Collections.Generic;
using UnityEngine;

public sealed class HUDController : IHUDController, IEventSubscriber
{
    private IStatPanelController SelectedStatPanel;
    private IPlayerPanel SelectedPlayerPanel;

    private IStatPanelController TargetStatPanel;
    private IPlayerPanel TargetPlayerPanel;

    private IAbilityPanelController AbilityPanelController;

    private EndTurnButton EndTurnButton;

    public HUDController(IStatPanelController selectedStatPanel, IPlayerPanel selectedPlayerPanel, IStatPanelController targetStatPanel, IPlayerPanel targetPlayerPanel, IAbilityPanelController abilityPanelController, EndTurnButton endTurnButton)
    {
        SelectedStatPanel = selectedStatPanel;
        SelectedPlayerPanel = selectedPlayerPanel;
        TargetStatPanel = targetStatPanel;
        TargetPlayerPanel = targetPlayerPanel;
        AbilityPanelController = abilityPanelController;
        EndTurnButton = endTurnButton;
    }

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
        Debug.Log("End Turn Button Animation Pulse start.");
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        if (type == typeof(StartNewTurnEvent))
        {
            EndTurnButton.Animator.SetBool("isPulsing", false);
            Debug.Log("End Turn Button Animation Pulse stop.");
        }
    }
}
