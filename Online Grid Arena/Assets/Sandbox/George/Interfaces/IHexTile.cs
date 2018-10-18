using UnityEngine;

public interface IHexTile
{
    int X {get;set;}
    int Y {get;set;}
    int Z {get;set;}
    void SetOccupant(GameObject occ);
    GameObject GetOccupant();
    void SetGrid(GridMapController refGrid);
    bool GetIsClicked();
    void SetIsClicked(bool newState);
    void InitializeTile(HexTileDefinition tileDefinition, MeshRenderer renderer);
    void OnMouseEnter();
    void OnMouseDown();
    void OnMouseExit();
}
