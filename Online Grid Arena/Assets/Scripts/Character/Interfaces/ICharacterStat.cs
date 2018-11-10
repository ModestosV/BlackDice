using System.Collections.Generic;

public interface ICharacterStat
{
    float Value { get; }
    float CurrentValue { get; set; }
    List<IStatModifier> StatModifiers { get; set; }

    void Refresh();
    void AddModifier(IStatModifier mod);
    bool RemoveModifier(IStatModifier mod);
    bool RemoveAllModifiersFromSource(object source);
}
