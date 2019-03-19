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
    private TargetTileAOEAbilitySelectionController targetTileAOEAbilitySelectionController;
    private TargetLineAbilitySelectionController targetLineAbilitySelectionController;
    private TargetLineAOEAbilitySelectionController targetLineAOEAbilitySelectionController;
    private TargetCharacterLineAbilitySelectionController targetCharacterLineAbilitySelectionController;
    private Dictionary<string, ISelectionController> selectionControllers;
    private SelectionManager selectionManager;

    private InputManager inputManager;
    private EndMatchMenu endMatchMenu;
    private MatchMenu matchMenu;
    private ControlsMenu controlsMenu;

    private List<ICharacterController> characterControllers;
    private List<IPlayer> players;
    private List<CharacterPanel> characterPanels;

    private Grid grid;

    private void Awake()
    {
        Debug.Log(ToString() + " Awake() begin");

        // Get all characters from scene
        characterPanels = FindObjectsOfType<CharacterPanel>().ToList();

        // Create to players
        players = new List<IPlayer>() { new Player("1"), new Player("2") };

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
        targetTileAOEAbilitySelectionController = new TargetTileAOEAbilitySelectionController(gridSelectionController);
        targetLineAbilitySelectionController = new TargetLineAbilitySelectionController(gridSelectionController);
        targetLineAOEAbilitySelectionController = new TargetLineAOEAbilitySelectionController(gridSelectionController);
        targetCharacterLineAbilitySelectionController = new TargetCharacterLineAbilitySelectionController(gridSelectionController);

        selectionControllers = new Dictionary<string, ISelectionController>()
        {
            { "free", freeSelectionController },
            { "movement", movementSelectionController },
            { "target_enemy", targetEnemyAbilitySelectionController },
            { "target_ally", targetAllyAbilitySelectionController },
            { "target_tile_aoe", targetTileAOEAbilitySelectionController },
            { "target_line", targetLineAbilitySelectionController },
            { "target_line_aoe", targetLineAOEAbilitySelectionController },
            { "target_tile", targetTileAbilitySelectionController},
            { "target_character_line", targetCharacterLineAbilitySelectionController}
        };

        // Initialize input manager
        inputManager = FindObjectOfType<InputManager>();

        // Initialize Grid
        grid = FindObjectOfType<Grid>();

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
        EventBus.Subscribe<AbilitySelectedEvent>(abilityPanelController);
        EventBus.Subscribe<AbilityClickEvent>(inputManager);
        EventBus.Subscribe<StartNewTurnEvent>(hudController);
        EventBus.Subscribe<StageCompletedEvent>(this);
        EventBus.Subscribe<SurrenderEvent>(this);

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

    private void StartGame()
    {
        grid.InitializeGrid(gridSelectionController);

        EventBus.Publish(new StartNewTurnEvent());
    }

    private void StartStageMovement()
    {
        //Set players and character's panels
        characterControllers = FindObjectsOfType<AbstractCharacter>().Select(x => x.Controller).ToList();
        players[0].AddCharacterController(characterControllers[0]);
        characterPanels[0].CharacterTiles[0].Setup(players[0].CharacterControllers[0]);

        // Initialize turn controller
        turnController = new TurnController(players);

        selectionManager = new SelectionManager(turnController, gridSelectionController, selectionControllers);
        inputManager.SelectionManager = selectionManager;

        // Initialize HUD controller
        hudController = new HUDController(statPanels[1].Controller, playerPanels[0], statPanels[0].Controller, playerPanels[1], abilityPanelController, FindObjectOfType<EndTurnButton>());

        // Initialize characters
        foreach (ICharacterController character in characterControllers)
        {
            character.HUDController = hudController;
        }

        InitializeSharedSubscriptions();

        StartGame();

        // this needs to be created after because it must not catch the first StartNewTurnEvent!
        Stage2Controller stage2Controller = new Stage2Controller(characterControllers[0], grid.GridController.GetTile((5, -13, 8)));

        EventBus.Subscribe<StartNewTurnEvent>(stage2Controller);
        EventBus.Subscribe<UpdateSelectionModeEvent>(stage2Controller);
        EventBus.Subscribe<SelectActivePlayerEvent>(stage2Controller);
        EventBus.Subscribe<DeselectSelectedTileEvent>(stage2Controller);
        EventBus.Subscribe<SelectTileEvent>(stage2Controller);
    }

    private void StartStageAttack()
    {
        // Marc's Tutorial Stage
    }

    private void StartStageHeal()
    {
        // Heal Stage 4
    }

    private void StartStageBuff()
    {
        //Set players and character's panels
        characterControllers = FindObjectsOfType<AbstractCharacter>().Select(x => x.Controller).ToList();
        players[0].AddCharacterController(characterControllers[0]);
        players[1].AddCharacterController(characterControllers[1]);
        players[1].AddCharacterController(characterControllers[2]);
        characterPanels[1].CharacterTiles[0].Setup(players[0].CharacterControllers[0]);
        characterPanels[0].CharacterTiles[0].Setup(players[1].CharacterControllers[1]);
        characterPanels[0].CharacterTiles[1].Setup(players[1].CharacterControllers[0]);

        // Initialize turn controller
        turnController = new TurnController(players);

        selectionManager = new SelectionManager(turnController, gridSelectionController, selectionControllers);
        inputManager.SelectionManager = selectionManager;

        // Initialize HUD controller
        hudController = new HUDController(statPanels[1].Controller, playerPanels[0], statPanels[0].Controller, playerPanels[1], abilityPanelController, FindObjectOfType<EndTurnButton>());

        // Initialize characters
        foreach (ICharacterController character in characterControllers)
        {
            character.HUDController = hudController;
        }

        InitializeSharedSubscriptions();

        StartGame();

        Stage5Controller stage5Controller = new Stage5Controller(characterControllers[0], characterControllers[2], characterControllers[1], gridSelectionController);
        EventBus.Subscribe<StartNewTurnEvent>(stage5Controller);
        EventBus.Subscribe<AbilitySelectedEvent>(stage5Controller);
        EventBus.Subscribe<UpdateSelectionModeEvent>(stage5Controller);
        EventBus.Subscribe<SelectTileEvent>(stage5Controller);
        EventBus.Subscribe<ExhaustCharacterEvent>(stage5Controller);
    }

    private void Start()
    {
        Debug.Log(ToString() + " Start() begin");

        tutorialStageStartMethods.Add(() => this.StartStageMovement());
        tutorialStageStartMethods.Add(() => this.StartStageAttack());
        tutorialStageStartMethods.Add(() => this.StartStageHeal());
        tutorialStageStartMethods.Add(() => this.StartStageBuff());

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
            Invoke("ExitStage", 3);
        }
    }
}