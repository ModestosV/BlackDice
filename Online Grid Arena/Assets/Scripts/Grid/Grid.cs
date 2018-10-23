using UnityEngine;

public class Grid : MonoBehaviour, IGrid
{
    public GridController controller;
    public GridTraversalController traversalController;
    public GridSelectionController selectionController;

    private void Start()
    {
        controller.SetHexTiles(GetComponentsInChildren<HexTile>());
    }

    #region IGrid implementation

    public void Init(IGridSelectionController gridSelectionController, IGridTraversalController gridTraversalController)
    {
        controller.Init(gridSelectionController, gridTraversalController);
    }

    #endregion

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
