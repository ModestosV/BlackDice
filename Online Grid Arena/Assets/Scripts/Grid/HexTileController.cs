[System.Serializable]
public class HexTileController
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public bool IsEnabled { get; set; }
    public bool IsSelected { get; set; }

    public IHexTileSelectionController HexTileSelectionController { get; set; }
    public IGridSelectionController GridSelectionController { get; set; }
    public IHexTile HexTile { get; set; }
    public ICharacterSelectionController selectionController { get; set; }

    public ICharacter occupantCharacter { get; set; }
    
    public void HoverPathfinding()
    {
        GridSelectionController.ScrubPathAll();
        Hover();
        GridSelectionController.DrawPath(HexTile);
    }

    public void Select()
    {
        if (!IsEnabled) return;

        GridSelectionController.BlurAll();
        GridSelectionController.ScrubPathAll();
        if (!IsSelected)
        {
            GridSelectionController.DeselectAll();
            IsSelected = true;
            HexTileSelectionController.Select();
            GridSelectionController.AddSelectedTile(HexTile);
            if (occupantCharacter != null)
            {
                selectionController.SelectedCharacter = occupantCharacter;
            }
        }
        else
        {
            IsSelected = false;
            HexTileSelectionController.Deselect();
            GridSelectionController.RemovedSelectedTile(HexTile);
            if (occupantCharacter != null)
            {
                selectionController.SelectedCharacter = null;
            }
        }
    }

    public void MultiSelect()
    {
        if (!IsEnabled) return;

        if (!IsSelected)
        {
            IsSelected = true;
            HexTileSelectionController.Select();
            GridSelectionController.AddSelectedTile(HexTile);
        }
    }

    public void Hover()
    {
        if (!IsEnabled) return;

        if (!IsSelected)
        {
            HexTileSelectionController.Hover();
            GridSelectionController.AddHoveredTile(HexTile);
        }
    }

    public void Blur()
    {
        if (!IsEnabled) return;

        if (!IsSelected)
        {
            HexTileSelectionController.Blur();
            GridSelectionController.RemoveHoveredTile(HexTile);
        }
    }

    public void Deselect()
    {
        if (!IsEnabled) return;

        if (IsSelected)
        {
            IsSelected = false;
            HexTileSelectionController.Deselect();
            GridSelectionController.RemovedSelectedTile(HexTile);
            if (occupantCharacter != null)
            {
                selectionController.SelectedCharacter = null;
            }
        }
    }

    public void MarkPath()
    {
        if (!IsEnabled) return;

        if (!IsSelected)
        {
            HexTileSelectionController.MarkPath();
            GridSelectionController.AddPathTile(HexTile);
        }
    }

    public void ScrubPath()
    {
        if (!IsEnabled) return;

        if (!IsSelected)
        {
            HexTileSelectionController.ScrubPath();
            GridSelectionController.RemovePathTile(HexTile);
        }
    }
}
