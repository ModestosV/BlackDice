using UnityEngine;
using UnityEngine.UI;

public class MatchMenuButton : MonoBehaviour
{
    public Button matchMenuButton;
    public IMatchMenu MatchMenu { protected get; set; }

    private void OnValidate()
    {
        matchMenuButton = GetComponent<Button>();
        MatchMenu = FindObjectOfType<MatchMenu>();
    }

    void Start () {
        matchMenuButton = GetComponent<Button>();
        MatchMenu = FindObjectOfType<MatchMenu>();
        matchMenuButton.onClick.AddListener(ToggleMatchMenu);
	}
	
	public void ToggleMatchMenu()
    {
        MatchMenu.Toggle();
    }
}
