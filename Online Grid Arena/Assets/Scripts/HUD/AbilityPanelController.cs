using UnityEngine;
using System.Collections.Generic;
using System;

public class AbilityPanelController : IAbilityPanelController
{
    IAbilityPanel AbilityPanel { get; set; }

    public AbilityPanelController(IAbilityPanel abilityPanel)
    {
        AbilityPanel = abilityPanel;
    }

    public void Hide()
    {
        AbilityPanel.Hide();
    }

    public void UpdateAbilityPanel(List<IAbility> abilities, List<IEffect> effects)
    {
        this.UpdateAbilityIcons(abilities, effects);
        this.UpdateAbilityCooldowns(abilities);
        AbilityPanel.Show();
    }

    public void UpdateAbilityCooldowns(List<IAbility> abilities)
    {
        AbilityPanel.UpdateCooldowns(abilities);
    }

    public void UpdateAbilityIcons(List<IAbility> abilities, List<IEffect> effects)
    {
        AbilityPanel.UpdateAbilityIcons(abilities);
        AbilityPanel.UpdateStackIcons(effects);
    }

    public IAbilityPanel GetAbilityPanel()
    {
        return this.AbilityPanel;
    }
}