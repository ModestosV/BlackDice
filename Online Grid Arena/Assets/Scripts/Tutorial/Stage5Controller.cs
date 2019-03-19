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
    private const String TUTORIAL_STEP_10 = "Press R";
    private const String TUTORIAL_STEP_11 = "Attack Rocket Cat";
    private const String TUTORIAL_STEP_12 = "Click on Rocket Cat";
    private const String TUTORIAL_STEP_13 = "Check Buff stack 4\nEnd Turn";
    private const String STAGE_COMPLETE = "Stage Completed!\nRedirecting Tutorial";
    private const int STAGE_INDEX = 5;

    private ICharacterController rocketCat;
    private ICharacterController pengwin;
    private ICharacterController defaultCharacter;
    private GridSelectionController gridSelectionController;

    private List<Action> stepMethods = new List<Action>();

    private int currentStepIndex = 0;

    private int abilityIndexSelected = -1;
    private SelectionMode selectionMode = SelectionMode.FREE;
    private ArrowIndicator arrowIndicator;
    private bool endTurnEventTrigger = false;

    public Stage5Controller(ICharacterController rocketCat, ICharacterController pengwin, ICharacterController defaultCharacter, GridSelectionController gridSelectionController)
    {
        this.rocketCat = rocketCat;
        this.pengwin = pengwin;
        this.defaultCharacter = defaultCharacter;
        this.gridSelectionController = gridSelectionController;

        this.arrowIndicator = GameObject.FindWithTag("ArrowIndicator").GetComponent<ArrowIndicator>();

        this.rocketCat.CharacterStats["moves"].BaseValue = 0;
        this.pengwin.CharacterStats["moves"].BaseValue = 0;
        this.defaultCharacter.CharacterStats["moves"].BaseValue = 0;

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

    private void handleEndTurnAtWrongMoment()
    {
        if (endTurnEventTrigger)
        {
            if (currentStepIndex < 3)
            {
                restartToStep1();
            }
            else if (currentStepIndex < 7)
            {
                handleStep4();
            }

            endTurnEventTrigger = false;
            EventBus.Publish(new StartNewTurnEvent());
        }
    }

    private void restartToStep1()
    {
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_1;
        currentStepIndex = 0;
    }

    private void handleStep1()
    {
        if (rocketCat == gridSelectionController.GetSelectedCharacter())
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_2;
            currentStepIndex = 1;
        }
    }

    private void handleStep2()
    {
        if (rocketCat == gridSelectionController.GetSelectedCharacter())
        {
            if (abilityIndexSelected == 0)
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_3;
                currentStepIndex = 2;
            }
        }
        else
        {
            restartToStep1();
        }
    }

    private void handleStep3()
    {
        if (pengwin.CharacterStats["health"].CurrentValue < pengwin.CharacterStats["health"].BaseValue && selectionMode == SelectionMode.FREE)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_4;
            currentStepIndex = 3;
            arrowIndicator.Show();
        }
        else if (abilityIndexSelected != 0 && selectionMode == SelectionMode.ABILITY)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_2;
            currentStepIndex = 1;
        }
        else if (pengwin.CharacterStats["health"].CurrentValue.Equals(pengwin.CharacterStats["health"].BaseValue) && selectionMode != SelectionMode.ABILITY)
        {
            restartToStep1();
        }
    }

    private void handleStep4()
    {
        arrowIndicator.Hide();
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_5;
        currentStepIndex = 4;
    }

    private void handleStep5()
    {
        if (pengwin == gridSelectionController.GetSelectedCharacter())
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_6;
            currentStepIndex = 5;
        }
    }

    private void handleStep6()
    {
        if (abilityIndexSelected == 2)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_7;
            currentStepIndex = 6;
            arrowIndicator.Show();
        }
    }

    private void handleStep7()
    {
        if (defaultCharacter == gridSelectionController.GetSelectedCharacter())
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_8;
            currentStepIndex = 7;
        }
    }

    private void handleStep8()
    {
        arrowIndicator.Hide();
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_9;
        currentStepIndex = 8;
        EventBus.Publish(new StartNewTurnEvent());
    }

    private void handleStep9()
    {
        if (pengwin.IsActive)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_10;
            currentStepIndex += 1;
        }
            
    }

    private void handleStep10()
    {
        if (pengwin.IsActive)
        {
            if (abilityIndexSelected == 3)
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_11;
                currentStepIndex += 1;
            }
        }
        else
        {
            handleStep7();
        }

    }

    private void handleStep11()
    {
        if (rocketCat.CharacterStats["health"].CurrentValue < rocketCat.CharacterStats["health"].BaseValue && selectionMode == SelectionMode.FREE)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_12;
            currentStepIndex += 1;
            arrowIndicator.Show();
            EventBus.Publish(new StartNewTurnEvent());
        }
        else if (abilityIndexSelected != 3 && selectionMode == SelectionMode.ABILITY)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_11;
            currentStepIndex -= 1;
        }
        else if (pengwin.CharacterStats["health"].CurrentValue.Equals(pengwin.CharacterStats["health"].BaseValue) && selectionMode != SelectionMode.ABILITY)
        {
            handleStep7();
        }
    }

    private void handleStep12()
    {
        if (rocketCat.IsActive)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_12;
            currentStepIndex += 1;
        }
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

        if (type == typeof(StartNewTurnEvent))
        {
            endTurnEventTrigger = true;
        }

        if (endTurnEventTrigger && currentStepIndex != 0 && currentStepIndex != 4
            && currentStepIndex != 7 && currentStepIndex != 8)
        {
            handleEndTurnAtWrongMoment();
        }

        stepMethods[currentStepIndex].Invoke();
        endTurnEventTrigger = false;

        Debug.Log(currentStepIndex);
    }
}