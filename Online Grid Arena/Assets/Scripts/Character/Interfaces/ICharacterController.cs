using System.Collections.Generic;
using UnityEngine;

public interface ICharacterController
{
    ICharacter Character { set; }
    IHexTileController OccupiedTile { get; set; }
    IHUDController HUDController { set; }

    Dictionary<string, ICharacterStat> CharacterStats { set; }
    List<IAbility> Abilities { set; }
    List<IEffect> Effects { set; }

    int AbilitiesRemaining { set; }

    string OwnedByPlayer { get; set; }    
    Texture CharacterIcon { set; }
    Color32 BorderColor { set; }
    IHealthBar HealthBar { set; }

    void Select();
    void Deselect();
    void ExecuteAbility(int abilityNumber, List<IHexTileController> targetTiles);
    void ExecuteMove(List<IHexTileController> path);
    void Refresh();
    float GetInitiative();
    void Damage(float damage);
    void Die();
    void Heal(float heal);
    void UpdateHealthBar();
    void ApplyEffect(IEffect effect);

    void UpdateSelectedHUD();
    void ClearSelectedHUD();
    void UpdateTargetHUD();
    void ClearTargetHUD();

    bool CanMove(int distance = 1);
    bool CanUseAbility(int abilityIndex);
    bool IsAlly(ICharacterController otherCharacter);

    void UpdateTurnTile(ITurnTile turnTileToUpdate);
    bool IsAbilityInRange(int abilityIndex, int range);
    bool HasAbility(int abilityIndex);
    void EndOfTurn();
}
