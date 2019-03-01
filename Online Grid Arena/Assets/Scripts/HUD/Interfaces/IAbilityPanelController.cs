using System.Collections.Generic;

public interface IAbilityPanelController
{
    void UpdateAbilityPanel(List<IAbility> abilities, List<IEffect> effects);
    void Hide();
}