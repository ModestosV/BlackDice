using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;

public class Stage5Controller : AbstractStageController, IEventSubscriber
{
    private const String TUTORIAL_STEP_1 = "Click on Rocket Cat";
    private const String TUTORIAL_STEP_2 = "Press Q";
    private const String TUTORIAL_STEP_3 = "Attack Pengwin";
    private const String TUTORIAL_STEP_4 = "Check Buff\nPress End Turn";
    private const String TUTORIAL_STEP_5 = "Click on Pengwin";
    private const String TUTORIAL_STEP_6 = "Press E to Buff\nNearby Ally";
    private const String TUTORIAL_STEP_7 = "Check Buff\nClick on DefaultCharacter";
    private const String TUTORIAL_STEP_8 = "Check Buff\nPress End Turn";
    private const String TUTORIAL_STEP_9 = "Click on Pengwin";
    private const String TUTORIAL_STEP_10 = "Press R\nAttack Rocket Cat";
    private const String TUTORIAL_STEP_11 = "End Turn";
    private const String TUTORIAL_STEP_12 = "Click on Rocket Cat";
    private const String TUTORIAL_STEP_13 = "Check Buff stack 4\nEnd Turn";
    private const String STAGE_COMPLETE = "Stage Completed!\nRedirecting Tutorial";
    private const int STAGE_INDEX = 5;

    private ICharacterController rocketCat;
    private ICharacterController pengwin;
    private ICharacterController defaultCharacter;

    private List<Action> stepMethods = new List<Action>();

    private int currentStepIndex = 0;

    private int abilityIndexSelected = -1;
    private IHexTileController selectedTile = null;
    private SelectionMode selectionMode = SelectionMode.FREE;

    public Stage5Controller(ICharacterController rocketCat, ICharacterController pengwin, ICharacterController defaultCharacter)
    {
        this.rocketCat = rocketCat;
        this.pengwin = pengwin;
        this.defaultCharacter = defaultCharacter;

        rocketCat.CharacterStats["moves"].BaseValue = 0;
        pengwin.CharacterStats["moves"].BaseValue = 0;
        defaultCharacter.CharacterStats["moves"].BaseValue = 0;

        stepMethods.Add(() => this.handleStep1());
        stepMethods.Add(() => this.handleStep2());
        stepMethods.Add(() => this.handleStep3());
        stepMethods.Add(() => this.handleStep4());
        stepMethods.Add(() => this.handleStep5());
        stepMethods.Add(() => this.handleStep6());
        stepMethods.Add(() => this.handleStep7());
        stepMethods.Add(() => this.handleStep8());
        stepMethods.Add(() => this.handleStep9());
        stepMethods.Add(() => this.handleStep10());
        stepMethods.Add(() => this.handleStep11());
        stepMethods.Add(() => this.handleStep12());
        stepMethods.Add(() => CompleteStage(STAGE_INDEX));
    }

    private void handleStep1()
    {
        if (rocketCat.IsActive)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_2;
            currentStepIndex += 1;
        }
    }

    private void handleStep2()
    {
        if (rocketCat.IsActive)
        {
            if (abilityIndexSelected == 0)
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_3;
                currentStepIndex += 1;
            }
        }
        else
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_1;
            currentStepIndex = 0;
        }
    }

    private void handleStep3()
    {
        if (pengwin.CharacterStats["health"].CurrentValue < pengwin.CharacterStats["health"].BaseValue && selectionMode == SelectionMode.FREE)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_4;
            currentStepIndex += 1;
            selectedTile = null;
        }
    }

    private void handleStep4()
    {

    }

    private void handleStep5()
    {

    }

    private void handleStep6()
    {

    }

    private void handleStep7()
    {

    }

    private void handleStep8()
    {

    }

    private void handleStep9()
    {

    }

    private void handleStep10()
    {

    }

    private void handleStep11()
    {

    }

    private void handleStep12()
    {

    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();

        if (type == typeof(AbilitySelectedEvent))
        {
            var abilitySelectedEvent = (AbilitySelectedEvent)@event;

            abilityIndexSelected = abilitySelectedEvent.AbilityIndex;
        }

        if (type == typeof(UpdateSelectionModeEvent))
        {
            var selectionModeEvent = (UpdateSelectionModeEvent)@event;

            selectionMode = selectionModeEvent.SelectionMode;
        }

        stepMethods[currentStepIndex].Invoke();
    }
}