﻿using System;
using System.Collections.Generic;

public interface IHexTileController
{
    Tuple<int, int, int> Coordinates { get; set; }

    int X { get; }
    int Y { get; }
    int Z { get; }

    bool IsEnabled { get; set; }
    bool IsSelected { set; }
    
    IGridSelectionController GridSelectionController { set; }
    IGridController GridController { set; }
    IHexTile HexTile { set; }
    ICharacterController OccupantCharacter { set; }

    IHexTileController GetNorthEastNeighbor();
    IHexTileController GetEastNeighbor();
    IHexTileController GetSouthEastNeighbor();
    IHexTileController GetSouthWestNeighbor();
    IHexTileController GetWestNeighbor();
    IHexTileController GetNorthWestNeighbor();
    List<IHexTileController> GetNeighbors();
    List<IHexTileController> GetPath(IHexTileController startTile);

    void Select();
    void Deselect();
    void Hover();
    void Blur();
    void HoverError();
    void Highlight();
    void Dehighlight();
}
