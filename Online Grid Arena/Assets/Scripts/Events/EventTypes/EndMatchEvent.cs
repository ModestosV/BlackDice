public class EndMatchEvent : AbstractEvent
{
    public string EndingText { get; }
    public EndMatchEvent(string endingText)
    {
        EndingText = endingText;
    }
}
