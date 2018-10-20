using UnityEngine;

public class Grid : MonoBehaviour, IMonoBehaviour
{
    public GridController controller;
    public GridTraversalController traversalController;
    public GridSelectionController selectionController;

    private void Awake()
    {
        controller.Init(traversalController, selectionController);
    }

    private void Start()
    {
        controller.SetHexTiles(GetComponentsInChildren<HexTile>());
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
