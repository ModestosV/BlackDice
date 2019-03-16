﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public sealed class Stage3Manager : MonoBehaviour, IEventSubscriber
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

    //HUD Objects
    private StatPanel[] statPanels;
    private PlayerPanel[] playerPanels;
    private AbilityPanel abilityPanel;
    private AbilityPanelController abilityPanelController;

    private InputManager inputManager;
    private EndMatchMenu endMatchMenu;
    private MatchMenu matchMenu;
    private AbstractCharacter[] characters;
    private List<ICharacterController> characterControllers;
    private List<IPlayer> players;
    private List<CharacterPanel> characterPanels;

    private Stage3Controller stageController;
    private ControlsMenu controlsMenu;
    private Dictionary<string, ISelectionController> selectionControllers;

    public void Handle(IEvent @event)
    {
        throw new System.NotImplementedException();
    }

    public void CompleteStage()
    {
        EventBus.Publish(new StageCompletedEvent(2));
        ExitStage();
    }

    public void ExitStage()
    {
        SceneManager.LoadScene(2);
    }

    private void Awake()
    {
        Debug.Log(ToString() + " Awake() begin");

        EventBus.Reset();

        // Initialize Menus
        endMatchMenu = FindObjectOfType<EndMatchMenu>();
        matchMenu = FindObjectOfType<MatchMenu>();
        controlsMenu = FindObjectOfType<ControlsMenu>();

        // Initialize HUD
        statPanels = FindObjectsOfType<StatPanel>();
        playerPanels = FindObjectsOfType<PlayerPanel>();
        abilityPanel = FindObjectOfType<AbilityPanel>();
        abilityPanelController = new AbilityPanelController(abilityPanel);
        characters = FindObjectsOfType<AbstractCharacter>();

        // Initialize selection controllers
        gridSelectionController = new GridSelectionController();
        freeSelectionController = new FreeSelectionController(gridSelectionController);
        movementSelectionController = new MovementSelectionController(gridSelectionController);
        targetEnemyAbilitySelectionController = new TargetEnemyAbilitySelectionController(gridSelectionController);
        targetAllyAbilitySelectionController = new TargetAllyAbilitySelectionController(gridSelectionController);
        targetTileAbilitySelectionController = new TargetTileAbilitySelectionController(gridSelectionController);
        targetLineAbilitySelectionController = new TargetLineAbilitySelectionController(gridSelectionController);
        targetLineAOEAbilitySelectionController = new TargetLineAOEAbilitySelectionController(gridSelectionController);

        selectionControllers = new Dictionary<string, ISelectionController>()
        {
            { "free", freeSelectionController },
            { "movement", movementSelectionController },
            { "target_enemy", targetEnemyAbilitySelectionController },
            { "target_ally", targetAllyAbilitySelectionController },
            { "target_tile", targetTileAbilitySelectionController },
            { "target_line", targetLineAbilitySelectionController },
            { "target_line_aoe", targetLineAOEAbilitySelectionController }
        };

        Debug.Log(ToString() + " Awake() end");
    }

    private void Start()
    {
        Debug.Log(ToString() + " Start() begin");

        // Get all characters from scene
        characterControllers = characters.Select(x => x.Controller).ToList();

        //Initialize players
        players = new List<IPlayer>() { new Player("1"), new Player("2") };

        foreach (ICharacterController characterController in characterControllers)
        {
            if (characterController.Owner.Equals("1"))
            {
                players[0].AddCharacterController(characterController);
            }
            else
            {
                players[1].AddCharacterController(characterController);
            }
        }

        //Initialize character panels
        characterPanels = FindObjectsOfType<CharacterPanel>().ToList();
        Debug.Log("Character Panels size: " + characterPanels.Count);
        Debug.Log("Character tiles size: " + characterPanels[0].CharacterTiles.Length);

        for (int i = 0; i < characterPanels[0].CharacterTiles.Length; i++)
        {
            characterPanels[0].CharacterTiles[i].Setup(players[0].CharacterControllers[i]);
            characterPanels[1].CharacterTiles[i].Setup(players[1].CharacterControllers[i]);
        }

        // Initialize turn controller
        turnController = new TurnController(players);

        // Initialize HUD Controller
        hudController = new HUDController(statPanels[1].Controller, playerPanels[0], statPanels[0].Controller, playerPanels[1], abilityPanelController, FindObjectOfType<EndTurnButton>());

        // Initialize stage controller
        stageController = new Stage3Controller(characterControllers, FindObjectsOfType<ArrowIndicator>(), players);

        selectionManager = new SelectionManager(turnController, gridSelectionController, selectionControllers);

        // Initialize input manager
        inputManager = FindObjectOfType<InputManager>();
        inputManager.SelectionManager = selectionManager;

        // Initialize characters
        foreach (ICharacterController character in characterControllers)
        {
            character.HUDController = hudController;
        }

        // Initialize Event Subscribing
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

        EventBus.Subscribe<AbilitySelectedEvent>(abilityPanelController);
        EventBus.Subscribe<UpdateSelectionModeEvent>(abilityPanelController);

        EventBus.Subscribe<AbilityClickEvent>(inputManager);

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

        //EventBus.Subscribe<SelectActivePlayerEvent>(FindObjectOfType<TurnIndicator>());

        // Events for the current tutorial stage
        EventBus.Subscribe<UpdateSelectionModeEvent>(stageController);
        EventBus.Subscribe<StartNewTurnEvent>(stageController);
        EventBus.Subscribe<ActiveCharacterEvent>(stageController);
        EventBus.Subscribe<AbilitySelectedEvent>(stageController);
        EventBus.Subscribe<AbilityClickEvent>(stageController);
        EventBus.Subscribe<ExhaustCharacterEvent>(stageController);
        EventBus.Subscribe<SelectCharacterEvent>(stageController);

        // Pengwin's Ultimate must handle DeathEvent
        var pengwin = characterControllers.Find(x => x.Character.GetType().Equals(typeof(Pengwin)));
        EventBus.Subscribe<DeathEvent>((IEventSubscriber)pengwin.Abilities[3]);

        FindObjectOfType<Grid>().InitializeGrid(gridSelectionController);
        EventBus.Publish(new StartNewTurnEvent());

        Debug.Log(ToString() + " Start() end");
    }
}