using UnityEngine;

public class HexTileManager: MonoBehaviour
{
    private IHexTile tile = new HexTile();
    public HexTileDefinition tileDefinition;
    public IHexTile Tile => tile;
    
    void Start()
    {
        tile.InitializeTile(tileDefinition, this.GetComponentInChildren<MeshRenderer>());
    }

    private void OnMouseEnter()
    {
        tile.OnMouseEnter();
    }

    private void OnMouseExit()
    {
        tile.OnMouseExit();
    }

    private void OnMouseDown()
    {
        tile.OnMouseDown();
    }
}
