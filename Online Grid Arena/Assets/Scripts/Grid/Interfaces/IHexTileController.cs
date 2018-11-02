public interface IHexTileController
{
    int X { get; set; }
    int Y { get; set; }
    int Z { get; set; }

    bool IsEnabled { get; set; }
    bool IsSelected { get; set; }
    
    IGridSelectionController GridSelectionController { get; set; }
    IGridTraversalController GridTraversalController { get; set; }
    IHexTile HexTile { get; set; }
    ICharacterController OccupantCharacter { get; set; }

    void Select();
    void Deselect();
    void Hover();
    void Blur();
    void HoverError();
    void MarkPath();
    void Dehighlight();
}
