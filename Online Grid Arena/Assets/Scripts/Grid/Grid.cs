using UnityEngine;

public class Grid : MonoBehaviour
{
    public GridController controller;
    public GridTraversalController traversalController;
    public GridSelectionController selectionController;

    private void OnValidate()
    {
        controller.Init(traversalController, selectionController);
        controller.SetHexTiles(GetComponentsInChildren<HexTile>());
    }
}
