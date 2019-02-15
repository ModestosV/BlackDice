using UnityEngine;

public interface IHexTile : IMonoBehaviour
{
    void SetHoverMaterial();
    void SetDefaultMaterial();
    void SetClickedMaterial();
    void SetHighlightMaterial();
    void SetErrorMaterial();
    void ShowInvalidTarget();
    void ShowDamagedTarget();
    void ClearTargetIndicators();
    bool IsMouseOver();

    IHexTileController Controller { get; }
    GameObject Obstruction { get; }

    void PlayAbilityAnimation(GameObject abilityAnimationPrefab);
}
