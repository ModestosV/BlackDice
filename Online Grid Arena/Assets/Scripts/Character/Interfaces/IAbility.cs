using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    AbilityType Type { get; set; }
    List<float> Values { get; set; }
}
