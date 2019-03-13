public class StatusEffectEvent : AbstractEvent
{
    public ICharacterController CharacterController { get; }
    public string Type { get; }
    public bool IsActive { get; }

    public StatusEffectEvent(string type, bool isActive, ICharacterController characterController)
    {
        CharacterController = characterController;
        Type = type;
        IsActive = isActive;
    }
}