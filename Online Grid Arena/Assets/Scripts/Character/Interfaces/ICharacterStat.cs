using System.Collections.Generic;

public interface ICharacterStat
{
    float Value { get; }

    void AddModifier(IStatModifier mod);
    bool RemoveModifier(IStatModifier mod);
    bool RemoveAllModifiersFromSource(object source);
    List<IStatModifier> StatModifiers { get; set; }
}
