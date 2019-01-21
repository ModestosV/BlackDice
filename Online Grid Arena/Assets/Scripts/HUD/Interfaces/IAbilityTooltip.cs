using UnityEngine.UI;

public interface IAbilityTooltip
{
    Text AbilityDescription { get; }

    void ShowTooltip(string Description);
}
