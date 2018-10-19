/*
 * Credit: Kryzarel's free Unity asset titled "Character Stats".
 * Obtained from Unity Asset Store on 2018/09/14. https://assetstore.unity.com/packages/tools/integration/character-stats-106351
*/

public enum StatModType
{
    Flat = 100,
    PercentAdd  = 200,
    PercentMult = 300
}

public class StatModifier
{
    public readonly float Value;
	public readonly StatModType Type;
	public readonly int Order;
	public readonly object Source;

	public StatModifier(float value, StatModType type, int order, object source)
	{
		Value = value;
		Type = type;
		Order = order;
		Source = source;
	}

	public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

	public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

	public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }

    public override string ToString()
    {
        string sourceString = Source == null ? "null" : string.Format("({0}|{1})", Source.ToString(), Source.GetHashCode());

        return string.Format("({0}: {1}, {2}, {3}, {4})", base.ToString(), Type.ToString(), Value.ToString(), sourceString, Order);
    }
}