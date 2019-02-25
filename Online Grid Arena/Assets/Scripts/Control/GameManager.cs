using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public sealed class GameManager : MonoBehaviour
{
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
    private List<ICharacterController> characterControllers;

    private void Awake()
    {
        Debug.Log(ToString() + " Awake() begin");

        EventBus.Reset();

        characterControllers = FindObjectsOfType<AbstractCharacter>().Select(x => x.Controller).ToList();

        // Initialize turn controller
        turnController = new TurnController(
            characterControllers,
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
        AbilityPanelController abilityPanelController = new AbilityPanelController(abilityPanel);
        EndTurnButton endTurnButton = FindObjectOfType<EndTurnButton>();

        hudController.SelectedStatPanel = statPanels[1].Controller;
        hudController.SelectedPlayerPanel = playerPanels[0];
        hudController.TargetStatPanel = statPanels[0].Controller;
        hudController.TargetPlayerPanel = playerPanels[1];
        hudController.AbilityPanelController = abilityPanelController;
        hudController.EndTurnButton = endTurnButton;

        // Initialize selection controllers
        gridSelectionController = new GridSelectionController();
        freeSelectionController = new FreeSelectionController(gridSelectionController);
        movementSelectionController = new MovementSelectionController(gridSelectionController);
        targetEnemyAbilitySelectionController = new TargetEnemyAbilitySelectionController(gridSelectionController);
        targetAllyAbilitySelectionController = new TargetAllyAbilitySelectionController(gridSelectionController);
        targetTileAbilitySelectionController = new TargetTileAbilitySelectionController(gridSelectionController);
        targetLineAbilitySelectionController = new TargetLineAbilitySelectionController(gridSelectionController);
        targetLineAOEAbilitySelectionController = new TargetLineAOEAbilitySelectionController(gridSelectionController);

        var selectionControllers = new Dictionary<string, ISelectionController>()
        {
            { "free", freeSelectionController },
            { "movement", movementSelectionController },
            { "target_enemy", targetEnemyAbilitySelectionController },
            { "target_ally", targetAllyAbilitySelectionController },
            { "target_tile", targetTileAbilitySelectionController },
            { "target_line", targetLineAbilitySelectionController },
            { "target_line_aoe", targetLineAOEAbilitySelectionController }
        };

        selectionManager = new SelectionManager(turnController, gridSelectionController, selectionControllers);

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
        EventBus.Subscribe<DeathEvent>(turnController);
        EventBus.Subscribe<EndMatchEvent>(endMatchMenu);
        EventBus.Subscribe<StartNewTurnEvent>(turnController);
        EventBus.Subscribe<SurrenderEvent>(turnController);
        EventBus.Subscribe<SurrenderEvent>(matchMenu);
        EventBus.Subscribe<UpdateSelectionModeEvent>(selectionManager);
        EventBus.Subscribe<DeselectSelectedTileEvent>(gridSelectionController);
        EventBus.Subscribe<SelectTileEvent>(gridSelectionController);
        EventBus.Subscribe<AbilitySelectedEvent>(abilityPanelController);
        EventBus.Subscribe<UpdateSelectionModeEvent>(abilityPanelController);
        EventBus.Subscribe<AbilityClickEvent>(inputManager);
        EventBus.Subscribe<SelectActivePlayerEvent>(turnController);

        // Pengwin's Ultimate must handle DeathEvent
        var pengwin = characterControllers.Find(x => x.Character.GetType().Equals(typeof(Pengwin)));
        EventBus.Subscribe<DeathEvent>((IEventSubscriber)pengwin.Abilities[3]);

        Debug.Log(ToString() + " Awake() end");
    }

    private void Start()
    {
        Debug.Log(ToString() + " Start() begin");

        FindObjectOfType<Grid>().InitializeGrid(gridSelectionController);
        EventBus.Publish(new StartNewTurnEvent());

        Debug.Log(ToString() + " Start() end");
    }
}