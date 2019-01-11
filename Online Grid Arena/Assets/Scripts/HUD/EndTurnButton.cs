using UnityEngine;
using UnityEngine.UI;


public sealed class EndTurnButton : MonoBehaviour
{
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
        EventBus.Publish(new DeselectSelectedTileEvent());
        EventBus.Publish(new StartNewTurnEvent());
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
