using System.Collections.Generic;

public interface ICharacterController : ICharacterMovementController
{
    CharacterStatNameSet CharacterStatNameSet { get; }
    List<ICharacterStat> CharacterStats { get; }
    int OwnedByPlayer { get; }
}
