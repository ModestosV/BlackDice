public class SelectTileEvent : AbstractEvent
{
    public IHexTileController SelectedTile { get; }

    public SelectTileEvent(IHexTileController selectedTile)
    {
        SelectedTile = selectedTile;
    }
}

