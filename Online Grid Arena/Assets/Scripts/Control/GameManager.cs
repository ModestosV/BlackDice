using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public sealed class GameManager : MonoBehaviour
{
    public SelectionMode SelectionMode { private get; set; }

    private TurnController turnController;
    private HUDController hudController;
    private GridSelectionController gridSelectionController;

    private FreeSelectionController freeSelectionController;
    private MovementSelectionController movementSelectionController;
    private TargetEnemyAbilitySelectionController targetEnemyAbilitySelectionController;
    private TargetAllyAbilitySelectionController targetAllyAbilitySelectionController;
    private SelectionManager selectionManager;

    private InputManager inputManager;
    private EndMatchMenu endMatchMenu;
    private MatchMenu matchMenu;


    private void Awake()
    {
        EventBus.Reset();

        // Initialize turn controller
        turnController = new TurnController();
        List<ICharacterController> charactersList = FindObjectsOfType<AbstractCharacter>().Select(x => x.Controller).ToList();
        foreach (ICharacterController character in charactersList)
        {
            turnController.AddCharacter(character);
        }

        // Initialize Menus
        endMatchMenu = FindObjectOfType<EndMatchMenu>();
        matchMenu = FindObjectOfType<MatchMenu>();

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

        // Initialize grid
        gridSelectionController = new GridSelectionController();

        // Initialize selection controllers
        selectionManager = new SelectionManager()
        {
            GridSelectionController = gridSelectionController,
            TurnController = turnController,
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
        List<ICharacterController> characters = FindObjectsOfType<AbstractCharacter>().Select(x => x.Controller).ToList();
        foreach (ICharacterController character in characters)
        {
            character.HUDController = hudController;
        }

        // Initialize turn panel
        turnController.TurnTracker = FindObjectOfType<TurnPanel>().Controller;
        turnController.SelectionManager = selectionManager;

        // Initialize Event Subscribing
        EventBus.Subscribe<DeathEvent>(turnController);
        EventBus.Subscribe<EndMatchEvent>(endMatchMenu);
        EventBus.Subscribe<StartNewTurnEvent>(turnController);
        EventBus.Subscribe<SurrenderEvent>(turnController);
        EventBus.Subscribe<SurrenderEvent>(matchMenu);
    }

    private void Start()
    {
        FindObjectOfType<Grid>().InitializeGrid(gridSelectionController);
        EventBus.Publish(new StartNewTurnEvent());
    }
}
