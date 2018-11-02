using System;
using UnityEngine;

public interface IHexTile : IMonoBehaviour
{
    void SetHoverMaterial();
    void SetErrorMaterial();
    void SetDefaultMaterial();
    void SetClickedMaterial();
    void SetHighlightMaterial();
    bool IsMouseOver();

    IHexTileController Controller { get; }
    void SetChild(GameObject childObject);
}
