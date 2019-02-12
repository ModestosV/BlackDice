using UnityEngine;
using UnityEngine.UI;


public sealed class EndTurnButton : BlackDiceMonoBehaviour
{
    private Button Button { get; set; }

    void Awake()
    {
        Debug.Log(ToString() + " Awake() begin");

        Button = GetComponent<Button>();

        Debug.Log(ToString() + " Begin() begin");
    }

    void Start()
    {
        Debug.Log(ToString() + " Start() begin");

        Button.onClick.AddListener(EndTurn);

        Debug.Log(ToString() + " Start() end");
    }

    public void EndTurn()
    {
        EventBus.Publish(new DeselectSelectedTileEvent());
        EventBus.Publish(new StartNewTurnEvent());
    }
}
