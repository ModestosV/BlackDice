using UnityEngine;

public class Grid : MonoBehaviour
{
    public GridController controller;
    public GridTraversalController traversalController;

    private void OnValidate()
    {
        controller.Init(traversalController);
        controller.SetHexTiles(GetComponentsInChildren<HexTile>());
    }
}
