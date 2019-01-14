using System;
using System.Collections.Generic;
using UnityEngine;

public interface IHexTileController
{
    Tuple<int, int, int> Coordinates { get; set; }

    int X { get; }
    int Y { get; }
    int Z { get; }

    bool IsEnabled { get; set; }
    bool IsSelected { set; }
    
    IGridSelectionController GridSelectionController { set; }
    IGridController GridController { set; }
    IHexTile HexTile { get; set; }
    ICharacterController OccupantCharacter { get; set; }

    void Select();
    void Deselect();
    void SetActive();
    void SetInactive();
    void Hover();
    void Blur();
    void HoverError();
    void Highlight();
    void Dehighlight();

    void ClearOccupant();
    bool IsOccupied();

    IHexTileController GetNorthEastNeighbor();
    IHexTileController GetEastNeighbor();
    IHexTileController GetSouthEastNeighbor();
    IHexTileController GetSouthWestNeighbor();
    IHexTileController GetWestNeighbor();
    IHexTileController GetNorthWestNeighbor();
    List<IHexTileController> GetNeighbors();
    List<IHexTileController> GetPath(IHexTileController startTile);

    void PlayAbilityAnimation(GameObject abilityAnimationPrefab);

    void Damage(float damage);
    void Heal(float healing);
}
