public class StageCompletedEvent : AbstractEvent
{
    public int StageIndex { get; }

    public StageCompletedEvent(int stageIndex)
    {
        StageIndex = stageIndex;
    }
}