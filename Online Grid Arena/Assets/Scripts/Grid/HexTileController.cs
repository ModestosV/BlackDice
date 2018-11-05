using System;

public class HexTileController : IHexTileController
{
    public Tuple<int, int, int> Coordinates { protected get; set; }

    public bool IsEnabled { protected get; set; }
    public bool IsSelected { protected get; set; }

    public IGridSelectionController GridSelectionController { protected get; set; }
    public IGridTraversalController GridTraversalController { protected get; set; }
    public IHexTile HexTile { protected get; set; }
    public ICharacterController OccupantCharacter { protected get; set; }

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
