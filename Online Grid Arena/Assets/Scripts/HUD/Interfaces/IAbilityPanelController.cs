using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public interface IAbilityPanelController
{
    void UpdateAbilityPanel(List<IAbility> abilities, List<IEffect> effects);
    void Hide();
    void UpdateAbilityCooldowns(List<IAbility> abilities);
    void UpdateAbilityIcons(List<IAbility> abilities, List<IEffect> effects);
}