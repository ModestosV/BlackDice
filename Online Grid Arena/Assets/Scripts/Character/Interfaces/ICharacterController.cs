using System.Collections.Generic;

public interface ICharacterController
{
    ICharacter Character { set; }
    IHexTileController OccupiedTile { set; }
    ITurnController TurnController { set; }
    IHUDController HUDController { set; }

    List<string> StatNames { set; }
    List<ICharacterStat> CharacterStats { set; }
    List<IAbility> Abilities { set; }

    int AbilitiesRemaining { set; }
    string OwnedByPlayer { get; set; }    

    void Select();
    void Deselect();
    void ExecuteAbility(int abilityNumber, ICharacterController targetCharacter);
    void ExecuteMove(List<IHexTileController> path);
    void Refresh();
    float GetInitiative();
    void Damage(float damage);
    void Die();

    void UpdateSelectedHUD();
    void ClearSelectedHUD();
    void UpdateTargetHUD();
    void ClearTargetHUD();

    bool CanMove(int distance = 1);
    bool CanUseAbility();
    bool IsActiveCharacter();
}
