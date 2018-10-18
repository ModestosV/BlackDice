using UnityEngine;

[System.Serializable]
public class HexTileController2 {

    public int x;
    public int y;
    public int z;

    public bool isEnabled;
    public bool isSelected;

    public IHexTileSelectionController hexTileSelectionController;
    public IGridSelectionController gridSelectionController;

    public void SetHexTileSelectionController(IHexTileSelectionController hexTileSelectionController)
    {
        this.hexTileSelectionController = hexTileSelectionController;
    }

    public void SetGridSelectionController(IGridSelectionController gridSelectionController)
    {
        this.gridSelectionController = gridSelectionController;
    }

    public void OnMouseEnter()
    {
        gridSelectionController.BlurAll();
        Hover();
        gridSelectionController.DrawPath((HexTile2)hexTileSelectionController);
    }

    public void OnMouseExit()
    {
        Blur();
    }

    public void Select()
    {
        gridSelectionController.BlurAll();
        if (!isSelected)
        {
            gridSelectionController.DeselectAll();
            isSelected = true;
            hexTileSelectionController.Select();
            gridSelectionController.AddSelectedTile((HexTile2)hexTileSelectionController);
        }
        else
        {
            isSelected = false;
            hexTileSelectionController.Deselect();
            gridSelectionController.RemovedSelectedTile((HexTile2)hexTileSelectionController);
        }
    }

    public void MultiSelect()
    {
        if (!isSelected)
        {
            isSelected = true;
            hexTileSelectionController.Select();
            gridSelectionController.AddSelectedTile((HexTile2)hexTileSelectionController);
        }
    }

    public void Hover()
    {
        if (!isSelected)
        {
            hexTileSelectionController.Hover();
            gridSelectionController.AddHoveredTile((HexTile2)hexTileSelectionController);
        }
    }

    public void Blur()
    {
        if (!isSelected)
        {
            hexTileSelectionController.Blur();
            gridSelectionController.RemoveHoveredTile((HexTile2)hexTileSelectionController);
        }
    }

    public void Deselect()
    {
        if (isSelected)
        {
            isSelected = false;
            hexTileSelectionController.Deselect();
            gridSelectionController.RemovedSelectedTile((HexTile2)hexTileSelectionController);
        }
    }



}
