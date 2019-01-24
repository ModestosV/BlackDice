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
    private TargetTileAbilitySelectionController targetTileAbilitySelectionController;
    private TargetLineAbilitySelectionController targetLineAbilitySelectionController;
    private TargetLineAOEAbilitySelectionController targetLineAOEAbilitySelectionController;
    private SelectionManager selectionManager;

    private InputManager inputManager;
    private EndMatchMenu endMatchMenu;
    private MatchMenu matchMenu;

    private void Awake()
    {
        EventBus.Reset();

        // Initialize turn controller
        turnController = new TurnController(
            FindObjectsOfType<AbstractCharacter>().Select(x => x.Controller).ToList(),
            new List<ICharacterController>(),
            FindObjectOfType<TurnPanel>().Controller);

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
        hudController.AbilityPanelController = new AbilityPanelController(abilityPanel);

        // Initialize grid
        gridSelectionController = new GridSelectionController();

        // Initialize selection controllers
        selectionManager = new SelectionManager()
        {
            GridSelectionController = gridSelectionController,
            TurnController = turnController
        };

        freeSelectionController = new FreeSelectionController()
        {
            GridSelectionController = gridSelectionController,
            TurnController = turnController
        };

        movementSelectionController = new MovementSelectionController()
        {
            GridSelectionController = gridSelectionController
        };

        targetEnemyAbilitySelectionController = new TargetEnemyAbilitySelectionController()
        {
            GridSelectionController = gridSelectionController
        };

        targetAllyAbilitySelectionController = new TargetAllyAbilitySelectionController()
        {
            GridSelectionController = gridSelectionController
        };

        targetTileAbilitySelectionController = new TargetTileAbilitySelectionController()
        {
            GridSelectionController = gridSelectionController
        };

        targetLineAbilitySelectionController = new TargetLineAbilitySelectionController()
        {
            GridSelectionController = gridSelectionController
        };

        targetLineAOEAbilitySelectionController = new TargetLineAOEAbilitySelectionController()
        {
            GridSelectionController = gridSelectionController
        };

        selectionManager.SelectionControllers = new Dictionary<string, ISelectionController>()
        {
            { "free", freeSelectionController },
            { "movement", movementSelectionController },
            { "target_enemy", targetEnemyAbilitySelectionController },
            { "target_ally", targetAllyAbilitySelectionController },
            { "target_tile", targetTileAbilitySelectionController },
            { "target_line", targetLineAbilitySelectionController },
            { "target_line_aoe", targetLineAOEAbilitySelectionController}
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

        // Initialize Event Subscribing
        EventBus.Subscribe<AbilityClickEvent>(inputManager);
        EventBus.Subscribe<DeathEvent>(turnController);
        EventBus.Subscribe<EndMatchEvent>(endMatchMenu);
        EventBus.Subscribe<StartNewTurnEvent>(turnController);
        EventBus.Subscribe<SurrenderEvent>(turnController);
        EventBus.Subscribe<SurrenderEvent>(matchMenu);
        EventBus.Subscribe<UpdateSelectionModeEvent>(selectionManager);
        EventBus.Subscribe<DeselectSelectedTileEvent>(gridSelectionController);
        EventBus.Subscribe<SelectTileEvent>(gridSelectionController);
    }

    private void Start()
    {
        FindObjectOfType<Grid>().InitializeGrid(gridSelectionController);
        EventBus.Publish(new StartNewTurnEvent());
    }
}
