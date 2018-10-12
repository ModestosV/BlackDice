public interface ICharacterStat
{
    float Value { get; }

    void AddModifier(StatModifier mod);
    bool RemoveModifier(StatModifier mod);
    bool RemoveAllModifiersFromSource(object source);
}
