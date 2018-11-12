using UnityEngine;
using UnityEngine.UI;

public class SurrenderButton : MonoBehaviour {

    private Button Button { get; set; }
    private IMatchMenu MatchMenu { get; set; }
    public ITurnController TurnController { protected get; set; }

    void OnValidate()
    {
        Button = GetComponent<Button>();
        MatchMenu = GetComponentInParent<MatchMenu>();
    }

    void Awake()
    {
        Button = GetComponent<Button>();
        MatchMenu = GetComponentInParent<MatchMenu>();
        Button.onClick.AddListener(Surrender);
	}

    public void Surrender()
    {
        TurnController.Surrender();
        MatchMenu.Toggle();
    }
}
