public interface ICharacter : IMonoBehaviour
{
    IHexTile GetOccupiedTile();
    CharacterController Controller();
}
