using UnityEngine;
using UnityEngine.UI;

public sealed class EndTurnButton : BlackDiceMonoBehaviour
{
    private Button button;
    public Animator Animator { get; private set; }

    void Awake()
    {
        Debug.Log(ToString() + " Awake() begin");

        button = GetComponent<Button>();
        Animator = GetComponent<Animator>();

        Debug.Log(ToString() + " Begin() begin");
    }

    void Start()
    {
        Debug.Log(ToString() + " Start() begin");

        button.onClick.AddListener(EndTurn);

        Debug.Log(ToString() + " Start() end");
    }

    public void EndTurn()
    {
        EventBus.Publish(new DeselectSelectedTileEvent());
        EventBus.Publish(new StartNewTurnEvent());
    }
}
