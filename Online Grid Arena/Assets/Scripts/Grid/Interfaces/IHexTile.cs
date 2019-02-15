using UnityEngine;

public interface IHexTile : IMonoBehaviour
{
    void SetHoverMaterial();
    void SetErrorMaterial();
    void SetDefaultMaterial();
    void SetClickedMaterial();
    void SetHighlightMaterial();
    void ShowInvalidTarget();
    void ClearTargetIndicator();
    bool IsMouseOver();

    IHexTileController Controller { get; }
    GameObject Obstruction { get; }

    void PlayAbilityAnimation(GameObject abilityAnimationPrefab);
}
