using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stage4Controller : AbstractStageController, IEventSubscriber
{
    private const string TUTORIAL_STEP_1 = "Click On Sheepadin";
    private const string TUTORIAL_STEP_2 = "Press W";
    private const string TUTORIAL_STEP_3 = "Heal Both characters";
    private const string STAGE_FAILED = "Stage Failed!\nWrong attack used!\nRedirecting Tutorial";
    private const int STAGE_INDEX = 4;

    private readonly List<Action> stepMethods = new List<Action>();
    private int currentStepIndex = 0;
    private int abilityIndexSelected = -1;

    private readonly ICharacterController rocketCat;
    private readonly ICharacterController sheepadin;
    private readonly GridSelectionController gridSelectionController;

    public Stage4Controller(ICharacterController sheepadin, ICharacterController rocketCat, GridSelectionController gridSelectionController)
    {
        this.rocketCat = rocketCat;
        this.sheepadin = sheepadin;
        this.gridSelectionController = gridSelectionController;

        this.rocketCat.CharacterStats["moves"].BaseValue = 0;
        this.sheepadin.CharacterStats["moves"].BaseValue = 0;

        this.rocketCat.CharacterStats["health"].CurrentValue = 20;
        this.sheepadin.CharacterStats["health"].CurrentValue = 20;

        stepMethods.Add(() => this.HandleStep1());
        stepMethods.Add(() => this.HandleStep2());
        stepMethods.Add(() => this.HandleStep3());
    }

    private void StageFailed()
    {
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = STAGE_FAILED;
        EventBus.Publish(new SurrenderEvent());
    }

    private void HandleStep1()
    {
        if (sheepadin == gridSelectionController.GetSelectedCharacter())
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_2;
            currentStepIndex = 1;

            if (sheepadin.IsExhausted())
            {
                StageFailed();
            }
        }
    }

    private void HandleStep2()
    {
        if (sheepadin == gridSelectionController.GetSelectedCharacter())
        {
            if (abilityIndexSelected == 1)
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_3;
                currentStepIndex = 2;
            }
            else if (sheepadin.IsExhausted())
            {
                StageFailed();
            }
        }
        else
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_1;
            currentStepIndex = 0;
        }
    }

    private void HandleStep3()
    {
        if (sheepadin.CharacterStats["health"].CurrentValue.Equals(sheepadin.CharacterStats["health"].BaseValue) && rocketCat.CharacterStats["health"].CurrentValue.Equals(rocketCat.CharacterStats["health"].BaseValue))
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = STAGE_COMPLETE;
            CompleteStage(STAGE_INDEX);
        }
        else if (sheepadin.IsExhausted())
        {
            StageFailed();
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

        stepMethods[currentStepIndex].Invoke();

        abilityIndexSelected = -1;

        Debug.Log(currentStepIndex);
    }
}