using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class AbilityPanel : HideableUI, IAbilityPanel
{
    public List<GameObject> abilityButtons;
    public List<GameObject> stacks;

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "AbilityButton")
            {
                abilityButtons.Add(child.gameObject);
            }
            else
            {
                stacks.Add(child.gameObject);
            }
        }
    }

    public void UpdateAbilityIcons(List<IAbility> abilities)
    {
        int i = 0;
        foreach (IAbility ability in abilities)
        {
            abilityButtons[i].GetComponentsInChildren<Image>().Last().sprite = ability.AbilityIcon;
            abilityButtons[i].GetComponent<AbilityButton>().Description = ability.Description;
            i++;
        }
    }

    public void UpdateStackIcons(List<IEffect> effects)
    {
        for (int inActive = effects.Count; inActive < stacks.Count; inActive++)
        {
            stacks[inActive].GetComponent<EffectStack>().UpdateStacks(0, 0);
        }

        int i = 0;
        foreach (IEffect effect in effects)
        {
            stacks[i].GetComponentsInChildren<Image>().Last().sprite = effect.EffectIcon;
            stacks[i].GetComponent<EffectStack>().Description = effect.Description;

            if (effect.Type == EffectType.STACK)
            {
                stacks[i].GetComponent<EffectStack>().UpdateStacks(((StackModifier)effect).Stacks, ((StackModifier)effect).durationRemaining);
            }
            i++;
        }
    }
}
