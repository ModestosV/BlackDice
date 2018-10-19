[System.Serializable]
public class HexTileController
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public bool IsEnabled { get; set; }
    public bool IsSelected { get; set; }

    public IHexTileSelectionController HexTileSelectionController { get; set; }
    public IGridSelectionController GridSelectionController  { get; set; }

    public void HoverPathfinding()
    {
        GridSelectionController.BlurAll();
        Hover();
        GridSelectionController.DrawPath((HexTile) HexTileSelectionController);
    }

    public void Select()
    {
        GridSelectionController.BlurAll();
        if (!IsSelected)
        {
            GridSelectionController.DeselectAll();
            IsSelected = true;
            HexTileSelectionController.Select();
            GridSelectionController.AddSelectedTile((HexTile)HexTileSelectionController);
        }
        else
        {
            IsSelected = false;
            HexTileSelectionController.Deselect();
            GridSelectionController.RemovedSelectedTile((HexTile)HexTileSelectionController);
        }
    }

    public void MultiSelect()
    {
        if (!IsSelected)
        {
            IsSelected = true;
            HexTileSelectionController.Select();
            GridSelectionController.AddSelectedTile((HexTile)HexTileSelectionController);
        }
    }

    public void Hover()
    {
        if (!IsSelected)
        {
            HexTileSelectionController.Hover();
            GridSelectionController.AddHoveredTile((HexTile)HexTileSelectionController);
        }
    }

    public void Blur()
    {
        if (!IsSelected)
        {
            HexTileSelectionController.Blur();
            GridSelectionController.RemoveHoveredTile((HexTile)HexTileSelectionController);
        }
    }

    public void Deselect()
    {
        if (IsSelected)
        {
            IsSelected = false;
            HexTileSelectionController.Deselect();
            GridSelectionController.RemovedSelectedTile((HexTile)HexTileSelectionController);
        }
    }
}
