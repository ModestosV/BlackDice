using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;

public class Stage5Controller : AbstractStageController, IEventSubscriber
{
    private const String TUTORIAL_STEP_1 = "Click on Rocket Cat";
    private const String TUTORIAL_STEP_2 = "Press Q\nattack Pengwin";
    private const String TUTORIAL_STEP_3 = "Check Buff\nPress End Turn";
    private const String TUTORIAL_STEP_4 = "Click on Pengwin";
    private const String TUTORIAL_STEP_5 = "Press E to Buff\nNearby Ally";
    private const String TUTORIAL_STEP_6 = "Check Buff\nClick on DefaultCharacter";
    private const String TUTORIAL_STEP_7 = "Check Buff\nPress End Turn";
    private const String TUTORIAL_STEP_8 = "Click on Pengwin";
    private const String TUTORIAL_STEP_9 = "Press R\nAttack Rocket Cat";
    private const String TUTORIAL_STEP_10 = "End Turn";
    private const String TUTORIAL_STEP_11 = "Click on Rocket Cat";
    private const String TUTORIAL_STEP_12 = "Check Buff stack 4\nEnd Turn";
    private const String STAGE_COMPLETE = "Stage Completed!\nRedirecting Tutorial";
    private const int STAGE_INDEX = 5;

    private ICharacterController rocketCat;
    private ICharacterController pengwin;
    private ICharacterController defaultCharacter;

    private List<Action> stepMethods = new List<Action>();

    private int currentStepIndex = 0;

    private SelectionMode selectionMode = SelectionMode.FREE;

    public Stage5Controller(ICharacterController rocketCat, ICharacterController pengwin, ICharacterController defaultCharacter)
    {
        this.rocketCat = rocketCat;
        this.pengwin = pengwin;
        this.defaultCharacter = defaultCharacter;

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

    }

    private void handleStep2()
    {

    }

    private void handleStep3()
    {

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

    }
}