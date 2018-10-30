using System.Collections.Generic;

public interface ICharacterController
{
    CharacterStatNameSet CharacterStatNameSet { get; }
    List<ICharacterStat> CharacterStats { get; }
    ITurnController TurnController { get; set; }
    IHexTile OccupiedTile { get; set; }
    int OwnedByPlayer { get; }
    List<IAbility> Abilities { get; }
    void Damage(float damage);
    int MovesRemaining { get; set; }
    int AbilitiesRemaining { get; set; }

    void ExecuteAbility(int abilityNumber, ICharacter targetCharacter);
    void MoveToTile(IHexTile targetTile);
    void Refresh();
    float GetInitiative();
}
