[System.Serializable]
public class HexTileController : IHexTileController
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public bool IsEnabled { get; set; }
    public bool IsSelected { get; set; }

    public IGridSelectionController GridSelectionController { get; set; }
    public IGridTraversalController GridTraversalController { get; set; }
    public IHexTile HexTile { get; set; }
    public ICharacterController OccupantCharacter { get; set; }

    public void Select()
    {
        if (!IsEnabled || IsSelected) return;

        GridSelectionController.DeselectAll();
        IsSelected = true;
        HexTile.SetClickedMaterial();
        GridSelectionController.AddSelectedTile(HexTile);
    }

    public void Deselect()
    {
        if (!IsEnabled || !IsSelected) return;

        IsSelected = false;
        if (HexTile.IsMouseOver())
        {
            HexTile.SetHoverMaterial();
        } else
        {
            HexTile.SetErrorMaterial();
        }
        GridSelectionController.RemoveSelectedTile(HexTile);
    }

    public void Hover()
    {
        if (!IsEnabled || IsSelected) return;

        HexTile.SetHoverMaterial();
        GridSelectionController.AddHoveredTile(HexTile);
    }

    public void HoverError()
    {
        if (!IsEnabled || IsSelected) return;
        
        HexTile.SetErrorMaterial();
        GridSelectionController.AddHoveredTile(HexTile);
    }

    public void Blur()
    {
        if (!IsEnabled || IsSelected) return;

        HexTile.SetDefaultMaterial();
        GridSelectionController.RemoveHoveredTile(HexTile);
    }

    public void Highlight()
    {
        if (!IsEnabled || IsSelected) return;

        HexTile.SetHighlightMaterial();
        GridSelectionController.AddPathTile(HexTile);
    }

    public void Dehighlight()
    {
        if (!IsEnabled || IsSelected) return;

        HexTile.SetDefaultMaterial();
        GridSelectionController.RemovePathTile(HexTile);
    }
}
