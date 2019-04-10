using UnityEngine.UI;

public class PlayerPanel : BlackDiceMonoBehaviour, IPlayerPanel
{
    private Text playerName;

    void OnValidate()
    {
        playerName = gameObject.GetComponentInChildren<Text>();
    }

    void Awake()
    {
        playerName = gameObject.GetComponentInChildren<Text>();
    }
    public void SetPlayerName(string playerName)
    {
        this.playerName.text = playerName;
    }

    public void ClearPlayerName()
    {
        this.playerName.text = "";
    }
}
