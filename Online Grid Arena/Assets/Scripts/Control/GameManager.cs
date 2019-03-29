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
    private TargetTileAOEAbilitySelectionController targetTileAOEAbilitySelectionController;
    private TargetTileAbilitySelectionController targetTileAbilitySelectionController;
    private TargetLineAbilitySelectionController targetLineAbilitySelectionController;
    private TargetLineAOEAbilitySelectionController targetLineAOEAbilitySelectionController;
    private TargetCharacterLineAbilitySelectionController targetCharacterLineAbilitySelectionController;
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

        EventBus.Reset();
        
        // Get all characters from scene
        characterControllers = FindObjectsOfType<AbstractCharacter>().Select(x => x.Controller).ToList();

        //Initialize players
        players = new List<IPlayer>() { new Player("1"), new Player("2") };

        foreach(ICharacterController characterController in characterControllers)
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

        for(int i = 0; i < characterPanels[0].CharacterTiles.Length; i++)
        {
            characterPanels[0].CharacterTiles[i].Setup(players[0].CharacterControllers[i]);
            characterPanels[1].CharacterTiles[i].Setup(players[1].CharacterControllers[i]);
        }

        // Initialize turn controller
        turnController = new TurnController(players);

        // Initialize Menus
        endMatchMenu = FindObjectOfType<EndMatchMenu>();
        matchMenu = FindObjectOfType<MatchMenu>();
        controlsMenu = FindObjectOfType<ControlsMenu>();

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
        targetTileAOEAbilitySelectionController = new TargetTileAOEAbilitySelectionController(gridSelectionController);
        targetLineAbilitySelectionController = new TargetLineAbilitySelectionController(gridSelectionController);
        targetLineAOEAbilitySelectionController = new TargetLineAOEAbilitySelectionController(gridSelectionController);
        targetCharacterLineAbilitySelectionController = new TargetCharacterLineAbilitySelectionController(gridSelectionController);

        var selectionControllers = new Dictionary<string, ISelectionController>()
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
            EventBus.Subscribe<AbilityUsedEvent>(tile);
            EventBus.Subscribe<StartNewTurnEvent>(tile);
        }

        foreach (AbstractCharacter c in FindObjectsOfType<AbstractCharacter>())
        {
            EventBus.Subscribe<ExhaustCharacterEvent>(c);
            EventBus.Subscribe<NewRoundEvent>(c);
            EventBus.Subscribe<SelectCharacterEvent>(c);
            EventBus.Subscribe<SelectActivePlayerEvent>(c);
            EventBus.Subscribe<StartNewTurnEvent>(c);
        }
        
        EventBus.Subscribe<SelectActivePlayerEvent>(FindObjectOfType<TurnIndicator>());

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
