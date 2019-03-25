using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;

public class Stage5Controller : AbstractStageController, IEventSubscriber
{
    private const string TUTORIAL_STEP_1 = "Click on Rocket Cat";
    private const string TUTORIAL_STEP_2 = "Press Q";
    private const string TUTORIAL_STEP_3 = "Attack Pengwin";
    private const string TUTORIAL_STEP_4 = "Check Buff";
    private const string TUTORIAL_STEP_5 = "Click on Pengwin";
    private const string TUTORIAL_STEP_6 = "Press E to Buff\nNearby Ally";
    private const string TUTORIAL_STEP_7 = "Check Buff";
    private const string TUTORIAL_STEP_8 = "Click on DefaultCharacter";
    private const string TUTORIAL_STEP_9 = "Check Buff";
    private const string TUTORIAL_STEP_10 = "Click on Pengwin";
    private const string TUTORIAL_STEP_11 = "Press R";
    private const string TUTORIAL_STEP_12 = "Attack Rocket Cat";
    private const string TUTORIAL_STEP_13 = "Click on Rocket Cat";
    private const string TUTORIAL_STEP_14 = "Check Buff stack 4";
    protected const string STAGE_FAILED = "Stage Failed!\nWrong attack used!\nRedirecting Tutorial";
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
    private bool buffChecked = false;

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
        stepMethods.Add(() => this.handleStep13());
        stepMethods.Add(() => this.handleStep14());
        stepMethods.Add(() => CompleteStage(STAGE_INDEX));
    }

    private void stageFailed()
    {
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = STAGE_FAILED;
        EventBus.Publish(new SurrenderEvent());
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

            if (!rocketCat.CanUseAbility(0))
            {
                stageFailed();
            }
        }
    }

    private void handleStep2()
    {
        if (rocketCat == gridSelectionController.GetSelectedCharacter())
        {
            if (!rocketCat.CanUseAbility(0))
            {
                stageFailed();
            }

            if (abilityIndexSelected == 0)
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_3;
                currentStepIndex = 2;
            }
            else
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_2;
            }
        }
    }

    private void handleStep3()
    {
        if (pengwin.CharacterStats["health"].CurrentValue < pengwin.CharacterStats["health"].BaseValue && selectionMode == SelectionMode.FREE)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_4;
            currentStepIndex = 3;
            arrowIndicator.Show();
            abilityIndexSelected = -1;
        }
        else if (pengwin.CharacterStats["health"].CurrentValue.Equals(pengwin.CharacterStats["health"].BaseValue) && selectionMode != SelectionMode.ABILITY)
        {
            restartToStep1();
        }
    }

    private void handleStep4()
    {
        if (buffChecked)
        {
            arrowIndicator.Hide();
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_5;
            currentStepIndex = 4;
            EventBus.Publish(new StartNewTurnEvent());
        }
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
        if (abilityIndexSelected != 2)
        {
            handleStep5();
            abilityIndexSelected = -1;
        }
        else if (rocketCat.CharacterStats["health"].CurrentValue < rocketCat.CharacterStats["health"].BaseValue)
        {
            stageFailed();
        }
        else if (pengwin.CharacterStats["defense"].CurrentValue.Equals(120))
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_7;
            currentStepIndex = 6;
            arrowIndicator.Show();
        }
    }

    private void handleStep7()
    {
        if (buffChecked)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_8;
            currentStepIndex = 7;
            arrowIndicator.Hide();
        }
    }

    private void handleStep8()
    {
        if (defaultCharacter == gridSelectionController.GetSelectedCharacter())
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_9;
            currentStepIndex = 8;
            arrowIndicator.Show();
        }
    }

    private void handleStep9()
    {
        if (buffChecked)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_10;
            currentStepIndex = 9;
            EventBus.Publish(new StartNewTurnEvent());
            pengwin.Refresh();
            pengwin.CharacterState = CharacterState.UNUSED;
            EventBus.Publish(new NewRoundEvent(pengwin));
            EventBus.Publish(new StartNewTurnEvent());

        }
    }

    private void handleStep10()
    {
        if (pengwin == gridSelectionController.GetSelectedCharacter())
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_11;
            currentStepIndex = 10;
        }
    }

    private void handleStep11()
    {
        if (pengwin == gridSelectionController.GetSelectedCharacter())
        {
            if (abilityIndexSelected == 3)
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_12;
                currentStepIndex = 11;
            }
        }
    }

    private void handleStep12()
    {
        if (rocketCat.CharacterStats["health"].CurrentValue < rocketCat.CharacterStats["health"].BaseValue && selectionMode == SelectionMode.FREE)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_13;
            currentStepIndex = 12;
        }
        else if (abilityIndexSelected != 3 && selectionMode == SelectionMode.ABILITY)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_11;
            currentStepIndex = 11;
        }
    }

    private void handleStep13()
    {
        if (rocketCat == gridSelectionController.GetSelectedCharacter())
        {
            arrowIndicator.Show();
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_14;
            currentStepIndex = 13;
        }
    }

    private void handleStep14()
    {
        if (buffChecked)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = STAGE_COMPLETE;
            CompleteStage(STAGE_INDEX);
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

        if (type == typeof(BuffCheckEvent))
        {
            buffChecked = true;
        }

        stepMethods[currentStepIndex].Invoke();
        buffChecked = false;

        Debug.Log(currentStepIndex);
    }
}