public class SelectTileEvent : IEvent
{
    public IHexTileController SelectedTile { get; }

    public SelectTileEvent(IHexTileController selectedTile)
    {
        SelectedTile = selectedTile;
    }
}

