using UnityEngine;

public class Character : MonoBehaviour, ICharacter
{
    public string playerName;

    private CharacterController characterController;
    
    public Texture characterIcon;
    public Color32 borderColor;

    void Awake()
    {
        characterController = new DefaultCharacterController
        {
            Character = this,
            OwnedByPlayer = playerName,
            CharacterIcon = characterIcon,
            BorderColor = borderColor,
            HealthBar = GetComponentInChildren<HealthBar>()
        };
    }

    void Start()
    {
        GetComponentInParent<HexTile>().Controller.OccupantCharacter = characterController;
        characterController.RefreshStats();
        characterController.UpdateHealthBar();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    #region ICharacter implementation

    public ICharacterController Controller { get { return characterController; } }

    public void MoveToTile(IHexTile targetTile)
    {
        gameObject.transform.parent = targetTile.GameObject.transform;
        GameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    #endregion

    public override string ToString()
    {
        return string.Format("(Character|{0}: {1})", this.GetHashCode(), characterController.ToString());
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
