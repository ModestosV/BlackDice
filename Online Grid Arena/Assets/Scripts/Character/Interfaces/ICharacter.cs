public interface ICharacter : IMonoBehaviour, ICharacterMovementController
{
    IHexTile GetOccupiedTile();
    ICharacterController Controller { get; }
}
