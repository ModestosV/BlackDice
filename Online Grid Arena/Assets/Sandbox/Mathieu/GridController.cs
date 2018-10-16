using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridController {

    [SerializeField]
    public List<HexTile> HexTiles;

    public GridController(List<HexTile> hexTile)
    {
        HexTiles = new List<HexTile>();
    }
}
