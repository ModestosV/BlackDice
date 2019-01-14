using System.Collections.Generic;
using UnityEngine;

public interface ICharacterController
{
    ICharacter Character { set; }
    IHexTileController OccupiedTile { set; }
    IHUDController HUDController { set; }

    Dictionary<string, ICharacterStat> CharacterStats { set; }
    List<IAbility> Abilities { set; }

    int AbilitiesRemaining { set; }

    string OwnedByPlayer { get; set; }    
    Texture CharacterIcon { set; }
    Color32 BorderColor { set; }
    IHealthBar HealthBar { set; }

    void Select();
    void Active();
    void ExecuteAbility(int abilityNumber, IHexTileController targetTile);
    void ExecuteMove(List<IHexTileController> path);
    void Refresh();
    float GetInitiative();
    void Damage(float damage);
    void Die();
    void Heal(float heal);
    void UpdateHealthBar();

    void UpdateSelectedHUD();
    void ClearSelectedHUD();
    void UpdateTargetHUD();
    void ClearTargetHUD();

    bool CanMove(int distance = 1);
    bool CanUseAbility(int abilityIndex);
    bool IsAlly(ICharacterController otherCharacter);

    void UpdateTurnTile(ITurnTile turnTileToUpdate);
    AbilityType GetAbilityType(int abilityIndex);
    bool IsAbilityInRange(int abilityIndex, int range);
    bool HasAbility(int abilityIndex);
}
