using UnityEngine;

public class Character : MonoBehaviour, ICharacter
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
}