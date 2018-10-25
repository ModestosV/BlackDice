public interface ICharacterStat
{
    float Value { get; }

    void AddModifier(IStatModifier mod);
    bool RemoveModifier(IStatModifier mod);
    bool RemoveAllModifiersFromSource(object source);
}
