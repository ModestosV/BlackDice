using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour, IPlayerPanel
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

    #region IPlayerPanel implementation

    public void SetPlayerName(string playerName)
    {
        this.playerName.text = playerName;
    }

    public void ClearPlayerName()
    {
        this.playerName.text = "";
    }

    #endregion

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
