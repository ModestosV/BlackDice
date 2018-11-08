using System.Collections.Generic;

public interface IAbility
{
    AbilityType Type { get; set; }
    List<float> Values { get; set; }
}
