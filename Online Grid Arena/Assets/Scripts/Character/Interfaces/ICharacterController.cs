using System.Collections.Generic;
using UnityEngine;

public interface ICharacterController
{
    ICharacter Character { set; }
    IHexTileController OccupiedTile { set; }
    ITurnController TurnController { set; }
    IHUDController HUDController { set; }

    Dictionary<string, ICharacterStat> CharacterStats { set; }
    List<IAbility> Abilities { set; }

    int AbilitiesRemaining { set; }

    string OwnedByPlayer { get; set; }    
    Texture CharacterIcon { set; }
    Color32 BorderColor { set; }

    void Select();
    void Deselect();
    void ExecuteAbility(int abilityNumber, IHexTileController targetTile);
    void ExecuteMove(List<IHexTileController> path);
    void Refresh();
    float GetInitiative();
    void Damage(float damage);
    void Die();
    void Heal(float heal);

    void UpdateSelectedHUD();
    void ClearSelectedHUD();
    void UpdateTargetHUD();
    void ClearTargetHUD();

    bool CanMove(int distance = 1);
    bool CanUseAbility();
    bool IsActiveCharacter();
    bool IsAlly(ICharacterController otherCharacter);

    void UpdateTurnTile(ITurnTile turnTileToUpdate);
    AbilityType GetAbilityType(int abilityIndex);
    bool IsAbilityInRange(int abilityIndex, int range);
    bool HasAbility(int abilityIndex);

    void InstantiateAbilityAnimation(GameObject abilityAnimationPrefab);
    void PlayAbilitySound(AudioClip abilitySound);
}
