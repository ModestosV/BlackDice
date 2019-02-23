using System.Collections.Generic;
using UnityEngine;

public interface ICharacterController
{
    ICharacter Character { get; }
    IHexTileController OccupiedTile { get; set; }
    IHUDController HUDController { set; }
    CharacterState CharacterState { get; set; }

    Dictionary<string, ICharacterStat> CharacterStats { get; set; }
    List<IAbility> Abilities { get; set; }
    List<IEffect> Effects { set; }

    string Owner { get; set; }    
    Texture CharacterIcon { set; }
    Color32 BorderColor { set; }
    IHealthBar HealthBar { set; }
    SpriteRenderer ActiveCircle { get; set; }
    
    void ExecuteAbility(int abilityNumber, List<IHexTileController> targetTiles);
    void ExecuteMove(List<IHexTileController> path);
    void Refresh();
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
    void StartOfTurn();
    void EndOfTurn();
    AbilityType GetAbilityType(int abilityIndex);
}
