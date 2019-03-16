using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public sealed class TutorialGameManager : MonoBehaviour, IEventSubscriber
{
    [SerializeField] private int tutorialStageIndex;

    private List<Action> tutorialStageStartMethods = new List<Action>();

    private TurnController turnController;
    private HUDController hudController;
    private GridSelectionController gridSelectionController;

    private StatPanel[] statPanels;
    private PlayerPanel[] playerPanels;
    private AbilityPanel abilityPanel;
    private AbilityPanelController abilityPanelController;

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
    private ControlsMenu controlsMenu;

    private List<ICharacterController> characterControllers;
    private List<IPlayer> players;
    private List<CharacterPanel> characterPanels;

    private void Awake()
    {
        Debug.Log(ToString() + " Awake() begin");

        // Initialize Menus
        endMatchMenu = FindObjectOfType<EndMatchMenu>();
        matchMenu = FindObjectOfType<MatchMenu>();
        controlsMenu = FindObjectOfType<ControlsMenu>();

        // Initialize HUD
        statPanels = FindObjectsOfType<StatPanel>();
        playerPanels = FindObjectsOfType<PlayerPanel>();
        abilityPanel = FindObjectOfType<AbilityPanel>();
        abilityPanelController = new AbilityPanelController(abilityPanel);

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

        Debug.Log(ToString() + " Awake() end");
    }

    private void InitializeSharedSubscriptions()
    {
        // Shared code between all tutorials
        EventBus.Subscribe<DeathEvent>(turnController);
        EventBus.Subscribe<StartNewTurnEvent>(turnController);
        EventBus.Subscribe<SurrenderEvent>(turnController);
        EventBus.Subscribe<SelectTileEvent>(turnController);
        EventBus.Subscribe<ActiveCharacterEvent>(turnController);

        EventBus.Subscribe<EndMatchEvent>(endMatchMenu);

        EventBus.Subscribe<SurrenderEvent>(matchMenu);
        EventBus.Subscribe<EscapePressedEvent>(matchMenu);

        EventBus.Subscribe<EscapePressedEvent>(controlsMenu);

        EventBus.Subscribe<UpdateSelectionModeEvent>(selectionManager);

        EventBus.Subscribe<DeselectSelectedTileEvent>(gridSelectionController);
        EventBus.Subscribe<SelectTileEvent>(gridSelectionController);
        EventBus.Subscribe<UpdateSelectionModeEvent>(abilityPanelController);

        EventBus.Subscribe<StartNewTurnEvent>(hudController);

        foreach (CharacterTile tile in FindObjectsOfType<CharacterTile>())
        {
            EventBus.Subscribe<DeathEvent>(tile);
            EventBus.Subscribe<SelectCharacterEvent>(tile);
            EventBus.Subscribe<ExhaustCharacterEvent>(tile);
            EventBus.Subscribe<NewRoundEvent>(tile);
            EventBus.Subscribe<StatusEffectEvent>(tile);
        }

        foreach (AbstractCharacter c in FindObjectsOfType<AbstractCharacter>())
        {
            EventBus.Subscribe<ExhaustCharacterEvent>(c);
            EventBus.Subscribe<NewRoundEvent>(c);
            EventBus.Subscribe<SelectCharacterEvent>(c);
            EventBus.Subscribe<SelectActivePlayerEvent>(c);
            EventBus.Subscribe<StartNewTurnEvent>(c);
        }
    }

    private void StartStageMovement()
    {
        // Get all characters from scene
        characterControllers = FindObjectsOfType<AbstractCharacter>().Select(x => x.Controller).ToList();

        //Initialize players
        players = new List<IPlayer>() { new Player("1"), new Player("2") };
        players[0].AddCharacterController(characterControllers[0]);

        //Initialize character panels
        characterPanels = FindObjectsOfType<CharacterPanel>().ToList();

        Debug.Log("Character Panels size: " + characterPanels.Count);
        Debug.Log("Character tiles size: " + characterPanels[0].CharacterTiles.Length);

        characterPanels[0].CharacterTiles[0].Setup(players[0].CharacterControllers[0]);

        // Initialize turn controller
        turnController = new TurnController(players);

        hudController = new HUDController(statPanels[1].Controller, playerPanels[0], statPanels[0].Controller, playerPanels[1], abilityPanelController, FindObjectOfType<EndTurnButton>());

        // Initialize characters
        List<ICharacterController> characters = FindObjectsOfType<AbstractCharacter>().Select(x => x.Controller).ToList();
        foreach (ICharacterController character in characters)
        {
            character.HUDController = hudController;
        }

        InitializeSharedSubscriptions();

        // Start Game
        Grid grid = FindObjectOfType<Grid>();
        grid.InitializeGrid(gridSelectionController);

        EventBus.Publish(new StartNewTurnEvent());

        // this needs to be created after because it must not catch the first StartNewTurnEvent!
        Stage2Controller stage2Controller = new Stage2Controller(characterControllers[0], grid.gridController.GetTile((5, -13, 8)));

        EventBus.Subscribe<StartNewTurnEvent>(stage2Controller);
        EventBus.Subscribe<UpdateSelectionModeEvent>(stage2Controller);
        EventBus.Subscribe<SelectActivePlayerEvent>(stage2Controller);
        EventBus.Subscribe<DeselectSelectedTileEvent>(stage2Controller);
        EventBus.Subscribe<SelectTileEvent>(stage2Controller);

        EventBus.Subscribe<StageCompletedEvent>(this);
        EventBus.Subscribe<SurrenderEvent>(this);
    }

    private void StartStageAttack()
    {
        // Marc's Tutorial Stage
    }

    private void Start()
    {
        Debug.Log(ToString() + " Start() begin");

        tutorialStageStartMethods.Add(() => this.StartStageMovement());
        tutorialStageStartMethods.Add(() => this.StartStageAttack());

        tutorialStageStartMethods[this.tutorialStageIndex].Invoke();

        Debug.Log(ToString() + " Start() end");
    }

    public void ExitStage()
    {
        SceneManager.LoadScene(2);
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();

        if (type == typeof(StageCompletedEvent))
        {
            Invoke("ExitStage", 3);
        }

        if (type == typeof(SurrenderEvent))
        {
            ExitStage();
        }
    }
}