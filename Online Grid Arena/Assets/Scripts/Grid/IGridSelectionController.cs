
public interface IGridSelectionController
{
    void AddSelectedTile(HexTile2 selectedTile);
    bool RemovedSelectedTile(HexTile2 removedTile);
    void AddHoveredTile(HexTile2 hoveredTile);
    bool RemoveHoveredTile(HexTile2 removedTile);
    void BlurAll();
    void DeselectAll();
    void DrawPath(HexTile2 endTile);
}
