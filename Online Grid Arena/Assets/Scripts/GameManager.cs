using UnityEngine;

public class GameManager : MonoBehaviour, IMonoBehaviour, IGameManager
{
    public SelectionController selectionController;
    public GridSelectionController gridSelectionController;
    public GridTraversalController gridTraversalController;

    #region IGameManager implementation

    public ISelectionController SelectionController
    {
        get { return selectionController; }
    }

    #endregion

    private void Awake()
    {
        selectionController.GridSelectionController = gridSelectionController;
        selectionController.GridTraversalController = gridTraversalController;
        selectionController.GameManager = this;
        selectionController.StatPanel = FindObjectOfType<StatPanel>();
        selectionController.StatPanel.GameObject.SetActive(false);

        FindObjectOfType<Grid>().Init(gridSelectionController, gridTraversalController);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool isEscapeButtonDown = Input.GetKeyDown(KeyCode.Escape);
        bool mouseIsOverGrid = Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Tile";
        bool isLeftClickDown = Input.GetMouseButtonDown(0);

        IHexTile targetTile = null;
        if (mouseIsOverGrid)
        {
            targetTile = hit.collider.gameObject.GetComponent<HexTile>();
        }

        selectionController.IsEscapeButtonDown = isEscapeButtonDown;
        selectionController.MouseIsOverGrid = mouseIsOverGrid;
        selectionController.IsLeftClickDown = isLeftClickDown;
        selectionController.TargetTile = targetTile;

        selectionController.Update();
    }

    #region IGameManager implementation

    public void QuitApplication()
    {
        Application.Quit();
    }

    #endregion

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
