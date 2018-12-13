using UnityEngine;

public interface ICharacter : IMonoBehaviour
{
    ICharacterController Controller { get; }
    void MoveToTile(IHexTile targetTile);
    void InstantiateAbilityAnimation(GameObject abilityAnimationPrefab);
    void Destroy();
}
