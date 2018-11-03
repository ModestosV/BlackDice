using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour, ICharacter
{
    public CharacterController controller;
    [SerializeField]
    public Texture characterIcon;

    private void Awake()
    {
        controller.Character = this;
        controller.OccupiedTile = GetComponentInParent<HexTile>();
    }

    #region ICharacter implementation

    public ICharacterController Controller { get { return controller; } }

    public void MoveToTile(IHexTile targetTile)
    {
        targetTile.SetChild(GameObject);
        GameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    #endregion

    public override string ToString()
    {
        return string.Format("(Character|{0}: {1})", this.GetHashCode(), controller.ToString());
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}