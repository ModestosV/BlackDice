using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class AbilityPanel : HideableUI, IAbilityPanel
{
    public List<Image> abilityButtons;

    void Start()
    {
        foreach (Transform child in transform)
        {
            abilityButtons.Add(child.GetComponentsInChildren<Image>().Last());
        }
    }

    public void UpdateAbilityIcons(List<IAbility> abilities)
    {
        int i = 0;
        foreach (IAbility ability in abilities)
        {
            abilityButtons[i].sprite = ability.AbilityIcon;
            i++;
        }
    }
}
