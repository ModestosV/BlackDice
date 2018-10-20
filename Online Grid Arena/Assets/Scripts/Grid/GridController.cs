using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GridController
{
    public int majorAxisLength;

    public IGridTraversalController GridTraversalController { get; set; }
    public IGridSelectionController GridSelectionController { get; set; }

    public void Init(IGridTraversalController traversalController, IGridSelectionController selectionController)
    {
        GridSelectionController = selectionController;
        GridSelectionController.Init();
        GridTraversalController = traversalController;
        GridTraversalController.Init();
        if (majorAxisLength == 0) // Possible bug. TODO: Investigate why this is sometimes 0.
            majorAxisLength = 19;
    }

    public void SetHexTiles(HexTile[] hexTiles)
    {
        Dictionary<Tuple<int, int, int>, IHexTile> hexTilesMap = new Dictionary<Tuple<int, int, int>, IHexTile>();

        for (int i = 0; i < hexTiles.Length; i++)
        {
            IHexTile hexTile = hexTiles[i];
            int col = i % majorAxisLength;
            int row = i / majorAxisLength;

            int cubeX = col - row / 2;
            int cubeY = -(col + (row + 1) / 2);
            int cubeZ = row;

            hexTile.Controller().X = cubeX;
            hexTile.Controller().Y = cubeY;
            hexTile.Controller().Z = cubeZ;

            hexTilesMap.Add(new Tuple<int, int, int>(cubeX, cubeY, cubeZ), hexTile);
        }

        // Arrange hex tiles
        for (int i = 0; i < hexTiles.Length; i++)
        {
            IHexTile hexTile = hexTiles[i];
            int col = i % majorAxisLength;
            int row = i / majorAxisLength;

            Vector3 meshSize = hexTile.GameObject.GetComponent<Renderer>().bounds.size;

            float rowOffset = row % 2 == 0 ? 0.0f : meshSize.x / 2.0f;

            hexTile.GameObject.transform.position = new Vector3(col * meshSize.x + rowOffset, 0, -(row * 0.75f * meshSize.z));
        }

        GridTraversalController.SetHexTiles(hexTilesMap);
    }
}