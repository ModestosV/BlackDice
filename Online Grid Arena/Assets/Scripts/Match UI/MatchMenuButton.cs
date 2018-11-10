using UnityEngine;
using UnityEngine.UI;

public class MatchMenuButton : MonoBehaviour
{
    public IMatchMenu MatchMenu { protected get; set; }
    private Button Button { get; set; }

    void OnValidate()
    {
        Button = GetComponent<Button>();
        MatchMenu = FindObjectOfType<MatchMenu>();
    }

    void Awake() {
        Button = GetComponent<Button>();
        MatchMenu = FindObjectOfType<MatchMenu>();
        Button.onClick.AddListener(ToggleMatchMenu);
	}
	
	public void ToggleMatchMenu()
    {
        MatchMenu.Toggle();
    }
}
