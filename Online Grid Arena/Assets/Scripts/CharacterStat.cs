/*
 * Credit: Kryzarel's free Unity asset titled "Character Stats".
 * Obtained from Unity Asset Store on 2018/09/14. https://assetstore.unity.com/packages/tools/integration/character-stats-106351
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class CharacterStat : ICharacterStat
{
	protected float _baseValue;
    public float BaseValue
    {
        get
        {
            return _baseValue;
        }
    }

	protected bool isDirty = true;
	protected float lastBaseValue;

	protected float _value;
	public virtual float Value {
		get {
			if(isDirty || lastBaseValue != _baseValue) {
				lastBaseValue = _baseValue;
				_value = CalculateFinalValue();
				isDirty = false;
			}
			return _value;
		}
	}

	protected readonly List<IStatModifier> statModifiers;
	protected readonly ReadOnlyCollection<IStatModifier> _statModifiersReadOnly;
    public ReadOnlyCollection<IStatModifier> StatModifiers
    {
        get
        {
            return _statModifiersReadOnly;
        }
    }

    public CharacterStat()
	{
		statModifiers = new List<IStatModifier>();
        _statModifiersReadOnly = statModifiers.AsReadOnly();
	}

	public CharacterStat(float baseValue) : this()
	{
		_baseValue = baseValue;
	}

	public virtual void AddModifier(IStatModifier mod)
	{
		isDirty = true;
		statModifiers.Add(mod);
	}

	public virtual bool RemoveModifier(IStatModifier mod)
	{
		if (statModifiers.Remove(mod))
		{
			isDirty = true;
			return true;
		}
		return false;
	}

	public virtual bool RemoveAllModifiersFromSource(object source)
	{
		int numRemovals = statModifiers.RemoveAll(mod => mod.Source == source);

		if (numRemovals > 0)
		{
			isDirty = true;
			return true;
		}
		return false;
	}

	protected virtual int CompareModifierOrder(IStatModifier a, IStatModifier b)
	{
		if (a.Order < b.Order)
			return -1;
		else if (a.Order > b.Order)
			return 1;
		return 0; //if (a.Order == b.Order)
	}
		
	protected virtual float CalculateFinalValue()
	{
		float finalValue = _baseValue;
		float sumPercentAdd = 0;

		statModifiers.Sort(CompareModifierOrder);

		for (int i = 0; i < statModifiers.Count; i++)
		{
			IStatModifier mod = statModifiers[i];

			if (mod.Type == StatModType.Flat)
			{
				finalValue += mod.Value;
			}
			else if (mod.Type == StatModType.PercentAdd)
			{
				sumPercentAdd += mod.Value;

				if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
				{
					finalValue *= 1 + sumPercentAdd;
					sumPercentAdd = 0;
				}
			}
			else if (mod.Type == StatModType.PercentMult)
			{
				finalValue *= 1 + mod.Value;
			}
		}

		// Workaround for float calculation errors, like displaying 12.00001 instead of 12
		return (float)Math.Round(finalValue, 4);
	}
}
