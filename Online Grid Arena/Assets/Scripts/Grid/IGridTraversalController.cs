using System;
using System.Collections.Generic;

public interface IGridTraversalController {
    void Init();
    void SetHexTiles(Dictionary<Tuple<int, int, int>, IHexTile> hexTiles);
    IHexTile GetHexTile(Tuple<int, int, int> coordinates);
    IHexTile GetNorthEastNeighbor(IHexTile tile);
    IHexTile GetEastNeighbor(IHexTile tile);
    IHexTile GetSouthEastNeighbor(IHexTile tile);
    IHexTile GetSouthWestNeighbor(IHexTile tile);
    IHexTile GetWestNeighbor(IHexTile tile);
    IHexTile GetNorthWestNeighbor(IHexTile tile);
    List<IHexTile> GetNeighbors(IHexTile tile);
    List<IHexTile> GetPath(IHexTile startTile, IHexTile endTile);

}
