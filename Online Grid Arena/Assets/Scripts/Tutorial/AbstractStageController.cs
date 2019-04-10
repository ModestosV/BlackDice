
public class AbstractStageController
{
    protected const string STAGE_COMPLETE = "Stage Completed!\nRedirecting Tutorial";

    protected void CompleteStage(int stageIndex)
    {
        EventBus.Publish(new StageCompletedEvent(stageIndex));
    }
}