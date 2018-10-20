using UnityEngine;

public class Character : MonoBehaviour, IMonoBehaviour, ICharacter
{
    public CharacterController controller;

    public IHexTile GetOccupiedTile()
    {
        return GetComponentInParent<HexTile>();
    }

    public override string ToString()
    {
        return string.Format("(Character|{0}: {1})", this.GetHashCode(), controller.ToString());
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}