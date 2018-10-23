using UnityEngine;

public class Character : MonoBehaviour, ICharacter
{
    public CharacterController controller;

    #region ICharacter implementation

    public IHexTile GetOccupiedTile()
    {
        return GetComponentInParent<HexTile>();
    }

    public ICharacterController Controller { get { return controller; } }

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