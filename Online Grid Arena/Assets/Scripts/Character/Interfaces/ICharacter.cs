using System.Collections.Generic;

public interface ICharacter : IMonoBehaviour
{
    ICharacterController Controller { get; }
    void MoveToTile(IHexTile targetTile);
    void FollowPath(List<IHexTileController> path, IHexTile targetTile);
    void Destroy();
}
