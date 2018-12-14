using UnityEngine;
using UnityEngine.UI;

public sealed class MatchMenuButton : MonoBehaviour
{
    public IMatchMenu MatchMenu { private get; set; }
    private Button button;

    void OnValidate()
    {
        button = GetComponent<Button>();
        MatchMenu = FindObjectOfType<MatchMenu>();
    }

    void Awake()
    {
        button = GetComponent<Button>();
        MatchMenu = FindObjectOfType<MatchMenu>();
        button.onClick.AddListener(ToggleMatchMenu);
	}
	
	public void ToggleMatchMenu()
    {
        MatchMenu.Toggle();
    }
}
