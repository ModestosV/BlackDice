using UnityEngine.UI;

public class AbilityToolTip : HideableUI, IAbilityToolTip
{
    public Text abilityDescription;

    void Start()
    {
        abilityDescription = transform.GetComponentInChildren<Text>();
        Hide();
    }

    public void ShowToolTip(IAbility ability)
    {
        Show();
        abilityDescription.text = ability.Description;
    }
}
