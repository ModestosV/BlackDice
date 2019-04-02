using System;
using System.Collections.Generic;

public class Stage4Controller : AbstractStageController, IEventSubscriber
{
    private const string TUTORIAL_STEP_1 = "Click On Sheepadin";
    private const string TUTORIAL_STEP_2 = "Press W";
    private const string TUTORIAL_STEP_3 = "Heal Both characters";

    private List<Action> stepMethods = new List<Action>();
    private int currentStepIndex = 0;

    private ICharacterController rocketCat;
    private ICharacterController sheepadin;
    private SelectionMode selectionMode = SelectionMode.FREE;

    public Stage4Controller(ICharacterController rocketCat, ICharacterController sheepadin)
    {
        this.rocketCat = rocketCat;
        this.sheepadin = sheepadin;
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();


    }
}