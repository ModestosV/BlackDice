public class ActiveCharacterEvent : AbstractEvent
{
    public ICharacterController CharacterController { get; }

    public ActiveCharacterEvent(ICharacterController characterController)
    {
        CharacterController = characterController;
    }
}
