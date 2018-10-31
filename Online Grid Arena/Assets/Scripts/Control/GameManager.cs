using UnityEngine;
using System.Collections.Generic;

public enum SelectionMode
{
    SELECTION,
    ABILITY,
    MOVEMENT
}

public class GameManager : MonoBehaviour, IGameManager
{
    public SelectionController selectionController;
    public AbilitySelectionController abilitySelectionController;
    public MovementSelectionController movementSelectionController;

    public GridSelectionController gridSelectionController;
    public GridTraversalController gridTraversalController;
    public TurnController turnController;
    public HUDController hudController;

    private InputParameters inputParameters;

    public SelectionMode SelectionMode { get; set; }

    #region IGameManager implementation

    public ISelectionController SelectionController
    {
        get { return selectionController; }
    }

    #endregion

    private void Awake()
    {
        StatPanel[] statPanels = FindObjectsOfType<StatPanel>();
        PlayerPanel[] playerPanels = FindObjectsOfType<PlayerPanel>();
        hudController.SelectedStatPanel = statPanels[1];
        hudController.SelectedPlayerPanel = playerPanels[1];
        hudController.TargetStatPanel = statPanels[0];
        hudController.TargetPlayerPanel = playerPanels[0];

        selectionController.GridSelectionController = gridSelectionController;
        abilitySelectionController.GridSelectionController = gridSelectionController;
        movementSelectionController.GridSelectionController = gridSelectionController;

        selectionController.HUDController = hudController;
        abilitySelectionController.HUDController = hudController;
        movementSelectionController.HUDController = hudController;

        movementSelectionController.GridTraversalController = gridTraversalController;

        abilitySelectionController.GameManager = this;
        movementSelectionController.GameManager = this;

        turnController.Init();
        turnController.HUDController = hudController;

        Character[] charactersArray = FindObjectsOfType<Character>();
        List<ICharacter> charactersList = new List<ICharacter>();
        foreach (ICharacter character in charactersArray)
        {
            character.Controller.TurnController = turnController;
            charactersList.Add(character);
        }
        turnController.RefreshedCharacters = charactersList;

        HexTile[] hexTilesArray = FindObjectsOfType<HexTile>();
        foreach (IHexTile hexTile in hexTilesArray)
        {
            hexTile.Controller.SelectionController = selectionController;
            hexTile.Controller.GridSelectionController = gridSelectionController;
            hexTile.Controller.GridTraversalController = gridTraversalController;
        }

        FindObjectOfType<Grid>().Init(gridSelectionController, gridTraversalController);

        SelectionMode = SelectionMode.SELECTION;
    }

    private void UpdateInputParameters()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isMouseOverGrid = Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Tile";
        IHexTile targetTile = null;
        if (isMouseOverGrid)
        {
            targetTile = hit.collider.gameObject.GetComponent<HexTile>();
        }

        inputParameters = new InputParameters()
        {
            IsKeyQDown = Input.GetKeyDown(KeyCode.Q),
            IsKeyWDown = Input.GetKeyDown(KeyCode.W),
            IsKeyEDown = Input.GetKeyDown(KeyCode.E),
            IsKeyRDown = Input.GetKeyDown(KeyCode.R),
            IsKeyFDown = Input.GetKeyDown(KeyCode.F),
            IsKeyEscapeDown = Input.GetKeyDown(KeyCode.Escape),

            IsLeftClickDown = Input.GetMouseButtonDown(0),
            IsRightClickDown = Input.GetMouseButtonDown(1),

            IsMouseOverGrid = isMouseOverGrid,
            TargetTile = targetTile
        };

        selectionController.InputParameters = inputParameters;
        abilitySelectionController.InputParameters = inputParameters;
        movementSelectionController.InputParameters = inputParameters;
    }

    private bool IsSelectedCharacterActive()
    {
        if (!(gridSelectionController.SelectedTiles.Count > 0))
            return false;

        IHexTile selectedTile = gridSelectionController.SelectedTiles[0];
        if (selectedTile == null)
            return false;

        ICharacter selectedCharacter = gridSelectionController.SelectedTiles[0].Controller.OccupantCharacter;
        if (selectedCharacter == null)
            return false;

        return turnController.ActiveCharacter == selectedCharacter;
    }

    private bool HasAbilitiesRemaining()
    {
        if (!(gridSelectionController.SelectedTiles.Count > 0))
            return false;

        IHexTile selectedTile = gridSelectionController.SelectedTiles[0];
        if (selectedTile == null)
            return false;

        ICharacter selectedCharacter = gridSelectionController.SelectedTiles[0].Controller.OccupantCharacter;
        if (selectedCharacter == null)
            return false;

        return selectedCharacter.Controller.AbilitiesRemaining > 0;
    }

    private bool HasMovesRemaining()
    {
        if (!(gridSelectionController.SelectedTiles.Count > 0))
            return false;

        IHexTile selectedTile = gridSelectionController.SelectedTiles[0];
        if (selectedTile == null)
            return false;

        ICharacter selectedCharacter = gridSelectionController.SelectedTiles[0].Controller.OccupantCharacter;
        if (selectedCharacter == null)
            return false;

        return selectedCharacter.Controller.MovesRemaining > 0;
    }

    private void SetSelectionMode()
    {
        if ((inputParameters.IsKeyQDown
            || inputParameters.IsKeyWDown
            || inputParameters.IsKeyEDown
            || inputParameters.IsKeyRDown)
            && IsSelectedCharacterActive() && HasAbilitiesRemaining())
        {
            SelectionMode = SelectionMode.ABILITY;
        }
        else if (inputParameters.IsKeyFDown && IsSelectedCharacterActive() && HasMovesRemaining())
        {
            SelectionMode = SelectionMode.MOVEMENT;
        }
    }

    private void Start()
    {
        turnController.StartNextTurn();
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
