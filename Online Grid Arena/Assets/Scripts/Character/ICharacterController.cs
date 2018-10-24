using System.Collections.Generic;

public interface ICharacterController
{
    CharacterStatNameSet CharacterStatNameSet { get; }
    List<ICharacterStat> CharacterStats { get; }
    int OwnedByPlayer { get; }
}
