
public class AbstractStageController
{
    protected const string STAGE_COMPLETE = "Stage Completed!\nRedirecting Tutorial";
    protected const string STAGE_FAILED = "Stage Failed!\nRedirecting Tutorial";

    public void CompleteStage(int stageIndex)
    {
        EventBus.Publish(new StageCompletedEvent(stageIndex));
    }
}