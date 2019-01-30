using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

public class AbilityPanel : HideableUI, IAbilityPanel
{
    public List<GameObject> AbilityButtons;
    public List<GameObject> Stacks;

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.tag.Equals("AbilityButton"))
            {
                AbilityButtons.Add(child.gameObject);
            }
            else
            {
                Stacks.Add(child.gameObject);
            }
        }
    }

    public void UpdateAbilityIcons(List<IAbility> abilities)
    {
        int i = 0;
        foreach (IAbility ability in abilities)
        {
            AbilityButtons[i].GetComponentsInChildren<Image>().Last().sprite = ability.AbilityIcon;

            if (ability.GetType().IsSubclassOf(typeof(AbstractActiveAbility)))
            {
                AbilityButtons[i].GetComponent<AbilityButton>().Description = ability.Description;
                AbilityButtons[i].GetComponent<AbilityButton>().Cooldown = ((AbstractActiveAbility)ability).Cooldown;
            }
            else
            {
                AbilityButtons[i].GetComponent<AbilityButton>().Description = ability.Description;
            }

            i++;
        }
    }

    public void UpdateStackIcons(List<IEffect> effects)
    {
        for (int inActive = effects.Count; inActive < Stacks.Count; inActive++)
        {
            Stacks[inActive].GetComponent<EffectStack>().UpdateStacks(0, 0);
        }

        int i = 0;
        foreach (IEffect effect in effects)
        {
            Stacks[i].GetComponentsInChildren<Image>().Last().sprite = effect.EffectIcon;
            Stacks[i].GetComponent<EffectStack>().Description = effect.Description;

            if (effect.Type == EffectType.STACK)
            {
                Stacks[i].GetComponent<EffectStack>().UpdateStacks(((StackModifier)effect).Stacks, ((StackModifier)effect).DurationRemaining);
            }
            i++;
        }
    }

    public void UpdateCooldowns(List<IAbility> abilities)
    {
        AbilityButton[] buttons = GetComponentsInChildren<AbilityButton>();
        for(int i = 0; i < abilities.Count; i++)
        {
            CooldownSquare square = buttons[i].GetComponentInChildren<CooldownSquare>();
            Text buttonText = buttons[i].GetComponentInChildren<Text>();
            if (abilities[i].GetType().IsSubclassOf(typeof(AbstractActiveAbility)))
            {
                IActiveAbility ability = (IActiveAbility) abilities[i];

                if ((ability != null) && ability.IsOnCooldown())
                {
                    square.UpdateSquare(true, ability.CooldownRemaining, buttonText);
                }
                else
                {
                    square.UpdateSquare(false, ability.CooldownRemaining, buttonText);
                }
            }
            else
            {
                square.UpdateSquare(false, 0, buttonText);
                continue;
            }
        }
    }
}
