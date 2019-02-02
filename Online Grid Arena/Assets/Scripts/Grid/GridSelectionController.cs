using System.Collections.Generic;

public sealed class GridSelectionController : IGridSelectionController, IEventSubscriber
{
    public IHexTileController SelectedTile { get; set; }
    public List<IHexTileController> HoveredTiles { private get; set; }
    public List<IHexTileController> HighlightedTiles { private get; set; }
    
    public GridSelectionController()
    {
        HoveredTiles = new List<IHexTileController>();
        HighlightedTiles = new List<IHexTileController>();
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
        return SelectedTile == targetTile;
    }

    public ICharacterController GetSelectedCharacter()
    {
        if (SelectedTile == null)
            return null;

        return SelectedTile.IsOccupied() ? SelectedTile.OccupantCharacter : null;
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        if (type == typeof(DeselectSelectedTileEvent))
        {
            if(SelectedTile != null)
            {
                SelectedTile.Deselect();
                SelectedTile = null;
            }
        }
        if (type == typeof(SelectTileEvent))
        {
            if(SelectedTile != null)
            {
                SelectedTile.Deselect();
            }
            SelectTileEvent selectTileEvent = (SelectTileEvent) @event;
            SelectedTile = selectTileEvent.SelectedTile;
            SelectedTile.Select();
        }
    }
}
