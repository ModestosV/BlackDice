using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GridController : IGridSelectionController
{
    public int majorAxisLength;

    public IGridTraversalController GridTraversalController { get; set; }

    private List<IHexTile> selectedTiles;
    private List<IHexTile> hoveredTiles;
    private List<IHexTile> pathTiles;

    public void Init(IGridTraversalController traversalController)
    {
        selectedTiles = new List<IHexTile>();
        hoveredTiles = new List<IHexTile>();
        pathTiles = new List<IHexTile>();
        GridTraversalController = traversalController;
        GridTraversalController.Init();
        if (majorAxisLength == 0)
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

    #region IGridSelectionController implementation

    public void AddSelectedTile(IHexTile selectedTile)
    {
        selectedTiles.Add(selectedTile);
    }

    public bool RemoveSelectedTile(IHexTile removedTile)
    {
        return selectedTiles.Remove(removedTile);
    }

    public void AddHoveredTile(IHexTile hoveredTile)
    {
        hoveredTiles.Add(hoveredTile);
    }

    public bool RemoveHoveredTile(IHexTile removedTile)
    {
        return hoveredTiles.Remove(removedTile);
    }

    public void AddPathTile(IHexTile pathTile)
    {
        pathTiles.Add(pathTile);
    }

    public bool RemovePathTile(IHexTile pathTile)
    {
        return pathTiles.Remove(pathTile);
    }

    public void DeselectAll()
    {
        for (int i = selectedTiles.Count - 1; i >= 0; i--)
        {
            selectedTiles[i].Controller().Deselect();
        }
    }

    public void BlurAll()
    {
        for (int i = hoveredTiles.Count - 1; i >= 0; i--)
        {
            hoveredTiles[i].Controller().Blur();
        }
    }

    public void ScrubPathAll()
    {
        for (int i = pathTiles.Count - 1; i >= 0; i--)
        {
            pathTiles[i].Controller().ScrubPath();
        }
    }

    public void DrawPath(IHexTile endTile)
    {
        if (selectedTiles.Count > 0)
        {
            foreach (IHexTile startTile in selectedTiles)
            {
                List<IHexTile> path = GridTraversalController.GetPath(startTile, endTile);

                foreach (IHexTile tile in path)
                {
                    tile.Controller().MarkPath();
                }
            }
        }
    }

    #endregion

}