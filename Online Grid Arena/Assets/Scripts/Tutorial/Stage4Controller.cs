using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stage4Controller : AbstractStageController, IEventSubscriber
{
    private const string TUTORIAL_STEP_1 = "Click On Sheepadin";
    private const string TUTORIAL_STEP_2 = "Press W";
    private const string TUTORIAL_STEP_3 = "Heal Both characters";

    private List<Action> stepMethods = new List<Action>();
    private int currentStepIndex = 0;

    private ICharacterController rocketCat;
    private ICharacterController sheepadin;
    private GridSelectionController gridSelectionController;

    public Stage4Controller(ICharacterController rocketCat, ICharacterController sheepadin, GridSelectionController gridSelectionController)
    {
        this.rocketCat = rocketCat;
        this.sheepadin = sheepadin;
        this.gridSelectionController = gridSelectionController;

        stepMethods.Add(() => this.handleStep1());
    }

    private void handleStep1()
    {
        if (sheepadin == gridSelectionController.GetSelectedCharacter())
        {
            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TUTORIAL_STEP_2;
            currentStepIndex = 1;
        }
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();


    }
}