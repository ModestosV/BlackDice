using System;
using UnityEngine;

public interface IHexTile
{
    HexTileController Controller();
    Tuple<int, int, int> Coordinates();
    void SetChild(GameObject childObject);
}
