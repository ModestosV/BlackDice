public class DeathEvent : IEvent
{
    public ICharacterController CharacterController { get; }

    public DeathEvent(ICharacterController characterController)
    {
        CharacterController = characterController;
    }
}