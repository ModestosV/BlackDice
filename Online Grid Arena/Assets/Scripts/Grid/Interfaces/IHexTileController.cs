using System;

public interface IHexTileController
{
    Tuple<int, int, int> Coordinates { set; }

    bool IsEnabled { set; }
    bool IsSelected { set; }
    
    IGridSelectionController GridSelectionController { set; }
    IGridTraversalController GridTraversalController { set; }
    IHexTile HexTile { set; }
    ICharacterController OccupantCharacter { set; }

    void Select();
    void Deselect();
    void Hover();
    void Blur();
    void HoverError();
    void Highlight();
    void Dehighlight();
}
