using System.Collections.Generic;

public interface IAbilityPanel
{
    void Show();
    void Hide();

    void SetAbilityColorUsed(int abilityIndex);
    void SetAbilityColorDefaultToAll();
    void UpdateAbilityIcons(List<IAbility> abilities);
    void UpdateStackIcons(List<IEffect> effects);
    void UpdateCooldownSquares(int abilityButtonIndex, bool isOnCooldown, int cooldownRemaining);
}
