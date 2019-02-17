using System.Collections.Generic;
using System;

public interface IGridController
{
    void GenerateGridMap(List<IHexTileController> hexTiles);
    IHexTileController GetTile((int, int, int) coordinates);
}
