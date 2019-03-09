public class ExhaustCharacterEvent : AbstractEvent
{
    public ICharacterController CharacterController { get; }

    public ExhaustCharacterEvent(ICharacterController characterController)
    {
        CharacterController = characterController;
    }
}
