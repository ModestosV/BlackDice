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
    public ICharacterSelectionController SelectionController { get; set; }

    public ICharacter OccupantCharacter { get; set; }
    
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
            if (OccupantCharacter != null)
            {
                SelectionController.SelectedCharacter = OccupantCharacter;
            }
        }
        else
        {
            IsSelected = false;
            HexTileSelectionController.Deselect();
            GridSelectionController.RemovedSelectedTile(HexTile);
            if (OccupantCharacter != null)
            {
                SelectionController.SelectedCharacter = null;
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
            if (OccupantCharacter != null)
            {
                SelectionController.SelectedCharacter = null;
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
