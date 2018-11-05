using System.Collections.Generic;

public class GridSelectionController : IGridSelectionController
{
    private List<IHexTileController> SelectedTiles { get; set; }
    private List<IHexTileController> HoveredTiles { get; set; }
    private List<IHexTileController> HighlightedTiles { get; set; }

    #region IGridSelectionController implementation

    public GridSelectionController()
    {
        SelectedTiles = new List<IHexTileController>();
        HoveredTiles = new List<IHexTileController>();
        HighlightedTiles = new List<IHexTileController>();
    }

    public void AddSelectedTile(IHexTileController selectedTile)
    {
        SelectedTiles.Add(selectedTile);
    }

    public bool RemoveSelectedTile(IHexTileController removedTile)
    {
        return SelectedTiles.Remove(removedTile);
    }

    public void AddHoveredTile(IHexTileController hoveredTile)
    {
        HoveredTiles.Add(hoveredTile);
    }

    public bool RemoveHoveredTile(IHexTileController removedTile)
    {
        return HoveredTiles.Remove(removedTile);
    }

    public void AddHighlightedTile(IHexTileController pathTile)
    {
        HighlightedTiles.Add(pathTile);
    }

    public bool RemoveHighlightedTile(IHexTileController pathTile)
    {
        return HighlightedTiles.Remove(pathTile);
    }

    public void DeselectAll()
    {
        for (int i = SelectedTiles.Count - 1; i >= 0; i--)
        {
            SelectedTiles[i].Deselect();
        }
    }

    public void BlurAll()
    {
        for (int i = HoveredTiles.Count - 1; i >= 0; i--)
        {
            HoveredTiles[i].Blur();
        }
    }

    public void DehighlightAll()
    {
        for (int i = HighlightedTiles.Count - 1; i >= 0; i--)
        {
            HighlightedTiles[i].Dehighlight();
        }
    }

    public bool IsSelectedTile(IHexTileController targetTile)
    {
        return SelectedTiles.Count > 0 && targetTile == SelectedTiles[0];
    }

    public IHexTileController GetSelectedTile()
    {
        return SelectedTiles.Count > 0 ? SelectedTiles[0] : null;
    }

    public ICharacterController GetSelectedCharacter()
    {
        if (!(SelectedTiles.Count > 0))
            return null;

        return SelectedTiles[0].IsOccupied() ? SelectedTiles[0].OccupantCharacter : null;
    }

    #endregion
}
