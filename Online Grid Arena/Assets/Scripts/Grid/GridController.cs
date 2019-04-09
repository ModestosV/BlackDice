using System.Collections.Generic;
using UnityEngine;

public sealed class GridController : IGridController
{
    private int minimumX, maximumX, minimumY, maximumY, minimumZ, maximumZ = 0;
    public int GridWidth { private get; set; }

    private Dictionary<(int, int, int), IHexTileController> GridMap { get; set; }
    
    public void GenerateGridMap(List<IHexTileController> hexTiles)
    {
        GridMap = new Dictionary<(int, int, int), IHexTileController>();
        for (int i = 0; i < hexTiles.Count; i++)
        {
            IHexTileController hexTile = hexTiles[i];
            int col = i % GridWidth;
            int row = i / GridWidth;

            int cubeX = col - row / 2;
            int cubeY = -(col + (row + 1) / 2);
            int cubeZ = row;
            (int, int, int) coordinates = (cubeX, cubeY, cubeZ);
            VerifyBounds(cubeX, cubeY, cubeZ);
            hexTile.Coordinates = coordinates;
            hexTile.GridController = this;

            GridMap.Add(coordinates, hexTile);
        }
    }

    public IHexTileController GetTile((int, int, int) coordinates)
    {
        IHexTileController tile;
        GridMap.TryGetValue(coordinates, out tile);
        return tile;
    }

    private void VerifyBounds(int X, int Y, int Z)
    {
        if (X < minimumX) minimumX = X;
        if (X > maximumX) maximumX = X;
        if (Y < minimumY) minimumY = Y;
        if (Y > maximumY) maximumY = Y;
        if (Z < minimumZ) minimumZ = Z;
        if (Z > maximumZ) maximumZ = Z;
    }

    public IHexTileController GetRandomTile()
    {
        Debug.Log("grid getting random");
        IHexTileController tile;
        System.Random randomizer = new System.Random();
        (int, int, int) XYZ = GetRandomCoordinates();
        if (XYZ.Item3 > maximumZ || XYZ.Item3 < minimumZ)
        {
            tile = GetTile((0, 0, 0));
            return tile;
        }
        tile = GetTile(XYZ);
        return tile;
    }

    private (int, int, int) GetRandomCoordinates()
    {
        System.Random randomizer = new System.Random();
        int x = randomizer.Next(minimumX, maximumX + 1);
        int y = randomizer.Next(minimumY, maximumY + 1);
        int z = -x - y;
        (int, int, int) xyz = (x, y, z);
        Debug.Log(x + " " + y + " " + z);
        return xyz;
    }
}
