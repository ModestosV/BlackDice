using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour, IPlayerPanel
{
    private Text PlayerName { get; set; }

    void OnValidate()
    {
        PlayerName = gameObject.GetComponentInChildren<Text>();
    }

    void Awake()
    {
        PlayerName = gameObject.GetComponentInChildren<Text>();
    }

    #region IPlayerPanel implementation

    public void SetPlayerName(string playerName)
    {
        this.PlayerName.text = playerName;
    }

    public void ClearPlayerName()
    {
        this.PlayerName.text = "";
    }

    #endregion

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
