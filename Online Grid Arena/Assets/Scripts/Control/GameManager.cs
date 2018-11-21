using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public SelectionMode SelectionMode { protected get; set; }

    private TurnController turnController;
    private HUDController hudController;
    private GridSelectionController gridSelectionController;

    private FreeSelectionController freeSelectionController;
    private MovementSelectionController movementSelectionController;
    private TargetEnemyAbilitySelectionController targetEnemyAbilitySelectionController;
    private TargetAllyAbilitySelectionController targetAllyAbilitySelectionController;
    private SelectionManager selectionManager;

    private InputManager inputManager;
    
    private void Awake()
    {
        // Initialize turn controller
        turnController = new TurnController
        {
            EndMatchPanel = FindObjectOfType<EndMatchMenu>()
        };
        List<ICharacterController> charactersList = FindObjectsOfType<Character>().Select(x => x.Controller).ToList();
        foreach (ICharacterController character in charactersList)
        {
            character.TurnController = turnController;
            turnController.AddCharacter(character);
        }

        // Initialize Menu
        FindObjectOfType<SurrenderButton>().TurnController = turnController;
        
        // Initialize HUD
        hudController = new HUDController();

        StatPanel[] statPanels = FindObjectsOfType<StatPanel>();
        PlayerPanel[] playerPanels = FindObjectsOfType<PlayerPanel>();
        AbilityPanel abilityPanel = FindObjectOfType<AbilityPanel>();

        hudController.SelectedStatPanel = statPanels[1].Controller;
        hudController.SelectedPlayerPanel = playerPanels[1];
        hudController.TargetStatPanel = statPanels[0].Controller;
        hudController.TargetPlayerPanel = playerPanels[0];
        hudController.AbilityPanel = abilityPanel;

        FindObjectOfType<EndTurnButton>().TurnController = turnController;

        // Initialize grid
        gridSelectionController = new GridSelectionController();

        // Initialize selection controllers
        selectionManager = new SelectionManager()
        {
            GridSelectionController = gridSelectionController,
            SelectionMode = SelectionMode.FREE
        };

        freeSelectionController = new FreeSelectionController()
        {
            GridSelectionController = gridSelectionController,
            TurnController = turnController,
            SelectionManager = selectionManager
        };

        movementSelectionController = new MovementSelectionController()
        {
            GridSelectionController = gridSelectionController,
            TurnController = turnController,
            SelectionManager = selectionManager
        };

        targetEnemyAbilitySelectionController = new TargetEnemyAbilitySelectionController()
        {
            GridSelectionController = gridSelectionController,
            TurnController = turnController,
            SelectionManager = selectionManager
        };

        targetAllyAbilitySelectionController = new TargetAllyAbilitySelectionController()
        {
            GridSelectionController = gridSelectionController,
            TurnController = turnController,
            SelectionManager = selectionManager
        };

        selectionManager.SelectionControllers = new Dictionary<string, ISelectionController>()
        {
            { "free", freeSelectionController },
            { "movement", movementSelectionController },
            { "target_enemy", targetEnemyAbilitySelectionController },
            { "target_ally", targetAllyAbilitySelectionController }
        };

        // Initialize input manager
        inputManager = FindObjectOfType<InputManager>();
        inputManager.SelectionManager = selectionManager;

        // Initialize characters
        List<ICharacterController> characters = FindObjectsOfType<Character>().Select(x => x.Controller).ToList();
        foreach (ICharacterController character in characters)
        {
            character.HUDController = hudController;
            character.TurnController = turnController;
        }

        // Initialize turn panel
        turnController.TurnTracker = FindObjectOfType<TurnPanel>().Controller;
    }

    private void Start()
    {
        FindObjectOfType<Grid>().InitializeGrid(gridSelectionController);
        turnController.StartNextTurn();
    }
}
