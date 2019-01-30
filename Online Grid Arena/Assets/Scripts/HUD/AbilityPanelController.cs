using UnityEngine;
using System.Collections.Generic;
using System;

public class AbilityPanelController : IAbilityPanelController
{
    [SerializeField]
    public IAbilityPanel AbilityPanel { get; set; }

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
        AbilityPanel.UpdateAbilityIcons(abilities);
        AbilityPanel.UpdateStackIcons(effects);
        AbilityPanel.UpdateCooldowns(abilities);
        AbilityPanel.Show();
    }
}