using UnityEngine;

public class HexTileManager: MonoBehaviour
{
    public HexTileDefinition tileDefinition;

    private IHexTile tile = new HexTile();
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
