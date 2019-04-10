using System.Collections.Generic;

public interface IGridController
{
    void GenerateGridMap(List<IHexTileController> hexTiles);
    IHexTileController GetTile((int, int, int) coordinates);
    IHexTileController GetRandomTile();
}
