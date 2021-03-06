﻿using System.Collections.Generic;
using UnityEngine;

public sealed class HUDController : IHUDController, IEventSubscriber
{
    private readonly IStatPanelController selectedStatPanel;
    private readonly IPlayerPanel selectedPlayerPanel;

    private readonly IStatPanelController targetStatPanel;
    private readonly IPlayerPanel targetPlayerPanel;

    private readonly IAbilityPanelController abilityPanelController;

    private readonly EndTurnButton endTurnButton;
    private static readonly int IsPulsing = Animator.StringToHash("isPulsing");

    public HUDController(IStatPanelController selectedStatPanel, 
        IPlayerPanel selectedPlayerPanel, 
        IStatPanelController targetStatPanel, 
        IPlayerPanel targetPlayerPanel, 
        IAbilityPanelController abilityPanelController, 
        EndTurnButton endTurnButton)
    {
        this.selectedStatPanel = selectedStatPanel;
        this.selectedPlayerPanel = selectedPlayerPanel;
        this.targetStatPanel = targetStatPanel;
        this.targetPlayerPanel = targetPlayerPanel;
        this.abilityPanelController = abilityPanelController;
        this.endTurnButton = endTurnButton;
    }

    public void ClearSelectedHUD()
    {
        selectedStatPanel.DisableStatDisplays();
        selectedPlayerPanel.ClearPlayerName();
        abilityPanelController.Hide();
    }

    public void UpdateSelectedHUD(Dictionary<string, ICharacterStat> characterStats, string playerName, List<IAbility> abilities, List<IEffect> effects)
    {
        selectedStatPanel.EnableStatDisplays();
        selectedStatPanel.CharacterStats = characterStats;
        selectedStatPanel.UpdateStatValues();
        selectedPlayerPanel.SetPlayerName($"Player {playerName}");
        abilityPanelController.UpdateAbilityPanel(abilities, effects);
    }

    public void ClearTargetHUD()
    {
        targetStatPanel.DisableStatDisplays();
        targetPlayerPanel.ClearPlayerName();
    }

    public void UpdateTargetHUD(Dictionary<string, ICharacterStat> characterStats, string playerName)
    {
        targetStatPanel.EnableStatDisplays();
        targetStatPanel.CharacterStats = characterStats;
        targetStatPanel.UpdateStatValues();
        targetPlayerPanel.SetPlayerName($"Player {playerName}");
    }

    public void PulseEndTurnButton()
    {
        endTurnButton.Animator.SetBool(IsPulsing, true);
        Debug.Log("End Turn Button Animation Pulse start.");
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        if (type == typeof(StartNewTurnEvent))
        {
            endTurnButton.Animator.SetBool(IsPulsing, false);
            Debug.Log("End Turn Button Animation Pulse stop.");
        }
    }
}
