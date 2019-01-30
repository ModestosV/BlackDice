using System;
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
        var values = new List<Tuple<bool, int>>();

        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].GetType().IsSubclassOf(typeof(AbstractActiveAbility)))
            {
                IActiveAbility ability = (IActiveAbility)abilities[i];

                if ((ability != null) && ability.IsOnCooldown())
                {
                    values.Add(Tuple.Create(true, ability.CooldownRemaining));
                }
                else
                {
                    values.Add(Tuple.Create(false, ability.CooldownRemaining));
                }
            }
            else
            {
                values.Add(Tuple.Create(false, 0));
                continue;
            }
        }
        abilityPanel.UpdateCooldownSquares(values);
    }

    public void UpdateAbilityIcons(List<IAbility> abilities, List<IEffect> effects)
    {
        abilityPanel.UpdateAbilityIcons(abilities);
        abilityPanel.UpdateStackIcons(effects);
    }
}