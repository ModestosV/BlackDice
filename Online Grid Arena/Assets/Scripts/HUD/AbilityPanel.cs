﻿using UnityEngine;
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
        int i = 0;
        int j = 0;
        IActiveAbility[] activeAbilities = new IActiveAbility[abilities.Count];

        foreach(IAbility a in abilities)
        {
            try
            {
                activeAbilities[j] = (IActiveAbility)a;
            }
            catch (InvalidCastException)
            {
                continue;
            }
            j++;
        }

        AbilityButton[] buttons = GetComponentsInChildren<AbilityButton>();
        foreach(IActiveAbility abilityObj in activeAbilities)
        {
            try
            {
                AbstractActiveAbility ability = (AbstractActiveAbility)abilityObj;
                CooldownSquare square = buttons[i].GetComponentInChildren<CooldownSquare>();

                if ((ability != null) && ability.IsOnCooldown())
                {
                    square.UpdateSquare(ability.Cooldown, ability.cooldownRemaining);
                }
                else
                {
                    square.Hide();
                }
            }
            catch(InvalidCastException)
            {
                continue;
            }
            i++;
        }
    }
}
