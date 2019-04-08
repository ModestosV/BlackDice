using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stage7Controller : AbstractStageController, IEventSubscriber
{
    private const string STAGE_FAILED = "Stage Failed!\nYou lost!\nRedirecting Tutorial";
    private const int STAGE_INDEX = 7;

    private GridSelectionController gridSelectionController;

    public Stage7Controller(GridSelectionController gridSelectionController)
    {
        this.gridSelectionController = gridSelectionController;
    }

    private void StageFailed()
    {
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = STAGE_FAILED;
        EventBus.Publish(new SurrenderEvent());
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
    }
}