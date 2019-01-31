using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
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
                AbilityButtons[i].GetComponent<AbilityButton>().Cooldown = ((AbstractActiveAbility)ability).CooldownRemaining;
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

    public void UpdateCooldownSquares(int abilityButtonIndex, bool isOnCooldown, int cooldownRemaining)
    {
        var letters = new List<string>(){ "Q", "W", "E", "R" };
        
        CooldownSquare square = AbilityButtons[abilityButtonIndex].GetComponentInChildren<CooldownSquare>();
        Text buttonText = AbilityButtons[abilityButtonIndex].GetComponentInChildren<Text>();

        if(isOnCooldown)
        {
            buttonText.text = cooldownRemaining.ToString();
            buttonText.color = Color.white;
            buttonText.alignment = TextAnchor.MiddleCenter;
            buttonText.fontSize = 30;
            buttonText.fontStyle = FontStyle.Bold;
            square.Show();
        }
        else
        {
            buttonText.color = Color.black;
            buttonText.alignment = TextAnchor.LowerLeft;
            buttonText.text = letters[abilityButtonIndex];
            buttonText.fontStyle = FontStyle.Normal;
            buttonText.fontSize = 14;
            square.Hide();
        }
        
    }
}
