using UnityEngine;

public enum Hero
{
    ROCKET_CAT,
    PENGWIN
}

public class Character : MonoBehaviour, ICharacter
{
    public string playerName;
    public Hero hero;

    private CharacterController characterController;
    
    public Texture characterIcon;
    public Color32 borderColor;

    void Awake()
    {
        switch (hero)
        {
            case Hero.ROCKET_CAT:
                characterController = new RocketCatCharacterController();
                break;
            case Hero.PENGWIN:
                // TODO: Pengwin hero
                break;
            default:
                characterController = new DefaultCharacterController();
                break;
        }

        characterController.Character = this;
        characterController.OwnedByPlayer = playerName;
        characterController.CharacterIcon = characterIcon;
        characterController.BorderColor = borderColor;
    }

    void Start()
    {
        GetComponentInParent<HexTile>().Controller.OccupantCharacter = characterController;
        characterController.RefreshStats();
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
        GameObject.transform.localPosition = new Vector3(0, GameObject.transform.localPosition.y, 0);
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
