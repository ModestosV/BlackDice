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
    private const string STAGE_FAILED = "Stage Failed!\nWrong attack used!\nRedirecting Tutorial";
    private const int STAGE_INDEX = 5;

    private readonly ICharacterController rocketCat;
    private readonly ICharacterController pengwin;
    private readonly ICharacterController defaultCharacter;
    private readonly GridSelectionController gridSelectionController;

    private readonly List<Action> stepMethods = new List<Action>();

    private int currentStepIndex = 0;

    private int abilityIndexSelected = -1;
    private SelectionMode selectionMode = SelectionMode.FREE;
    private readonly ArrowIndicator arrowIndicator;
    private bool buffChecked = false;

    public Stage5Controller(ICharacterController rocketCat, ICharacterController pengwin, ICharacterController defaultCharacter, GridSelectionController gridSelectionController)
    {
        this.rocketCat = rocketCat;
        this.pengwin = pengwin;
        this.defaultCharacter = defaultCharacter;
        this.gridSelectionController = gridSelectionController;

        this.arrowIndicator = GameObject.FindWithTag("ArrowIndicator").GetComponent<ArrowIndicator>();

        this.rocketCat.CharacterStats["moves"].BaseValue = 0;
        this.rocketCat.CharacterStats["health"].BaseValue = 140;
        this.rocketCat.CharacterStats["health"].CurrentValue = 140;
        this.pengwin.CharacterStats["moves"].BaseValue = 0;
        this.defaultCharacter.CharacterStats["moves"].BaseValue = 0;

        stepMethods.Add(() => this.HandleStep1());
        stepMethods.Add(() => this.HandleStep2());
        stepMethods.Add(() => this.HandleStep3());
        stepMethods.Add(() => this.HandleStep4());
        stepMethods.Add(() => this.HandleStep5());
        stepMethods.Add(() => this.HandleStep6());
        stepMethods.Add(() => this.HandleStep7());
        stepMethods.Add(() => this.HandleStep8());
        stepMethods.Add(() => this.HandleStep9());
        stepMethods.Add(() => this.HandleStep10());
        stepMethods.Add(() => this.HandleStep11());
        stepMethods.Add(() => this.HandleStep12());
        stepMethods.Add(() => this.HandleStep13());
        stepMethods.Add(() => this.HandleStep14());
        stepMethods.Add(() => CompleteStage(STAGE_INDEX));
    }

    private void StageFailed()
    {
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = STAGE_FAILED;
        EventBus.Publish(new SurrenderEvent());
    }

    private void HandleStep1()
    {
        if (rocketCat == gridSelectionController.GetSelectedCharacter())
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_2;
            currentStepIndex = 1;

            if (rocketCat.IsExhausted())
            {
                StageFailed();
            }
        }
    }

    private void HandleStep2()
    {
        if (rocketCat == gridSelectionController.GetSelectedCharacter())
        {
            if (abilityIndexSelected == 0)
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_3;
                currentStepIndex = 2;
            }
            else if (rocketCat.IsExhausted())
            {
                StageFailed();
            }
        }
    }

    private void HandleStep3()
    {
        if (pengwin.CharacterStats["health"].CurrentValue < pengwin.CharacterStats["health"].BaseValue && selectionMode == SelectionMode.FREE)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_4;
            currentStepIndex = 3;
            arrowIndicator.Show();
        }
        else if (pengwin.CharacterStats["health"].CurrentValue.Equals(pengwin.CharacterStats["health"].BaseValue) && selectionMode != SelectionMode.ABILITY)
        {
            HandleStep1();
        }
    }

    private void HandleStep4()
    {
        if (buffChecked)
        {
            arrowIndicator.Hide();
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_5;
            currentStepIndex = 4;
            EventBus.Publish(new DeselectSelectedTileEvent());
            EventBus.Publish(new StartNewTurnEvent());
        }
    }

    private void HandleStep5()
    {
        if (pengwin == gridSelectionController.GetSelectedCharacter())
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_6;
            currentStepIndex = 5;

            if (pengwin.IsExhausted())
            {
                StageFailed();
            }
        }
    }

    private void HandleStep6()
    {
        if (pengwin.CharacterStats["defense"].CurrentValue.Equals(120))
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_7;
            currentStepIndex = 6;
            arrowIndicator.Show();
        }
        else
        {
            HandleStep5();
        }
    }

    private void HandleStep7()
    {
        if (buffChecked)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_8;
            currentStepIndex = 7;
            arrowIndicator.Hide();
        }
    }

    private void HandleStep8()
    {
        if (defaultCharacter == gridSelectionController.GetSelectedCharacter())
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_9;
            currentStepIndex = 8;
            arrowIndicator.Show();
        }
    }

    private void HandleStep9()
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

    private void HandleStep10()
    {
        if (pengwin == gridSelectionController.GetSelectedCharacter())
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_11;
            currentStepIndex = 10;
        }
    }

    private void HandleStep11()
    {
        if (pengwin == gridSelectionController.GetSelectedCharacter())
        {
            if (abilityIndexSelected == 3)
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_12;
                currentStepIndex = 11;
            }
            else if (pengwin.IsExhausted())
            {
                StageFailed();
            }
        }
    }

    private void HandleStep12()
    {
        if (rocketCat.CharacterStats["health"].CurrentValue < rocketCat.CharacterStats["health"].BaseValue && selectionMode == SelectionMode.FREE)
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_13;
            currentStepIndex = 12;
        }
        else
        {
            HandleStep11();
        }
    }

    private void HandleStep13()
    {
        if (rocketCat == gridSelectionController.GetSelectedCharacter())
        {
            arrowIndicator.Show();
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_14;
            currentStepIndex = 13;
        }
    }

    private void HandleStep14()
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
        abilityIndexSelected = -1;

        Debug.Log(currentStepIndex);
    }
}