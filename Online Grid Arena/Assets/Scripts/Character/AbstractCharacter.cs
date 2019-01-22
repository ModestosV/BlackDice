using UnityEngine;

public abstract class AbstractCharacter : MonoBehaviour, ICharacter
{
    [SerializeField] protected string playerName;

    protected CharacterController characterController;

    [SerializeField] protected Texture characterIcon;
    [SerializeField] protected Color32 borderColor;

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public ICharacterController Controller { get { return characterController; } }

    public void MoveToTile(IHexTile targetTile)
    {
        gameObject.transform.parent = targetTile.GameObject.transform;
        GameObject.transform.localPosition = new Vector3(0, GameObject.transform.localPosition.y, 0);
    }

    public override string ToString()
    {
        return string.Format("(Character|{0}: {1})", this.GetHashCode(), characterController.ToString());
    }

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    void Start()
    {
        GetComponentInParent<HexTile>().Controller.OccupantCharacter = characterController;
        characterController.RefreshStats();
        characterController.UpdateHealthBar();
        characterController.ActiveCircle.enabled = false;
    }
}