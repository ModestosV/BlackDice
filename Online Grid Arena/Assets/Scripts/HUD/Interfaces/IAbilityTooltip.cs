using UnityEngine.UI;

public interface IAbilityTooltip
{
    Text AbilityDescription { get; }

    void ShowTooltip(string description);
    void ShowTooltip(string description, int cooldown);
}
