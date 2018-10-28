public interface ICharacter : IMonoBehaviour, ICharacterMovementController
{
    ICharacterController Controller { get; }
}
