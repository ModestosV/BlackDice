﻿using UnityEngine;

public class SelectTileEvent : AbstractEvent
{
    public IHexTileController SelectedTile { get; }

    public SelectTileEvent(IHexTileController selectedTile)
    {
        Debug.Log($"Selected tile contains parameters {selectedTile.ToString()} + with occupied value of: {selectedTile.IsOccupied()}");
        SelectedTile = selectedTile;
    }
}

