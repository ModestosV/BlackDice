using UnityEngine;

public class Character : MonoBehaviour, ICharacter
{
    public CharacterController controller;

    private void Awake()
    {
        controller.Character = this;
    }

    #region ICharacter implementation

    public IHexTile GetOccupiedTile()
    {
        return GetComponentInParent<HexTile>();
    }

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