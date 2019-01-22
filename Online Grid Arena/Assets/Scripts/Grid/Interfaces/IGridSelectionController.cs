using System.Collections.Generic;

public interface IGridSelectionController
{
    IHexTileController SelectedTile { get; set; }
    List<IHexTileController> HoveredTiles { set; }
    List<IHexTileController> HighlightedTiles { set; }
    
    void AddHoveredTile(IHexTileController hoveredTile);
    bool RemoveHoveredTile(IHexTileController removedTile);
    void AddHighlightedTile(IHexTileController hoveredTile);
    bool RemoveHighlightedTile(IHexTileController removedTile);
    void BlurAll();
    void DehighlightAll();
    bool IsSelectedTile(IHexTileController targetTile);
    ICharacterController GetSelectedCharacter();
}
