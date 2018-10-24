using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour, IMonoBehaviour, IPlayerPanel {

    public Text playerName;

    private void OnValidate()
    {
        playerName = gameObject.GetComponentInChildren<Text>();
    }

    private void Awake()
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
