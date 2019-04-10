public enum StatModType
{
    Flat = 100,
    PercentAdd  = 200,
    PercentMult = 300
}

public class StatModifier : IStatModifier
{
    public float Value { get; }
	public StatModType Type { get; }
	public int Order { get; }
	public object Source { get; }

	public StatModifier(float value, StatModType type, int order, object source = null)
	{
		Value = value;
		Type = type;
		Order = order;
		Source = source;
	}

	public StatModifier(float value, StatModType type) : this(value, type, (int)type) { }

    public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }

    public override string ToString()
    {
        string sourceString = Source == null ? "null" : $"({Source}|{Source.GetHashCode()})";

        return $"({base.ToString()}: {Type.ToString()}, {Value.ToString()}, {sourceString}, {Order})";
    }
}
