public interface ICharacter : IMonoBehaviour
{
    IHexTile GetOccupiedTile();
    ICharacterController Controller { get; }
}
