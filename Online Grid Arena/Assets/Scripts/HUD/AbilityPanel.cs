using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

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

    public void SetAbilityColorUsed(int abilityIndex)
    {
        AbilityButtons[abilityIndex].GetComponent<Image>().color = Color.grey;
    }

    public void SetAbilityColorDefaultToAll()
    {
        for (int i = 0; i < AbilityButtons.Count; i++)
        {
            AbilityButtons[i].GetComponent<Image>().color = new Color(0.7490196f, 0.7803922f, 0.8000001f, 1f);
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
                AbilityButtons[i].GetComponent<AbilityButton>().Cooldown = ((AbstractActiveAbility)ability).Cooldown;
            }
            else
            {
                AbilityButtons[i].GetComponent<AbilityButton>().Cooldown = 0;
            }

            AbilityButtons[i].GetComponent<AbilityButton>().Description = ability.Description;
            AbilityButtons[i].GetComponent<AbilityButton>().Index = i;

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
