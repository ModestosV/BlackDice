using System.Collections.Generic;
using System;

[Serializable]
public class GridController
{
    public int gridWidth;

    public IGridTraversalController GridTraversalController { get; set; }
    public IGridSelectionController GridSelectionController { get; set; }

    public void Init(IGridSelectionController selectionController, IGridTraversalController traversalController)
    {
        GridSelectionController = selectionController;
        GridSelectionController.Init();
        GridTraversalController = traversalController;
        GridTraversalController.Init();
        if (gridWidth == 0) // Possible bug. TODO: Investigate why this is sometimes 0.
            gridWidth = 19;
    }

    public void SetHexTiles(IHexTile[] hexTiles)
    {
        Dictionary<Tuple<int, int, int>, IHexTile> hexTilesMap = new Dictionary<Tuple<int, int, int>, IHexTile>();

        for (int i = 0; i < hexTiles.Length; i++)
        {
            IHexTile hexTile = hexTiles[i];
            int col = i % gridWidth;
            int row = i / gridWidth;

            int cubeX = col - row / 2;
            int cubeY = -(col + (row + 1) / 2);
            int cubeZ = row;

            hexTile.Controller.X = cubeX;
            hexTile.Controller.Y = cubeY;
            hexTile.Controller.Z = cubeZ;

            hexTilesMap.Add(new Tuple<int, int, int>(cubeX, cubeY, cubeZ), hexTile);
        }

        GridTraversalController.SetHexTiles(hexTilesMap);
    }
}