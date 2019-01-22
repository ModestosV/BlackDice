using System.Collections.Generic;

public interface IAbilityPanel
{
    void Show();
    void Hide();

    void UpdateAbilityIcons(List<IAbility> abilities);
    void UpdateStackIcons(List<IEffect> effects);
}
