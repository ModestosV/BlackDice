using UnityEngine;
using UnityEngine.UI;

public class AbilityTooltip : HideableUI, IAbilityTooltip
{
    public Text AbilityDescription { get; private set; }

    void Start()
    {
        AbilityDescription = transform.GetComponentInChildren<Text>();
        Hide();
    }

    public void ShowTooltip(string description)
    {
        Debug.Log("In Show Tooltip 1 params");
        Show();
        AbilityDescription.text = description;
    }

    public void ShowTooltip(string description, int cooldown)
    {
        Debug.Log("In Show Tooltip 2 params w/ cooldown" + cooldown);
        Show();
        AbilityDescription.text = description;

        AbilityDescription.text += "\n" + cooldown.ToString() + " turn cooldown";
    }
}