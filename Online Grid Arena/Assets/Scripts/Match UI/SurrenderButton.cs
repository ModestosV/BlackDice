using UnityEngine;
using UnityEngine.UI;

public class SurrenderButton : MonoBehaviour {

    public Button surrenderButton;
    public IMatchMenu matchMenu;
    public ITurnController TurnController { protected get; set; }

    void OnValidate()
    {
        surrenderButton = GetComponent<Button>();
        matchMenu = GetComponentInParent<MatchMenu>();
    }

    void Start ()
    {
        surrenderButton = GetComponent<Button>();
        matchMenu = GetComponentInParent<MatchMenu>();
        surrenderButton.onClick.AddListener(Surrender);
	}

    public void Surrender()
    {
        TurnController.Surrender();
        matchMenu.Toggle();
    }
}
