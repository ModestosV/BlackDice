using System.Collections.Generic;
using System;

public class GridController : IGridController
{
    public int GridWidth { protected get; set; }

    private Dictionary<Tuple<int, int, int>, IHexTileController> GridMap { get; set; }
    
    public void GenerateGridMap(List<IHexTileController> hexTiles)
    {
        GridMap = new Dictionary<Tuple<int, int, int>, IHexTileController>();

        for (int i = 0; i < hexTiles.Count; i++)
        {
            IHexTileController hexTile = hexTiles[i];
            int col = i % GridWidth;
            int row = i / GridWidth;

            int cubeX = col - row / 2;
            int cubeY = -(col + (row + 1) / 2);
            int cubeZ = row;

            Tuple<int, int, int> coordinates = new Tuple<int, int, int>(cubeX, cubeY, cubeZ);

            hexTile.Coordinates = coordinates;
            hexTile.GridController = this;

            GridMap.Add(coordinates, hexTile);
        }
    }

    public IHexTileController GetTile(Tuple<int, int, int> coordinates)
    {
        IHexTileController tile;
        GridMap.TryGetValue(coordinates, out tile);
        return tile;
    }
}