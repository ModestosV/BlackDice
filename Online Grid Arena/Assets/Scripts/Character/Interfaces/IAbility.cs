using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    AbilityType Type { get; }
    List<float> Values { get; }
}
