using UnityEngine;

public interface IHexTile
{
    int X {get;set;}
    int Y {get;set;}
    int Z {get;set;}
    void setOccupant(GameObject occ);
    GameObject getOccupant();
    void setGrid(GridMap refGrid);
    bool getIsClicked();
    void setIsClicked(bool newState);
    void InitializeTile(HexTileDefinition tileDefinition, MeshRenderer renderer);
    void OnMouseEnter();
    void OnMouseDown();
    void OnMouseExit();
}
