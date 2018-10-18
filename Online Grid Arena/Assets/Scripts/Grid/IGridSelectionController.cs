
public interface IGridSelectionController
{
    void AddSelectedTile(HexTile selectedTile);
    bool RemovedSelectedTile(HexTile removedTile);
    void AddHoveredTile(HexTile hoveredTile);
    bool RemoveHoveredTile(HexTile removedTile);
    void BlurAll();
    void DeselectAll();
    void DrawPath(HexTile endTile);
}
