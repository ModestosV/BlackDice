
public interface IGridSelectionController
{
    void AddSelectedTile(IHexTile selectedTile);
    bool RemovedSelectedTile(IHexTile removedTile);
    void AddHoveredTile(IHexTile hoveredTile);
    bool RemoveHoveredTile(IHexTile removedTile);
    void BlurAll();
    void DeselectAll();
    void DrawPath(IHexTile endTile);
}
