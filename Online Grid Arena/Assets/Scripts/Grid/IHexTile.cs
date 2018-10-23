using System;
using UnityEngine;

public interface IHexTile : IMonoBehaviour
{
    IHexTileController Controller { get; }
    Tuple<int, int, int> Coordinates();
    void SetChild(GameObject childObject);
}
