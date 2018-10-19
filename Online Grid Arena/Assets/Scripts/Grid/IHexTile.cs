using System;

public interface IHexTile
{
    HexTileController Controller();
    Tuple<int, int, int> Coordinates();
}
