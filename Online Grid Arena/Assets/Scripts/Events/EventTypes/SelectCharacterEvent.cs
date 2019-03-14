public class SelectCharacterEvent : AbstractEvent
{
    public ICharacterController Character { get; }
    public SelectCharacterEvent(ICharacterController character)
    {
        Character = character;
    }
}
