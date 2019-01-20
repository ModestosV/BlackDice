using UnityEngine.UI;

public interface IAbilityTooltip
{
    Text AbilityDescription { get; }

    void ShowToolTip(string Description);
}
