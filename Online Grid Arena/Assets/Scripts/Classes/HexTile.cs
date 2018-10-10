using UnityEngine;

public class HexTile: MonoBehaviour
{
    private IHexTile tile = new HexTileController();
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
