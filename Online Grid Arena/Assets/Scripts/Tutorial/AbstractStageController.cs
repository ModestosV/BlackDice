public class AbstractStageController
{
    public void CompleteStage(int stageIndex)
    {
        EventBus.Publish(new StageCompletedEvent(stageIndex));
    }
}