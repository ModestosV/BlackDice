public class SelectActivePlayerEvent : AbstractEvent
{
    public IPlayer ActivePlayer { get; }
    public SelectActivePlayerEvent(IPlayer activePlayer)
    {
        ActivePlayer = activePlayer;
    }
}