using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    public SelectionController selectionController;
    public GridSelectionController gridSelectionController;
    public GridTraversalController gridTraversalController;
    public AbilitySelectionController abilitySelectionController;
    public AbilityExecutionController abilityExecutionController;

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
        selectionController.AbilitySelectionController = abilitySelectionController;
        selectionController.AbilitySelectionController.SelectionController = selectionController;

        selectionController.AbilitySelectionController.GridSelectionController = gridSelectionController;
        selectionController.AbilitySelectionController.GridTraversalController = gridTraversalController;
        selectionController.AbilitySelectionController.AbilityExecutionController = abilityExecutionController;

        IStatPanel statPanel = FindObjectOfType<StatPanel>();
        abilityExecutionController.StatPanel = statPanel;

        selectionController.GameManager = this;
        selectionController.StatPanel = statPanel;
        selectionController.StatPanel.Controller.DisableStatDisplays();
        selectionController.PlayerPanel = FindObjectOfType<PlayerPanel>();

        FindObjectOfType<Grid>().Init(gridSelectionController, gridTraversalController);
    }

    private void HandleAbilityInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectionController.SetAbility(0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            selectionController.SetAbility(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            selectionController.SetAbility(2);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            selectionController.SetAbility(3);
        }
    }

    void Update()
    {
        HandleAbilityInput();

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
