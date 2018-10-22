using UnityEngine;

public class GameManager : MonoBehaviour, IMonoBehaviour, IGameManager
{

    #region ICharacterSelectionController implementation

    public SelectionController SelectionController;

    #endregion

    private void Start()
    {
        SelectionController.GridSelectionController = FindObjectOfType<Grid>().controller.GridSelectionController;
        SelectionController.GridTraversalController = FindObjectOfType<Grid>().controller.GridTraversalController;
        SelectionController.GameManager = FindObjectOfType<GameManager>();
        SelectionController.StatPanel = FindObjectOfType<StatPanel>();
        SelectionController.StatPanel.GameObject.SetActive(false);
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

        SelectionController.IsEscapeButtonDown = isEscapeButtonDown;
        SelectionController.MouseIsOverGrid = mouseIsOverGrid;
        SelectionController.IsLeftClickDown = isLeftClickDown;
        SelectionController.TargetTile = targetTile;

        SelectionController.Update();
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
