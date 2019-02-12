public class DeathEvent : AbstractEvent
{
    public ICharacterController CharacterController { get; }

    public DeathEvent(ICharacterController characterController)
    {
        CharacterController = characterController;
    }
}