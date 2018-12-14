using UnityEngine;
using UnityEngine.UI;


public sealed class EndTurnButton : MonoBehaviour
{
    public ITurnController TurnController { private get; set; }

    private Button Button { get; set; }

    void OnValidate()
    {
        Button = GetComponent<Button>();
    }

    void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(EndTurn);
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
