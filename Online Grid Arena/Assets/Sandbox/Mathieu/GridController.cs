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
        HexTiles = new Dictionary<Tuple<int, int, int>, HexTile2>();

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

            HexTiles.Add(new Tuple<int, int, int>(cubeX, cubeY, cubeZ), hexTile);

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

    public HexTile2 getNorthEast(HexTile2 tile)
    {
        HexTile2 neighborNorthEast;
        int x = tile.Controller.x + 1;
        int y = tile.Controller.y;
        int z = tile.Controller.z - 1;
        HexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborNorthEast);

        Debug.Log($"NE: {neighborNorthEast}");
        return neighborNorthEast;
    }

    public HexTile2 getEast(HexTile2 tile)
    {
        HexTile2 neighborEast;
        int x = tile.Controller.x + 1;
        int y = tile.Controller.y - 1;
        int z = tile.Controller.z;
        HexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborEast);

        Debug.Log($"E: {neighborEast}");
        return neighborEast;
    }

    public HexTile2 getSouthEast(HexTile2 tile)
    {
        HexTile2 neighborSouthEast;
        int x = tile.Controller.x;
        int y = tile.Controller.y - 1;
        int z = tile.Controller.z + 1;
        HexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborSouthEast);

        Debug.Log($"SE: {neighborSouthEast}");
        return neighborSouthEast;
    }

    public HexTile2 getSouthWest(HexTile2 tile)
    {
        HexTile2 neighborSouthWest;
        int x = tile.Controller.x - 1;
        int y = tile.Controller.y;
        int z = tile.Controller.z + 1;
        HexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborSouthWest);

        Debug.Log($"SW: {neighborSouthWest}");
        return neighborSouthWest;
    }

    public HexTile2 getWest(HexTile2 tile)
    {
        HexTile2 neighborWest;
        int x = tile.Controller.x - 1;
        int y = tile.Controller.y + 1;
        int z = tile.Controller.z;
        HexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborWest);

        Debug.Log($"W: {neighborWest}");
        return neighborWest;
    }

    public HexTile2 getNorthWest(HexTile2 tile)
    {
        HexTile2 neighborNorthWest;
        int x = tile.Controller.x;
        int y = tile.Controller.y + 1;
        int z = tile.Controller.z - 1;
        HexTiles.TryGetValue(new Tuple<int, int, int>(x, y, z), out neighborNorthWest);

        Debug.Log($"NW: {neighborNorthWest}");
        return neighborNorthWest;
    }

    public List<HexTile2> getNeighbors(HexTile2 tile)
    {
        List<HexTile2> neighbors = new List<HexTile2>
        {
            getNorthEast(tile),
            getEast(tile),
            getSouthEast(tile),
            getSouthWest(tile),
            getWest(tile),
            getNorthWest(tile)
        };

        neighbors.RemoveAll(item => item == null);

        return neighbors;
    }

}
