using UnityEngine.UI;

public class AbilityToolTip : HideableUI, IAbilityToolTip
{
    public Text AbilityDescription { get; private set; }

    void Start()
    {
        AbilityDescription = transform.GetComponentInChildren<Text>();
        Hide();
    }

    public void ShowToolTip(string Description)
    {
        Show();
        AbilityDescription.text = Description;
    }
}
