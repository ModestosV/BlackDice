using UnityEngine;
using UnityEngine.UI;

public sealed class MatchMenuButton : MonoBehaviour
{
    private IMatchMenu matchMenu;
    private Button button;

    void OnValidate()
    {
        button = GetComponent<Button>();
        matchMenu = FindObjectOfType<MatchMenu>();
    }

    void Awake()
    {
        button = GetComponent<Button>();
        matchMenu = FindObjectOfType<MatchMenu>();
        button.onClick.AddListener(ToggleMatchMenu);
	}

    private void ToggleMatchMenu()
    {
        matchMenu.Toggle();
    }
}
