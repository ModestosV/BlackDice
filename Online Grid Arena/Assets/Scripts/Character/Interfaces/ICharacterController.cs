using System.Collections.Generic;

public interface ICharacterController : ICharacterMovementController
{
    CharacterStatNameSet CharacterStatNameSet { get; }
    List<ICharacterStat> CharacterStats { get; }
    ITurnController TurnController { get; set; }
    IHexTile OccupiedTile { get; set; }
    int OwnedByPlayer { get; }
    List<IAbility> Abilities { get; }
    void Damage(float damage);

    
    void Refresh();
    float GetInitiative();
}
