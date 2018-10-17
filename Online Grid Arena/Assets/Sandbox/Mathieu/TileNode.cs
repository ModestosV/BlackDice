
public class TileNode {

    public HexTile2 tile;
    public TileNode parent;
    public int depth;
    public int f;

    public TileNode(HexTile2 tile, TileNode parent, int depth, int f)
    {
        this.tile = tile;
        this.parent = parent;
        this.depth = depth;
        this.f = f;
    }

}
