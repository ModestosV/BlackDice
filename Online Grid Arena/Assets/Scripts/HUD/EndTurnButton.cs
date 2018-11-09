using UnityEngine;
using UnityEngine.UI;


public class EndTurnButton : MonoBehaviour
{
    public Button endTurnButton;
    public ITurnController TurnController {protected get; set; }

    void OnValidate()
    {
        endTurnButton = GetComponent<Button>();
    }

    void Start()
    {
        endTurnButton = GetComponent<Button>();
        endTurnButton.onClick.AddListener(EndTurn);
    }

    public void EndTurn()
    {
        TurnController.StartNextTurn();
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
