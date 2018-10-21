using System.Collections.Generic;

public interface IGridSelectionController
{
    List<IHexTile> SelectedTiles { get; set; }
    List<IHexTile> HoveredTiles { get; set; }
    List<IHexTile> PathTiles { get; set; }

    void Init();
    void AddSelectedTile(IHexTile selectedTile);
    bool RemoveSelectedTile(IHexTile removedTile);
    void AddHoveredTile(IHexTile hoveredTile);
    bool RemoveHoveredTile(IHexTile removedTile);
    void AddPathTile(IHexTile hoveredTile);
    bool RemovePathTile(IHexTile removedTile);
    void BlurAll();
    void DeselectAll();
    void ScrubPathAll();
    void HighlightPath(List<IHexTile> path);
}
