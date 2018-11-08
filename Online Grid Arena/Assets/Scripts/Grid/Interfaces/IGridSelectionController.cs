using System.Collections.Generic;

public interface IGridSelectionController
{
    List<IHexTileController> SelectedTiles { set; }
    List<IHexTileController> HoveredTiles { set; }
    List<IHexTileController> HighlightedTiles { set; }

    void AddSelectedTile(IHexTileController selectedTile);
    bool RemoveSelectedTile(IHexTileController removedTile);
    void AddHoveredTile(IHexTileController hoveredTile);
    bool RemoveHoveredTile(IHexTileController removedTile);
    void AddHighlightedTile(IHexTileController hoveredTile);
    bool RemoveHighlightedTile(IHexTileController removedTile);
    void DeselectAll();
    void BlurAll();
    void DehighlightAll();
    bool IsSelectedTile(IHexTileController targetTile);
    IHexTileController GetSelectedTile();
    ICharacterController GetSelectedCharacter();
}
