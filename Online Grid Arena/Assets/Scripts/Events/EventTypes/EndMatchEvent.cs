public class EndMatchEvent : IEvent
{
    public string EndingText { get; }
    public EndMatchEvent(string endingText)
    {
        EndingText = endingText;
    }
}
