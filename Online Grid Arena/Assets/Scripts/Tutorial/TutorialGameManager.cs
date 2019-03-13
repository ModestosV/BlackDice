using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public sealed class TutorialGameManager : MonoBehaviour
{
    [SerializeField] private int tutorialStageIndex;

    private List<Action> tutorialStageAwakeMethods = new List<Action>();

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

        EventBus.Reset();

        tutorialStageAwakeMethods.Add(() => this.AwakeStageMovement());
        tutorialStageAwakeMethods.Add(() => this.AwakeStageAttack());

        tutorialStageAwakeMethods[this.tutorialStageIndex].Invoke();

        Debug.Log(ToString() + " Awake() end");
    }

    private void AwakeStageMovement()
    {

    }

    private void AwakeStageAttack()
    {
        // Marc's Tutorial Stage
    }

    private void Start()
    {
        Debug.Log(ToString() + " Start() begin");

        FindObjectOfType<Grid>().InitializeGrid(gridSelectionController);
        EventBus.Publish(new StartNewTurnEvent());

        Debug.Log(ToString() + " Start() end");
    }
}