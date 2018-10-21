using System;
using UnityEngine;

public interface IHexTile : IMonoBehaviour
{
    HexTileController Controller { get; }
    Tuple<int, int, int> Coordinates();
    void SetChild(GameObject childObject);
}
