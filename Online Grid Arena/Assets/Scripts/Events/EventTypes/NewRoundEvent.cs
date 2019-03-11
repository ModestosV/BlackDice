public class NewRoundEvent : AbstractEvent
{
    public ICharacterController CharacterController { get; }
    public NewRoundEvent(ICharacterController characterController)
    {
        CharacterController = characterController;
    }
}
