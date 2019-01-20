using UnityEngine.UI;

public interface IAbilityToolTip
{
    Text AbilityDescription { get; }

    void ShowToolTip(string Description);
}
