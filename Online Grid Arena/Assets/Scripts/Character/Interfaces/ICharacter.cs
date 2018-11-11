public interface ICharacter : IMonoBehaviour
{
    ICharacterController Controller { get; }
    void MoveToTile(IHexTile targetTile);
    void Destroy();
}
