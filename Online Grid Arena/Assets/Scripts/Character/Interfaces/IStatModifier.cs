public interface IStatModifier
{
    float Value { get; }
    StatModType Type { get; }
    int Order { get; }
    object Source { get; }

    string ToString();
}
