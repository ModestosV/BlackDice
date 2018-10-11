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
    public float BaseValue;

	protected bool isDirty = true;
	protected float lastBaseValue;

	protected float _value;
	public virtual float Value {
		get {
			if(isDirty || lastBaseValue != BaseValue) {
				lastBaseValue = BaseValue;
				_value = CalculateFinalValue();
				isDirty = false;
			}
			return _value;
		}
	}

	protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    public CharacterStat() : this (0.0f, new List<StatModifier>())
    {

    }

	public CharacterStat(float baseValue) : this(baseValue, new List<StatModifier>())
	{
		BaseValue = baseValue;
	}

    public CharacterStat(float baseValue, List<StatModifier> statModifierList)
    {
        BaseValue = baseValue;
        statModifiers = statModifierList;
        StatModifiers = statModifiers.AsReadOnly();
    }

	public virtual void AddModifier(StatModifier mod)
	{
		isDirty = true;
		statModifiers.Add(mod);
	}

	public virtual bool RemoveModifier(StatModifier mod)
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
		int numRemovals = statModifiers.RemoveAll(mod => mod.source == source);

		if (numRemovals > 0)
		{
			isDirty = true;
			return true;
		}
		return false;
	}

	protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
	{
		if (a.order < b.order)
			return -1;
		else if (a.order > b.order)
			return 1;
		return 0; 
	}
		
	protected virtual float CalculateFinalValue()
	{
		float finalValue = BaseValue;
		float sumPercentAdd = 0;

		statModifiers.Sort(CompareModifierOrder);

		for (int i = 0; i < statModifiers.Count; i++)
		{
			StatModifier mod = statModifiers[i];

			if (mod.type == StatModType.Flat)
			{
				finalValue += mod.value;
			}
			else if (mod.type == StatModType.PercentAdd)
			{
				sumPercentAdd += mod.value;

				if (i + 1 >= statModifiers.Count || statModifiers[i + 1].type != StatModType.PercentAdd)
				{
					finalValue *= 1 + sumPercentAdd;
					sumPercentAdd = 0;
				}
			}
			else if (mod.type == StatModType.PercentMult)
			{
				finalValue *= 1 + mod.value;
			}
		}

		// Workaround for float calculation errors, like displaying 12.00001 instead of 12
		return (float)Math.Round(finalValue, 4);
	}
}
