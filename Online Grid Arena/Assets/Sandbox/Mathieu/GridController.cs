using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GridController {

    public Dictionary<Tuple<int, int, int>, HexTile2> HexTiles;
    private HexTile2[] hexTilesArray;
    public int majorAxisLength;

    public void SetHexTiles(HexTile2[] hexTiles)
    {
            
        for (int i = 0; i < hexTiles.Length; i++)
        {
            hexTilesArray = hexTiles;

            HexTile2 hexTile = hexTiles[i];
            int col = i % majorAxisLength;
            int row = i / majorAxisLength;

            int cubeX = col - row / 2;
            int cubeY = -(col + (row + 1) / 2);
            int cubeZ = row;

            hexTile.Controller.x = cubeX;
            hexTile.Controller.y = cubeY;
            hexTile.Controller.z = cubeZ;

            Dictionary<Tuple<int, int, int>, HexTile2> hexTilesDictionary = new Dictionary<Tuple<int, int, int>, HexTile2>();
            hexTilesDictionary.Add(new Tuple<int, int, int>(cubeX, cubeY, cubeZ), hexTile);

            HexTiles = hexTilesDictionary;

            ArrangeHexTiles();
        }
    }

    private void ArrangeHexTiles()
    {
        for (int i = 0; i < hexTilesArray.Length; i++)
        {
            HexTile2 hexTile = hexTilesArray[i];
            int col = i % majorAxisLength;
            int row = i / majorAxisLength;

            Vector3 meshSize = hexTile.gameObject.GetComponent<Renderer>().bounds.size;

            float rowOffset = row % 2 == 0 ? 0.0f : meshSize.x / 2.0f;

            hexTile.gameObject.transform.position = new Vector3(col * meshSize.x + rowOffset, 0, -(row * 0.75f * meshSize.z));
        }
    }

    public void DeselectAll()
    {
        foreach (HexTile2 tile in hexTilesArray) {
            tile.Controller.Deselect();
        }
    }
}
