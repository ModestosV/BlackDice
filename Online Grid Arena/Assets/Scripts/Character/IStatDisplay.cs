using UnityEngine.UI;

public interface IStatDisplay : IMonoBehaviour
{
    IStatDisplayController Controller { get; }
    Text NameText { get; set; }
    Text ValueText { get; set; }

    void SetNameText(string nameText);
    void SetValueText(string valueText);
}
