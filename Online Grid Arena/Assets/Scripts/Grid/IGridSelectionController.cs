public interface IGridSelectionController
{
    void AddSelectedTile(IHexTile selectedTile);
    bool RemovedSelectedTile(IHexTile removedTile);
    void AddHoveredTile(IHexTile hoveredTile);
    bool RemoveHoveredTile(IHexTile removedTile);
    void AddPathTile(IHexTile hoveredTile);
    bool RemovePathTile(IHexTile removedTile);
    void BlurAll();
    void DeselectAll();
    void ScrubPathAll();
    void DrawPath(IHexTile endTile);
}
