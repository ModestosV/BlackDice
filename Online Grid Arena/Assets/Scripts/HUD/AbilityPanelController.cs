using System.Collections.Generic;
using UnityEngine;

public class AbilityPanelController : IAbilityPanelController, IEventSubscriber
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
        Debug.Log("Ability Panel updated");
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

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();

        if (type == typeof(AbilityUsedEvent))
        {
            var newAbilityClicked = (AbilityUsedEvent)@event;
            abilityPanel.SetAbilityColorDefaultToAll();
            abilityPanel.SetAbilityColorUsed(newAbilityClicked.AbilityIndex);
        }
        if (type == typeof(UpdateSelectionModeEvent))
        {
            abilityPanel.SetAbilityColorDefaultToAll();
        }
    }
}