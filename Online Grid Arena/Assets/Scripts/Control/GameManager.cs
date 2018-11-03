using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum SelectionMode
{
    SELECTION,
    ABILITY,
    MOVEMENT
}

public class GameManager : MonoBehaviour, IGameManager
{
<<<<<<< HEAD
    public SelectionMode SelectionMode { protected get; set; }

    private InputParameters inputParameters;
    private TurnController turnController;
    private HUDController hudController;
    private GridSelectionController gridSelectionController;
    private SelectionController selectionController;
    private MovementSelectionController movementSelectionController;
    private AbilitySelectionController abilitySelectionController;

=======
    public SelectionController selectionController;
    public GridSelectionController gridSelectionController;
    public GridTraversalController gridTraversalController;
    public TurnController turnController;
    public SkipTurnButtonController skipTurnButtonController;
>>>>>>> #40 add interfaces for button and its controller and make button work

    #region IGameManager implementation

    public ISelectionController SelectionController
    {
        get { return selectionController; }
    }

    #endregion

    private void Awake()
    {
        // Initialize turn controller
        turnController = new TurnController();
        List<ICharacterController> charactersList = FindObjectsOfType<Character>().Select(x => x.Controller).ToList();
        foreach (ICharacterController character in charactersList)
        {
            character.TurnController = turnController;
            turnController.AddCharacter(character);
        }
        
        // Initialize HUD
        hudController = new HUDController();

        StatPanel[] statPanels = FindObjectsOfType<StatPanel>();
        PlayerPanel[] playerPanels = FindObjectsOfType<PlayerPanel>();
        hudController.SelectedStatPanel = statPanels[1].Controller;
        hudController.SelectedPlayerPanel = playerPanels[1];
        hudController.TargetStatPanel = statPanels[0].Controller;
        hudController.TargetPlayerPanel = playerPanels[0];

        // Initialize grid
        gridSelectionController = new GridSelectionController();

        // Initialize selection controllers
        selectionController = new SelectionController()
        {
            GridSelectionController = gridSelectionController,
            TurnController = turnController
        };

        movementSelectionController = new MovementSelectionController()
        {
            GridSelectionController = gridSelectionController,
            GameManager = this
        };

        abilitySelectionController = new AbilitySelectionController()
        {
            GridSelectionController = gridSelectionController,
            GameManager = this
        };

        // Initialize characters
        List<ICharacterController> characters = FindObjectsOfType<Character>().Select(x => x.Controller).ToList();
        foreach (ICharacterController character in characters)
        {
            character.HUDController = hudController;
            character.TurnController = turnController;
        }
        
        SelectionMode = SelectionMode.SELECTION;

<<<<<<< HEAD
        FindObjectOfType<SkipTurnButton>().Controller.TurnController = turnController;
=======
        skipTurnButtonController.TurnController = turnController;
        FindObjectOfType<SkipTurnButton>().Controller = skipTurnButtonController;
        FindObjectOfType<Grid>().Init(gridSelectionController, gridTraversalController);
>>>>>>> #40 add interfaces for button and its controller and make button work
    }

    private void Start()
    {
        FindObjectOfType<Grid>().InitializeGrid(gridSelectionController);
        turnController.StartNextTurn();
    }

    private void UpdateInputParameters()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isMouseOverGrid = Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Tile";
        IHexTileController targetTile = null;
        if (isMouseOverGrid)
        {
            targetTile = hit.collider.gameObject.GetComponent<HexTile>().Controller;
        }

        inputParameters = new InputParameters()
        {
            IsKeyQDown = Input.GetKeyDown(KeyCode.Q),
            IsKeyWDown = Input.GetKeyDown(KeyCode.W),
            IsKeyEDown = Input.GetKeyDown(KeyCode.E),
            IsKeyRDown = Input.GetKeyDown(KeyCode.R),
            IsKeyFDown = Input.GetKeyDown(KeyCode.F),
            IsKeyEscapeDown = Input.GetKeyDown(KeyCode.Escape),
            IsKeyTabDown = Input.GetKeyDown(KeyCode.Tab),

            IsLeftClickDown = Input.GetMouseButtonDown(0),
            IsRightClickDown = Input.GetMouseButtonDown(1),

            IsMouseOverGrid = isMouseOverGrid,
            TargetTile = targetTile
        };

        selectionController.InputParameters = inputParameters;
        abilitySelectionController.InputParameters = inputParameters;
        movementSelectionController.InputParameters = inputParameters;
    }

    private bool CanMove()
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();

        if (selectedCharacter == null)
            return false;

        return selectedCharacter.CanMove();
    }

    private bool CanUseAbility()
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();

        if (selectedCharacter == null)
            return false;

        return selectedCharacter.CanUseAbility();
    }

    private void SetSelectionMode()
    {
        if ((inputParameters.IsKeyQDown
            || inputParameters.IsKeyWDown
            || inputParameters.IsKeyEDown
            || inputParameters.IsKeyRDown)
            && CanUseAbility())
        {
            SelectionMode = SelectionMode.ABILITY;
        }
        else if (inputParameters.IsKeyFDown && CanMove())
        {
            SelectionMode = SelectionMode.MOVEMENT;
        }
    }

    void Update()
    {
        UpdateInputParameters();
        SetSelectionMode();

        switch (SelectionMode)
        {
            case SelectionMode.SELECTION:
                selectionController.Update();
                break;
            case SelectionMode.MOVEMENT:
                movementSelectionController.Update();
                break;
            case SelectionMode.ABILITY:
                abilitySelectionController.Update();
                break;
            default:
                break;
        }
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
