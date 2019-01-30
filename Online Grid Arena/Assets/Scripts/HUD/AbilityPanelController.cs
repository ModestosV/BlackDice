using System.Collections.Generic;

public class AbilityPanelController : IAbilityPanelController
{
    private IAbilityPanel abilityPanel;

    public AbilityPanelController(IAbilityPanel abilityPanel)
    {
        this.abilityPanel = abilityPanel;
    }

    public void Hide()
    {
        abilityPanel.Hide();
    }

    public void UpdateAbilityPanel(List<IAbility> abilities, List<IEffect> effects)
    {
        this.UpdateAbilityIcons(abilities, effects);
        this.UpdateAbilityCooldowns(abilities);
        abilityPanel.Show();
    }

    public void UpdateAbilityCooldowns(List<IAbility> abilities)
    {
        abilityPanel.UpdateCooldowns(abilities);
    }

    public void UpdateAbilityIcons(List<IAbility> abilities, List<IEffect> effects)
    {
        abilityPanel.UpdateAbilityIcons(abilities);
        abilityPanel.UpdateStackIcons(effects);
    }
}