using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public sealed class TutorialGameManager : MonoBehaviour
{
    [SerializeField] private int tutorialStageIndex;

    private List<Action> tutorialStageStartMethods = new List<Action>();

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
    private List<IPlayer> players;
    private List<CharacterPanel> characterPanels;

    private void Awake()
    {
        Debug.Log(ToString() + " Awake() begin");

        Debug.Log(ToString() + " Awake() end");
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

        // Initialize Menus
        endMatchMenu = FindObjectOfType<EndMatchMenu>();
        matchMenu = FindObjectOfType<MatchMenu>();

        // Initialize HUD
        StatPanel[] statPanels = FindObjectsOfType<StatPanel>();
        PlayerPanel[] playerPanels = FindObjectsOfType<PlayerPanel>();
        AbilityPanel abilityPanel = FindObjectOfType<AbilityPanel>();
        AbilityPanelController abilityPanelController = new AbilityPanelController(abilityPanel);

        hudController = new HUDController(statPanels[1].Controller, playerPanels[0], statPanels[0].Controller, playerPanels[1], abilityPanelController, FindObjectOfType<EndTurnButton>());

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
        EventBus.Subscribe<StartNewTurnEvent>(turnController);
        EventBus.Subscribe<UpdateSelectionModeEvent>(selectionManager);
        EventBus.Subscribe<DeselectSelectedTileEvent>(gridSelectionController);
        EventBus.Subscribe<SelectTileEvent>(gridSelectionController);
        EventBus.Subscribe<SelectActivePlayerEvent>(turnController);
        EventBus.Subscribe<SelectTileEvent>(turnController);
        EventBus.Subscribe<StartNewTurnEvent>(hudController);

        foreach (CharacterTile tile in FindObjectsOfType(typeof(CharacterTile)))
        {
            EventBus.Subscribe<ActiveCharacterEvent>(tile);
            EventBus.Subscribe<ExhaustCharacterEvent>(tile);
            EventBus.Subscribe<NewRoundEvent>(tile);
            EventBus.Subscribe<StatusEffectEvent>(tile);
        }

        // Start Game
        Grid grid = FindObjectOfType<Grid>();
        grid.InitializeGrid(gridSelectionController);

        EventBus.Publish(new StartNewTurnEvent());

        Stage2Controller stage2Controller = new Stage2Controller(characterControllers[0], grid.gridController.GetTile((5, -13, 8)));
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
}