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

    private List<Action> stepMethods = new List<Action>();
    private int currentStepIndex = 0;
    private int abilityIndexSelected = -1;

    private ICharacterController rocketCat;
    private ICharacterController sheepadin;
    private GridSelectionController gridSelectionController;

    public Stage4Controller(ICharacterController rocketCat, ICharacterController sheepadin, GridSelectionController gridSelectionController)
    {
        this.rocketCat = rocketCat;
        this.sheepadin = sheepadin;
        this.gridSelectionController = gridSelectionController;

        this.rocketCat.CharacterStats["moves"].BaseValue = 0;
        this.sheepadin.CharacterStats["moves"].BaseValue = 0;

        stepMethods.Add(() => this.handleStep1());
    }

    private void stageFailed()
    {
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = STAGE_FAILED;
        EventBus.Publish(new SurrenderEvent());
    }

    private void handleStep1()
    {
        if (sheepadin == gridSelectionController.GetSelectedCharacter())
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_2;
            currentStepIndex = 1;

            if (sheepadin.IsExhausted())
            {
                stageFailed();
            }
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
    }
}