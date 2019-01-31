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

    private void UpdateAbilityIcons(List<IAbility> abilities, List<IEffect> effects)
    {
        abilityPanel.UpdateAbilityIcons(abilities);
        abilityPanel.UpdateStackIcons(effects);
    }

    private void UpdateAbilityCooldowns(List<IAbility> abilities)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].GetType().IsSubclassOf(typeof(AbstractActiveAbility)))
            {
                IActiveAbility ability = (IActiveAbility)abilities[i];

                if ((ability != null) && ability.IsOnCooldown())
                {
                    abilityPanel.UpdateCooldownSquares(i, true, ability.CooldownRemaining);
                }
                else
                {
                    abilityPanel.UpdateCooldownSquares(i, false, ability.CooldownRemaining);
                }
            }
            else
            {
                abilityPanel.UpdateCooldownSquares(i, false, 0);
            }
        }
    }
}