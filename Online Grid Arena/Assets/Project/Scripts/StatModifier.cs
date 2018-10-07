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
    public readonly float value;
	public readonly StatModType type;
	public readonly int order;
	public readonly object source;

	public StatModifier(float value, StatModType type, int order, object source)
	{
		this.value = value;
		this.type = type;
		this.order = order;
		this.source = source;
	}

	public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

	public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

	public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}
