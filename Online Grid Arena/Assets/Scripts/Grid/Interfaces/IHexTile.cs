using UnityEngine;

public interface IHexTile : IMonoBehaviour
{
    void SetHoverMaterial();
    void SetErrorMaterial();
    void SetDefaultMaterial();
    void SetClickedMaterial();
    void SetHighlightMaterial();
    bool IsMouseOver();

    IHexTileController Controller { get; }
    GameObject Obstruction { get; }

    void PlayAbilityAnimation(GameObject abilityAnimationPrefab);
}
