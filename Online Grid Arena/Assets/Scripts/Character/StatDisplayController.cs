using System;

[Serializable]
public class StatDisplayController : IStatDisplayController {
    public ICharacterStat CharacterStat { get; set; }
}
