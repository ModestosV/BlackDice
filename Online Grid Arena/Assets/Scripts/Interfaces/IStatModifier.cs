public enum StatModType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300,
}

public interface IStatModifier
{
    float Value { get; }

    StatModType Type { get; }

    int Order { get; }

    object Source { get; }

}
