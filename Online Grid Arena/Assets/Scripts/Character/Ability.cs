using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    ATTACK,
    HEAL,
    BUFF,
    DEBUFF
}

[CreateAssetMenu(menuName = "Ability")]
public class Ability : ScriptableObject, IAbility
{
    public AbilityType type;
    public List<float> values;

    public AbilityType Type { get { return type; } set { type = value; } }
    public List<float> Values { get { return values; } set { values = value; } }
}
