using System.Collections.Generic;

public interface ICharacterController : ICharacterMovementController
{
    CharacterStatNameSet CharacterStatNameSet { get; }
    List<ICharacterStat> CharacterStats { get; }
    ITurnController TurnController { get; set; }
    IHexTile OccupiedTile { get; set; }
    int OwnedByPlayer { get; }

    
    void Refresh();
    float GetInitiative();
}
