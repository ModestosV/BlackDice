/*
 * Credit: Kryzarel's free Unity asset titled "Character Stats".
 * Obtained from Unity Asset Store on 2018/09/14. https://assetstore.unity.com/packages/tools/integration/character-stats-106351
*/

public class StatModifier : IStatModifier
{
    private readonly float _value;
	private readonly StatModType _type;
	private readonly int _order;
	private readonly object _source;

	public StatModifier(float value, StatModType type, int order, object source)
	{
		_value = value;
		_type = type;
		_order = order;
		_source = source;
	}
    
    public float Value
    {
        get
        {
            return _value;
        }
    }

    public StatModType Type
    {
        get
        {
            return _type;
        }
    }

    public int Order
    {
        get
        {
            return _order;
        }
    }

    public object Source
    {
        get
        {
            return _source;
        }
    }

	public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

	public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

	public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}
