using System.Collections.Generic;

[System.Serializable]
public class GridSelectionController : IGridSelectionController
{
    public List<IHexTile> SelectedTiles { get; set; }
    public List<IHexTile> HoveredTiles { get; set; }
    public List<IHexTile> PathTiles { get; set; }

    #region IGridSelectionController implementation

    public void Init()
    {
        SelectedTiles = new List<IHexTile>();
        HoveredTiles = new List<IHexTile>();
        PathTiles = new List<IHexTile>();
    }

    public void AddSelectedTile(IHexTile selectedTile)
    {
        SelectedTiles.Add(selectedTile);
    }

    public bool RemoveSelectedTile(IHexTile removedTile)
    {
        return SelectedTiles.Remove(removedTile);
    }

    public void AddHoveredTile(IHexTile hoveredTile)
    {
        HoveredTiles.Add(hoveredTile);
    }

    public bool RemoveHoveredTile(IHexTile removedTile)
    {
        return HoveredTiles.Remove(removedTile);
    }

    public void AddPathTile(IHexTile pathTile)
    {
        PathTiles.Add(pathTile);
    }

    public bool RemovePathTile(IHexTile pathTile)
    {
        return PathTiles.Remove(pathTile);
    }

    public void DeselectAll()
    {
        for (int i = SelectedTiles.Count - 1; i >= 0; i--)
        {
            SelectedTiles[i].Controller().Deselect();
        }
    }

    public void BlurAll()
    {
        for (int i = HoveredTiles.Count - 1; i >= 0; i--)
        {
            HoveredTiles[i].Controller().Blur();
        }
    }

    public void ScrubPathAll()
    {
        for (int i = PathTiles.Count - 1; i >= 0; i--)
        {
            PathTiles[i].Controller().ScrubPath();
        }
    }

    public void HighlightPath(List<IHexTile> path)
    {
        foreach (IHexTile tile in path)
        {
            tile.Controller().MarkPath();
        }
    }

    #endregion
}
