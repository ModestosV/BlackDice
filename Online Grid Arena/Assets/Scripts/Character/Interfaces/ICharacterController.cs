using System.Collections.Generic;

public interface ICharacterController
{
    CharacterStatNameSet CharacterStatNameSet { get; }
    List<ICharacterStat> CharacterStats { get; }
    ITurnController TurnController { get; set; }
    IHexTileController OccupiedTile { get; set; }
    int OwnedByPlayer { get; }
    List<IAbility> Abilities { get; }
    void Damage(float damage);
    int MovesRemaining { get; set; }
    int AbilitiesRemaining { get; set; }

    void Select();
    void Deselect();
    void ExecuteAbility(int abilityNumber, ICharacterController targetCharacter);
    void ExecuteMove(IHexTileController targetTile);
    void Refresh();
    float GetInitiative();
}
