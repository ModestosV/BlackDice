public interface IGridSelectionController
{
    void AddSelectedTile(IHexTileController selectedTile);
    bool RemoveSelectedTile(IHexTileController removedTile);
    void AddHoveredTile(IHexTileController hoveredTile);
    bool RemoveHoveredTile(IHexTileController removedTile);
    void AddHighlightedTile(IHexTileController hoveredTile);
    bool RemoveHighlightedTile(IHexTileController removedTile);
    void DeselectAll();
    void BlurAll();
    void DehighlightAll();
}
