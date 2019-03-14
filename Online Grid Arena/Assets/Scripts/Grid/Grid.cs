using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public sealed class Grid : BlackDiceMonoBehaviour, IGrid
{
    [SerializeField] private int gridWidth;

    public GridController gridController;

    private readonly int defaultGridWidth = 19;

    public void InitializeGrid(IGridSelectionController gridSelectionController)
    {
        if (!(gridWidth > 0))
            gridWidth = defaultGridWidth;

        gridController = new GridController()
        {
            GridWidth = gridWidth
        };

        HexTile[] hexTiles = GetComponentsInChildren<HexTile>();
        ArrangeHexTilesInGridFormation(hexTiles);

        List<IHexTileController> hexTilesList = hexTiles.Select(x => x.Controller).ToList();
        gridController.GenerateGridMap(hexTilesList);

        foreach (IHexTileController hexTile in hexTilesList)
        {
            hexTile.GridSelectionController = gridSelectionController;
        }
    }
    
    private void ArrangeHexTilesInGridFormation(HexTile[] hexTiles)
    {
        for (int i = 0; i < hexTiles.Length; i++)
        {
            IHexTile hexTile = hexTiles[i];
            int col = i % gridWidth;
            int row = i / gridWidth;
            
            Vector3 meshSize = hexTile.GameObject.GetComponent<Renderer>().bounds.size;
            float rowOffset = row % 2 == 0 ? 0.0f : meshSize.x / 2.0f;
            hexTile.GameObject.transform.position = new Vector3(col * meshSize.x + rowOffset, 0, -(row * 0.75f * meshSize.z));
        }
    }
}
