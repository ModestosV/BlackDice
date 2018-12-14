using UnityEngine;
using UnityEngine.UI;

public sealed class SurrenderButton : MonoBehaviour {

    public ITurnController TurnController { private get; set; }
    private Button button;
    private IMatchMenu matchMenu;

    void OnValidate()
    {
        button = GetComponent<Button>();
        matchMenu = GetComponentInParent<MatchMenu>();
    }

    void Awake()
    {
        button = GetComponent<Button>();
        matchMenu = GetComponentInParent<MatchMenu>();
        button.onClick.AddListener(Surrender);
	}

    public void Surrender()
    {
        TurnController.Surrender();
        matchMenu.Toggle();
    }
}
