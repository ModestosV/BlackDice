using UnityEngine.UI;

public class AbilityTooltip : HideableUI, IAbilityTooltip
{
    public Text AbilityDescription { get; private set; }

    void Start()
    {
        AbilityDescription = transform.GetComponentInChildren<Text>();
        Hide();
    }

    public void ShowToolTip(string description)
    {
        Show();
        AbilityDescription.text = description;
    }

    public void ShowToolTip(string description, int cooldown)
    {
        Show();
        AbilityDescription.text = description;

        AbilityDescription.text += "\n" + cooldown.ToString() + " turn cooldown";
    }
}
