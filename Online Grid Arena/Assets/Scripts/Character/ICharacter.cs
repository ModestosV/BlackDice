using UnityEngine;

public interface ICharacter : IMonoBehaviour
{
    IHexTile GetOccupiedTile();
    GameObject GetGameObject();
}
