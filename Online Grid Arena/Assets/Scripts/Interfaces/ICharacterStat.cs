using System.Collections.ObjectModel;

public interface ICharacterStat
{
    float BaseValue { get; }

    float Value { get; }

    ReadOnlyCollection<IStatModifier> StatModifiers { get; }

    void AddModifier(IStatModifier mod);
    bool RemoveModifier(IStatModifier mod);
    bool RemoveAllModifiersFromSource(object source);
}
