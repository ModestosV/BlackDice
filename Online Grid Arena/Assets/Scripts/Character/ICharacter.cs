using UnityEngine;

public interface ICharacter {
    IHexTile GetOccupiedTile();
    GameObject GetGameObject();
}
