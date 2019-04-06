using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class AbilityPanel : HideableUI, IAbilityPanel
{
    public List<GameObject> AbilityButtons;
    public List<GameObject> Stacks;

    [SerializeField] private Sprite passiveButtonSprite;
    [SerializeField] private Sprite activeButtonSprite;

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
        AbilityButtons[abilityIndex].GetComponentsInChildren<Image>()[0].color = Color.grey;
    }

    public void SetAbilityColorDefaultToAll()
    {
        for (int i = 0; i < AbilityButtons.Count; i++)
        {
            AbilityButtons[i].GetComponentsInChildren<Image>()[0].color = new Color(0.95f, 0.95f, 0.95f, 1f);
        }
    }

    public void UpdateAbilityIcons(List<IAbility> abilities)
    {
        int i = 0;
        foreach (IAbility ability in abilities)
        {
            var abilityImage = AbilityButtons[i].GetComponentsInChildren<Image>()[1];
            abilityImage.sprite = ability.AbilityIcon;
            abilityImage.color = new Color(1, 1, 1, 1);

            if (ability.GetType().IsSubclassOf(typeof(AbstractActiveAbility)))
            {
                AbilityButtons[i].GetComponent<AbilityButton>().Cooldown = ((AbstractActiveAbility)ability).Cooldown;
                AbilityButtons[i].GetComponentsInChildren<Image>()[0].sprite = activeButtonSprite;
                AbilityButtons[i].GetComponentsInChildren<Image>()[2].sprite = activeButtonSprite;
            }
            else
            {
                AbilityButtons[i].GetComponent<AbilityButton>().Cooldown = 0;
                AbilityButtons[i].GetComponentsInChildren<Image>()[0].sprite = passiveButtonSprite;
                AbilityButtons[i].GetComponentsInChildren<Image>()[2].sprite = passiveButtonSprite;
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
