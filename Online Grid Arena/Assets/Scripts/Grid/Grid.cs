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

    private void ArrangeHexTileInGridFormation(HexTile[] hexTiles)
    {
        for (int i = 0; i < hexTiles.Length; i++)
        {
            int gridWidth = controller.gridWidth;
            IHexTile hexTile = hexTiles[i];
            int col = i % gridWidth;
            int row = i / gridWidth;
            
            Vector3 meshSize = hexTile.GameObject.GetComponent<Renderer>().bounds.size;
            float rowOffset = row % 2 == 0 ? 0.0f : meshSize.x / 2.0f;
            hexTile.GameObject.transform.position = new Vector3(col * meshSize.x + rowOffset, 0, -(row * 0.75f * meshSize.z));
        }
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
